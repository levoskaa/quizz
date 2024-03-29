﻿using Dapper;
using MediatR;
using Quizz.GameService.Application.DomainEvents;
using Quizz.GameService.Data;
using Quizz.GameService.Data.Repositories;
using Quizz.GameService.Infrastructure.Services;
using Quizz.Questions.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Quizz.Questions.Protos.Questions;

namespace Quizz.GameService.Application.Commands;

public class UpdateGameQuestionsCommandHandler : IRequestHandler<UpdateGameQuestionsCommand>
{
    private readonly QuestionsClient questionsClient;
    private readonly IGameValidatorService gameValidatorService;
    private readonly DapperContext dapper;
    private readonly IGameRepository gameRepository;

    public UpdateGameQuestionsCommandHandler(
        QuestionsClient questionsClient,
        IGameValidatorService gameValidatorService,
        DapperContext dapper,
        IGameRepository gameRepository)
    {
        this.questionsClient = questionsClient;
        this.gameValidatorService = gameValidatorService;
        this.dapper = dapper;
        this.gameRepository = gameRepository;
    }

    public async Task<Unit> Handle(UpdateGameQuestionsCommand request, CancellationToken cancellationToken)
    {
        await gameValidatorService.CheckGameOwnershipAsync(request.GameId, request.UserId);
        var oldQuestionIds = await GetQuestionIdsAsync(request.GameId);
        var grpcRequest = BuildGrpcRequest(oldQuestionIds, request.Questions);
        var grpcReply = await questionsClient.ReplaceQuestionsAsync(grpcRequest, cancellationToken: cancellationToken);
        var newQuestionIds = grpcReply.NewQuestionIds.ToList()
            .Select(id => Guid.Parse(id));
        await UpdateGameQuestionIds(request.GameId, newQuestionIds, cancellationToken);
        return Unit.Value;
    }

    private async Task<IEnumerable<Guid>> GetQuestionIdsAsync(int gameId)
    {
        var query = @"SELECT QuestionId FROM GameQuestion
                      WHERE GameId=@gameId;";
        IEnumerable<Guid> questionIds = new List<Guid>();
        using (var connection = dapper.CreateConnection())
        {
            questionIds = await connection.QueryAsync<Guid>(query, new { gameId });
        }
        return questionIds;
    }

    private ReplaceQuestionsRequest BuildGrpcRequest(IEnumerable<Guid> oldQuestionIds, IEnumerable<Common.Dtos.QuestionDto> newQuestions)
    {
        var grpcRequest = new ReplaceQuestionsRequest();
        grpcRequest.QuestionIds.AddRange(oldQuestionIds.Select(x => x.ToString()));
        var questionProtos = QuestionsGrpcConverter.QuestionDtosToQuestionDtoProtos(newQuestions);
        grpcRequest.QuestionDtos.AddRange(questionProtos);
        return grpcRequest;
    }

    private async Task UpdateGameQuestionIds(int gameId, IEnumerable<Guid> questionIds, CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetAsync(gameId);
        game.ReplaceQuestionIds(questionIds);
        var updateTime = DateTime.UtcNow;
        game.AddDomainEvent(new GameQuestionsUpdatedDomainEvent(game, updateTime));
        await gameRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
using Dapper;
using MediatR;
using Quizz.GameService.Data;
using Quizz.GameService.Infrastructure.Services;
using Quizz.Questions.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Quizz.Questions.Protos.Questions;

namespace Quizz.GameService.Application.Commands;

public class GetGameQuestionsCommandHandler : IRequestHandler<GetGameQuestionsCommand, IEnumerable<Common.Models.Question>>
{
    private readonly DapperContext dapper;
    private readonly QuestionsClient questionsClient;
    private readonly IGameValidatorService gameValidatorService;

    public GetGameQuestionsCommandHandler(
        DapperContext dapper,
        QuestionsClient questionsClient,
        IGameValidatorService gameValidatorService)
    {
        this.dapper = dapper;
        this.questionsClient = questionsClient;
        this.gameValidatorService = gameValidatorService;
    }

    public async Task<IEnumerable<Common.Models.Question>> Handle(GetGameQuestionsCommand request, CancellationToken cancellationToken)
    {
        await gameValidatorService.CheckGameOwnershipAsync(request.GameId, request.UserId);
        var query = @"SELECT QuestionId FROM GameQuestion
                      WHERE GameId=@gameId";
        IEnumerable<Guid> questionIds = new List<Guid>();
        using (var connection = dapper.CreateConnection())
        {
            questionIds = await connection.QueryAsync<Guid>(query, new { gameId = request.GameId });
        }
        var grpcRequest = new GetQuestionsRequest();
        grpcRequest.QuestionIds.AddRange(questionIds.Select(x => x.ToString()));
        var grpcReply = await questionsClient.GetQuestionsAsync(grpcRequest, cancellationToken: cancellationToken);
        var questions = QuestionsGrpcConverter.QuestionProtosToQuestions(grpcReply.Questions.ToList());
        return questions;
    }
}
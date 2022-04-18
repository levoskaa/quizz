using AutoMapper;
using Dapper;
using MediatR;
using Quizz.GameService.Data;
using Quizz.GameService.Infrastructure.Services;
using Quizz.Questions.Protos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Quizz.Questions.Protos.Questions;

namespace Quizz.GameService.Application.Commands;

public class GetGameQuestionsCommandHandler : IRequestHandler<GetGameQuestionsCommand, IEnumerable<Common.Models.Question>>
{
    private readonly DapperContext dapper;
    private readonly QuestionsClient questionsClient;
    private readonly IMapper mapper;
    private readonly IGameValidatorService gameValidatorService;

    public GetGameQuestionsCommandHandler(
        DapperContext dapper,
        QuestionsClient questionsClient,
        IMapper mapper,
        IGameValidatorService gameValidatorService)
    {
        this.dapper = dapper;
        this.questionsClient = questionsClient;
        this.mapper = mapper;
        this.gameValidatorService = gameValidatorService;
    }

    public async Task<IEnumerable<Common.Models.Question>> Handle(GetGameQuestionsCommand request, CancellationToken cancellationToken)
    {
        await gameValidatorService.CheckGameOwnershipAsync(request.GameId, request.UserId);
        var query = @"SELECT QuestionId FROM GameQuestion
                      WHERE GameId=@gameId";
        IEnumerable<string> questionIds = new List<string>();
        using (var connection = dapper.CreateConnection())
        {
            questionIds = await connection.QueryAsync<string>(query, new { gameId = request.GameId });
        }
        var grpcRequest = new GetQuestionsRequest();
        grpcRequest.QuestionIds.AddRange(questionIds);
        var grpcReply = await questionsClient.GetQuestionsAsync(grpcRequest, cancellationToken: cancellationToken);
        var questions = mapper.Map<IEnumerable<Common.Models.Question>>(grpcReply.Questions);
        return questions;
    }
}
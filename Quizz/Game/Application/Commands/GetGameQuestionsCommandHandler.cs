using AutoMapper;
using Dapper;
using MediatR;
using Quizz.Common.ErroHandling;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Data;
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

    public GetGameQuestionsCommandHandler(
        DapperContext dapper,
        QuestionsClient questionsClient,
        IMapper mapper)
    {
        this.dapper = dapper;
        this.questionsClient = questionsClient;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<Common.Models.Question>> Handle(GetGameQuestionsCommand request, CancellationToken cancellationToken)
    {
        await CheckGameOwnership(request);
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

    private async Task CheckGameOwnership(GetGameQuestionsCommand request)
    {
        var query = @"SELECT COUNT(*) FROM Game
                      LEFT JOIN GameQuestion
                      ON Game.Id=GameQuestion.GameId
                      WHERE Game.OwnerId=@userId AND Game.Id=@gameId;";
        int queryResult = 0;
        using (var connection = dapper.CreateConnection())
        {
            queryResult = await connection.ExecuteScalarAsync<int>(query, new { gameId = request.GameId, userId = request.UserId });
        }
        if (queryResult == 0)
        {
            throw new EntityNotFoundException($"Game with id {request.GameId} not found", ValidationError.GameNotFound);
        }
    }
}
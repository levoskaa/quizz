using MediatR;
using Quizz.Common.Models;
using System.Collections.Generic;

namespace Quizz.GameService.Application.Commands;

public class GetGameQuestionsCommand : IRequest<IEnumerable<Question>>
{
    public int GameId { get; init; }

    public string UserId { get; init; }

    public GetGameQuestionsCommand(int gameId, string userId)
    {
        GameId = gameId;
        UserId = userId;
    }
}
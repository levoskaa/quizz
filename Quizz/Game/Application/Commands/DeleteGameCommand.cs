using MediatR;

namespace Quizz.GameService.Application.Commands;

public class DeleteGameCommand : IRequest
{
    public int GameId { get; set; }

    public string UserId { get; set; }

    public DeleteGameCommand(int gameId, string userId)
    {
        GameId = gameId;
        UserId = userId;
    }
}

using MediatR;

namespace Quizz.GameService.Application.Commands;

public class UpdateGameCommand : IRequest
{
    public int GameId { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }
}

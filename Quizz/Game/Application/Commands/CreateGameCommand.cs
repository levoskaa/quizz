using MediatR;

namespace Quizz.GameService.Application.Commands
{
    public class CreateGameCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
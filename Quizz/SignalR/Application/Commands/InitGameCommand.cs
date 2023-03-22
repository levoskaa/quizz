using MediatR;
using Quizz.SignalR.Application.ViewModels;

namespace Quizz.SignalR.Application.Commands
{
    public class InitGameCommand : IRequest<GameInitializedViewModel>
    {
        public int GameId { get; set; }
        public string UserId { get; set; }
    }
}

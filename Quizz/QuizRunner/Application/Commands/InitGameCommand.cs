using MediatR;
using Quizz.QuizRunner.Application.ViewModels;

namespace Quizz.QuizRunner.Application.Commands
{
    public class InitGameCommand : IRequest<GameInitializedViewModel>
    {
        public int GameId { get; set; }
        public string UserId { get; set; }
    }
}

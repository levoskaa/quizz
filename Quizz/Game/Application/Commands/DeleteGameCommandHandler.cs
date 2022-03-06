using MediatR;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Commands
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IGameRepository gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<Unit> Handle(DeleteGameCommand deleteGameCommand, CancellationToken cancellationToken)
        {
            // TODO: fire Integration Event to delete related questions
            var game = await gameRepository.GetAsync(deleteGameCommand.GameId);
            ValidateCommand(deleteGameCommand, game);
            gameRepository.Remove(game);
            await gameRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }

        private void ValidateCommand(DeleteGameCommand command, Game game)
        {
            if (command.UserId != game.OwnerId)
            {
                throw new ForbiddenException();
            }
        }
    }
}
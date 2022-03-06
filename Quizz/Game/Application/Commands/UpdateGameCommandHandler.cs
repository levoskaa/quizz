using AutoMapper;
using MediatR;
using Quizz.Common.ErrorHandling;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Commands
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public UpdateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGameCommand updateGameCommand, CancellationToken cancellationToken)
        {
            await ValidateCommand(updateGameCommand);
            var game = mapper.Map<Game>(updateGameCommand);
            gameRepository.Update(game);
            await gameRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task ValidateCommand(UpdateGameCommand updateGameCommand)
        {
            var currentGame = await gameRepository.GetAsync(updateGameCommand.GameId);
            if (currentGame.OwnerId != updateGameCommand.UserId)
            {
                throw new ForbiddenException();
            }
        }
    }
}
using AutoMapper;
using MediatR;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Commands
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, int>
    {
        private readonly IGameRepository gameRepository;
        private readonly IMapper mapper;

        public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateGameCommand createGameCommand, CancellationToken cancellationToken)
        {
            var game = mapper.Map<Game>(createGameCommand);
            var createdId = gameRepository.Add(game);
            await gameRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return createdId;
        }
    }
}
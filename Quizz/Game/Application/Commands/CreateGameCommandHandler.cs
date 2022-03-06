using AutoMapper;
using MediatR;
using Quizz.Common.Services;
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
        private readonly IIdentityService identityService;

        public CreateGameCommandHandler(
            IGameRepository gameRepository,
            IMapper mapper,
            IIdentityService identityService)
        {
            this.gameRepository = gameRepository;
            this.mapper = mapper;
            this.identityService = identityService;
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
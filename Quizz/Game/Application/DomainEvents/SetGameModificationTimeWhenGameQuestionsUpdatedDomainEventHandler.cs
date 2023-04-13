using MediatR;
using Quizz.GameService.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.DomainEvents
{
    public class SetGameModificationTimeWhenGameQuestionsUpdatedDomainEventHandler
        : INotificationHandler<GameQuestionsUpdatedDomainEvent>
    {
        private readonly IGameRepository gameRepository;

        public SetGameModificationTimeWhenGameQuestionsUpdatedDomainEventHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task Handle(GameQuestionsUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            notification.Game.UpdatedAt = notification.UpdatedAt;
            await gameRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
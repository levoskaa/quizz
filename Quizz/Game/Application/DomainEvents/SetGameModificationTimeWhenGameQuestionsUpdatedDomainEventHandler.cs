using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.DomainEvents
{
    public class SetGameModificationTimeWhenGameQuestionsUpdatedDomainEventHandler
        : INotificationHandler<GameQuestionsUpdatedDomainEvent>
    {
        public async Task Handle(GameQuestionsUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            notification.Game.UpdatedAt = notification.UpdatedAt;
        }
    }
}
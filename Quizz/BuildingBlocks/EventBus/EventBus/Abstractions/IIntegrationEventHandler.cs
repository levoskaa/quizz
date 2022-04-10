using Quizz.BuildingBlocks.EventBus.Events;
using System.Threading.Tasks;

namespace Quizz.BuildingBlocks.EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IntegrationEvent
{
    Task Handle(TIntegrationEvent @event);
}

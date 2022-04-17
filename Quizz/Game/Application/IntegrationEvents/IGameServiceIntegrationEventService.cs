using Quizz.BuildingBlocks.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.IntegrationEvents;

public interface IGameServiceIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);

    Task AddAndSaveEventAsync(IntegrationEvent evt);
}
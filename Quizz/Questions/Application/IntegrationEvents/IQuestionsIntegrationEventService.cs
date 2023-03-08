using Quizz.BuildingBlocks.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace Quizz.Questions.Application.IntegrationEvents;

public interface IQuestionsIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);

    Task AddAndSaveEventAsync(IntegrationEvent evt);
}
using Microsoft.Extensions.Logging;
using Quizz.BuildingBlocks.EventBus.Abstractions;
using Quizz.BuildingBlocks.EventBus.Events;
using Quizz.BuildingBlocks.IntegrationEventLog.Services;
using Quizz.GameService.Data;
using System;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.IntegrationEvents;

public class GameServiceIntegrationEventService : IGameServiceIntegrationEventService
{
    private readonly IEventBus eventBus;
    private readonly GameContext gameContext;
    private readonly IIntegrationEventLogService eventLogService;
    private readonly ILogger<GameServiceIntegrationEventService> logger;

    public GameServiceIntegrationEventService(
        IEventBus eventBus,
        GameContext gameContext,
        IIntegrationEventLogService eventLogService,
        ILogger<GameServiceIntegrationEventService> logger)
    {
        this.gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
        this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        this.eventLogService = eventLogService;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingLogEvents = await eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

        foreach (var logEvt in pendingLogEvents)
        {
            logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})",
                logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

            try
            {
                await eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
                eventBus.Publish(logEvt.IntegrationEvent);
                await eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}",
                    logEvt.EventId, Program.AppName);

                await eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
            }
        }
    }

    public async Task AddAndSaveEventAsync(IntegrationEvent evt)
    {
        logger.LogInformation("----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

        await eventLogService.SaveEventAsync(evt, gameContext.CurrentTransaction);
    }
}
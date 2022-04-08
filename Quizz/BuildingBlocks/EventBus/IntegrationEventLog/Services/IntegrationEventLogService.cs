using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Quizz.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Quizz.BuildingBlocks.IntegrationEventLog.Services;

public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
{
    private readonly IntegrationEventLogContext integrationEventLogContext;
    private readonly DbConnection dbConnection;
    private readonly List<Type> eventTypes;
    private volatile bool isDisposed;

    public IntegrationEventLogService(DbConnection dbConnection)
    {
        this.dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        integrationEventLogContext = new IntegrationEventLogContext(
            new DbContextOptionsBuilder<IntegrationEventLogContext>()
                .UseSqlServer(this.dbConnection)
                .Options);

        eventTypes = Assembly.Load(Assembly.GetEntryAssembly().FullName)
            .GetTypes()
            .Where(t => t.Name.EndsWith(nameof(IntegrationEvent)))
            .ToList();
    }

    public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(Guid transactionId)
    {
        var tid = transactionId.ToString();

        var result = await integrationEventLogContext.IntegrationEventLogs
            .Where(e => e.TransactionId == tid && e.State == EventStateEnum.NotPublished)
            .ToListAsync();

        if (result != null && result.Any())
        {
            return result.OrderBy(e => e.CreationTime)
                .Select(e => e.DeserializeJsonContent(eventTypes.Find(t => t.Name == e.EventTypeShortName)));
        }

        return new List<IntegrationEventLogEntry>();
    }

    public Task SaveEventAsync(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (transaction == null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        var eventLogEntry = new IntegrationEventLogEntry(@event, transaction.TransactionId);

        integrationEventLogContext.Database.UseTransaction(transaction.GetDbTransaction());
        integrationEventLogContext.IntegrationEventLogs.Add(eventLogEntry);

        return integrationEventLogContext.SaveChangesAsync();
    }

    public Task MarkEventAsPublishedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.Published);
    }

    public Task MarkEventAsInProgressAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.InProgress);
    }

    public Task MarkEventAsFailedAsync(Guid eventId)
    {
        return UpdateEventStatus(eventId, EventStateEnum.PublishedFailed);
    }

    private Task UpdateEventStatus(Guid eventId, EventStateEnum status)
    {
        var eventLogEntry = integrationEventLogContext.IntegrationEventLogs
            .Single(e => e.EventId == eventId);
        eventLogEntry.State = status;

        if (status == EventStateEnum.InProgress)
        {
            eventLogEntry.TimesSent++;
        }

        integrationEventLogContext.IntegrationEventLogs.Update(eventLogEntry);

        return integrationEventLogContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            integrationEventLogContext?.Dispose();
            isDisposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
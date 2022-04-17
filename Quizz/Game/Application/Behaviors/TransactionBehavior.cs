using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quizz.BuildingBlocks.EventBus.Extensions;
using Quizz.GameService.Application.IntegrationEvents;
using Quizz.GameService.Data;
using Serilog.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Application.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly GameContext dbContext;
    private readonly IGameServiceIntegrationEventService gameServiceIntegrationEventService;
    private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> logger;

    public TransactionBehaviour(GameContext dbContext,
        IGameServiceIntegrationEventService gameServiceIntegrationEventService,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        this.gameServiceIntegrationEventService = gameServiceIntegrationEventService ?? throw new ArgumentException(nameof(gameServiceIntegrationEventService));
        this.logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                using (var transaction = await dbContext.BeginTransactionAsync())
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})",
                        transaction.TransactionId, typeName, request);

                    response = await next();

                    logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                        transaction.TransactionId, typeName);

                    await dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await gameServiceIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
            throw;
        }
    }
}
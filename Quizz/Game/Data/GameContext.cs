using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Configuration;
using Quizz.GameService.Infrastructure.Extensions;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Data;

public class GameContext : DbContext, IUnitOfWork
{
    public DbSet<Game> Games { get; set; }
    public IDbContextTransaction CurrentTransaction => currentTransaction;
    public bool HasActiveTransaction => currentTransaction != null;

    private readonly IMediator mediator;
    private IDbContextTransaction currentTransaction;

    public GameContext(DbContextOptions<GameContext> options, IMediator mediator) : base(options)
    {
        this.mediator = mediator;
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events collection.
        await mediator.DispatchDomainEventsAsync(this);
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (currentTransaction != null)
        {
            return null;
        }
        currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        return currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction != currentTransaction)
        {
            throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        }

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            currentTransaction?.Rollback();
            throw;
        }
        finally
        {
            if (currentTransaction != null)
            {
                currentTransaction.Dispose();
                currentTransaction = null;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyEntityConfigurations(modelBuilder);
    }

    private static void ApplyEntityConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FindCorrectOrderQuestionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GameEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GameQuestionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MultipleChoiceQuestionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TypeInAnswerQuestionEntityTypeConfiguration());
    }
}
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;
using Quizz.GameService.Data.Configuration;
using Quizz.GameService.Infrastructure.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.GameService.Data
{
    public class GameContext : DbContext, IUnitOfWork
    {
        public DbSet<Game> Games { get; set; }

        private readonly IMediator mediator;

        public GameContext(DbContextOptions<GameContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyEntityConfigurations(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch domain events collection.
            await mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
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
}
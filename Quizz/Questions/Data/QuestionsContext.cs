using MediatR;
using Microsoft.EntityFrameworkCore;
using Quizz.Common.DataAccess;
using Quizz.Common.Models;
using Quizz.GameService.Data.Configuration;
using Quizz.Questions.Infrastructure.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Quizz.Questions.Data
{
    public class QuestionsContext : DbContext, IUnitOfWork
    {
        private readonly IMediator mediator;

        public DbSet<Question> Questions { get; set; }

        public QuestionsContext(IMediator mediator)
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyEntityConfigurations(modelBuilder);
        }

        private static void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AnswerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FindCorrectOrderQuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MultipleChoiceQuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TypeInAnswerQuestionEntityTypeConfiguration());
        }
    }
}
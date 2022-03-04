using MediatR;
using Microsoft.EntityFrameworkCore;
using Quizz.Common.DataAccess;
using Quizz.GameService.Application.Models;
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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch domain events collection.
            await mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
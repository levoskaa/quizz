using MediatR;
using Quizz.Common.DataAccess;
using Quizz.GameService.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Quizz.GameService.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, GameContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(x => x.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}

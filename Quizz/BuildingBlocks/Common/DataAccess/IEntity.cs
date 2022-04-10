using MediatR;
using System.Collections.Generic;

namespace Quizz.Common.DataAccess;

public interface IEntity
{
    IReadOnlyCollection<INotification> DomainEvents { get; }

    void AddDomainEvent(INotification eventItem);

    void RemoveDomainEvent(INotification eventItem);

    void ClearDomainEvents();
}
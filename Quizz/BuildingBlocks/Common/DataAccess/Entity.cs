using MediatR;
using System.Collections.Generic;

namespace Quizz.Common.DataAccess
{
    public abstract class Entity
    {
        private int id;

        public virtual int Id
        {
            get
            {
                return id;
            }
            protected set
            {
                id = value;
            }
        }

        private readonly List<INotification> domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents.AsReadOnly();

        public Entity()
        {
            domainEvents = new List<INotification>();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents.Clear();
        }
    }
}
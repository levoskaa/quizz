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

        private List<INotification> domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            domainEvents ??= new List<INotification>();
            domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }
    }
}
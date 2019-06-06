using System.Collections.Generic;

namespace Core
{
    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

        protected AggregateRoot(EntityId id) : base(id)
        {
        }

        public virtual IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            domainEvents.Clear();
        }
    }
}

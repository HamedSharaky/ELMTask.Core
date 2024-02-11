using CSharpFunctionalExtensions;

namespace ELM.Core.Domain.Common
{
    public interface IAggregateRoot
    {
        ICollection<IDomainEvent> GetEventsThenClear();
    }

    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot where TId : IComparable<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TId id)
            : base(id)
        {
        }

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        protected void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public ICollection<IDomainEvent> GetEventsThenClear()
        {
            var eventsCopy = _domainEvents.ToList();

            _domainEvents.Clear();

            return eventsCopy;
        }
    }
}

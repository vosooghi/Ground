using Ground.Core.Domain.Events;

namespace Ground.Core.Domain.Entities
{
    /// <summary>
    /// Represents an aggregate root in the domain-driven design (DDD) context. 
    /// </summary>
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}

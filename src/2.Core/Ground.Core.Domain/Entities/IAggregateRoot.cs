using Ground.Core.Domain.Events;

namespace Ground.Core.Domain.Entities
{
    public interface IAggregateRoot
    {
        void ClearEvents();
        IEnumerable<IDomainEvent> GetEvents();
    }
}

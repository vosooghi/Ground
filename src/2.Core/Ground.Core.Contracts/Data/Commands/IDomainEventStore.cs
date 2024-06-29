using Ground.Core.Domain.Events;

namespace Ground.Core.Contracts.Data.Commands
{
    /// <summary>
    /// To save and restore events
    /// </summary>
    public interface IDomainEventStore
    {
        void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
        Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
    }
}

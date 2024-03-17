using Ground.Core.Domain.Events;

namespace Ground.Core.Contracts.ApplicationServices.Events
{
    public interface IEventDispatcher
    {
        Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
    }
}

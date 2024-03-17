using Ground.Core.Contracts.ApplicationServices.Events;
using Ground.Core.Domain.Events;

namespace Ground.Extensions.MessageBus.MessageInbox.Sample.EventDisptacher
{
    public class EventDistpacher : IEventDispatcher
    {
        public Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            throw new NotImplementedException();
        }
    }
}

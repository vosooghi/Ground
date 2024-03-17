using Ground.Core.Domain.Events;

namespace Ground.Core.Contracts.ApplicationServices.Events
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        Task Handle(TDomainEvent Event);
    }
}

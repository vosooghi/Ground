using Ground.Core.Domain.Events;

namespace Ground.Extensions.MessageBus.MessageInbox.Sample.Models
{
    public class PersonEvent : IDomainEvent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

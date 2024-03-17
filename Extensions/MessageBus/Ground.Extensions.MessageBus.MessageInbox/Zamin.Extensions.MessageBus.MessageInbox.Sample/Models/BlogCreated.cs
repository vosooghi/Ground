using Ground.Core.Domain.Events;

namespace Ground.Extensions.MessageBus.MessageInbox.Sample.Models
{
    public class BlogCreated : IDomainEvent
    {
        public string BusinessId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public BlogCreated(string businessId, string title, string description)
        {
            BusinessId = businessId;
            Title = title;
            Description = description;
        }
    }
}

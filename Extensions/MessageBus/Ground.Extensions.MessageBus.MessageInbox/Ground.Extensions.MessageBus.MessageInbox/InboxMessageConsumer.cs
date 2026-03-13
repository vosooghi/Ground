using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.Contracts.ApplicationServices.Events;
using Ground.Core.Domain.Events;
using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.MessageInbox.Options;
using Ground.Extensions.Serializers.Abstractions;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Windows.Input;

namespace Ground.Extensions.MessageBus.MessageInbox
{
    /// <summary>
    /// Consumes and processes command and event messages from an inbox, dispatching them to the appropriate handlers.
    /// </summary>
    /// <remarks>InboxMessageConsumer is responsible for handling incoming messages by deserializing their
    /// contents and invoking the corresponding command or domain event handlers. It ensures that each message is
    /// processed only once, based on its unique identifier and sender. This class is typically used in scenarios where
    /// reliable message delivery and deduplication are required, such as distributed systems or event-driven
    /// architectures.</remarks>
    public class InboxMessageConsumer : IMessageConsumer
    {
        private readonly MessageInboxOptions _messageInboxOptions;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMessageInboxItemRepository _messageInboxItemRepository;
        private readonly List<Type> _domainEventTypes = [];
        private readonly List<Type> _commandTypes = []; 
        public InboxMessageConsumer(IOptions<MessageInboxOptions> messageInboxOptions,
                                    IJsonSerializer jsonSerializer,
                                    IMessageInboxItemRepository messageInboxItemRepository,
                                    ICommandDispatcher commandDispatcher,
                                    IEventDispatcher eventDispatcher)
        {
            _messageInboxOptions = messageInboxOptions.Value;
            _eventDispatcher = eventDispatcher;
            _jsonSerializer = jsonSerializer;
            _commandDispatcher = commandDispatcher;
            _messageInboxItemRepository = messageInboxItemRepository;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            _domainEventTypes.AddRange(assemblies.SelectMany(assembly => assembly.GetTypes().Where(c => c.IsAssignableTo(typeof(IDomainEvent)) && c.IsClass).ToList()).ToList());
            _commandTypes.AddRange(assemblies.SelectMany(assembly => assembly.GetTypes().Where(c => c.IsAssignableTo(typeof(ICommand)) && c.IsClass).ToList()).ToList());
        }

        public Task<bool> ConsumeCommand(string sender, Parcel parcel)
        {
            throw new NotImplementedException();
            //if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            //{
            //    var mapToClass = _messageTypeMap[parcel.Route];
            //    var commandType = Type.GetType(mapToClass);
            //    dynamic command = _jsonSerializer.Deserialize(parcel.MessageBody, commandType);
            //    _commandDispatcher.Send(command);
            //    _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            //}    }

        }
        /// <summary>
        /// This method processes incoming event messages by first checking if the message can be received (i.e., it has not been processed before from the same sender). If allowed, it identifies the corresponding domain event type based on the message name, deserializes the message body into an instance of that event type, and then publishes the event using the event dispatcher. Finally, it marks the message as received in the repository to prevent future duplicate processing.
        /// </summary>
        /// <param name="sender">The sender of the message.</param>
        /// <param name="parcel">The parcel containing the message.</param>
        /// <returns>A task representing the asynchronous operation, with a boolean result indicating success.</returns>
        public async Task<bool> ConsumeEvent(string sender, Parcel parcel)
        {
            if (await _messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var eventType = _domainEventTypes.FirstOrDefault(c => c.Name == parcel.MessageName);
                if (eventType != null)
                {
                    dynamic @event = _jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                    await _eventDispatcher.PublishDomainEventAsync(@event);
                    await _messageInboxItemRepository.Receive(parcel.MessageId, sender, parcel.MessageBody);
                }
            }
            return true;
        }
    }
}

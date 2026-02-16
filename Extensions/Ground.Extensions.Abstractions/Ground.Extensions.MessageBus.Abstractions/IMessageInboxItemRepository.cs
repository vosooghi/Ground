namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// Defines methods for determining message receipt eligibility and processing incoming messages in an inbox
    /// repository.
    /// </summary>
    /// <remarks>Implementations of this interface are responsible for managing the receipt and storage of
    /// messages identified by a unique message ID and originating service. The interface enables callers to check
    /// whether a message can be received and to process the receipt of messages. Thread safety and persistence
    /// guarantees depend on the specific implementation.</remarks>
    public interface IMessageInboxItemRepository
    {
        /// <summary>
        /// Checks if a message with the specified ID from the given service is allowed to be received.
        /// </summary>
        /// <param name="messageId">The unique identifier of the message. Cannot be null or empty.</param>
        /// <param name="fromService">The identifier of the service that sent the message. Cannot be null or empty.</param>
        /// <returns>True if the message is allowed to be received; otherwise, false.</returns>
        Task<bool> AllowReceive(string messageId, string fromService);
        /// <summary>
        /// Processes an incoming message identified by the specified message ID, originating from the given service,
        /// and containing the provided payload.
        /// </summary>
        /// <param name="messageId">The unique identifier for the incoming message. Cannot be null or empty.</param>
        /// <param name="fromService">The name of the service that sent the message. Cannot be null or empty.</param>
        /// <param name="payload">The content of the message to be processed. Cannot be null.</param>
        Task Receive(string messageId, string fromService, string payload);
    }
}

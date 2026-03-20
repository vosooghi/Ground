namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// Defines an interface for sending messages, commands, and parcels to other services or components.
    /// </summary>
    public interface ISendMessageBus
    {
        /// <summary>
        /// Sends a message of type TInput to all subscribers.
        /// </summary>
        /// <typeparam name="TInput">The type of the message to publish.</typeparam>
        /// <param name="input">The message instance to publish.</param>
        Task Publish<TInput>(TInput input);

        /// <summary>
        /// Sends a command with the specified data to a target service for processing.
        /// </summary>
        /// <typeparam name="TCommandData">The type of the data payload to include with the command.</typeparam>
        /// <param name="destinationService">The name or identifier of the service that should receive the command. Cannot be null or empty.</param>
        /// <param name="commandName">The name of the command to send. This determines the action to be performed by the destination service.
        /// Cannot be null or empty.</param>
        /// <param name="commandData">The data associated with the command. The structure and content should match the expectations of the
        /// destination service for the specified command.</param>
        Task SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData);

        /// <summary>
        /// Sends a command with the specified data to a target service for processing.
        /// </summary>
        /// <typeparam name="TCommandData">The type of the data payload to include with the command.</typeparam>
        /// <param name="destinationService">The name or identifier of the service that should receive the command. Cannot be null or empty.</param>
        /// <param name="commandName">The name of the command to send. This identifies the action to be performed by the destination service.
        /// Cannot be null or empty.</param>
        /// <param name="correlationId">A unique identifier used to correlate this command with related operations or responses. Cannot be null or
        /// empty.</param>
        /// <param name="commandData">The data payload associated with the command. The structure and content should match the expectations of the
        /// destination service for the specified command.</param>
        Task SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData);

        /// <summary>
        /// Sends the specified parcel for delivery using the configured transport mechanism.
        /// </summary>
        /// <param name="parcel">The parcel to be sent. Cannot be null.</param>
        Task Send(Parcel parcel);

        Task InitializeAsync();
    }
}

namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// Defines an interface for subscribing to and receiving messages from a message bus, enabling services to listen
    /// for events and commands.
    /// </summary>
    public interface IReceiveMessageBus
    {
        /// <summary>
        /// Subscribes to a specified event from the given service.
        /// </summary>
        /// <param name="serviceId">The unique identifier of the service to subscribe to. Cannot be null or empty.</param>
        /// <param name="eventName">The name of the event to subscribe to. Cannot be null or empty.</param>
        Task Subscribe(string serviceId, string eventName);

        /// <summary>
        /// Processes the specified command by name, triggering the associated action or handler.
        /// </summary>
        /// <param name="commandName">The name of the command to process. Cannot be null or empty.</param>
        Task Receive(string commandName);

        Task InitializeAsync();
    }
}

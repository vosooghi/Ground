namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// Defines a contract for consuming events and commands from a message sender using a parcel payload.
    /// </summary>
    /// <remarks>Implementations of this interface are responsible for processing incoming messages, such as
    /// events or commands, identified by the sender and encapsulated in a parcel. The interface does not specify
    /// message transport or error handling strategies; these are determined by the implementing class.</remarks>
    public interface IMessageConsumer
    {
        /// <summary>
        /// Processes the specified event parcel sent by the given sender and indicates whether the event was
        /// successfully consumed.
        /// </summary>
        /// <param name="sender">The identifier of the event sender. Cannot be null or empty.</param>
        /// <param name="parcel">The event parcel to be processed. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the event
        /// was successfully consumed; otherwise, <see langword="false"/>.</returns>
        Task<bool> ConsumeEvent(string sender, Parcel parcel);
        /// <summary>
        /// Processes a command received from the specified sender using the provided parcel data asynchronously.
        /// </summary>
        /// <param name="sender">The identifier of the sender issuing the command. Cannot be null or empty.</param>
        /// <param name="parcel">The parcel containing the command data to be processed. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the command
        /// was successfully consumed; otherwise, <see langword="false"/>.</returns>
        Task<bool> ConsumeCommand(string sender, Parcel parcel);
    }
}

namespace Ground.Extensions.MessageBus.MessageInbox.Options
{
    /// <summary>
    /// The options for configuring the Message Inbox feature.
    /// </summary>
    public class MessageInboxOptions
    {
        /// <summary>
        /// Gets or sets the name of the application. This can be useful for identifying the source of messages in a distributed system.
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;
    }
}

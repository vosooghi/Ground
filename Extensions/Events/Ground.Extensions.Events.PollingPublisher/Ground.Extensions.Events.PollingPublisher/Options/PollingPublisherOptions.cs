namespace Ground.Extensions.Events.PollingPublisher.Options
{
    /// <summary>
    /// The options for configuring the Polling Publisher.
    /// </summary>
    public class PollingPublisherOptions
    {
        /// <summary>
        /// The interval in milliseconds between each polling attempt to send events.
        /// </summary>
        public int SendInterval { get; set; } = 1000;
        /// <summary>
        /// The interval in milliseconds to wait after an exception before retrying.
        /// </summary>
        public int ExceptionInterval { get; set; } = 10000;
        /// <summary>
        /// The number of events to send in each polling attempt.
        /// </summary>
        public int SendCount { get; set; } = 100;
        /// <summary>
        /// The name of the application using the Polling Publisher.
        /// </summary>
        public string ApplicationName { get; set; } = "UnknownApplication";
    }
}

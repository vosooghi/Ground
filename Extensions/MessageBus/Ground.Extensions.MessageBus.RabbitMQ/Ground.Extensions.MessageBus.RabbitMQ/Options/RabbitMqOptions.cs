namespace Ground.Extensions.MessageBus.RabbitMQ.Options
{
    /// <summary>
    /// The class represents the configuration options for connecting to a RabbitMQ message broker.
    /// </summary>
    public class RabbitMqOptions
    {
        /// <summary>
        /// The URL of the RabbitMQ server, including the protocol, host, and port.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Indicates whether messages should be persisted.
        /// </summary>
        public bool PersistMessage { get; set; }
        /// <summary>
        /// The name of the exchange to use for message routing.
        /// </summary>  
        public string ExchangeName { get; set; }
        /// <summary>
        /// The name of the service using the RabbitMQ message broker.
        /// </summary>  
        public string ServiceName { get; set; }
    }
}

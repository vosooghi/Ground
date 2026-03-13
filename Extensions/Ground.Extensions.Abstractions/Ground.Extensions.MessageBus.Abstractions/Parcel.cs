namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// Data structure representing a message parcel for transmission.
    /// </summary>
    public class Parcel
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        public string MessageId { get; set; } = string.Empty;
        /// <summary>
        /// Correlation identifier.
        /// </summary>
        public string CorrelationId { get; set; } = string.Empty;
        /// <summary>
        /// Name of the message.
        /// </summary>
        public string MessageName { get; set; } = string.Empty;
        /// <summary>
        /// Headers associated with the message.
        /// </summary>
        public Dictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// Body of the message.
        /// </summary>
        public string MessageBody { get; set; } = string.Empty;
        /// <summary>
        /// Route for the message.
        /// </summary>
        public string Route { get; set; } = string.Empty;
    }
}

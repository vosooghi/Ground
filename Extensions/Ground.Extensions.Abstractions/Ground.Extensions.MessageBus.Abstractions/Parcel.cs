namespace Ground.Extensions.MessageBus.Abstractions
{
    /// <summary>
    /// the structure of the message
    /// </summary>
    public class Parcel
    {
        public string MessageId { get; set; } = string.Empty;
        public string CorrelationId { get; set; } = string.Empty;
        public string MessageName { get; set; } = string.Empty;
        public Dictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();
        public string MessageBody { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
    }
}

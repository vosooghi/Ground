namespace Ground.Extensions.Events.Abstractions
{
    /// <summary>
    /// Represents an event item stored in the outbox for reliable event processing and delivery.
    /// </summary>
    public class OutBoxEventItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the outbox event item.
        /// </summary>
        public long OutBoxEventItemId { get; set; }
        /// <summary>
        /// The unique identifier of the event in the service's context.
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// The identifier of the user who triggered or is associated with the event.
        /// </summary>
        public string AccuredByUserId { get; set; }
        /// <summary>
        /// The date and time when the item was accrued.
        /// </summary>
        public DateTime AccuredOn { get; set; }
        /// <summary>
        /// The name of the aggregate associated with the event.
        /// </summary>
        public string AggregateName { get; set; }
        /// <summary>
        /// The name of the aggregate type associated with the current instance.
        /// </summary>
        public string AggregateTypeName { get; set; }
        /// <summary>
        /// The ID for the aggregate instance.
        /// </summary>
        public string AggregateId { get; set; }
        /// <summary>
        /// The name of the event associated with this instance.
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// The name of the event type associated with this instance.
        /// </summary>
        public string EventTypeName { get; set; }
        /// <summary>
        /// The serialized payload data associated with the event.
        /// </summary>
        public string EventPayload { get; set; }
        /// <summary>
        /// Gets or sets the unique identifier used to trace the operation across distributed systems.
        /// </summary>        
        public string? TraceId { get; set; }
        /// <summary>
        /// The identifier for the current span in a trace context.
        /// </summary>
        public string? SpanId { get; set; }
        /// <summary>
        /// Indicates whether the event has been processed.
        /// </summary>
        public bool IsProcessed { get; set; }
    }
}

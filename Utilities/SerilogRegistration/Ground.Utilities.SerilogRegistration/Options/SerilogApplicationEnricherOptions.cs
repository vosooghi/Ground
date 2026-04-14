namespace Ground.Utilities.SerilogRegistration.Options
{
    /// <summary>
    /// The options for adding a fixed set of contextual properties to every Serilog LogEvent
    /// </summary>
    public class SerilogApplicationEnricherOptions
    {
        public string ApplicationName { get; set; } = "UnknownApplication";
        public string ServiceName { get; set; } = "UnknownService";
        public string ServiceVersion { get; set; } = "UnknownVersion";
        public string ServiceId { get; set; } = "UnknownServiceId";
    }
}

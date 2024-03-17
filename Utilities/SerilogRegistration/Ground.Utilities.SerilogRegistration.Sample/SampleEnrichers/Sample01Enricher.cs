using Ground.Utilities.SerilogRegistration.Options;
using Microsoft.Extensions.Options;
using Serilog.Core;
using Serilog.Events;

namespace Ground.Utilities.SerilogRegistration.Sample.SampleEnrichers
{
    public class Sample01Enricher : ILogEventEnricher
    {
        private readonly SerilogApplicationEnricherOptions _options;
        public Sample01Enricher(IOptions<SerilogApplicationEnricherOptions> options)
        {
            _options = options.Value;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var MyFirstNameProperty = propertyFactory.CreateProperty("MyFirstName", "Abbas");
            var MyLastNameProperty = propertyFactory.CreateProperty("MyLastName", "Vosoughi");
            logEvent.AddPropertyIfAbsent(MyFirstNameProperty);
            logEvent.AddPropertyIfAbsent(MyLastNameProperty);
        }
    }
}

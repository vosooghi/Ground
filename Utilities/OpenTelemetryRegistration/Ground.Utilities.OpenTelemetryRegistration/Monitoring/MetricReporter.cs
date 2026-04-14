using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace Ground.Utilities.OpenTelemetryRegistration.Monitoring
{
    /// <summary>
    /// Provides functionality to record OpenTelemetry counters and histograms for HTTP request monitoring.
    /// </summary>
    public class MetricReporter
    {
        private readonly Counter<int> _requestCounter;
        private readonly Histogram<double> _responseTimeHistogram;

        public string MetricName { get; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MetricReporter"/> class.
        /// </summary>
        /// <param name="meterName">The name of the meter.</param>
        /// <param name="prefix">The prefix for the metric names.</param>
        public MetricReporter(string meterName, string prefix)
        {
            var meter = new Meter(meterName);
            MetricName = meterName;

            _requestCounter = meter.CreateCounter<int>(
                name: $"{prefix}_total_request",
                unit: "hits",
                description: "the total number of requests");

            _responseTimeHistogram = meter.CreateHistogram<double>(
                name: $"{prefix}_request_duration_seconds",
                unit: "double",
                description: "The duration in seconds between the response to a request.");
        }
        
        /// <summary>
        /// Registers a request for monitoring.
        /// </summary>
        /// <param name="path">The path of the request.</param>
        public void RegisterRequest(string? path)
            => _requestCounter.Add(1, new KeyValuePair<string, object?>("path", path));
        
        /// <summary>
        /// Registers the response time for a request.
        /// </summary>
        /// <param name="statusCode">The HTTP status code of the response.</param>
        /// <param name="httpMethod">The HTTP method of the request.</param>
        /// <param name="path">The path of the request.</param>
        /// <param name="elapsed">The elapsed time for the request.</param>
        public void RegisterResponseTime(int statusCode, string httpMethod, string? path, TimeSpan elapsed)
        {
            var keyValuePairs = new KeyValuePair<string, object?>[3]
            {
            new("statusCode", statusCode),
            new("httpMethod", httpMethod),
            new("path", path)
            };
            var tags = new TagList(keyValuePairs);
            _responseTimeHistogram.Record(elapsed.TotalSeconds, tags);
        }

    }
}

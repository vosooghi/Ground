using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Ground.Utilities.OpenTelemetryRegistration.Monitoring
{
    /// <summary>
    /// Middleware that records metrics for HTTP responses, including request counts and response times, for each
    /// request except those to the /metrics endpoint.
    /// </summary>
    /// <remarks>This middleware should be registered early in the pipeline to ensure accurate measurement of
    /// request durations and counts. Requests to the /metrics endpoint are excluded from metric collection to prevent
    /// self-reporting.</remarks>
    /// <param name="request">The next middleware in the HTTP request pipeline to invoke.</param>
    public class ResponseMetricMiddleware(RequestDelegate request)
    {                
        public async Task Invoke(HttpContext httpContext, MetricReporter reporter)
        {
            var path = httpContext.Request.Path.Value;

            if (path == "/metrics")
            {
                await request.Invoke(httpContext);
                return;
            }
            var sw = Stopwatch.StartNew();

            try
            {
                await request.Invoke(httpContext);
            }
            finally
            {
                sw.Stop();
                reporter.RegisterRequest(path);
                reporter.RegisterResponseTime(httpContext.Response.StatusCode,
                    httpContext.Request.Method, path, sw.Elapsed);
            }
        }

    }
}

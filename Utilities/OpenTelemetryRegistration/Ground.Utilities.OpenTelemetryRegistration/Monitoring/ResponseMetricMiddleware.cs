using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Ground.Utilities.OpenTelemetryRegistration.Monitoring
{
    public class ResponseMetricMiddleware//(RequestDelegate request) c#12
    {
        public ResponseMetricMiddleware(RequestDelegate request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }
        private readonly RequestDelegate _request;

        public async Task Invoke(HttpContext httpContext, MetricReporter reporter)
        {
            var path = httpContext.Request.Path.Value;

            if (path == "/metrics")
            {
                await _request.Invoke(httpContext);
                return;
            }
            var sw = Stopwatch.StartNew();

            try
            {
                await _request.Invoke(httpContext);
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

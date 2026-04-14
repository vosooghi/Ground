using Ground.Utilities.OpenTelemetryRegistration.Monitoring;
using Ground.Utilities.OpenTelemetryRegistration.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Ground.Extensions.DependencyInjection
{
    public static class OpenTeletmetryServiceCollectionExtensions
    {
        private const string DefaultSectionName = "OpenTeletmetryOptions";

        public static IServiceCollection AddGroundObservabilitySupport(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(DefaultSectionName);

            services.Configure<OpenTeletmetryOptions>(section);

            RegisterTraceServices(services, section);
            RegisterMetricService(services, section);

            return services;
        }

        public static IServiceCollection AddGroundObservabilitySupport(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            var section = configuration.GetSection(sectionName);

            services.Configure<OpenTeletmetryOptions>(section);

            RegisterTraceServices(services, section);
            RegisterMetricService(services, section);

            return services;
        }

        private static void RegisterTraceServices(IServiceCollection services, IConfiguration section)
        {
            var options = section.Get<OpenTeletmetryOptions>() ?? new OpenTeletmetryOptions();

            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                        var serviceName = $"{options.ApplicationName}.{options.ServiceName}";

                    tracerProviderBuilder
                        .SetResourceBuilder(ResourceBuilder.CreateDefault()
                            .AddService(serviceName: serviceName, serviceVersion: options.ServiceVersion, serviceInstanceId: options.ServiceId))
                        .AddHttpClientInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddSqlClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .SetSampler(new TraceIdRatioBasedSampler(options.SamplingProbability))
                        .AddOtlpExporter(oltpOptions =>
                        {
                            oltpOptions.Endpoint = new Uri(options.OltpEndpoint);
                            oltpOptions.ExportProcessorType = options.ExportProcessorType;
                        });
                });
        }

        private static void RegisterMetricService(IServiceCollection services, IConfiguration section)
        {
            var options = section.Get<OpenTeletmetryOptions>() ?? new OpenTeletmetryOptions();

            services.AddOpenTelemetry()
                .WithMetrics(opts => opts
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(options.ApplicationName))
                    .AddMeter(options.ApplicationName)
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter());

            services.AddSingleton(new MetricReporter(options.ApplicationName, options.ServiceName));
        }

        public static IApplicationBuilder UseGroundObservabilityMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseOpenTelemetryPrometheusScrapingEndpoint();
            return app;
        }
    }
}

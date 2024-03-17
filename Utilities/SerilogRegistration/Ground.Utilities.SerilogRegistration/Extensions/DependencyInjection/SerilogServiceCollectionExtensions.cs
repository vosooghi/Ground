using Ground.Utilities.SerilogRegistration.Enrichers;
using Ground.Utilities.SerilogRegistration.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

namespace Ground.Extensions.DependencyInjection
{
    public static class SerilogServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, IConfiguration configuration, params Type[] enrichersType)
        {

            builder.Services.Configure<SerilogApplicationEnricherOptions>(configuration);
            return AddServices(builder, enrichersType);
        }

        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string sectionName, params Type[] enrichersType)
        {
            return builder.AddGroundSerilog(configuration.GetSection(sectionName), enrichersType);
        }

        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, Action<SerilogApplicationEnricherOptions> setupAction, params Type[] enrichersType)
        {
            builder.Services.Configure(setupAction);
            return AddServices(builder, enrichersType);
        }

        private static WebApplicationBuilder AddServices(WebApplicationBuilder builder, params Type[] enrichersType)
        {

            List<ILogEventEnricher> logEventEnrichers = new();

            builder.Services.AddTransient<GroundUserInfoEnricher>();
            builder.Services.AddTransient<GroundApplicaitonEnricher>();
            foreach (var enricherType in enrichersType)
            {
                builder.Services.AddTransient(enricherType);
            }

            builder.Host.UseSerilog((ctx, services, lc) => {
                logEventEnrichers.Add(services.GetRequiredService<GroundUserInfoEnricher>());
                logEventEnrichers.Add(services.GetRequiredService<GroundApplicaitonEnricher>());
                foreach (var enricherType in enrichersType)
                {
                    logEventEnrichers.Add(services.GetRequiredService(enricherType) as ILogEventEnricher);
                }

                lc
                //.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .Enrich.FromLogContext()
                .Enrich.With(logEventEnrichers.ToArray())//([.. logEventEnrichers]) c#12
                .Enrich.WithExceptionDetails()
                .Enrich.WithSpan()
                .ReadFrom.Configuration(ctx.Configuration);
            });
            return builder;
        }
    }
}

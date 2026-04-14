using Ground.Utilities.SerilogRegistration.Enrichers;
using Ground.Utilities.SerilogRegistration.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

namespace Ground.Extensions.DependencyInjection
{
    /// <summary>
    /// Methods to “plug in” Serilog into a service built on the Ground framework, including registering and wiring up log enrichers.
    /// </summary>
    public static class SerilogServiceCollectionExtensions
    {
        /// <summary>
        ///  Configures Serilog logging for the application using settings from the specified configuration and optional enricher types.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance.</param>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="enrichersType">An array of custom enricher types.</param>
        /// <returns>The WebApplicationBuilder instance.</returns>
        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, IConfiguration configuration, params Type[] enrichersType)
        {

            builder.Services.Configure<SerilogApplicationEnricherOptions>(configuration);
            return AddServices(builder, enrichersType);
        }

        /// <summary>
        /// Configures Serilog logging for the application using settings from the specified configuration section and
        /// optional enricher types.
        /// </summary>
        /// <remarks>This method allows you to configure Serilog logging by specifying a configuration
        /// section and optional enrichers. It is intended to be called during application startup as part of the
        /// builder configuration pipeline.</remarks>
        /// <param name="builder">The WebApplicationBuilder instance to configure.</param>
        /// <param name="configuration">The configuration source containing application settings.</param>
        /// <param name="sectionName">The name of the configuration section that contains Serilog settings.</param>
        /// <param name="enrichersType">An array of types representing Serilog enrichers to be added to the logger configuration.</param>
        /// <returns>The same WebApplicationBuilder instance, enabling method chaining.</returns>
        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, IConfiguration configuration, string sectionName, params Type[] enrichersType)
        {
            return builder.AddGroundSerilog(configuration.GetSection(sectionName), enrichersType);
        }

        /// <summary>
        /// Configures Serilog logging for the application using settings from the specified configuration section and optional enricher types.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance to configure.</param>
        /// <param name="setupAction">An action to configure SerilogApplicationEnricherOptions.</param>
        /// <param name="enrichersType">An array of types representing Serilog enrichers to be added to the logger configuration.</param>
        /// <returns>The same WebApplicationBuilder instance, enabling method chaining.</returns>
        public static WebApplicationBuilder AddGroundSerilog(this WebApplicationBuilder builder, Action<SerilogApplicationEnricherOptions> setupAction, params Type[] enrichersType)
        {
            builder.Services.Configure(setupAction);
            return AddServices(builder, enrichersType);
        }

        private static WebApplicationBuilder AddServices(WebApplicationBuilder builder, params Type[] enrichersType)
        {

            List<ILogEventEnricher> logEventEnrichers = new();

            //IHttpContextAccessor is not registered by default.
            //https://github.com/aspnet/Hosting/issues/793
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
                .Enrich.FromLogContext()
                .Enrich.With([.. logEventEnrichers])
                .Enrich.WithExceptionDetails()
                .Enrich.WithSpan()
                .ReadFrom.Configuration(ctx.Configuration);
            });
            return builder;
        }
    }
}

using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.MessageInbox;
using Ground.Extensions.MessageBus.MessageInbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    /// <summary>
    /// Add services for the Message Inbox feature to the DI container.
    /// </summary>
    public static class MessageInboxServiceCollectionExtensions
    {
        /// <summary>
        /// Add services for the Message Inbox feature to the DI container, using configuration from the specified IConfiguration instance. The configuration should be bound to the MessageInboxOptions class, which contains settings for the Message Inbox feature.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The IConfiguration instance containing the settings for the Message Inbox feature.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddGroundMessageInbox(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessageInboxOptions>(configuration);
            AddServices(services);
            return services;
        }

        /// <summary>
        /// Add services for the Message Inbox feature to the DI container, using configuration from a specific section of the provided IConfiguration instance. The section should be bound to the MessageInboxOptions class, which contains settings for the Message Inbox feature.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The IConfiguration instance containing the settings for the Message Inbox feature.</param>
        /// <param name="sectionName">The name of the configuration section to bind to the MessageInboxOptions class.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddGroundMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddGroundMessageInbox(configuration.GetSection(sectionName));
            return services;
        }

        /// <summary>
        /// Adds and configures the ground message inbox services to the specified service collection.  
        /// </summary>
        /// <remarks>Call this method during application startup to enable ground message inbox
        /// functionality. The configuration provided by <paramref name="setupAction"/> determines the behavior of the
        /// inbox services.</remarks>
        /// <param name="services">The service collection to which the ground message inbox services will be added.</param>
        /// <param name="setupAction">An action to configure the options for the ground message inbox.</param>
        /// <returns>The service collection with the ground message inbox services registered.</returns>
        public static IServiceCollection AddGroundMessageInbox(this IServiceCollection services, Action<MessageInboxOptions> setupAction)
        {
            services.Configure(setupAction);
            AddServices(services);
            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IMessageConsumer, InboxMessageConsumer>();
        }
    }
}

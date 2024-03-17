using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.MessageInbox;
using Ground.Extensions.MessageBus.MessageInbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class MessageInboxServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundMessageInbox(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessageInboxOptions>(configuration);
            AddServices(services);
            return services;
        }

        public static IServiceCollection AddGroundMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddGroundMessageInbox(configuration.GetSection(sectionName));
            return services;
        }

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

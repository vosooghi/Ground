using Ground.Extensions.MessageBus.Abstractions;
using Ground.Extensions.MessageBus.MessageInbox.Dal.Dapper;
using Ground.Extensions.MessageBus.MessageInbox.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class MessageInboxServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MessageInboxDalDapperOptions>(configuration);
            AddServices(services);
            return services;
        }

        public static IServiceCollection AddGroundMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddGroundMessageInboxDalSql(configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddGroundMessageInboxDalSql(this IServiceCollection services, Action<MessageInboxDalDapperOptions> setupAction)
        {
            services.Configure(setupAction);
            AddServices(services);
            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageInboxItemRepository, SqlMessageInboxItemRepository>();
        }
    }
}

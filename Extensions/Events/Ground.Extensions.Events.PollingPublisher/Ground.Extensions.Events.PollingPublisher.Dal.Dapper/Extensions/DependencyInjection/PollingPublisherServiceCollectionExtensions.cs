using Ground.Extensions.Events.Abstractions;
using Ground.Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess;
using Ground.Extensions.Events.PollingPublisher.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.Events.PollingPublisher.Dal.Dapper.Extensions.DependencyInjection
{
    public static class PollingPublisherServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PollingPublisherDalRedisOptions>(configuration);
            AddServices(services);
            return services;
        }

        public static IServiceCollection AddGroundPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddGroundPollingPublisherDalSql(configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddGroundPollingPublisherDalSql(this IServiceCollection services, Action<PollingPublisherDalRedisOptions> setupAction)
        {
            services.Configure(setupAction);
            AddServices(services);
            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IOutBoxEventItemRepository, SqlOutBoxEventItemRepository>();
        }
    }
}

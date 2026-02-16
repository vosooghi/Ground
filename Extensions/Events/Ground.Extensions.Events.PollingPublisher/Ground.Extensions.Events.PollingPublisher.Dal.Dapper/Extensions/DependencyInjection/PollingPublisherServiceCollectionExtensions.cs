using Ground.Extensions.Events.Abstractions;
using Ground.Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess;
using Ground.Extensions.Events.PollingPublisher.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering the polling publisher DAL services with SQL Server using Dapper in the dependency injection container.
    /// </summary>
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

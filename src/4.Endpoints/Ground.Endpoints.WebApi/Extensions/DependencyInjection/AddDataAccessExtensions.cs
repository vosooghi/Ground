using Ground.Core.Contracts.Data.Commands;
using Ground.Core.Contracts.Data.Queries;
using System.Reflection;

namespace Ground.Endpoints.WebApi.Extensions.DependencyInjection
{

    /// <summary>
    /// Provides extension methods for registering data access services in the DI container.
    /// </summary>
    public static class AddDataAccessExtensions
    {
        
        public static IServiceCollection AddGroundDataAccess(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddRepositories(assembliesForSearch).AddUnitOfWorks(assembliesForSearch);

        private static IServiceCollection AddRepositories(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandRepository<,>), typeof(IQueryRepository));

        private static IServiceCollection AddUnitOfWorks(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IUnitOfWork));
    }
}

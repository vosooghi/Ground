using Ground.Extensions.Caching.Abstractions;
using Ground.Extensions.Caching.InMemory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering in-memory caching services and adapters.
    /// </summary>
    public static class InMemoryCachingServiceCollectionExtensions
    {
        /// <summary>
        /// Adds in-memory caching services and registers the ground cache adapter implementation to the specified
        /// service collection.
        /// </summary>
        /// <remarks>This method registers the default .NET in-memory cache and configures the dependency
        /// injection container to use the ground cache adapter implementation for caching operations. Call this method
        /// during application startup to enable caching support.</remarks>
        /// <param name="services">The service collection to which the in-memory caching and cache adapter services will be added. Cannot be
        /// null.</param>
        /// <returns>The same service collection instance, with in-memory caching and the ground cache adapter registered.</returns>
        public static IServiceCollection AddGroundInMemoryCaching(this IServiceCollection services)
            => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
    }
}

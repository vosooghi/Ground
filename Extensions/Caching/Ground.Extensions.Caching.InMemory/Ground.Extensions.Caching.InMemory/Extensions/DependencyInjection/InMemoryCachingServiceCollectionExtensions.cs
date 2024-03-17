using Ground.Extensions.Caching.Abstractions;
using Ground.Extensions.Caching.InMemory.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class InMemoryCachingServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundInMemoryCaching(this IServiceCollection services)
            => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
    }
}

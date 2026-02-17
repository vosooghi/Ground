using Ground.Extensions.Logger.Abstractions;
using Ground.Utilities;

namespace Ground.Endpoints.WebApi.Extentions.DependencyInjection
{
    /// <summary>
    /// An extension class for adding Ground services to the DI container.
    /// </summary>
    public static class AddGroundServicesExtentions
    {
        public static IServiceCollection AddGroundUntilityServices(
            this IServiceCollection services)
        {            
            services.AddScoped<IScopeInformation, ScopeInformation>();
            services.AddTransient<GroundServices>();
            return services;
        }

    }

}

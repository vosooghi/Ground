using Ground.Extensions.Logger.Abstractions;
using Ground.Utilities;

namespace Ground.Endpoints.WebApi.Extentions.DependencyInjection
{

    public static class AddGroundServicesExtentions
    {
        public static IServiceCollection AddGroundUntilityServices(
            this IServiceCollection services)
        {
            //Missing
            services.AddScoped<IScopeInformation, ScopeInformation>();
            services.AddTransient<GroundServices>();
            return services;
        }

    }

}

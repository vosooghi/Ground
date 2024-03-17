using Ground.Extensions.UsersManagement.Abstractions;
using Ground.Extensions.UsersManagement.Options;
using Ground.Extensions.UsersManagement.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class UserInfoServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundWebUserInfoService(this IServiceCollection services, IConfiguration configuration, bool useFake = false)
        {
            if (useFake)
            {
                services.AddSingleton<IUserInfoService, FakeUserInfoService>();

            }
            else
            {
                services.Configure<UserManagementOptions>(configuration);
                services.AddSingleton<IUserInfoService, WebUserInfoService>();

            }
            return services;
        }


        public static IServiceCollection AddGroundWebUserInfoService(this IServiceCollection services, IConfiguration configuration, string sectionName, bool useFake = false)
        {
            services.AddGroundWebUserInfoService(configuration.GetSection(sectionName), useFake);
            return services;
        }

        public static IServiceCollection AddGroundWebUserInfoService(this IServiceCollection services, Action<UserManagementOptions> setupAction, bool useFake = false)
        {
            if (useFake)
            {
                services.AddSingleton<IUserInfoService, FakeUserInfoService>();

            }
            else
            {
                services.Configure(setupAction);
                services.AddSingleton<IUserInfoService, WebUserInfoService>();

            }
            return services;
        }
    }

}

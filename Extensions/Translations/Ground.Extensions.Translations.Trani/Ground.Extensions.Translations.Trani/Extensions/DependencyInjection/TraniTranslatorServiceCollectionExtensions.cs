using Ground.Extensions.Translations.Abstractions;
using Ground.Extensions.Translations.Trani.Options;
using Ground.Extensions.Translations.Trani.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ground.Extensions.DependencyInjection
{
    public static class TraniTranslatorServiceCollectionExtensions
    {
        public static IServiceCollection AddGroundTraniTranslator(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITranslator, TraniTranslator>();
            services.Configure<TraniTranslatorOptions>(configuration);            
            return services;
        }

        public static IServiceCollection AddGroundTraniTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            services.AddGroundTraniTranslator(configuration.GetSection(sectionName));
            services.AddSingleton<ITranslator, TraniTranslator>();
            services.Configure<TraniTranslatorOptions>(options =>configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddGroundTraniTranslator(this IServiceCollection services, Action<TraniTranslatorOptions> setupAction)
        {
            services.AddSingleton<ITranslator, TraniTranslator>();
            services.Configure(setupAction);
            return services;
        }
    }
}

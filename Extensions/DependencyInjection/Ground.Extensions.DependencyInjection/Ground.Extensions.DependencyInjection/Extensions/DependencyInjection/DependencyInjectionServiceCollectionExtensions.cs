
using Ground.Extensions.DependencyInjection.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using Ground.Extensions.DependencyInjection.Abstractions;

namespace Ground.Extensions.DependencyInjection.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering custom dependencies and configuring service lifetimes.
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// Adds custom dependencies to the specified service collection using configuration options.
        /// </summary>
        /// <param name="services">The service collection to which the dependencies will be added.</param>
        /// <param name="configuration">The configuration source containing dependency injection options.</param>
        /// <returns>The same service collection instance with the custom dependencies and options configured.</returns>
        public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.Get<DependencyInjectionOption>();

            services.AddWithTransientLifetime(option.AssmblyNamesForLoad)
                .AddWithScopeLifetime(option.AssmblyNamesForLoad)
                .AddWithSingletoneLifetime(option.AssmblyNamesForLoad)
                .Configure<DependencyInjectionOption>(configuration);

            return services;
        }

        /// <summary>
        /// Adds custom dependencies to the service collection using configuration from the specified section.
        /// </summary>
        /// <param name="services">The service collection to which the custom dependencies will be added.</param>
        /// <param name="configuration">The application configuration source used to retrieve the specified section.</param>
        /// <param name="sectionName">The name of the configuration section containing settings for the custom dependencies.</param>
        /// <returns>The same service collection instance with the custom dependencies registered.</returns>
        public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IConfiguration configuration, string sectionName)
            => services.AddCustomeDepenecies(configuration.GetSection(sectionName));

        /// <summary>
        /// Adds custom dependencies to the specified service collection using the provided configuration action.
        /// </summary>
        /// <param name="services">The service collection to which the dependencies will be added.</param>
        /// <param name="setupAction">An action that configures the dependency injection options.</param>
        /// <returns>The same service collection instance with the custom dependencies registered.</returns>
        public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, Action<DependencyInjectionOption> setupAction)
        {
            var option = new DependencyInjectionOption();
            setupAction.Invoke(option);

            services.AddWithTransientLifetime(option.AssmblyNamesForLoad)
                .AddWithScopeLifetime(option.AssmblyNamesForLoad)
                .AddWithSingletoneLifetime(option.AssmblyNamesForLoad)
                .Configure(setupAction);

            return services;
        }

        /// <summary>
        /// Registers all classes implementing the ITransientLifetime interface from the specified assemblies with
        /// transient lifetime in the dependency injection container.
        /// </summary>
        /// <remarks>Use this method to automatically register services with transient lifetime based on
        /// the ITransientLifetime marker interface. Only types found in the specified assemblies that implement
        /// ITransientLifetime will be registered. This method is typically used during application startup to simplify
        /// service registration.</remarks>
        /// <param name="services">The IServiceCollection to which the discovered services will be added.</param>
        /// <param name="assmblyNames">A comma-separated list of assembly names to scan for types implementing ITransientLifetime. Each name should
        /// correspond to a loaded assembly.</param>
        /// <returns>The IServiceCollection instance with the discovered transient services registered.</returns>
        private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(ITransientLifetime)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        /// <summary>
        /// Registers all classes implementing the IScopeLifetime interface from the specified assemblies with a scoped
        /// lifetime in the service collection.
        /// </summary>
        /// <remarks>Use this method to automatically register all implementations of IScopeLifetime from
        /// the given assemblies with scoped lifetimes. This is useful for dependency injection scenarios where services
        /// should be created once per request or scope.</remarks>
        /// <param name="services">The IServiceCollection to which the discovered services will be added.</param>
        /// <param name="assmblyNames">A comma-separated list of assembly names to scan for types implementing IScopeLifetime. Cannot be null or
        /// empty.</param>
        /// <returns>The IServiceCollection instance with the scoped services registered. The same instance as provided in the
        /// services parameter.</returns>
        private static IServiceCollection AddWithScopeLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(IScopeLifetime)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        /// <summary>
        /// Registers all classes implementing the ISingletonLifetime interface from the specified assemblies into the
        /// service collection with singleton lifetime.
        /// </summary>
        /// <remarks>Only classes assignable to ISingletonLifetime found in the specified assemblies are
        /// registered. Each service is registered as its implemented interfaces with singleton lifetime. This method is
        /// intended for use in dependency injection setup during application startup.</remarks>
        /// <param name="services">The IServiceCollection to which the discovered singleton services will be added.</param>
        /// <param name="assmblyNames">A comma-separated list of assembly names to scan for types implementing ISingletonLifetime. Each name should
        /// correspond to a valid, loadable assembly.</param>
        /// <returns>The IServiceCollection instance with the singleton services registered.</returns>
        private static IServiceCollection AddWithSingletoneLifetime(this IServiceCollection services, string assmblyNames)
            => services.Scan(s => s.FromAssemblies(GetAssemblies(assmblyNames))
            .AddClasses(c => c.AssignableToAny(typeof(ISingletonLifetime)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        /// <summary>
        /// Retrieves a list of assemblies that match the specified assembly names from the application's runtime dependencies.
        /// </summary>
        /// <remarks>This method loads assemblies based on their names from the application's runtime
        /// libraries. Assemblies that are not present in the runtime dependencies or whose names do not match the
        /// provided list will not be included in the result. Use caution when loading assemblies dynamically, as this
        /// may affect application performance and security.</remarks>
        /// <param name="assmblyNames">A comma-separated list of assembly names to search for within the application's runtime libraries. Each name
        /// should correspond to an assembly present in the application's dependencies.</param>
        /// <returns>A list of <see cref="Assembly"/> objects representing the assemblies found that match the specified names.
        /// If no matching assemblies are found, the list will be empty.</returns>
        private static List<Assembly> GetAssemblies(string assmblyNames)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library, assmblyNames.Split(',')))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }

            return assemblies;
        }

        /// <summary>
        /// Determines whether the specified compilation library or any of its dependencies matches any of the provided assembly names.
        /// </summary>
        /// <param name="compilationLibrary">The runtime library to evaluate for candidate compilation status. Must not be null.</param>
        /// <param name="assmblyName">An array of assembly name strings to match against the library and its dependencies. Cannot be null or
        /// contain null elements.</param>
        /// <returns>true if the compilation library name or any of its dependency names contains any of the specified assembly
        /// names; otherwise, false.</returns>
        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
            => assmblyName.Any(d => compilationLibrary.Name.Contains(d)) || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
    }
}

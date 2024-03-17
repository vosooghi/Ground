using FluentValidation.AspNetCore;
using Ground.Endpoints.WebApi.Middlewares.ApiExceptionHandler;
using Microsoft.Data.SqlClient;

namespace Ground.Endpoints.WebApi.Extentions.DependencyInjection
{
     
    public static class AddApiConfigurationExtentions
    {
        /// <summary>
        /// Add Controllers support
        /// </summary>
        /// <param name="services">service provider</param>
        /// <param name="assemblyNamesForLoad">the name of assemblies</param>
        /// <returns></returns>
        public static IServiceCollection AddGroundApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
        {
            services.AddControllers(options =>
            {
                // options.Filters.Add(typeof(TrackActionPerformanceFilter));
            }).AddFluentValidation();
            services.AddGroundDependencies(assemblyNamesForLoad);
            return services;
        }


        public static void UseGroundApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseApiExceptionHandler(options =>
            {
                //if the exception is a type of sql
                options.AddResponseDetails = (context, ex, error) =>
                {
                    if (ex.GetType().Name == typeof(SqlException).Name)
                    {
                        error.Detail = "Exception was a database exception!";
                    }
                };
                //if the error is related to the network
                options.DetermineLogLevel = ex =>
                {
                    if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                        ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return LogLevel.Critical;
                    }
                    return LogLevel.Error;
                };
                // we can add other type of exceptions
            });

        }




    }
}

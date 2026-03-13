namespace Ground.Endpoints.WebApi.Middlewares.ApiExceptionHandler
{
    public static class ApiExceptionMiddlewareExtensions
    {
        /// <summary>
        /// A middleware that sits early in the HTTP pipeline and provides global exception handling + consistent JSON error responses + scoped logging.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }

        /// <summary>
        /// A middleware that sits early in the HTTP pipeline and provides global exception handling + consistent JSON error responses + scoped logging.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <param name="configureOptions">A delegate to configure the middleware options.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder,
            Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);

            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}


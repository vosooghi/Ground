using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.Contracts.ApplicationServices.Events;
using Ground.Core.Contracts.ApplicationServices.Queries;
using Ground.Utilities;

namespace Ground.Endpoints.WebApi.Extentions
{
    /// <summary>
    /// Provides extension methods for accessing application services and the Ground application context from the HttpContext.
    /// </summary>
    public static class HttpContextExtentions
    {
        /// <summary>
        /// Gets the command dispatcher from the HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext instance.</param>
        /// <returns>The command dispatcher.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
            (ICommandDispatcher)(httpContext.RequestServices.GetService(typeof(ICommandDispatcher))
                ?? throw new InvalidOperationException("ICommandDispatcher service is not registered."));

        /// <summary>
        /// Gets the query dispatcher from the HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext instance.</param>
        /// <returns>The query dispatcher.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)(httpContext.RequestServices.GetService(typeof(IQueryDispatcher))
                ?? throw new InvalidOperationException("IQueryDispatcher service is not registered."));

        /// <summary>
        /// Gets the event dispatcher from the HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext instance.</param>
        /// <returns>The event dispatcher.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
        public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
            (IEventDispatcher)(httpContext.RequestServices.GetService(typeof(IEventDispatcher))
                ?? throw new InvalidOperationException("IEventDispatcher service is not registered."));

        /// <summary>
        /// Gets the Ground application context from the HttpContext.
        /// </summary>
        /// <param name="httpContext">The HttpContext instance.</param>
        /// <returns>The Ground application context.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
        public static GroundServices GroundApplicationContext(this HttpContext httpContext) =>
            (GroundServices)(httpContext.RequestServices.GetService(typeof(GroundServices))
                ?? throw new InvalidOperationException("GroundServices is not registered."));
    }
}

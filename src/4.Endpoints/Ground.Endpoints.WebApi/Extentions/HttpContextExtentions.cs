using Ground.Core.Contracts.ApplicationServices.Commands;
using Ground.Core.Contracts.ApplicationServices.Events;
using Ground.Core.Contracts.ApplicationServices.Queries;
using Ground.Utilities;

namespace Ground.Endpoints.WebApi.Extentions
{
    /// <summary>
    /// for accessing dependencies, it finds the service form di container.
    /// </summary>
    public static class HttpContextExtentions
    {
        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
            (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
            (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
        public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
            (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
        public static GroundServices GroundApplicationContext(this HttpContext httpContext) =>
            (GroundServices)httpContext.RequestServices.GetService(typeof(GroundServices));
    }
}

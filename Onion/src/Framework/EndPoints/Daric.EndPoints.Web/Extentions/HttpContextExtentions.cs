using Daric.Core.Contracts.ApplicationServices.Commands;
using Daric.Core.Contracts.ApplicationServices.Events;
using Daric.Core.Contracts.ApplicationServices.Queries;

using Microsoft.AspNetCore.Http;


namespace Daric.EndPoints.Web.Extentions;

public static class HttpContextExtensions
{
    public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
        (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

    public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
        (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
    public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
        (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher)); 
}

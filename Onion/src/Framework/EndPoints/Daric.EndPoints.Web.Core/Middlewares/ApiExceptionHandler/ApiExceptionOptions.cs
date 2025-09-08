using Microsoft.AspNetCore.Http;

namespace Daric.EndPoints.Web.Core.Middlewares.ApiExceptionHandler;

public class ApiExceptionOptions
{
    public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    public Func<Exception, LogLevel> DetermineLogLevel { get; set; }
}

using Daric.Core.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net; 

namespace Daric.EndPoints.Web.Core.Middlewares.ApiExceptionHandler;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;
    private readonly ApiExceptionOptions _options;
   // private readonly IJsonSerializer _serializer; 

    public ApiExceptionMiddleware(ApiExceptionOptions options, RequestDelegate next,
        ILogger<ApiExceptionMiddleware> logger
        //, IJsonSerializer serializer
        )
    {
        _next = next;
        _logger = logger;
        _options = options;
       // _serializer = serializer; 
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = new ApiError
        {
            Id = Guid.NewGuid().ToString(),
            Status = (short)HttpStatusCode.InternalServerError,
            Title = "خطایی در سرویس api رخ داده است."
        };

        _options.AddResponseDetails?.Invoke(context, exception, error);

        string innerExMessage = GetInnermostExceptionMessage(exception);

        LogLevel level = _options.DetermineLogLevel?.Invoke(exception) ?? LogLevel.Error;
        _logger.Log(level, exception, "BADNESS!!! " + innerExMessage + " -- {ErrorId}.", error.Id);

        //string result = _serializer.Serialize(error);
        string result = System.Text.Json.JsonSerializer.Serialize(error);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync(result);
    }
    private string GetInnermostExceptionMessage(Exception exception)
    {
        if (exception.InnerException != null)
        {
            return GetInnermostExceptionMessage(exception.InnerException);
        }

        return exception.Message;
    }
}

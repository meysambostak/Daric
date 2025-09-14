

using Daric.EndPoints.Web.Middlewares.ApiExceptionHandler;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Daric.EndPoints.Web.Extentions;

public static class AddApiConfigurationExtensions
{
    public static IServiceCollection AddFrameworkApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
      

        services.AddControllers().AddFluentValidation();

        services?.AddFrameworkDependencies(assemblyNamesForLoad);

        return services;
    }
     

    public static void UseFrameworkApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseApiExceptionHandler(options =>
        {
            options.AddResponseDetails = (context, ex, error) =>
            {
                if (ex.GetType().Name == typeof(Microsoft.Data.SqlClient.SqlException).Name)
                {
                    error.Detail = "Exception was a database exception!";
                }
            };
            options.DetermineLogLevel = ex =>
            {
                if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                    ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                {
                    return LogLevel.Critical;
                }
                return LogLevel.Error;
            };
        });
    }
}


using Serilog;
using Serilog.Events;
using System.IO;

namespace DaricTemplate.EndPoints.API.Extensions;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilogServices(
        this WebApplicationBuilder builder)
    {
        // Ensure log directory exists
        string logDirectory = Path.Combine(AppContext.BaseDirectory, "logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        // Load serilog configuration file
        builder.Configuration.AddJsonFile("serilog.json", optional: false, reloadOnChange: true);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", 
                    builder.Environment.ApplicationName)
            .CreateLogger();

        // Register Serilog as the logging provider
        builder.Host.UseSerilog();

        return builder;
    }

    public static WebApplication UseSerilogRequestLoggingExtended(this WebApplication app)
    {
        // Configure request logging
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
            
            // Customize the logging level based on status code
            options.GetLevel = (httpContext, elapsed, ex) => 
            {
                if (ex != null || httpContext.Response.StatusCode > 499)
                {
                    return LogEventLevel.Error;
                }

                if (httpContext.Response.StatusCode > 399)
                {
                    return LogEventLevel.Warning;
                }

                return LogEventLevel.Information;
            };
            
            // Attach additional properties if needed
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("UserAgent", httpContext.Request.Headers["User-Agent"].ToString());
                if (httpContext.User.Identity?.IsAuthenticated == true)
                {
                    diagnosticContext.Set("UserName", httpContext.User.Identity.Name);
                }
            };
        });
        
        return app;
    }
}

using DaricTemplate.EndPoints.API.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

try
{
    // Configure Serilog
    builder.AddSerilogServices();

    Log.Information("Starting up application");

    // Add application services
    builder.ConfigureServices();

    WebApplication app = builder.Build();

    // Configure the application pipeline
    app.ConfigurePipeline();


    // Add Serilog request logging with extended options
    app.UseSerilogRequestLoggingExtended();

    Log.Information("Application configured successfully");

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
    return 1;
}
finally
{
    Log.Information("Shutdown complete");
    Log.CloseAndFlush();
}

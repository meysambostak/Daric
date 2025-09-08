using System.Configuration;
using System.Text;
using System.Text.Json;
using Daric.Core.ApplicationServices.Commands;
using Daric.Core.ApplicationServices.Events;
using Daric.Core.ApplicationServices.Queries;
using Daric.Core.Contracts.Data.Commands;
using Daric.EndPoints.Web.Core.Extentions.DependencyInjection;
using Daric.EndPoints.Web.Core.Extentions.ModelBinding;
using Daric.Extensions.ObjectMappers.AutoMapper.Extensions.DependencyInjection;
using Daric.Infra.Data.Commands;
using Daric.Infra.Data.Commands.Interceptors;
using DaricTemplate.Core.Contracts.BaseInfo.CountryLocations;
using DaricTemplate.EndPoints.API.CustomDecorators;
using DaricTemplate.Infra.Data.Commands.BaseInfo.CountryLocations;
using DaricTemplate.Infra.Data.Commands.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

namespace DaricTemplate.EndPoints.API.Extensions;

public static class HostingExtensions
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;

        // Register decorators
        builder.Services.AddSingleton<CommandDispatcherDecorator, CustomCommandDecorator>();
        builder.Services.AddSingleton<QueryDispatcherDecorator, CustomQueryDecorator>();
        builder.Services.AddSingleton<EventDispatcherDecorator, CustomEventDecorator>();
         
        builder.Services.AddFrameworkApiCore("Daric", "DaricTemplate");
       


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
      //  builder.Services.AddOpenApi(); // اضافه کردن OpenAPI برای Scalar

        // Configure OpenAPI
        builder.Services.AddOpenApi(options => options.AddDocumentTransformer((document, context, _) =>
            {
                document.Info = new()
                {
                    Title = "Daric Catalog API",
                    Version = "v1",
                    Description = """
                            Modern API for Daric API Catalogs.
                            """,

                };
                return Task.CompletedTask;
            }));

        builder.Services.AddNonValidatingValidator();
        builder.Services.AddFrameworkAutoMapperProfiles(configuration, "AutoMapper");

        // Register DbContext
        builder.Services.AddDbContext<DaricTemplateCommandDbContext>(c => 
            c.UseSqlServer(configuration.GetConnectionString("CommandDb_ConnectionString"))
            .AddInterceptors(
                new SetPersianYeKeInterceptor(),
                new AddAuditDataInterceptor()
            )
        );

        // Register BaseCommandDbContext to use DaricTemplateCommandDbContext as implementation
        builder.Services.AddScoped<BaseCommandDbContext>(provider => 
            provider.GetRequiredService<DaricTemplateCommandDbContext>()
        );

        // Register IUnitOfWork
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider => {
            DaricTemplateCommandDbContext context = serviceProvider.GetRequiredService<DaricTemplateCommandDbContext>();
            IdentityServicesRegistry.SetCascadeOnSaveChanges(context);
            return context;
        });

        // Register repositories
        builder.Services.AddScoped<ICountryLocationCommandRepository, CountryLocationCommandRepository>();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseFrameworkApiExceptionHandler();
        app.UseStatusCodePages();

        app.UseCors(delegate (CorsPolicyBuilder builder)
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });

        app.UseHttpsRedirection();
       

        app.MapControllers();
        app.MapOpenApi().CacheOutput();
        app.MapScalarApiReference();    

        ////Redirect root to Scalar UI
        app.MapGet("/", () => Results.Redirect("/scalar/v1"))
           .ExcludeFromDescription();


        return app;
    }
}

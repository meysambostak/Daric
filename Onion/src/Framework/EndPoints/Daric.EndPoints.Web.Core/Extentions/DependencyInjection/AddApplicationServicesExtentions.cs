using System.Reflection;
using Daric.Core.ApplicationServices.Commands;
using Daric.Core.ApplicationServices.Events;
using Daric.Core.ApplicationServices.Queries;
using Daric.Core.Contracts.ApplicationServices.Commands;
using Daric.Core.Contracts.ApplicationServices.Events;
using Daric.Core.Contracts.ApplicationServices.Queries;
using FluentValidation; 

namespace Daric.EndPoints.Web.Core.Extentions.DependencyInjection;

public static class AddApplicationServicesExtensions
{
    public static IServiceCollection AddFrameworkApplicationServices(this IServiceCollection services,
                                                                 IEnumerable<Assembly> assembliesForSearch)
        => services.AddCommandHandlers(assembliesForSearch)
                   .AddCommandDispatcherDecorators()
                   .AddQueryHandlers(assembliesForSearch)
                   .AddQueryDispatcherDecorators()
                   .AddEventHandlers(assembliesForSearch)
                   .AddEventDispatcherDecorators()
                   .AddFluentValidators(assembliesForSearch);

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));

    private static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<CommandDispatcher, CommandDispatcher>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<CommandDispatcherDecorator, CommandDispatcherValidationDecorator>();

        services.AddTransient<ICommandDispatcher>(c =>
        {
            CommandDispatcher commandDispatcher = c.GetRequiredService<CommandDispatcher>();
            var decorators = c.GetServices<CommandDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                int listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                    {
                        decorators[i].SetCommandDispatcher(commandDispatcher);

                    }
                    else
                    {
                        decorators[i].SetCommandDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return commandDispatcher;
        });
        return services;
    }

    private static IServiceCollection AddQueryHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,>));

    private static IServiceCollection AddQueryDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<QueryDispatcher, QueryDispatcher>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<QueryDispatcherDecorator, QueryDispatcherValidationDecorator>();

        services.AddTransient<IQueryDispatcher>(c =>
        {
            QueryDispatcher queryDispatcher = c.GetRequiredService<QueryDispatcher>();
            var decorators = c.GetServices<QueryDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                int listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                    {
                        decorators[i].SetQueryDispatcher(queryDispatcher);

                    }
                    else
                    {
                        decorators[i].SetQueryDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return queryDispatcher;
        });
        return services;
    }

    private static IServiceCollection AddEventHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        => services.AddWithTransientLifetime(assembliesForSearch, typeof(IDomainEventHandler<>));

    private static IServiceCollection AddEventDispatcherDecorators(this IServiceCollection services)
    {
        services.AddTransient<EventDispatcher, EventDispatcher>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherDomainExceptionHandlerDecorator>();
        services.AddTransient<EventDispatcherDecorator, EventDispatcherValidationDecorator>();

        services.AddTransient<IEventDispatcher>(c =>
        {
            EventDispatcher queryDispatcher = c.GetRequiredService<EventDispatcher>();
            var decorators = c.GetServices<EventDispatcherDecorator>().ToList();
            if (decorators?.Any() == true)
            {
                decorators = decorators.OrderBy(c => c.Order).ToList();
                int listFinalIndex = decorators.Count - 1;
                for (int i = 0; i <= listFinalIndex; i++)
                {
                    if (i == listFinalIndex)
                    {
                        decorators[i].SetEventDispatcher(queryDispatcher);

                    }
                    else
                    {
                        decorators[i].SetEventDispatcher(decorators[i + 1]);
                    }
                }
                return decorators[0];
            }
            return queryDispatcher;
        });
        return services;
    }

    private static IServiceCollection AddFluentValidators(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
    {
        // Try to use FluentValidation.DependencyInjectionExtensions if available
        if (TryAddValidatorsViaPackage(services, assembliesForSearch))
            return services;

        // Fallback: manually scan and register IValidator<T> as transient
        foreach (var assembly in assembliesForSearch)
        {
            Type[] types;
            try { types = assembly.GetTypes(); }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t is not null).Cast<Type>().ToArray();
            }

            foreach (var type in types)
            {
                if (type is null || type.IsAbstract || type.IsInterface)
                    continue;

                var validatorInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .ToArray();

                if (validatorInterfaces.Length == 0)
                    continue;

                foreach (var @interface in validatorInterfaces)
                {
                    services.AddTransient(@interface, type);
                }
            }
        }

        return services;
    }

    private static bool TryAddValidatorsViaPackage(IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
    {
        try
        {
            var asm = AppDomain.CurrentDomain.GetAssemblies()
                        .FirstOrDefault(a => a.GetName().Name == "FluentValidation.DependencyInjectionExtensions")
                      ?? Assembly.Load(new AssemblyName("FluentValidation.DependencyInjectionExtensions"));

            var extType = asm?.GetType("FluentValidation.DependencyInjectionExtensions.ServiceCollectionExtensions");
            if (extType is null) return false;

            var method = extType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m =>
                    m.Name == "AddValidatorsFromAssemblies" &&
                    m.GetParameters().Length >= 2 &&
                    typeof(IServiceCollection).IsAssignableFrom(m.GetParameters()[0].ParameterType) &&
                    typeof(IEnumerable<Assembly>).IsAssignableFrom(m.GetParameters()[1].ParameterType));

            if (method is null) return false;

            var parameters = method.GetParameters();
            object?[] args;

            if (parameters.Length == 2)
            {
                args = new object?[] { services, assembliesForSearch };
            }
            else
            {
                // Supply defaults for optional parameters when present
                var list = new List<object?> { services, assembliesForSearch };
                for (int i = 2; i < parameters.Length; i++)
                {
                    var p = parameters[i];
                    if (p.HasDefaultValue)
                        list.Add(p.DefaultValue);
                    else if (p.ParameterType.IsEnum && p.ParameterType.Name == "ServiceLifetime")
                        list.Add(Enum.Parse(p.ParameterType, "Transient"));
                    else
                        list.Add(null);
                }
                args = list.ToArray();
            }

            method.Invoke(null, args);
            return true;
        }
        catch
        {
            return false;
        }
    }
}


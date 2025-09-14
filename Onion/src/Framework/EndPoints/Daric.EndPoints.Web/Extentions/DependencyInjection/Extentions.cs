using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Daric.EndPoints.Web.Extentions;

public static class Extensions
{

    public static IServiceCollection AddFrameworkDependencies(this IServiceCollection services,
        params string[] assemblyNamesForSearch)
    { 
        List<Assembly> assemblies = GetAssemblies(assemblyNamesForSearch);
        services.AddFrameworkApplicationServices(assemblies)
            .AddFramewotkDataAccess(assemblies) 
            .AddCustomeDepenecies(assemblies);
        return services;
    }
    public static IServiceCollection AddCustomeDepenecies(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        return services.AddWithTransientLifetime(assemblies, typeof(ITransientLifetime))
            .AddWithScopedLifetime(assemblies, typeof(IScopeLifetime))
            .AddWithSingletonLifetime(assemblies, typeof(ISingletoneLifetime));
    }

    public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
        return services;
    }
    public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
       IEnumerable<Assembly> assembliesForSearch,
       params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

    public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch,
        params Type[] assignableTo)
    {
        services.Scan(s => s.FromAssemblies(assembliesForSearch)
            .AddClasses(c => c.AssignableToAny(assignableTo))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        return services;
    }



    private static List<Assembly> GetAssemblies(string[] assmblyName)
    {
        var assemblies = new List<Assembly>();
        DependencyContext? dependencyContext = DependencyContext.Default 
            ?? throw new InvalidOperationException("DependencyContext.Default is null. Ensure the application is running in a supported environment.");

        IReadOnlyList<RuntimeLibrary> dependencies = dependencyContext.RuntimeLibraries;
        foreach (RuntimeLibrary library in dependencies)
        {
            if (IsCandidateCompilationLibrary(library, assmblyName))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
        }
        return assemblies;
    }
    private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
    {
        return assmblyName.Any(d => compilationLibrary.Name.Contains(d))
            || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
    }

}

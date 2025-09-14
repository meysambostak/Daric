using Daric.Infra.Data.Commands;
using Daric.Infra.Data.Commands.Interceptors;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection; 

namespace DaricTemplate.EndPoints.API.Extensions;

public static class IdentityServicesRegistry
{

    public static void SetCascadeOnSaveChanges(BaseCommandDbContext context)
    {
        context.ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
        context.ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
    }

   
    //public static void AddInterceptors(this IServiceCollection services)
    //{
    //    services.AddSingleton<AddOutBoxEventItemInterceptor>();
    //    services.AddSingleton<AuditableEntitiesInterceptor>();
    //}
}

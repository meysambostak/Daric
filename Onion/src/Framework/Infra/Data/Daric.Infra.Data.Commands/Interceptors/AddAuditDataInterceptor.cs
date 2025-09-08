using Daric.Infra.Data.Commands.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics; 

namespace Daric.Infra.Data.Commands.Interceptors;

public class AddAuditDataInterceptor : SaveChangesInterceptor
{

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddShadowProperties(eventData);
        return base.SavingChanges(eventData, result);
    }


    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddShadowProperties(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    private static void AddShadowProperties(DbContextEventData eventData)
    {
        Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker changeTracker = eventData?.Context?.ChangeTracker; 
        changeTracker?.SetAuditableEntityPropertyValues( );
    }
}

using Microsoft.EntityFrameworkCore;

namespace DaricTemplate.Infra.Data.Commands;

public static class ApplyEntitiesMapping
{

    public static void AddCustomEntitiesMappings(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplyEntitiesMapping).Assembly); 
    }
}

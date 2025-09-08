

using Daric.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Daric.Infra.Data.Commands.Extensions;
public static class AuditableShadowProperties
{
 
    public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime =
                                    entity => EF.Property<DateTime?>(entity, CreatedDateTime);
    public static readonly string CreatedDateTime = nameof(CreatedDateTime);

    public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime =
                                    entity => EF.Property<DateTime?>(entity, ModifiedDateTime);
    public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

    public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach ( IMutableEntityType? entityType in modelBuilder.Model.GetEntityTypes().Where(c => typeof(IAuditableEntity).IsAssignableFrom(c.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(CreatedDateTime);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(ModifiedDateTime);
        }
    }

    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker)
    {

        DateTime now = DateTime.UtcNow;

        IEnumerable<EntityEntry<IAuditableEntity>> modifiedEntries = 
            changeTracker
            .Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Modified);
        foreach (EntityEntry<IAuditableEntity> modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
        }

        IEnumerable<EntityEntry<IAuditableEntity>> addedEntries = 
            changeTracker.
            Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Added);
        foreach (EntityEntry<IAuditableEntity> addedEntry in addedEntries)
        {
            addedEntry.Property(CreatedDateTime).CurrentValue = now;
        }
    }

}


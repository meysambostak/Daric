using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Daric.Core.Contracts.Data.Commands;
using Daric.Infra.Data.Commands.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace Daric.Infra.Data.Commands;

public abstract class BaseCommandDbContext : DbContext
    , IUnitOfWork
{

    private bool _isDisposed;
    private IDbContextTransaction _transaction;
     

    public BaseCommandDbContext(DbContextOptions options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // we can't use constructor injection anymore, because we are using the `AddDbContextPool<>`
        // Adds all of the ASP.NET Core Identity related mappings at once.
        // builder.AddCustomIdentityMappings();

        // This should be placed here, at the end.
        builder.AddAuditableShadowProperties();

    }

    #region Method 
    public IEnumerable<string> GetIncludePaths(Type clrEntityType)
    {
        IEntityType? entityType = Model.FindEntityType(clrEntityType);
        var includedNavigations = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var entityNavigations = new List<INavigation>();
            foreach (INavigation navigation in entityType.GetNavigations())
            {
                if (includedNavigations.Add(navigation))
                {
                    entityNavigations.Add(navigation);
                }
            }
            if (entityNavigations.Count == 0)
            {
                if (stack.Count > 0)
                {
                    yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
                }
            }
            else
            {
                foreach (INavigation navigation in entityNavigations)
                {
                    INavigation? inverseNavigation = navigation.Inverse;
                    if (inverseNavigation != null)
                    {
                        includedNavigations.Add(inverseNavigation);
                    }
                }
                stack.Push(entityNavigations.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
            {
                stack.Pop();
            }

            if (stack.Count == 0)
            {
                break;
            }

            entityType = stack.Peek().Current.TargetEntityType;
        }
    }

    public void MigrateDatabase()
    {
        Database.Migrate();
    }



    public void BeginTransaction()
    {
        _transaction = Database.BeginTransaction();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await Database.BeginTransactionAsync();
    }

    public async Task BeginTransactionAsync(IsolationLevel isolationLevel)
    {
        _transaction = await Database.BeginTransactionAsync(isolationLevel);
    }

    public int Commit()
    {
        int result = base.SaveChanges();
        return result;
    }

    public async Task<int> CommitAsync()
    {
        int result = await base.SaveChangesAsync();
        return result;
    }

    public void CommitTransaction()
    {
        _transaction.Commit();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    public void RollbackTransaction()
    {
        _transaction.Rollback();
    }

    public Task RollbackTransactionAsync()
    {
        return _transaction.RollbackAsync();

    }


    [SuppressMessage("Microsoft.Usage", "CA2215:Dispose methods should call base class dispose",
   Justification = "base.Dispose() is called")]
    public sealed override void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            try
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                    _transaction = null;
                }
            }
            finally
            {
                _isDisposed = true;
            }
        }

        base.Dispose();
    }
    //public int SaveChanges() => SaveChanges();

    public async Task<int> SaveChangesAsync()
    {
        int result = await base.SaveChangesAsync();
        return result;
    }

    //public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => SaveChangesAsync(cancellationToken);

    public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
    {
        object? value = base.Entry(entity).Property(propertyName).CurrentValue;
        return value != null
            ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
            : default;
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

    public object GetShadowPropertyValue(object entity, string propertyName) => base.Entry(entity).Property(propertyName).CurrentValue;

 

    #endregion

}





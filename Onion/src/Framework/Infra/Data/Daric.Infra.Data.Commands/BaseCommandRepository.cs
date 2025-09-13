using System.Data;
using System.Linq.Expressions;

using Daric.Core.Contracts.Data.Commands;
using Daric.Core.Domain.Entities;
using Daric.Core.Domain.ValueObjects.Common;

using Microsoft.EntityFrameworkCore;

namespace Daric.Infra.Data.Commands;

public class BaseCommandRepository<TEntity, TDbContext, TId> : ICommandRepository<TEntity, TId>, IUnitOfWork
    where TEntity : AggregateRoot<TId>
    where TDbContext : BaseCommandDbContext
     where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{

    protected readonly TDbContext _dbContext;

    public BaseCommandRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Method



    public void Delete(TId id)
    {
        TEntity? entity = _dbContext.Set<TEntity>().Find(id);
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void DeleteGraph(TId id)
    {
        TEntity? entity = GetGraph(id);
        if (entity is not null && !entity.Id.Equals(default))
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }





    #region insert

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
    }
    #endregion

    #region Get Single Item
    public TEntity Get(TId id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }

    public TEntity Get(BusinessId businessId)
    {
        return _dbContext.Set<TEntity>().FirstOrDefault(c => c.BusinessId == businessId);
    }

    public async Task<TEntity> GetAsync(TId id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity> GetAsync(BusinessId businessId)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(c => c.BusinessId == businessId);
    }


    public bool Exists(Expression<Func<TEntity, bool>> expression)
    {
        TEntity? res = _dbContext.Set<TEntity>().SingleOrDefault(expression);
        return res == null ? false : true;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        TEntity? res = await _dbContext.Set<TEntity>().SingleOrDefaultAsync(expression);
        return res == null ? false : true;
    }
    #endregion

    #region Get single item with graph
    public TEntity GetGraph(TId id)
    {
        IEnumerable<string> graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        var temp = graphPath.ToList();
        foreach (string item in graphPath)
        {
            query = query.Include(item);
        }
        return query.FirstOrDefault(c => c.Id.Equals(id));
    }

    public TEntity GetGraph(BusinessId businessId)
    {
        IEnumerable<string> graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        var temp = graphPath.ToList();
        foreach (string item in graphPath)
        {
            query = query.Include(item);
        }
        return query.FirstOrDefault(c => c.BusinessId == businessId);
    }

    public async Task<TEntity> GetGraphAsync(TId id)
    {
        IEnumerable<string> graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (string item in graphPath)
        {
            query = query.Include(item);
        }
        return await query.FirstOrDefaultAsync(c => c.Id.Equals(id));
    }

    public async Task<TEntity> GetGraphAsync(BusinessId businessId)
    {
        IEnumerable<string> graphPath = _dbContext.GetIncludePaths(typeof(TEntity));
        IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsQueryable();
        foreach (string item in graphPath)
        {
            query = query.Include(item);
        }
        return await query.FirstOrDefaultAsync(c => c.BusinessId == businessId);
    }

    #endregion

    #endregion

    #region Unit of Work

    public void BeginTransaction() => _dbContext.BeginTransaction();

    public Task BeginTransactionAsync() => _dbContext.BeginTransactionAsync();

    public Task BeginTransactionAsync(IsolationLevel isolationLevel) => _dbContext.BeginTransactionAsync(isolationLevel);
    public void CommitTransaction() => _dbContext.CommitTransaction();

    public Task CommitTransactionAsync() => _dbContext.CommitTransactionAsync();

    public void RollbackTransaction() => _dbContext.RollbackTransaction();

    public Task RollbackTransactionAsync() => _dbContext.RollbackTransactionAsync();

    public DbSet<TEntity> Set<TEntity>() where TEntity : class => _dbContext.Set<TEntity>();

    public int SaveChanges() => _dbContext.SaveChanges();

    public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);

    public T GetShadowPropertyValue<T>(object entity,
                                       string propertyName) where T : IConvertible =>
                                                                    _dbContext.GetShadowPropertyValue<T>(entity, propertyName);
    public object GetShadowPropertyValue(object entity,
                                         string propertyName) => _dbContext.Entry(entity).Property(propertyName).CurrentValue;

    public void MigrateDatabase() => _dbContext.MigrateDatabase();



    #endregion


}

public class BaseCommandRepository<TEntity, TDbContext> : BaseCommandRepository<TEntity, TDbContext, long>
    where TEntity : AggregateRoot
    where TDbContext : BaseCommandDbContext
{
    public BaseCommandRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

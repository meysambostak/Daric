using Microsoft.EntityFrameworkCore;

namespace Daric.Core.Contracts.Data.Commands;
 
public interface IUnitOfWork
{
    void MigrateDatabase();
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible;
    object GetShadowPropertyValue(object entity, string propertyName);

    //void Add<TEntity>(TEntity entity) where TEntity : class;
    //void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    //void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    //EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    //void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
    //T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible;
    //object GetShadowPropertyValue(object entity, string propertyName);

    //void ExecuteSqlInterpolatedCommand(FormattableString query);
    //void ExecuteSqlRawCommand(string query, params object[] parameters);




    /// <summary>
    /// در صورت نیاز به کنترل تراکنش‌ها از این متد جهت شروع تراکنش استفاده می‌شود.
    /// </summary>
    void BeginTransaction();
    Task BeginTransactionAsync();
    Task BeginTransactionAsync(System.Data.IsolationLevel isolationLevel);

    /// <summary>
    /// در صورت کنترل دستی تراکنش از این متد جهت پایان موفقیت آمیز تراکنش استفاده می‌شود.
    /// </summary>
    void CommitTransaction();
    Task CommitTransactionAsync();

    /// <summary>
    /// در صورت بروز خطا در فرایند‌ها از این متد جهت بازگشت تغییرات استفاده می‌شود.
    /// </summary>
    void RollbackTransaction();
    Task RollbackTransactionAsync();



    /// <summary>
    /// برای تایید تراکنشی که اتوماتیک توسط سیستم ایجاد شده است از این متد استفاده می‌شود.
    /// </summary>
    /// <returns></returns>
 
    int SaveChanges();
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}

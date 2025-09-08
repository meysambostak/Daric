using System.Linq.Expressions;
using System.Security.Cryptography;
using Daric.Core.Domain.Entities;
using Daric.Core.Domain.ValueObjects.Common;

namespace Daric.Core.Contracts.Data.Commands;
 
public interface ICommandRepository<TEntity,TId> : IUnitOfWork
    where TEntity : AggregateRoot<TId>
     where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
 
    void Delete(TId id);

 
    void DeleteGraph(TId id);
     
    void Delete(TEntity entity);
     
    void Insert(TEntity entity);
     
    Task InsertAsync(TEntity entity);
     
    TEntity Get(TId id);
    Task<TEntity> GetAsync(TId id);
    TEntity Get(BusinessId businessId);
    Task<TEntity> GetAsync(BusinessId businessId);
    TEntity GetGraph(TId id);
    Task<TEntity> GetGraphAsync(TId id);
    TEntity GetGraph(BusinessId businessId);
    Task<TEntity> GetGraphAsync(BusinessId businessId);
    bool Exists(Expression<Func<TEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
}

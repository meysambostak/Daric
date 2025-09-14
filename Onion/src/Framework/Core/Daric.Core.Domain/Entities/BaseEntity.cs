using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Daric.Core.Domain.ValueObjects.Common;

namespace Daric.Core.Domain.Entities;


public abstract class BaseEntity<TId> : IAuditableEntity
          where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{
    public TId Id { get; protected set; }


    public BusinessId BusinessId { get; protected set; } = BusinessId.FromGuid(Guid.CreateVersion7(DateTime.Now));

    protected BaseEntity() { }


    #region Equality Check
    public bool Equals(BaseEntity<TId>? other) => this == other;
    public override bool Equals(object? obj) =>
         obj is BaseEntity<TId> otherObject && Id.Equals(otherObject.Id);

    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals((object)right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        => !(right == left);

    #endregion
}


public abstract class BaseEntity : BaseEntity<long>
{

}

using System.Reflection;
using Daric.Core.Domain.Events;



namespace Daric.Core.Domain.Entities;


/// https://martinfowler.com/bliki/DDD_Aggregate.html
public abstract class AggregateRoot<TId> : BaseEntity<TId>, IAggregateRoot where TId : struct,
          IComparable,
          IComparable<TId>,
          IConvertible,
          IEquatable<TId>,
          IFormattable
{

    private readonly List<IDomainEvent> _events;
    protected AggregateRoot() => _events = new();

 
    public AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null || !events.Any())
        {
            return;
        }

        foreach (IDomainEvent @event in events)
        {
            Mutate(@event);
        }
    }

    protected void Apply(IDomainEvent @event)
    {
        Mutate(@event);
        AddEvent(@event);
    }

    private void Mutate(IDomainEvent @event)
    {
        MethodInfo? onMethod = GetType().GetMethod("On", BindingFlags.Instance | BindingFlags.NonPublic, [@event.GetType()]);
        onMethod.Invoke(this, new[] { @event });
    }


    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);
    public IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();
    public void ClearEvents() => _events.Clear();
}



public abstract class AggregateRoot : AggregateRoot<long>
{

}

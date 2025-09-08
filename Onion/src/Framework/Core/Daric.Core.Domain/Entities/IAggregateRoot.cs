



using Daric.Core.Domain.Events;

namespace Daric.Core.Domain.Entities;

public interface IAggregateRoot
{
    void ClearEvents();
    IEnumerable<IDomainEvent> GetEvents();
}

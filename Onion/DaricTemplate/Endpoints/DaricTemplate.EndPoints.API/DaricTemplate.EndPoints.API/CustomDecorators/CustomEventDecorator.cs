﻿

using Daric.Core.ApplicationServices.Events;

namespace DaricTemplate.EndPoints.API.CustomDecorators;

public class CustomEventDecorator : EventDispatcherDecorator
{
    public override int Order => 0;

    public override async Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event)
    {
        await _eventDispatcher.PublishDomainEventAsync(@event);
    }
}

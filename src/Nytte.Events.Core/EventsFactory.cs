using System;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventsFactory : IEventsFactory
    {
        public IEventRegistration Create<T>(IEventHandler<T> handler) where T : IEvent
        {
            throw new System.NotImplementedException();
        }

        public string GetEventKey<T>() where T : IEvent
        {
            throw new System.NotImplementedException();
        }

        public ScopedEventHandlerAsync CreateHandler<TEvent>() where TEvent : IEvent
        {
            return (provider, eventObject) =>
            {
                using var scope = provider.CreateScope();
                var @event = (TEvent) eventObject;
                var handler = provider.GetService<IEventHandler<TEvent>>();
                
                //TODO: look how to use options here may be global options for the event bus as we as implementation specific ones

                if (handler is null)
                    throw new InvalidOperationException($"No handler found to handler event {typeof(TEvent).Name}");
                
                return handler.HandleAsync(@event);
            };
        }
    }
}
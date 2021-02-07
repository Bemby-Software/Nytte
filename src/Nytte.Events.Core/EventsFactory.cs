using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventsFactory : IEventsFactory
    {
        public IEventRegistration CreateRegistration<T>(ScopedEventHandlerAsync handler, string key) where T : IEvent 
            => EventRegistration.Create<T>(key, handler);

        public string GetEventKey<T>() where T : IEvent
        {
            var eventType = typeof(T);
            
            var attribute = eventType.GetAttribute<EventOwnerAttribute>();

            return attribute is null
                ? eventType.Name
                : $@"{attribute.Owner.ToLowerInvariant()}/{eventType.Name}";
        }

        public ScopedEventHandlerAsync CreateHandler<TEvent>() where TEvent : IEvent
        {
            return async (provider, packedEvent) =>
            {
                var transporter = provider.GetRequiredService<IEventTransporter>();
                
                using var scope = provider.CreateScope();
                
                var @event = await transporter.UnPackAsync<TEvent>(packedEvent);
                
                var handler = provider.GetService<IEventHandler<TEvent>>();
                
                //TODO: look how to use options here may be global options for the event bus as we as implementation specific ones

                if (handler is null)
                    throw new InvalidOperationException($"No handler found to handler event {typeof(TEvent).Name}");
                
                await handler.HandleAsync(@event);
            };
        }
    }
}
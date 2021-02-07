using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventsFactory : IEventsFactory
    {
        private readonly EventsOptions _options;

        public EventsFactory(EventsOptions options)
        {
            _options = options;
        }
        
        public IEventRegistration CreateRegistration<T>(ScopedEventHandlerAsync handler, string key) where T : IEvent 
            => EventRegistration.Create<T>(key, handler);

        public string GetEventKey<T>() where T : IEvent
        {
            var eventType = typeof(T);

            if (_options.UseOwnersInKeys)
            {
                var attribute = eventType.GetAttribute<EventOwnerAttribute>();
                if (attribute is null)
                {
                    throw new InvalidOperationException($"A owner attribute is required.");
                }

                return $"{attribute.Owner}/{eventType.Name}";
            }

            return eventType.Name;
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
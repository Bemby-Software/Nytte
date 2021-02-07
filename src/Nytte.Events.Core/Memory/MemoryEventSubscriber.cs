using System.Diagnostics;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public class MemoryEventSubscriber : IEventSubscriber
    {
        private readonly IEventRegistry _registry;
        private readonly IEventsFactory _factory;

        public MemoryEventSubscriber(IEventRegistry registry, IEventsFactory factory)
        {
            _registry = registry;
            _factory = factory;
        }
        
        public  IEventSubscriber Subscribe<TEvent, THandler>() where TEvent : IEvent
        {
            var key = _factory.GetEventKey<TEvent>();

            var handler = _factory.CreateHandler<TEvent>();

            var registration = _factory.CreateRegistration<TEvent>(handler);

            _registry.Add(registration);

            return this;
        }
    }
}
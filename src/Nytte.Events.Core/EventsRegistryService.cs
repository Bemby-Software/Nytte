using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventsRegistryService : IEventsRegistryService
    {
        private readonly IEventsFactory _factory;
        private readonly IEventRegistry _registry;

        public EventsRegistryService(IEventsFactory factory, IEventRegistry registry)
        {
            _factory = factory;
            _registry = registry;
        }
        
        public void RegisterEvent<TEvent>() where TEvent : IEvent
        {
            var key = _factory.GetEventKey<TEvent>();

            var handler = _factory.CreateHandler<TEvent>();

            var registration = _factory.CreateRegistration<TEvent>(handler, key);

            _registry.Add(registration);
        }
    }
}
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public class MemoryEventSubscriber : IEventSubscriber
    {
        private readonly IEventsRegistryService _eventsRegistryService;

        public MemoryEventSubscriber(IEventsRegistryService eventsRegistryService)
        {
            _eventsRegistryService = eventsRegistryService;
        }
        
        public IEventSubscriber Subscribe<TEvent>() where TEvent : IEvent
        {
           _eventsRegistryService.RegisterEvent<TEvent>();
           return this;
        }
    }
}
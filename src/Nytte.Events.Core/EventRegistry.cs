using System.Collections.Generic;
using System.Linq;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core
{
    public class EventRegistry : IEventRegistry
    {
        private readonly IEventsFactory _eventsFactory;
        private readonly List<IEventRegistration> _events;
        private readonly object _eventsLock = new object();

        public EventRegistry(IEventsFactory eventsFactory)
        {
            _eventsFactory = eventsFactory;
            _events = new List<IEventRegistration>();
        }

        public void Add(IEventRegistration registration)
        {
            lock (_eventsLock)
            {
                _events.Add(registration);    
            }
        }

        public IReadOnlyList<IEventRegistration> GetRegistrations<T>() where T : IEvent
        {
            IEnumerable<IEventRegistration> events;

            var key = _eventsFactory.GetEventKey<T>();
            
            lock (_eventsLock)
            {
                events = _events
                    .Where(x => x.Key == key);
            }

            return events.ToList();
        }
    }
}
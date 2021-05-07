using System;
using System.Linq;
using System.Threading.Tasks;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public class MemoryEventPublisher : IEventPublisher
    {
        private readonly IEventRegistry _registry;
        private readonly IEventTransporter _transporter;
        private readonly IServiceProvider _serviceProvider;
        private readonly EventsOptions _options;

        public MemoryEventPublisher(IEventRegistry registry, IEventTransporter transporter, IServiceProvider serviceProvider, EventsOptions options)
        {
            _registry = registry;
            _transporter = transporter;
            _serviceProvider = serviceProvider;
            _options = options;
        }
        
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var packedEvent = await _transporter.PackAsync(@event);

            var registrations = _registry.GetRegistrations<TEvent>();

            if (_options.UseStrictMode)
            {
                if (!registrations.Any())
                    throw new InvalidOperationException($"No event handlers for event {@event.GetType().FullName}");
            }

            foreach (var eventRegistration in registrations)
            {
                await eventRegistration.AsyncHandler(_serviceProvider, packedEvent);
            }
        }
    }
}
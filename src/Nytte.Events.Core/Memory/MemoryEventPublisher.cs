using System;
using System.Threading.Tasks;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public class MemoryEventPublisher : IEventPublisher
    {
        private readonly IEventRegistry _registry;
        private readonly IEventTransporter _transporter;
        private readonly IServiceProvider _serviceProvider;

        public MemoryEventPublisher(IEventRegistry registry, IEventTransporter transporter, IServiceProvider serviceProvider)
        {
            _registry = registry;
            _transporter = transporter;
            _serviceProvider = serviceProvider;
        }
        
        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var packedEvent = await _transporter.PackAsync(@event);

            var handlers = _registry.GetRegistrations<TEvent>();

            foreach (var eventRegistration in handlers)
            {
                await eventRegistration.AsyncHandler(_serviceProvider, packedEvent);
            }
        }
    }
}
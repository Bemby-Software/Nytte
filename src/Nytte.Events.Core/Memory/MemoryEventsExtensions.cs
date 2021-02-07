using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    public static class MemoryEventsExtensions
    {
        public static INytteBuilder AddInMemoryEventsBus(this INytteBuilder builder)
        {
            builder.AddEventsCore();
            builder.Services.AddSingleton<IEventPublisher, MemoryEventPublisher>();
            builder.Services.AddSingleton<IEventSubscriber, MemoryEventSubscriber>();
            
            return builder;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;

namespace Nytte.Events.Core
{
    public static class Extensions
    {
        public static INytteBuilder AddEventsCore(this INytteBuilder builder)
        {
            builder.Services.AddSingleton<IEventsFactory, EventsFactory>();
            builder.Services.AddSingleton<IEventsRegistryService, EventsRegistryService>();
            builder.Services.AddSingleton<IEventRegistry, EventRegistry>();
            return builder;
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;

namespace Nytte.Events.Core
{
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        public static INytteBuilder AddEventsCore(this INytteBuilder builder, Action<EventsOptions> options)
        {
            var opts = new EventsOptions();
            options?.Invoke(opts);

            builder.Services.AddSingleton(opts);
            builder.Services.AddSingleton<IEventsFactory, EventsFactory>();
            builder.Services.AddSingleton<IEventsRegistryService, EventsRegistryService>();
            builder.Services.AddSingleton<IEventRegistry, EventRegistry>();
            return builder;
        }
    }
}
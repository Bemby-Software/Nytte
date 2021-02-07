using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;

namespace Nytte.Events.Core.Memory
{
    [ExcludeFromCodeCoverage]
    public static class MemoryEventsExtensions
    {
        public static INytteBuilder AddInMemoryEventsBus(this INytteBuilder builder, Action<EventsOptions> options = null)
        {
            builder.AddEventsCore(options);
            builder.Services.AddScoped<IEventPublisher, MemoryEventPublisher>();
            builder.Services.AddSingleton<IEventSubscriber, MemoryEventSubscriber>();
            return builder;
        }

        public static IEventSubscriber UseInMemoryEventBus(this IApplicationBuilder app) 
            => app.ApplicationServices.GetRequiredService<IEventSubscriber>();
    }
}
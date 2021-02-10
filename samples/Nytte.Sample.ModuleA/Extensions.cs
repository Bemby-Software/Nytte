
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Sample.ModuleA.Events;
using Nytte.Sample.ModuleA.Events.External;

namespace Nytte.Sample.ModuleA
{
    public static class Extensions
    {
        public static IServiceCollection AddModuleA(this IServiceCollection services)
        {
            services.AddNytte()
                .AddInMemoryEventsBus();

            services.AddScoped<IEventHandler<SampleMessage>, SampleMessageEventHandler>();

            return services;
        }

        public static IApplicationBuilder UseModuleA(this IApplicationBuilder app)
        {
            app.UseInMemoryEventBus()
                .Subscribe<SampleMessage>();
            return app;
        }

    }
}
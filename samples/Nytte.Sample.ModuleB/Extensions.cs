using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Core;
using Nytte.Events.Core.Memory;

namespace Nytte.Sample.ModuleB
{
    public static class Extensions
    {
        public static IServiceCollection AddModuleB(this IServiceCollection services)
        {
            services.AddNytte()
                .AddInMemoryEventsBus();

            services.AddScoped<ModuleBService>();
            
            return services;
        }

        public static IApplicationBuilder UseModuleB(this IApplicationBuilder app)
        {
            app.UseInMemoryEventBus();
            return app;
        }
    }
}
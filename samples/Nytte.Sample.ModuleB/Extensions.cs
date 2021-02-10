using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Core;
using Nytte.Events.Core.Memory;
using Nytte.Modules.Requests.Abstractions;
using Nytte.Sample.ModuleB.Dtos;
using Nytte.Sample.ModuleB.Queries;

namespace Nytte.Sample.ModuleB
{
    public static class Extensions
    {
        public static IServiceCollection AddModuleB(this IServiceCollection services)
        {
            services.AddNytte()
                .AddInMemoryEventsBus()
                .AddModuleRequests()
                .AddModuleQueryHandler<UserDto, GetUserById, GetUserByIdHandler>();

            services.AddScoped<ModuleBService>();
            
            return services;
        }

        public static IApplicationBuilder UseModuleB(this IApplicationBuilder app)
        {
            app.UseInMemoryEventBus();
            app.UseModuleRequests()
                .RegisterQueryHandler<UserDto, GetUserById>();
            
            return app;
        }
    }
}
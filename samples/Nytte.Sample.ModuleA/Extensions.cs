
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Memory;
using Nytte.Modules.Requests;
using Nytte.Sample.ModuleA.Dtos;
using Nytte.Sample.ModuleA.Events;
using Nytte.Sample.ModuleA.Events.External;
using Nytte.Sample.ModuleA.Requests;

namespace Nytte.Sample.ModuleA
{
    public static class Extensions
    {
        public static IServiceCollection AddModuleA(this IServiceCollection services)
        {
            services.AddNytte()
                .AddInMemoryEventsBus()
                .AddModuleRequests()
                .AddModuleRequestHandler<UserDto, GetUserById, GetUserByIdHandler>();

            services.AddScoped<IEventHandler<SampleMessage>, SampleMessageEventHandler>();

            return services;
        }

        public static IApplicationBuilder UseModuleA(this IApplicationBuilder app)
        {
            app.UseInMemoryEventBus()
                .Subscribe<SampleMessage>();
            
            app.UseModuleRequests()
                .UseRequest<UserDto, GetUserById>();
            return app;
        }

    }
}
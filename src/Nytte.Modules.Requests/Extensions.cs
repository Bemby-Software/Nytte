using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Modules.Requests.Abstractions;

namespace Nytte.Modules.Requests
{
    public static class Extensions
    {
        public static INytteBuilder AddModuleRequests(this INytteBuilder builder)
        {
            builder.Services.AddSingleton<IModuleRequestRegistry, ModuleRequestRegistry>();
            builder.Services.AddSingleton<IModuleRequestFactory, ModuleRequestFactory>();
            builder.Services.AddScoped<IModuleClient, ModuleRequestClient>();
            builder.Services.AddTransient<IModuleRequestManager, ModuleRequestManager>();
            return builder;
        }

        public static INytteBuilder AddModuleRequestHandler<TReturns, TRequest, THandler>(this INytteBuilder builder)
        where TReturns : class
        where TRequest : IModuleRequest
        where THandler : class, IModuleRequestHandler<TReturns, TRequest>
        {
            builder.Services.AddScoped<IModuleRequestHandler<TReturns, TRequest>, THandler>();
            return builder;
        }

        public static IModuleRequestManager UseModuleRequests(this IApplicationBuilder app) 
            => app.ApplicationServices.GetRequiredService<IModuleRequestManager>();
    }
}
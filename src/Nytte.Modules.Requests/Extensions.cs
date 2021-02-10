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

        public static INytteBuilder AddModuleQueryHandler<TReturns, TQuery, THandler>(this INytteBuilder builder)
        where TReturns : class
        where TQuery : IModuleQuery
        where THandler : class, IModuleQueryHandler<TReturns, TQuery>
        {
            builder.Services.AddScoped<IModuleQueryHandler<TReturns, TQuery>, THandler>();
            return builder;
        }

        public static IModuleRequestManager UseModuleRequests(this IApplicationBuilder app) 
            => app.ApplicationServices.GetRequiredService<IModuleRequestManager>();
    }
}
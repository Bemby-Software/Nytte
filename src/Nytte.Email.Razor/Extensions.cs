using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Email;
using Razor.Templating.Core;

namespace Bemby.AccountModule.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddNytteEmail(this IServiceCollection services)
        {
            services.AddRazorTemplating();
            return services;
        }
        
        public static IApplicationBuilder UseNytteEmail(this IApplicationBuilder app)
        {
            RazorTemplateEngine.Initialize();
            return app;
        }
    }
}
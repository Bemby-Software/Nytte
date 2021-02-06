using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Email;
using Nytte.Email.Razor;
using Razor.Templating.Core;

namespace Nytte.Email.Razor
{
    public static class Extensions
    {
        public static IServiceCollection AddNytteRazorEmails(this IServiceCollection services)
        {
            services.AddSingleton<IRazorEmailMessageBuilder, RazorEmailMessageBuilder>();
            services.AddRazorTemplating();
            return services;
        }
        
        public static IApplicationBuilder UseNytteRazorEmails(this IApplicationBuilder app)
        {
            RazorTemplateEngine.Initialize();
            return app;
        }
    }
}
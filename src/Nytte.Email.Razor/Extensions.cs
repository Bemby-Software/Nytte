using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nytte.Email;
using Nytte.Email.Abstractions;
using Nytte.Email.Razor;
using Nytte.Email.Razor.Abstractions;
using Razor.Templating.Core;

namespace Nytte.Email.Razor
{
    public static class Extensions
    {
        public static IServiceCollection AddNytteRazorEmails(this IServiceCollection services)
        {
            services.AddRazorTemplating();
            services.AddSingleton<IRazorEmailMessageBuilder, RazorEmailMessageBuilder>();
            services.AddSingleton<IRazorPageStringRenderer, RazorTemplateEngineRenderer>();
            return services;
        }
        
        public static IApplicationBuilder UseNytteRazorEmails(this IApplicationBuilder app)
        {
            RazorTemplateEngine.Initialize();
            app.ApplicationServices.GetRequiredService<IEmailBuilderRegister>().RegisterBuilderForBlueprint<RazorEmailMessageBlueprint, IRazorEmailMessageBuilder>();
            
            return app;
        }
    }
}
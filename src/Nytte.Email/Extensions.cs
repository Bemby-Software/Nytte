using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Nytte.Email.Abstractions;

namespace Nytte.Email
{
    public static class Extensions
    {
        public static IServiceCollection NytteEmailConfigureSmtpServer(this IServiceCollection services, IConfiguration configuration, string section="SmtpServerConfiguration")
        {
            services.AddOptions();
            services.Configure<SmtpServerConfiguration>(configuration.GetSection(section));

            return services;
        }
        
        public static IServiceCollection AddNytteEmails(this IServiceCollection services, Func<SmtpServerConfiguration> configureSmtpServer)
        {
            var smtpServerConfiguration = configureSmtpServer.Invoke();
            
            services.AddSingleton<IEmailService, EmailService>();
            
            var smtpClient = new EmailServiceSmtpClient(smtpServerConfiguration);
            services.AddSingleton<IEmailServiceSmtpClient>(smtpClient);
            
            return services;
        }
        
        public static IServiceCollection AddNytteEmails(this IServiceCollection services, SmtpServerConfiguration smtpServerConfiguration)
        {
            services.AddSingleton<IEmailService, EmailService>();
            
            var smtpClient = new EmailServiceSmtpClient(smtpServerConfiguration);
            services.AddSingleton<IEmailServiceSmtpClient>(smtpClient);
            
            return services;
        }
        
        public static IServiceCollection AddNytteEmail(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEmailServiceSmtpClient, EmailServiceSmtpClient>();
            
            return services;
        }
        
        public static IApplicationBuilder UseNytteEmails(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
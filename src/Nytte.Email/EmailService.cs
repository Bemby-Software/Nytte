using System;
using System.Data;
using System.Threading.Tasks;
using EmailValidation;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Nytte.Email.Core;

namespace Nytte.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailServiceSmtpServerConfiguration _serviceSmtpServerConfiguration;

        public EmailService(IEmailServiceSmtpServerConfiguration serviceSmtpServerConfiguration)
        {
            _serviceSmtpServerConfiguration = serviceSmtpServerConfiguration;
        }
        
        public bool IsValidEmailAddress(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailValidator.Validate(email);
        }

        public void SendEmail(MimeMessage message)
        {
            using var smtpConnection = _serviceSmtpServerConfiguration.CreateConnection();
            smtpConnection.Send(message);
            smtpConnection.Disconnect(true);
        }

        public void SendEmail<T, TU>(T emailServiceMessageBuilder, TU emailServiceMessageBlueprint) where T : IEmailServiceMessageBuilder where TU : IEmailServiceMessageBlueprint
        {
            SendEmail(emailServiceMessageBuilder.BuildMessage(emailServiceMessageBlueprint));
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            using var smtpConnection = await _serviceSmtpServerConfiguration.CreateConnectionAsync();
            await smtpConnection.SendAsync(message);
            await smtpConnection.DisconnectAsync(true);
        }

        public async Task SendEmailAsync<T, TU>(T emailServiceMessageBuilder, TU emailServiceMessageBlueprint) where T : IEmailServiceMessageBuilder where TU : IEmailServiceMessageBlueprint
        {
            await SendEmailAsync(await emailServiceMessageBuilder.BuildMessageAsync(emailServiceMessageBlueprint));
        }
    }
}
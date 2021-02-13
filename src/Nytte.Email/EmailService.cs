using System;
using System.Data;
using System.Threading.Tasks;
using EmailValidation;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Nytte.Email.Abstractions;

namespace Nytte.Email
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBuilderFactory _emailBuilderFactory;
        private readonly IEmailServiceSmtpClient _emailServiceSmtpClient;

        public EmailService(IEmailBuilderFactory emailBuilderFactory, IEmailServiceSmtpClient emailServiceSmtpClient)
        {
            _emailBuilderFactory = emailBuilderFactory;
            _emailServiceSmtpClient = emailServiceSmtpClient;
        }

        public bool IsValidEmailAddress(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && EmailValidator.Validate(email);
        }

        public void SendEmail(MimeMessage message)
        {
            using var smtpConnection = _emailServiceSmtpClient.CreateConnection();
            smtpConnection.Send(message);
            smtpConnection.Disconnect(true);
        }

        public async Task SendEmailAsync(MimeMessage message)
        {
            using var smtpConnection = await _emailServiceSmtpClient.CreateConnectionAsync();
            await smtpConnection.SendAsync(message);
            await smtpConnection.DisconnectAsync(true);
        }
        
        public void SendEmail<TBlueprint>(TBlueprint emailServiceMessageBlueprint) where TBlueprint : IEmailServiceMessageBlueprint
        {
            SendEmail(_emailBuilderFactory.ResolveBuilder<TBlueprint>().BuildMessage(emailServiceMessageBlueprint));
        }
        
        public async Task SendEmailAsync<TBlueprint>(TBlueprint emailServiceMessageBlueprint) where TBlueprint : IEmailServiceMessageBlueprint
        {
            await SendEmailAsync(await _emailBuilderFactory.ResolveBuilder<TBlueprint>().BuildMessageAsync(emailServiceMessageBlueprint));
        }
    }
}
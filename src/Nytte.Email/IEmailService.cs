using System.Threading.Tasks;
using MimeKit;
using Nytte.Email.Core;

namespace Nytte.Email
{
    public interface IEmailService
    {
        bool IsValidEmailAddress(string email);
        void SendEmail(MimeMessage message);

        void SendEmail<TBuilder, TBlueprint>(TBuilder emailServiceMessageBuilder, TBlueprint emailServiceMessageBlueprint)  where TBuilder : IEmailServiceMessageBuilder 
                                                                                            where TBlueprint : IEmailServiceMessageBlueprint;
        Task SendEmailAsync(MimeMessage message);
        Task SendEmailAsync<TBuilder, TBlueprint>(TBuilder emailServiceMessageBuilder, TBlueprint emailServiceMessageBlueprint)  where TBuilder : IEmailServiceMessageBuilder 
            where TBlueprint : IEmailServiceMessageBlueprint;
    }
}
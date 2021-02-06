using System.Threading.Tasks;
using MimeKit;
using Nytte.Email.Core;

namespace Nytte.Email
{
    public interface IEmailService
    {
        bool IsValidEmailAddress(string email);
        void SendEmail(MimeMessage message);

        void SendEmail<T, TU>(T emailServiceMessageBuilder, TU emailServiceMessageBlueprint)  where T : IEmailServiceMessageBuilder 
                                                                                            where TU : IEmailServiceMessageBlueprint;
        Task SendEmailAsync(MimeMessage message);
        Task SendEmailAsync<T, TU>(T emailServiceMessageBuilder, TU emailServiceMessageBlueprint)  where T : IEmailServiceMessageBuilder 
            where TU : IEmailServiceMessageBlueprint;
    }
}
using System.Threading.Tasks;
using MimeKit;

namespace Nytte.Email.Abstractions
{
    public interface IEmailService
    {
        bool IsValidEmailAddress(string email);
        void SendEmail(MimeMessage message);

        void SendEmail<TBlueprint>(TBlueprint emailServiceMessageBlueprint) where TBlueprint : IEmailServiceMessageBlueprint;
        Task SendEmailAsync(MimeMessage message);
        Task SendEmailAsync<TBlueprint>(TBlueprint emailServiceMessageBlueprint)  where TBlueprint : IEmailServiceMessageBlueprint;
    }
}
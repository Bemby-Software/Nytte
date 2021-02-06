using System.Threading.Tasks;
using MimeKit;

namespace Nytte.Email
{
    public interface IEmailService
    {
        bool IsValidEmailAddress(string email);
        void SendEmail(MimeMessage message);
        Task SendEmailAsync(MimeMessage message);
    }
}
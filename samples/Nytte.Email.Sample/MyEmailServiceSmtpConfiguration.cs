using Bemby.AccountModule.Application.Interfaces.Services;
using MailKit.Security;

namespace Nytte.Email.Sample
{
    public class MyEmailServiceSmtpConfiguration : EmailServiceSmtpServerConfiguration, IAccountEmailServiceSmtpConfiguration
    {
        public MyEmailServiceSmtpConfiguration() : base("smtp.office365.com", 587, "serverUsername", "serverPassword", SecureSocketOptions.StartTls)
        {
        }
    }
}
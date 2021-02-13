using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Nytte.Email.Abstractions
{
    public interface IEmailServiceSmtpClient
    {
        public string ServerAddress { set; }
        public int ServerPort { set; }
        public string ServerUserName { set; }
        public string ServerPassword { set; }

        SmtpClient CreateConnection();
        Task<SmtpClient> CreateConnectionAsync();
    }
}
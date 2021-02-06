using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Nytte.Email
{
    public abstract class EmailServiceSmtpServerConfiguration : IEmailServiceSmtpServerConfiguration
    {
        public string ServerAddress { protected get; set; }
        public int ServerPort { protected get; set; }
        public string ServerUserName { protected get; set; }
        public string ServerPassword { protected get; set; }
        public SecureSocketOptions ConnectionSocketOptions { protected get; set; }

        protected EmailServiceSmtpServerConfiguration()
        {
            
        }
        
        protected EmailServiceSmtpServerConfiguration(string serverAddress, int serverPort, string serverUserName, string serverPassword, SecureSocketOptions connectionSocketOptions)
        {
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            ServerUserName = serverUserName;
            ServerPassword = serverPassword;
            ConnectionSocketOptions = connectionSocketOptions;
        }
        
        public SmtpClient CreateConnection()
        {
            var smtpClient = new SmtpClient();
            smtpClient.Connect(ServerAddress, ServerPort, ConnectionSocketOptions);
            smtpClient.Authenticate(ServerUserName, ServerPassword);
            
            return smtpClient;
        }

        public async Task<SmtpClient> CreateConnectionAsync()
        {
            var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(ServerAddress, ServerPort, ConnectionSocketOptions);
            await smtpClient.AuthenticateAsync(ServerUserName, ServerPassword);
            
            return smtpClient;
        }
    }
}
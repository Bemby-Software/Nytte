using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Nytte.Email.Abstractions;

namespace Nytte.Email
{
    public class EmailServiceSmtpClient : IEmailServiceSmtpClient
    {
        public string ServerAddress { protected get; set; }
        public int ServerPort { protected get; set; }
        public string ServerUserName { protected get; set; }
        public string ServerPassword { protected get; set; }
        public SecureSocketOptions ConnectionSocketOptions { protected get; set; }

        public EmailServiceSmtpClient(IOptions<SmtpServerConfiguration> smtpServerConfigurationOptions)
        {
            ServerAddress = smtpServerConfigurationOptions.Value.ServerAddress;
            ServerPort = smtpServerConfigurationOptions.Value.ServerPort;
            ServerUserName = smtpServerConfigurationOptions.Value.ServerUserName;
            ServerPassword = smtpServerConfigurationOptions.Value.ServerPassword;
            ConnectionSocketOptions = smtpServerConfigurationOptions.Value.ConnectionSocketOptions;
        }
        
        public EmailServiceSmtpClient(SmtpServerConfiguration smtpServerConfiguration)
        {
            ServerAddress = smtpServerConfiguration.ServerAddress;
            ServerPort = smtpServerConfiguration.ServerPort;
            ServerUserName = smtpServerConfiguration.ServerUserName;
            ServerPassword = smtpServerConfiguration.ServerPassword;
            ConnectionSocketOptions = smtpServerConfiguration.ConnectionSocketOptions;
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
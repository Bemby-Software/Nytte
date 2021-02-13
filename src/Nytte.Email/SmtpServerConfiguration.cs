using MailKit.Security;
using System.Text.Json.Serialization;

namespace Nytte.Email
{
    public class SmtpServerConfiguration
    {
        public SmtpServerConfiguration()
        {

        }

        [JsonConstructor]
        SmtpServerConfiguration(string serverAddress, int serverPort, string serverUserName, string serverPassword, SecureSocketOptions connectionSocketOptions)
        {
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            ServerUserName = serverUserName;
            ServerPassword = serverPassword;
            ConnectionSocketOptions = connectionSocketOptions;
        }

        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public string ServerUserName { get; set; }
        public string ServerPassword { get; set; }
        public SecureSocketOptions ConnectionSocketOptions { get; set; }
    }
}
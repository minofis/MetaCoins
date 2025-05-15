using System.Net;
using System.Net.Mail;
using MetaCoins.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace MetaCoins.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var clientEmail = _config["SMTPClientCredentials:ClinetEmail"];
            var clientPassword = _config["SMTPClientCredentials:ClinetPassword"];
            var clientHost = _config["SMTPClientCredentials:ClientHost"];
            var clientPort = int.Parse(_config["SMTPClientCredentials:ClientPort"]);

            var client = new SmtpClient(clientHost, clientPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(clientEmail, clientPassword)
            };

            return client.SendMailAsync(
                new MailMessage(
                    from: clientEmail,
                    to: email,
                    subject,
                    message
                ));
        }
    }
}
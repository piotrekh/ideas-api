using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Ideas.Mailing
{
    public class MailingClient : IMailingClient
    {
        private MailingSettings _settings;

        public MailingClient(IOptions<MailingSettings> settings)
        {
            _settings = settings.Value;
        }

        public void Send(string recipient, string subject, string body)
        {
            SmtpClient client = new SmtpClient(_settings.Server, _settings.Port);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage(_settings.Username, recipient);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            client.Send(mailMessage);
        }
    }
}

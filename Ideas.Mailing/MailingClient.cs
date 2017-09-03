using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Ideas.Mailing
{
    public class MailingClient : IMailingClient
    {
        private MailingSettings _settings;

        public MailingClient(IOptions<MailingSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task Send(MimeMessage message)
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_settings.Server, _settings.Port, _settings.UseSsl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                smtp.AuthenticationMechanisms.Remove("XOAUTH2");

                await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}

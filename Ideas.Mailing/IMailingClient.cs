using MimeKit;
using System.Threading.Tasks;

namespace Ideas.Mailing
{
    public interface IMailingClient
    {
        Task Send(MimeMessage message);
    }
}

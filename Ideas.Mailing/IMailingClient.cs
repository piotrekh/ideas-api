using System.Threading.Tasks;

namespace Ideas.Mailing
{
    public interface IMailingClient
    {
        void Send(string recipient, string subject, string body);
    }
}

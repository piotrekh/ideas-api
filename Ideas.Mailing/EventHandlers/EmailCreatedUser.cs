using Ideas.Domain.Users.Events;
using Ideas.Domain.Users.Services;
using MediatR;
using System.Threading.Tasks;

namespace Ideas.Mailing.EventHandlers
{
    public class EmailCreatedUser : IAsyncNotificationHandler<UserCreated>
    {
        private IMailingClient _mailingClient;
        private IUsersService _usersService;

        public EmailCreatedUser(IMailingClient mailingClient,
            IUsersService usersService)
        {
            _mailingClient = mailingClient;
            _usersService = usersService;
        }

        public async Task Handle(UserCreated notification)
        {
            var passwordToken = await _usersService.GeneratePasswordResetToken(notification.User);
            
            string subject = "You have been invited to Ideas";
            //TODO: Email an url with password reset token to allow user to set his password and login to system
            string body = $"Hi {notification.User.FirstName} {notification.User.LastName}, you have been invited to Ideas. This is your password token: {passwordToken}";

            _mailingClient.Send(notification.User.Email, subject, body);
        }
    }
}

using Ideas.DataAccess;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Services;
using MediatR;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.CommandHandlers
{
    public class ActivateUserHandler : IAsyncRequestHandler<ActivateUser>
    {
        private IUsersService _usersService;
        private IUnitOfWork _uow;

        public ActivateUserHandler(IUsersService usersService,
            IUnitOfWork uow)
        {
            _usersService = usersService;
            _uow = uow;
        }

        public async Task Handle(ActivateUser message)
        {
            var user = await _usersService.FindByEmail(message.Email);
            if (user == null)
                return;

            await _usersService.ResetPassword(user, message.Token, message.Password);

            user.EmailConfirmed = true;
            _uow.SaveChanges();
        }
    }
}

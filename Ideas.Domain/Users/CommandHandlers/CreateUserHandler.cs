using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Events;
using Ideas.Domain.Users.Exceptions;
using Ideas.Domain.Users.Services;
using MediatR;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.CommandHandlers
{
    public class CreateUserHandler : IAsyncRequestHandler<CreateUser>
    {
        private readonly IUsersService _usersService;
        private readonly IMediator _mediator;

        public CreateUserHandler(IUsersService usersService,
            IMediator mediator)
        {
            _usersService = usersService;
            _mediator = mediator;
        }

        public async Task Handle(CreateUser message)
        {            
            var existingUser = await _usersService.FindByEmail(message.Email);
            if (existingUser != null)
                throw new UserAlreadyExistsException();

            var user = new User()
            {
                Email = message.Email,
                UserName = message.Email,
                FirstName = message.FirstName,
                LastName = message.LastName
            };

            await _usersService.Create(user, message.Role);
            
            await _mediator.Publish(new UserCreated() { User = user });
        }
    }
}

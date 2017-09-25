using Ideas.DataAccess;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Exceptions;
using Ideas.Domain.Users.Models;
using Ideas.Domain.Users.Services;
using MediatR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.CommandHandlers
{
    public class GetAccessTokenHandler : IAsyncRequestHandler<GetAccessToken, AuthenticationToken>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsersService _usersService;
        private readonly IMediator _mediator;

        public GetAccessTokenHandler(IUnitOfWork uow,
            IUsersService usersService,
            IMediator mediator)
        {
            _uow = uow;
            _usersService = usersService;
            _mediator = mediator;
        }

        public async Task<AuthenticationToken> Handle(GetAccessToken message)
        {
            //check if client id is correct
            if (message.ClientId == Guid.Empty)
                throw new InvalidClientIdException();
            var client = _uow.ApiClients.FirstOrDefault(x => x.ExternalId == message.ClientId);
            if (client == null)
                throw new InvalidClientIdException();

            //check if user exists
            var user = await _usersService.FindByEmail(message.Username);
            if (user == null)
                throw new LoginFailedException();

            //check if password is correct
            bool correctPassword = await _usersService.CheckPassword(user, message.Password);
            if (!correctPassword)
                throw new LoginFailedException();

            //check if user has activated his account by confirming email
            if (!user.EmailConfirmed)
                throw new EmailUnconfirmedException();

            var token = await _mediator.Send(new GenerateAuthenticationTokens()
            {
                User = user,
                Client = client
            });
            return token;
        }        
    }
}

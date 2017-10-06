using Ideas.DataAccess;
using Ideas.Domain.Users.Commands;
using Ideas.Domain.Users.Exceptions;
using Ideas.Domain.Users.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.CommandHandlers
{
    public class RefreshAccessTokenHandler : IAsyncRequestHandler<RefreshAccessToken, AuthenticationToken>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _uow;

        public RefreshAccessTokenHandler(IMediator mediator,
            IUnitOfWork uow)
        {
            _mediator = mediator;
            _uow = uow;
        }

        public async Task<AuthenticationToken> Handle(RefreshAccessToken message)
        {
            //check if client id is correct
            if (message.ClientId == Guid.Empty)
                throw new InvalidClientIdException();
            var client = _uow.ApiClients.FirstOrDefault(x => x.ExternalId == message.ClientId);
            if (client == null)
                throw new InvalidClientIdException();

            //check if the access token exists in database and if it hasn't expired yet
            var token = _uow.RefreshTokens.Include(x => x.User)
                .Include(x => x.Client)
                .Where(x => x.Token == message.RefreshToken)
                .Select(x => new
                {
                    Token = x.Token,
                    ExpirationDate = x.ExpirationDate,
                    User = x.User,
                    ClientId = x.ApiClientId,
                    ClientExternalId = x.Client.ExternalId
                })
                .FirstOrDefault();
            if (token == null || token.ExpirationDate <= DateTime.UtcNow)
                throw new InvalidRefreshTokenException();

            //check if the given client id matches the id saved in database for the refresh token
            if (message.ClientId != token.ClientExternalId)
                throw new InvalidClientIdException();

            //generate new access token
            var accessToken = await _mediator.Send(new GenerateAuthenticationTokens()
            {
                Client = client,
                User = token.User
            });

            //delete the old refresh token
            _uow.BatchDelete(_uow.RefreshTokens.Where(x => x.Token == message.RefreshToken));
            _uow.SaveChanges();

            return accessToken;
        }
    }
}

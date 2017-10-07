using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Authorization.Models;
using MediatR;

namespace Ideas.Domain.Authorization.Commands
{
    public class GenerateAuthenticationTokens : IRequest<AuthenticationToken>
    {
        public User User { get; set; }

        public ApiClient Client { get; set; }
    }
}

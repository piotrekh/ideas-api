using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Users.Models;
using MediatR;
using System;

namespace Ideas.Domain.Users.Commands
{
    public class GenerateAuthenticationTokens : IRequest<AuthenticationToken>
    {
        public User User { get; set; }

        public ApiClient Client { get; set; }

        public Guid? RefreshToken { get; set; }
    }
}

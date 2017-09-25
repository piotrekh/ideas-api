using Ideas.Domain.Users.Models;
using MediatR;
using System;

namespace Ideas.Domain.Users.Commands
{
    public class RefreshAccessToken : IRequest<AuthenticationToken>
    {
        public Guid ClientId { get; set; }

        public Guid RefreshToken { get; set; }
    }
}

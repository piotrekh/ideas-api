using Ideas.Domain.Authorization.Models;
using MediatR;
using System;

namespace Ideas.Domain.Authorization.Commands
{
    public class RefreshAccessToken : IRequest<AuthenticationToken>
    {
        public Guid ClientId { get; set; }

        public Guid RefreshToken { get; set; }
    }
}

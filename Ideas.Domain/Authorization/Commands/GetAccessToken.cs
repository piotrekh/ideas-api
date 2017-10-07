using Ideas.Domain.Authorization.Models;
using MediatR;
using System;

namespace Ideas.Domain.Authorization.Commands
{
    public class GetAccessToken : IRequest<AuthenticationToken>
    {
        public Guid ClientId { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}

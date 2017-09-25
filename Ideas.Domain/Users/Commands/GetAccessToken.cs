using Ideas.Domain.Users.Models;
using MediatR;
using System;

namespace Ideas.Domain.Users.Commands
{
    public class GetAccessToken : IRequest<AuthenticationToken>
    {
        public Guid ClientId { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}

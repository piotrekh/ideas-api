using MediatR;

namespace Ideas.Domain.Users.Commands
{
    public class ActivateUser : IRequest
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }
    }
}

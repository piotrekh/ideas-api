using Ideas.Domain.Common.Enums;
using MediatR;

namespace Ideas.Domain.Users.Commands
{
    public class CreateUser : IRequest
    {
        public string Email { get; set; }

        public RoleName Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

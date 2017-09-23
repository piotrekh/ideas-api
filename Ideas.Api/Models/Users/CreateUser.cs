using Ideas.Domain.Common.Enums;

namespace Ideas.Api.Models.Users
{
    public class CreateUser
    {
        public string Email { get; set; }

        public RoleName Role { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }    
}

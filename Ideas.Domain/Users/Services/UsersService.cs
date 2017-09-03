using Ideas.DataAccess.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;

        public UsersService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public Task<User> FindByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<IdentityResult> Create(User user)
        {
            return _userManager.CreateAsync(user);
        }
    }
}

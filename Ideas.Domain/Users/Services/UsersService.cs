using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Identity;
using System.Linq;
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

        public async Task Create(User user, RoleName role)
        {
            var creationResult = await _userManager.CreateAsync(user);
            if (!creationResult.Succeeded)
                throw new CreateUserFailedException(creationResult.Errors.FirstOrDefault()?.Description);

            await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public Task<string> GeneratePasswordResetToken(User user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task ResetPassword(User user, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
                throw new ResetPasswordFailedException(result.Errors.FirstOrDefault()?.Description);
        }
    }
}

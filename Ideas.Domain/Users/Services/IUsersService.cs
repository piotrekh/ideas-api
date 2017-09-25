using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Common.Enums;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.Services
{
    public interface IUsersService
    {
        Task<User> FindByEmail(string email);

        Task Create(User user, RoleName role);

        Task<string> GeneratePasswordResetToken(User user);

        Task ResetPassword(User user, string token, string newPassword);

        Task<bool> CheckPassword(User user, string password);

        Task<IEnumerable<Claim>> GetUserSimpleClaims(User user);

        Task<Dictionary<string, object>> GetUserComplexClaims(User user);
    }
}

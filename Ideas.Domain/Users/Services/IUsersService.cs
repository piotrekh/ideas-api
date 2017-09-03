using Ideas.DataAccess.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Ideas.Domain.Users.Services
{
    public interface IUsersService
    {
        Task<User> FindByEmail(string email);

        Task<IdentityResult> Create(User user);
    }
}

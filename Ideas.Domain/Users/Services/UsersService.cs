using Ideas.DataAccess.Entities.Identity;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task<bool> CheckPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IEnumerable<Claim>> GetUserSimpleClaims(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            }.Union(userClaims);

            return claims;
        }

        public async Task<Dictionary<string, object>> GetUserComplexClaims(User user)
        {
            var dict = new Dictionary<string, object>();

            //get roles
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            dict.Add("roles", userRoles);

            return dict;
        }
    }
}

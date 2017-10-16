using Ideas.Domain.Common.Context;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace Ideas.Api.Context
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string Email
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(ClaimTypes.Email);
                return claim?.Value;
            }
        }

        public int? Id
        {
            get
            {
                var claim = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).FindFirst(ClaimTypes.NameIdentifier);
                string value = claim?.Value;
                if (string.IsNullOrEmpty(value))
                    return null;
                else
                    return int.Parse(value);
            }
        }

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsInRole(RoleName role)
        {
            var claims = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            var roles = claims.FindAll(claims.RoleClaimType).Select(x => x.Value).ToList();
            return roles.Contains(role.ToString());
        }
    }
}

using System;

namespace Ideas.Domain.Users.Models
{
    public class AuthenticationToken
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public long ExpiresIn { get; set; }

        public Guid RefreshToken { get; set; }
    }
}

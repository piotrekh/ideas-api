namespace Ideas.Domain.Settings
{
    public class AuthSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        /// <summary>
        /// In minutes
        /// </summary>
        public int TokenExpiration { get; set; }

        /// <summary>
        /// In minutes
        /// </summary>
        public int RefreshTokenExpiration { get; set; }
    }
}

namespace Ideas.Domain.Settings
{
    public class AuthSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecurityKey { get; set; }

        public double TokenExpiration { get; set; }
    }
}

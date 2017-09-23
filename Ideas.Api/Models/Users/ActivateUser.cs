namespace Ideas.Api.Models.Users
{
    public class ActivateUser
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }
    }
}

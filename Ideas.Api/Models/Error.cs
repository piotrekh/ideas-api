namespace Ideas.Api.Models
{
    public enum Error
    {
        UserAlreadyExists,
        CreateUserFailed,
        ResetPasswordFailed,
        EmailUnconfirmed,
        InvalidClientId,
        LoginFailed,
        InvalidRefreshToken,
        InvalidGrantType
    }
}

using System;

namespace Ideas.Domain.Users.Exceptions
{
    public class ResetPasswordFailedException : Exception
    {
        public ResetPasswordFailedException(string message) : base(message) { }
    }
}

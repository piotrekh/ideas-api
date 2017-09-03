using System;

namespace Ideas.Domain.Users.Exceptions
{
    public class CreateUserFailedException : Exception
    {
        public CreateUserFailedException(string message) : base(message) { }
    }
}

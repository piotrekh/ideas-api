using Ideas.Api.Models;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class LoginFailedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is LoginFailedException)
            {
                context.Result = new ErrorResult(Error.LoginFailed, "Login failed - incorrect email or password.", HttpStatusCode.Unauthorized);
                context.ExceptionHandled = true;
            }
        }
    }
}

using Ideas.Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class UserAlreadyExistsExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ErrorResult(Error.UserAlreadyExists, "User with this email address already exists", HttpStatusCode.ExpectationFailed);
        }
    }
}

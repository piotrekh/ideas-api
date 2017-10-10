using Ideas.Api.Models;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class UserAlreadyExistsExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is UserAlreadyExistsException)
            {
                context.Result = new ErrorResult(Error.UserAlreadyExists, "User with this email address already exists", HttpStatusCode.ExpectationFailed);
                context.ExceptionHandled = true;
            }
        }
    }
}

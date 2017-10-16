using Ideas.Api.Models;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class AccessDeniedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AccessDeniedException)
            {
                context.Result = new ErrorResult(Error.AccessDenied, "The action cannot be performed by the current user", HttpStatusCode.Forbidden);
                context.ExceptionHandled = true;
            }
        }
    }
}

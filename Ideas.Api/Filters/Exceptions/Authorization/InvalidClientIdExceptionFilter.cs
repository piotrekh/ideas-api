using Ideas.Api.Models;
using Ideas.Domain.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Authorization
{
    public class InvalidClientIdExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidClientIdException)
            {
                context.Result = new ErrorResult(Error.InvalidClientId, "Invalid client id.", HttpStatusCode.Unauthorized);
                context.ExceptionHandled = true;
            }
        }
    }
}

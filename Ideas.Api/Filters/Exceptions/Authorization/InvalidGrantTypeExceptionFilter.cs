using Ideas.Api.Models;
using Ideas.Domain.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Authorization
{
    public class InvalidGrantTypeExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidGrantTypeException)
            {
                context.Result = new ErrorResult(Error.InvalidGrantType, "Invalid grant type", HttpStatusCode.Unauthorized);
                context.ExceptionHandled = true;
            }
        }
    }
}

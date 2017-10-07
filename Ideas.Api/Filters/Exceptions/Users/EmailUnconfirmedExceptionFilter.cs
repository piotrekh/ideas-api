using Ideas.Api.Models;
using Ideas.Domain.Authorization.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class EmailUnconfirmedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is EmailUnconfirmedException)
            {
                context.Result = new ErrorResult(Error.EmailUnconfirmed, "Email has not been confirmed yet.", HttpStatusCode.Forbidden);
                context.ExceptionHandled = true;
            }
        }
    }
}

using Ideas.Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class CreateUserFailedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ErrorResult(Error.CreateUserFailed, context.Exception.Message, HttpStatusCode.ExpectationFailed);
        }
    }
}

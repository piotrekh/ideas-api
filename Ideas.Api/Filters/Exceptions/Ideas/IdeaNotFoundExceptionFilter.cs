using Ideas.Api.Models;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Ideas.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Ideas
{
    public class IdeaNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is IdeaNotFoundException)
            {
                context.Result = new ErrorResult(Error.InvalidIdeaId, "Idea not found", HttpStatusCode.NotFound);
                context.ExceptionHandled = true;
            }
        }
    }
}

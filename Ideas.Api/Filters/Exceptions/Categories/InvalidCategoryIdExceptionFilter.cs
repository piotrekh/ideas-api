using Ideas.Api.Models;
using Ideas.Domain.Categories.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Categories
{
    public class InvalidCategoryIdExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidCategoryIdException)
            {
                context.Result = new ErrorResult(Error.InvalidCategoryId, "Invalid category id", HttpStatusCode.BadRequest);
                context.ExceptionHandled = true;
            }
        }
    }
}

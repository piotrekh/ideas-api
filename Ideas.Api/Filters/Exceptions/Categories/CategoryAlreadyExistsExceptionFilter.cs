using Ideas.Api.Models;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Categories
{
    public class CategoryAlreadyExistsExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is CategoryAlreadyExistsException)
            {
                context.Result = new ErrorResult(Error.CategoryAlreadyExists, "Category with this name already exists", HttpStatusCode.ExpectationFailed);
                context.ExceptionHandled = true;
            }
        }
    }
}

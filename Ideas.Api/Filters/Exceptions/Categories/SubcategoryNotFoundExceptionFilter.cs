using Ideas.Api.Models;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Categories
{
    public class SubcategoryNotFoundExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is SubcategoryNotFoundException)
            {
                context.Result = new ErrorResult(Error.InvalidSubcategoryId, "Subcategory not found", HttpStatusCode.NotFound);
                context.ExceptionHandled = true;
            }
        }
    }
}

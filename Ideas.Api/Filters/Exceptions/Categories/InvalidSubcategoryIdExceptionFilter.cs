using Ideas.Api.Models;
using Ideas.Domain.Categories.Exceptions;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Categories
{
    public class InvalidSubcategoryIdExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidSubcategoryIdException)
            {
                context.Result = new ErrorResult(Error.InvalidSubcategoryId, "Invalid subcategory id", HttpStatusCode.BadRequest);
                context.ExceptionHandled = true;
            }
        }
    }
}

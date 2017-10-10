using FluentValidation;
using FluentValidation.Results;
using Ideas.Api.Models;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;

namespace Ideas.Api.Filters.Exceptions
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;
            if (exception != null)
            {
                ValidationFailure failure = exception.Errors.First();
                Error validationError = (Error)Enum.Parse(typeof(Error), failure.ErrorCode, true);
                context.Result = new ErrorResult(validationError, $"Validation failed on '{failure.PropertyName}' property", HttpStatusCode.BadRequest);
                context.ExceptionHandled = true;
            }
        }
    }
}

﻿using Ideas.Api.Models;
using Ideas.Domain.Common.Enums;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class ResetPasswordFailedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ResetPasswordFailedException)
            {
                context.Result = new ErrorResult(Error.CreateUserFailed, context.Exception.Message, HttpStatusCode.ExpectationFailed);
                context.ExceptionHandled = true;
            }
        }
    }
}

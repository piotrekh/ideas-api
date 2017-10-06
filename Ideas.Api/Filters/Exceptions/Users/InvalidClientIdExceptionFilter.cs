﻿using Ideas.Api.Models;
using Ideas.Domain.Users.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Users
{
    public class InvalidClientIdExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidClientIdException)
            {
                context.Result = new ErrorResult(Error.InvalidClientId, "Invalid client id.", HttpStatusCode.Unauthorized);
                context.ExceptionHandled = true;
            }
        }
    }
}
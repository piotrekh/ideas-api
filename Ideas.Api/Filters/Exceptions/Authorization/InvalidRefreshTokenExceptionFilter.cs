﻿using Ideas.Api.Models;
using Ideas.Domain.Authorization.Exceptions;
using Ideas.Domain.Common.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Ideas.Api.Filters.Exceptions.Authorization
{
    public class InvalidRefreshTokenExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidRefreshTokenException)
            {
                context.Result = new ErrorResult(Error.InvalidRefreshToken, "Invalid refresh token.", HttpStatusCode.Unauthorized);
                context.ExceptionHandled = true;
            }
        }
    }
}

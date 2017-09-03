using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ideas.Api.Models
{
    public class ErrorResult : ObjectResult
    {
        public ErrorResult(Error error, string reason, HttpStatusCode statusCode) : this(CreateErrorMessage(error, reason), statusCode)
        {
        }

        public ErrorResult(ErrorMessage error, HttpStatusCode statusCode) : base(error)
        {
            StatusCode = (int)statusCode;
        }

        private static ErrorMessage CreateErrorMessage(Error error, string reason)
        {
            return new ErrorMessage()
            {
                Error = error.ToString(),
                Reason = reason
            };
        }
    }
}

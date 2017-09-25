using Ideas.Api.Filters.Exceptions;
using Ideas.Api.Filters.Exceptions.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ideas.Api.Filters
{
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            filterCollection.Add<InvalidGrantTypeExceptionFilter>();

            //Users
            filterCollection.Add<CreateUserFailedExceptionFilter>();
            filterCollection.Add<EmailUnconfirmedExceptionFilter>();
            filterCollection.Add<InvalidClientIdExceptionFilter>();
            filterCollection.Add<InvalidRefreshTokenExceptionFilter>();
            filterCollection.Add<LoginFailedExceptionFilter>();
            filterCollection.Add<ResetPasswordFailedExceptionFilter>();
            filterCollection.Add<UserAlreadyExistsExceptionFilter>();            
        }
    }
}

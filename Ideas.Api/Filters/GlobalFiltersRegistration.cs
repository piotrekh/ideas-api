using Ideas.Api.Filters.Exceptions.Authorization;
using Ideas.Api.Filters.Exceptions.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ideas.Api.Filters
{
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            //Authorization
            filterCollection.Add<InvalidClientIdExceptionFilter>();
            filterCollection.Add<InvalidGrantTypeExceptionFilter>();
            filterCollection.Add<InvalidRefreshTokenExceptionFilter>();
            filterCollection.Add<LoginFailedExceptionFilter>();

            //Users
            filterCollection.Add<CreateUserFailedExceptionFilter>();
            filterCollection.Add<EmailUnconfirmedExceptionFilter>();                        
            filterCollection.Add<ResetPasswordFailedExceptionFilter>();
            filterCollection.Add<UserAlreadyExistsExceptionFilter>();            
        }
    }
}

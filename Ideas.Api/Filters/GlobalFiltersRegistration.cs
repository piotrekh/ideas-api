using Ideas.Api.Filters.Exceptions.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ideas.Api.Filters
{
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            //Users
            filterCollection.Add<CreateUserFailedExceptionFilter>();
            filterCollection.Add<UserAlreadyExistsExceptionFilter>();
            filterCollection.Add<ResetPasswordFailedExceptionFilter>();
        }
    }
}

using Ideas.Api.Filters.Exceptions;
using Ideas.Api.Filters.Exceptions.Authorization;
using Ideas.Api.Filters.Exceptions.Categories;
using Ideas.Api.Filters.Exceptions.Ideas;
using Ideas.Api.Filters.Exceptions.Users;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ideas.Api.Filters
{
    public static class GlobalFiltersRegistration
    {
        public static void AddGlobalExceptionFilters(this FilterCollection filterCollection)
        {
            filterCollection.Add<ValidationExceptionFilter>();

            //Authorization
            filterCollection.Add<InvalidClientIdExceptionFilter>();
            filterCollection.Add<InvalidGrantTypeExceptionFilter>();
            filterCollection.Add<InvalidRefreshTokenExceptionFilter>();
            filterCollection.Add<LoginFailedExceptionFilter>();

            //Categories
            filterCollection.Add<CategoryAlreadyExistsExceptionFilter>();
            filterCollection.Add<InvalidCategoryIdExceptionFilter>();
            filterCollection.Add<InvalidSubcategoryIdExceptionFilter>();
            filterCollection.Add<SubcategoryNotFoundExceptionFilter>();

            //Ideas
            filterCollection.Add<IdeaNotFoundExceptionFilter>();

            //Users
            filterCollection.Add<CreateUserFailedExceptionFilter>();
            filterCollection.Add<EmailUnconfirmedExceptionFilter>();                        
            filterCollection.Add<ResetPasswordFailedExceptionFilter>();
            filterCollection.Add<UserAlreadyExistsExceptionFilter>();            
        }
    }
}

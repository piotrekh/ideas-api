using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ideas.DataAccess
{
    public interface IUnitOfWork
    {
        DbSet<User> Users { get; }


        DbSet<IdeaCategory> IdeaCategories { get; }

        DbSet<IdeaSubcategory> IdeaSubcategories { get; }

        DbSet<Idea> Ideas { get; }

        DbSet<AssignedIdeaSubcategory> AssignedIdeaSubcategories { get; }

        DbSet<ApiClient> ApiClients { get; }

        DbSet<RefreshToken> RefreshTokens { get; }


        ITransaction BeginTransaction();

        void SaveChanges();

        //We need to have a separate batch delete method on unit of work instead of
        //directly using Z.EntityFramework.Plus Delete() extension method because
        //the library doesn't work properly with EF Core in memory db with separate
        //service provider for each new DbContext instance. This way we will be able
        //to override this method in in memory unit of work implementation. 
        void BatchDelete<T>(IQueryable<T> query) where T : class;
    }
}

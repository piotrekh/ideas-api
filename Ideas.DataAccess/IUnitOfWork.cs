using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Ideas.DataAccess
{
    public interface IUnitOfWork
    {
        DbSet<IdeaCategory> IdeaCategories { get; }

        DbSet<IdeaSubcategory> IdeaSubcategories { get; }

        DbSet<Idea> Ideas { get; }

        DbSet<AssignedIdeaSubcategory> AssignedIdeaSubcategories { get; }

        DbSet<ApiClient> ApiClients { get; }

        DbSet<RefreshToken> RefreshTokens { get; }


        ITransaction BeginTransaction();

        void SaveChanges();
    }
}

using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Entities.Identity;
using Ideas.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z.EntityFramework.Plus;

namespace Ideas.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IdeasDbContext _dbContext;


        public DbSet<User> Users => _dbContext.Users;


        public DbSet<IdeaCategory> IdeaCategories => _dbContext.IdeaCategories;

        public DbSet<IdeaSubcategory> IdeaSubcategories => _dbContext.IdeaSubcategories;

        public DbSet<Idea> Ideas => _dbContext.Ideas;

        public DbSet<AssignedIdeaSubcategory> AssignedIdeaSubcategories => _dbContext.AssignedIdeaSubcategories;

        public DbSet<ApiClient> ApiClients => _dbContext.ApiClients;

        public DbSet<RefreshToken> RefreshTokens => _dbContext.RefreshTokens;


        public UnitOfWork(IdeasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        //We need to have a separate batch delete method on unit of work instead of
        //directly using Z.EntityFramework.Plus Delete() extension method because
        //the library doesn't work properly with EF Core in memory db with separate
        //service provider for each new DbContext instance. This way we will be able
        //to override this method in in memory unit of work implementation.        
        public virtual void BatchDelete<T>(IQueryable<T> query) where T : class
        {
            query.Delete();
        }

        public virtual ITransaction BeginTransaction()
        {
            var dbTransaction = _dbContext.Database.BeginTransaction();
            return new Transaction(dbTransaction);
        }
    }
}

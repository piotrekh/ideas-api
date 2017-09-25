using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Ideas.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdeasDbContext _dbContext;

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

        public virtual ITransaction BeginTransaction()
        {
            var dbTransaction = _dbContext.Database.BeginTransaction();
            return new Transaction(dbTransaction);
        }
    }
}

using Ideas.DataAccess;
using Ideas.DataAccess.Transactions;
using Moq;
using System.Linq;

namespace Ideas.UnitTests.TestDoubles
{
    public class InMemoryUnitOfWork : UnitOfWork
    {
        public InMemoryUnitOfWork() : base(new InMemoryDbContext())
        {
        }

        public override ITransaction BeginTransaction()
        {
            var transactionMock = new Mock<ITransaction>();
            return transactionMock.Object;
        }

        public override void BatchDelete<T>(IQueryable<T> query)
        {
            var list = query.ToList();
            _dbContext.RemoveRange(list);
            _dbContext.SaveChanges();
        }
    }
}

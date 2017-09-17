using Microsoft.EntityFrameworkCore.Storage;

namespace Ideas.DataAccess.Transactions
{
    public class Transaction : ITransaction
    {
        private IDbContextTransaction _dbTransaction;

        public Transaction(IDbContextTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            _dbTransaction.Commit();
        }

        public void Dispose()
        {
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public void Rollback()
        {
            _dbTransaction.Rollback();
        }
    }
}

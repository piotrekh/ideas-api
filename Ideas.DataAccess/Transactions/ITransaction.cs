using System;

namespace Ideas.DataAccess.Transactions
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}

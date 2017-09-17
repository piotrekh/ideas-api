namespace Ideas.DataAccess.Transactions
{
    public interface ITransaction
    {
        void Commit();

        void Dispose();

        void Rollback();
    }
}

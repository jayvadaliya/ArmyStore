using System.Data;

namespace ArmyStore.Interfaces
{
    public interface IDapperContext : IDisposable
    {
        bool CanStartNewTransaction { get; }
        IDbConnection Connection { get; }
        void Commit();
        void Rollback();
        IDbTransaction GetTransaction();

        IDbTransaction GetTransaction(IsolationLevel level);

        event EventHandler TransactionCommitted;
        event EventHandler TransactionRollbacked;
    }
}
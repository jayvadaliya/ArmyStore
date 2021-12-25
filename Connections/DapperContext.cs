using System.Data;
using ArmyStore.Configurations;
using ArmyStore.Interfaces;
using Microsoft.Extensions.Options;

namespace ArmyStore.Connections
{
    public class DapperContext : IDapperContext
    {
        private readonly ILogger _logger;
        private bool _isDisposed = false;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public event EventHandler TransactionCommitted;
        public event EventHandler TransactionRollbacked;
        private readonly IOptions<DataBaseConfigurations> _dbConfigs;

        public DapperContext(ILoggerFactory loggerFactory, IOptions<DataBaseConfigurations> dbConfigs)
        {
            _logger = loggerFactory.CreateLogger<DapperContext>();
            _dbConfigs = dbConfigs;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {

                    _logger.LogTrace($"Creating a new connection to: {_dbConfigs.Value.DbConnectionString}");
                    var connection = new MySql.Data.MySqlClient.MySqlConnection(_dbConfigs.Value.DbConnectionString);
                    _connection = connection;
                }
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _logger.LogTrace($"Opening a new connection to: {_dbConfigs.Value.DbConnectionString}");
                    _connection.Open();
                }
                return _connection;
            }
        }

        public bool CanStartNewTransaction
        {
            get { return _transaction == null; }
        }

        /// <summary>
        /// Start a new transaction if one is not already available with the default isolation level (REPEATABLE READ)
        /// </summary>
        /// <remarks></remarks>
        public virtual IDbTransaction GetTransaction()
        {
            if (CanStartNewTransaction)
            {
                _transaction = Connection.BeginTransaction();
                _logger.LogDebug($"Transaction {_transaction.GetHashCode()} was started.");
            }
            return _transaction;
        }

        /// <summary>
        /// Start a new transaction if one is not already available
        /// </summary>
        /// <param name="level">The isolation level</param>
        public virtual IDbTransaction GetTransaction(IsolationLevel level)
        {
            if (CanStartNewTransaction)
            {

                _transaction = Connection.BeginTransaction(level);
                _logger.LogDebug($"Transaction {_transaction.GetHashCode()} was started.");
            }
            return _transaction;
        }

        public virtual void Commit()
        {
            try
            {
                // Commit transaction
                _transaction.Commit();
                _logger.LogDebug($"Transaction {_transaction.GetHashCode()} was committed.");

                _transaction.Dispose();
                _transaction = null;

                // Send event
                if (TransactionCommitted != null)
                    TransactionCommitted(this, new EventArgs());
            }
            catch (Exception ex)
            {
                if (_transaction != null && _transaction.Connection != null)
                    Rollback();
                throw new NullReferenceException("Tried commit on closed Transaction", ex);
            }
        }

        public virtual void Rollback()
        {
            try
            {
                if (_transaction != null)
                {
                    // Rollback transaction
                    _transaction.Rollback();
                    _logger.LogWarning($"Transaction {_transaction.GetHashCode()} was rollbacked.");

                    _transaction.Dispose();
                    _transaction = null;

                    // Send event
                    if (TransactionRollbacked != null)
                        TransactionRollbacked(this, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                throw new NullReferenceException("Tried Rollback on closed Transaction", ex);
            }
        }

        #region IDisposable Support        
        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed) return;
            if (isDisposing)
            {
                if (_transaction != null)
                {
                    var transaction = _transaction as MySql.Data.MySqlClient.MySqlTransaction;
                    _logger.LogWarning($"Transaction {transaction.GetHashCode()} was disposed without commit or rollback.");

                    _transaction.Dispose();
                    _transaction = null;
                }

                if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }

                _isDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MySqlDapperContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);   
        }
        #endregion
    }
}
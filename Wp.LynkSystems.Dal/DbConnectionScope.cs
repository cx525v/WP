using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
//using IsolationLevel = System.Transactions.IsolationLevel;

namespace Wp.CIS.LynkSystems.Dal
{
    /// Provides a disposable interface that will cleanup the connection appropriately when finished
    /// </summary>
    public class DbConnectionScope : ITransactedDbConnectionScope
    {

        private DbConnection _connection;
        private bool _connectionRequiredOpening;
        //private readonly bool _transactionRequiredStarting;
       // private TransactionScope _transactionScope;
        private bool _disposed;

        public DbConnectionScope(DbConnection connection, bool createTransactionScope)
        {
            _connection = connection;
            _disposed = false;
            _connectionRequiredOpening = false;

            //// Begin the scope
            //if (createTransactionScope && Transaction.Current == null)
            //{
            //    _transactionRequiredStarting = true;

            //    _transactionScope = new TransactionScope(TransactionScopeOption.Required,
            //        new TransactionOptions()
            //        {
            //            IsolationLevel = IsolationLevel.ReadCommitted,
            //            Timeout = TransactionManager.MaximumTimeout,
            //        });
            //}
        }

        #region IDisposable Implementation

        void IDisposable.Dispose()
        {
            if (_disposed) return;

            DisposeImpl();
            _disposed = true;
        }

        protected virtual void DisposeImpl()
        {
            // If something at a higher level didn't open the connection Dispose() it here
            if (_connectionRequiredOpening && _connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            //if (_transactionRequiredStarting && _transactionScope != null)
            //{
            //    _transactionScope.Dispose();
            //    _transactionScope = null;
            //}
        }

        #endregion

        #region IDbConnectionWrapper Implementation

        /// <summary>
        /// Opens the IDbConnection and returns the handle
        /// </summary>
        /// <returns></returns>
        IDbConnection IDbConnectionScope.GetOpenedDbConnection()
        {
            return GetOpenedDbConnectionCore();
        }

        #endregion

        #region ITransactedDbConnectionWrapper Implementation

        public virtual void Complete()
        {
            //if (!_disposed && _transactionRequiredStarting)
            //{
            //    //Debug.Assert(_transactionScope != null);

            //    //_transactionScope.Complete();
            //}
        }

        #endregion

        protected IDbConnection GetOpenedDbConnectionCore()
        {
            // If the connection is not opened we are not the root. Open the connection.
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Close();
                _connection.Open();
                _connectionRequiredOpening = true;
            }
            return _connection;
        }

        public IDbConnection GetOpenedDbConnection()
        {
            // If the connection is not opened we are not the root. Open the connection.
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Close();
                _connection.Open();
                _connectionRequiredOpening = true;
            }
            return _connection;
        }
    }
}
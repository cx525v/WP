using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;


namespace Wp.CIS.LynkSystems.Dal
{
    /// <summary>
    /// Supports IDbConnectionFactory and IDbExecutorFactory which allows for reusing an IDbConnection. 
    /// (e.x. if this objects lifetime is per request no more than one connection will be active at a time for that request)
    /// This should prevent unnecessary promotion to the DTC when nested handlers can use the same connection.
    /// </summary>
    public class DbConnectionScopeFactory : IDbConnectionScopeFactory, IDbExecutorFactory
    {
        private readonly Func<string, DbConnection> _connectionCreator;
        private readonly Func<DbConnection, bool, ITransactedDbExecutor> _executorCreator;
        private readonly Dictionary<string, DbConnection> _dbConnections = new Dictionary<string, DbConnection>();

        private readonly string connectionStringName = "ApEntities";

        /// <summary>
        /// Constructor that does not support ITransactedDbExecutor. If this version is used methods implementing IDbExecutorFactory functionality will not be supported.
        /// </summary>
        /// <param name="connectionCreator"></param>
        public DbConnectionScopeFactory(Func<string, DbConnection> connectionCreator)
        {
            _connectionCreator = connectionCreator;
        }

        /// <summary>
        /// Constructor that supports ITransactedDbExecutor. If this version is used methods implementing IDbConnectionFactory and IDbExecutorFactory functionality will be supported.
        /// </summary>
        /// <param name="connectionCreator"></param>
        /// <param name="executorCreator"></param>
        public DbConnectionScopeFactory(Func<string, DbConnection> connectionCreator, Func<DbConnection, bool, ITransactedDbExecutor> executorCreator)
            : this(connectionCreator)
        {
            _executorCreator = executorCreator;
        }

        #region IDbConnectionFactory Implementation

        /// <summary>
        /// Gets a connection wrapper that will expose an IDbConnection without creating a new transaction scope
        /// </summary>
        /// <returns></returns>
        public IDbConnectionScope GetConnectionScope()
        {
            return new DbConnectionScope(GetDbConnection(connectionStringName), false);
        }

        /// <summary>
        /// Gets a connection wrapper that will expose an IDbConnection within a new transaction scope
        /// </summary>
        /// <returns></returns>
        public ITransactedDbConnectionScope GetTransactedConnectionScope()
        {
            return new DbConnectionScope(GetDbConnection(connectionStringName), true);
        }

        #endregion

        #region IDbExecutorFactory Implementation

        /// <summary>
        /// Gets an IDbExecutor without creating a transaction scope
        /// </summary>
        /// <returns></returns>
        public IDbExecutor GetExecutor()
        {
            if (_executorCreator == null)
            {
                throw new NotImplementedException("GetExecutor is not supported. Please use the constructor overload that supports ITransactedDbExecutor.");
            }

            // Return an executor with the existing connection. Do not create a new transaction scope.
            return _executorCreator.Invoke(GetDbConnection(connectionStringName), false);
        }

        /// <summary>
        /// Gets a db executor within a new transaction scope
        /// </summary>
        /// <returns></returns>
        public ITransactedDbExecutor GetTransactedExecutor()
        {
            if (_executorCreator == null)
            {
                throw new NotSupportedException("GetExecutor is not supported. Please use the constructor overload that supports ITransactedDbExecutor.");
            }

            // Return an executor with the existing connection. Create a new transaction scope if it does not exist.
            return _executorCreator.Invoke(GetDbConnection(connectionStringName), true);
        }

        #endregion

        /// <summary>
        /// Get or create the DbConnection
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        private DbConnection GetDbConnection(string connectionStringName)
        {

            DbConnection connection;
            if (!_dbConnections.TryGetValue(connectionStringName, out connection))
            {
                // Create the connection
                connection = _connectionCreator.Invoke(connectionStringName);
                _dbConnections.Add(connectionStringName, connection);

                // Remove the connection from the Dictionary when its disposed and unsubscribe from the event
                var handlerWrapper = new ConnectionDisposedHandler();
                System.Data.StateChangeEventHandler handler = (sender, e) =>
                {
                    if (connection != null)
                    {
                        connection.StateChange -= handlerWrapper.DisposedHandler;
                    }

                    // Remove the connection string from the list once its disposed
                    if (connectionStringName != null && _dbConnections.ContainsKey(connectionStringName))
                        _dbConnections.Remove(connectionStringName);
                };

                handlerWrapper.DisposedHandler = handler;
                connection.StateChange += handlerWrapper.DisposedHandler;

            }
            return connection;
        }

        private class ConnectionDisposedHandler
        {
            public System.Data.StateChangeEventHandler DisposedHandler { get; set; }
        }
    }
}
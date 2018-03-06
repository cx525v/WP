using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Dal
{
    /// <summary>
    /// Interface that can issue commands to the database
    /// </summary>
    public interface IDbExecutor : IDisposable
    {
        #region Execute

        int Execute(string sql);

        int Execute(string sql, object param);

        int Execute(string sql, CommandType commandType);

        int Execute(string sql, object param, CommandType commandType);

        int Execute(string sql, object param, int commandTimeout, CommandType commandType);

        Task<int> ExecuteAsync(string sql);

        Task<int> ExecuteAsync(string sql, object param);

        Task<int> ExecuteAsync(string sql, CommandType commandType);

        Task<int> ExecuteAsync(string sql, object param, CommandType commandType);

        Task<int> ExecuteAsync(string sql, object param, int commandTimeout, CommandType commandType);

        #endregion

        #region Execute Scalar

        T ExecuteScalar<T>(string sql);

        T ExecuteScalar<T>(string sql, object param);

        T ExecuteScalar<T>(string sql, CommandType commandType);

        T ExecuteScalar<T>(string sql, object param, CommandType commandType);

        T ExecuteScalar<T>(string sql, object param, int commandTimeout, CommandType commandType);

        Task<T> ExecuteScalarAsync<T>(string sql);

        Task<T> ExecuteScalarAsync<T>(string sql, object param);

        Task<T> ExecuteScalarAsync<T>(string sql, CommandType commandType);

        Task<T> ExecuteScalarAsync<T>(string sql, object param, CommandType commandType);

        Task<T> ExecuteScalarAsync<T>(string sql, object param, int commandTimeout, CommandType commandType);

        #endregion

        #region Generic Query<T>

        IEnumerable<T> Query<T>(string sql);

        IEnumerable<T> Query<T>(string sql, object param);

        IEnumerable<T> Query<T>(string sql, CommandType commandType);

        IEnumerable<T> Query<T>(string sql, object param, CommandType commandType);

        IEnumerable<T> Query<T>(string sql, object param, int commandTimeout, bool buffered, CommandType commandType);

        Task<IEnumerable<T>> QueryAsync<T>(string sql);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, CommandType commandType);

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param, CommandType commandType);

        #endregion

        #region Generic Query<dynamic>

        IEnumerable<dynamic> Query(string sql);

        IEnumerable<dynamic> Query(string sql, object param);

        IEnumerable<dynamic> Query(string sql, CommandType commandType);

        IEnumerable<dynamic> Query(string sql, object param, CommandType commandType);

        IEnumerable<dynamic> Query(string sql, object param, int commandTimeout, bool buffered, CommandType commandType);

        Task<IEnumerable<dynamic>> QueryAsync(string sql);

        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param);

        Task<IEnumerable<dynamic>> QueryAsync(string sql, CommandType commandType);

        Task<IEnumerable<dynamic>> QueryAsync(string sql, object param, CommandType commandType);

        #endregion

        #region QueryMultiple

        IDbExecutorReader QueryMultiple(string sql);

        IDbExecutorReader QueryMultiple(string sql, object param);

        IDbExecutorReader QueryMultiple(string sql, CommandType commandType);

        IDbExecutorReader QueryMultiple(string sql, object param, CommandType commandType);

        IDbExecutorReader QueryMultiple(string sql, object param, int commandTimeout, CommandType commandType);

        Task<IDbExecutorReader> QueryMultipleAsync(string sql);

        Task<IDbExecutorReader> QueryMultipleAsync(string sql, object param);

        Task<IDbExecutorReader> QueryMultipleAsync(string sql, CommandType commandType);

        Task<IDbExecutorReader> QueryMultipleAsync(string sql, object param, CommandType commandType);

        Task<IDbExecutorReader> QueryMultipleAsync(string sql, object param, int commandTimeout, CommandType commandType);

        #endregion
    }

    /// <summary>
    /// Provides transactional handling to the DbExecutor
    /// </summary>
    public interface ITransactedDbExecutor : IDbExecutor
    {
        void Complete();
    }

    /// <summary>
    /// Provides access to DbExecutor read through multiple result sets
    /// </summary>
    public interface IDbExecutorReader : IDisposable
    {
        IEnumerable<dynamic> Read(bool buffered = true);

        IEnumerable<T> Read<T>(bool buffered = true);

        Task<IEnumerable<object>> ReadAsync(bool buffered = true);

        Task<IEnumerable<T>> ReadAsync<T>(bool buffered = true);
    }
}
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;

namespace Worldpay.CIS.DataAccess.Connection
{
    public class BaseRepository: IDatabaseConnectionFactory
    { 
        private string _connectionString;
        private int _commandTimeout;

        public BaseRepository(IOptions<DataContext> optionsAccessor)
        {
            _connectionString = optionsAccessor.Value.CisConnectionString;

            this._commandTimeout = optionsAccessor.Value.CommandTimeout;
        }

        public BaseRepository(string connectionString, int commandTimeout)
        {
            _connectionString = connectionString;

            _commandTimeout = commandTimeout;
        }

        async  Task<T> IDatabaseConnectionFactory.GetConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            
            try
            {
                using (var connection = new CisCustomDbConnection(new SqlConnection(_connectionString), this._commandTimeout))
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for buffered queries that do not return a type
        async Task IDatabaseConnectionFactory.GetConnection(Func<IDbConnection, Task> getData)
        {
           
            try
            {
                using (var connection = new CisCustomDbConnection(new SqlConnection(_connectionString), this._commandTimeout))
                {
                    await connection.OpenAsync();
                    await getData(connection);

                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for non-buffered queries that return a type
        async Task<TResult> IDatabaseConnectionFactory.GetConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData, Func<TRead, Task<TResult>> process)
        {
            
            try
            {
                using (var connection = new CisCustomDbConnection(new SqlConnection(_connectionString), this._commandTimeout))
                {
                    await connection.OpenAsync();
                    var data = await getData(connection);
                    return await process(data);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.GetConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

      
    }
}

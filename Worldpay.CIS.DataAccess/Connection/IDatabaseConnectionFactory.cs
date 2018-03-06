using System;
using System.Data;
using System.Threading.Tasks;

namespace Worldpay.CIS.DataAccess.Connection
{
    public interface IDatabaseConnectionFactory
    {
        Task<T> GetConnection<T>(Func<IDbConnection, Task<T>> getData);
        Task GetConnection(Func<IDbConnection, Task> getData);

        Task<TResult> GetConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData,
            Func<TRead, Task<TResult>> process);


    }
}

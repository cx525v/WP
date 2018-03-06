using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Wp.CIS.LynkSystems.Dal
{
    public interface IDbConnectionScope : IDisposable
    {
        IDbConnection GetOpenedDbConnection();
    }
}
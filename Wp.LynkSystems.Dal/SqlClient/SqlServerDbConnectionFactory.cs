using System;

using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Wp.CIS.LynkSystems.Dal.SqlClient
{
    /// <summary>
    /// IDbConnectionScopeFactory for a Sql Server Database
    /// </summary>
    public class SqlServerDbConnectionFactory : IDbConnectionScopeFactory
    {
        private readonly DbConnectionScopeFactory _scopeFactory;

        public SqlServerDbConnectionFactory()
        {
            _scopeFactory = new DbConnectionScopeFactory(CreateConnection);
        }

        public IDbConnectionScope GetConnectionScope()
        {
            return _scopeFactory.GetConnectionScope();
        }

        public ITransactedDbConnectionScope GetTransactedConnectionScope()
        {
            return _scopeFactory.GetTransactedConnectionScope();
        }

        private static DbConnection CreateConnection(string connectionString)
        {
            var connectionSetting = "Data Source=DSPADWSTAR;Initial Catalog=CIS;Integrated Security=False;User Id=CISPlusUser;Password=!Savannah123;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;";

            if (connectionSetting == null)
            {
                string msg = String.Format("Could not find a connection string named {0} please check your app.config file.",
                        connectionString);

                throw new InvalidOperationException(msg);
            }

            return new SqlConnection("Data Source=DSPADWSTAR;Initial Catalog=CIS;Integrated Security=False;User Id=CISPlusUser;Password=!Savannah123;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;");
        }
    }
}
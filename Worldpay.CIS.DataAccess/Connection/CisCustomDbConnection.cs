using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
//using System.Transactions;

namespace Worldpay.CIS.DataAccess.Connection
{
    public class CisCustomDbConnection : DbConnection
    {
        private DbConnection _Connection;

        private int _commandTimeout;

        public CisCustomDbConnection(DbConnection connection, 
            int commandTimeout)
        {
            _Connection = connection;

            this._commandTimeout = commandTimeout;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _Connection != null)
            {
                _Connection.Dispose();
            }
            _Connection = null;
            base.Dispose(disposing);
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _Connection.BeginTransaction(isolationLevel);
        }

        public override void Close()
        {
            _Connection.Close();
        }

        public override void ChangeDatabase(String databaseName)
        {
            _Connection.ChangeDatabase(databaseName);
        }

        public override void Open()
        {
            _Connection.Open();
        }

        public override String ConnectionString
        {
            get { return _Connection.ConnectionString; }
            set { _Connection.ConnectionString = value; }
        }

        public override String Database
        {
            get { return _Connection.Database; }
        }

        public override ConnectionState State
        {
            get { return _Connection.State; }
        }

        public override String DataSource
        {
            get { return _Connection.DataSource; }
        }

        public override String ServerVersion
        {
            get { return _Connection.ServerVersion; }
        }

        protected override DbCommand CreateDbCommand()
        {
            var result = _Connection.CreateCommand();
            result.CommandTimeout = this._commandTimeout;
            return result;
        }
    }
}

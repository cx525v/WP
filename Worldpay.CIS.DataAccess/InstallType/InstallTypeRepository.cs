using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.InstallType
{
    public class InstallTypeRepository : IInstallTypeRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public InstallTypeRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IInstallTypeRepository Implementation

        public async Task<IEnumerable<InstallTypeModel>> GetAllInstallTypesAsync()
        {
            var retVal = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<InstallTypeModel> response = null;

                var p = new DynamicParameters();

                p.Add("Lid", 0, DbType.Int32);
                response = await c.QueryAsync<InstallTypeModel>(sql: "uspSelectInstallTypes", param: p, commandType: CommandType.StoredProcedure);
                return response;

            });

            return retVal;
        }

        #endregion
    }
}

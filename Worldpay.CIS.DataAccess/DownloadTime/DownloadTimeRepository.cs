using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.DownloadTime
{
    public class DownloadTimeRepository : IDownloadTimeRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public DownloadTimeRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IDownloadTimeRepository Implementation

        public async Task<IEnumerable<DownloadTimeModel>> GetAllDownloadTimesAsync()
        {
            var retVal = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<DownloadTimeModel> response = null;

                var p = new DynamicParameters();

                p.Add("DUMMY", 0, DbType.Int32);
                response = await c.QueryAsync<DownloadTimeModel>(sql: "USP_CISBIS_ProdComp_GetDownloadTimes", param: p, commandType: CommandType.StoredProcedure);
                return response;

            });

            return retVal;
        }
        #endregion
    }

}

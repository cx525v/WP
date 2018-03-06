using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.MobileLookup
{
    public class MobileLookupRepository : IMobileLookupRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public MobileLookupRepository(IOptions<DataContext> optionsAccessor,
            IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        //public MobileLookupRepository()
        //{
        //    var i = 0;
        //    i++;
        //}

        #endregion

        #region IMobileLookupRepository Implementation

        public async Task<IEnumerable<MobileLookupModel>> GetAllMobileLookupsAsync()
        {
            var response = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<MobileLookupModel> mobileLookups = null;

                var p = new DynamicParameters();

                p.Add("Lid", 0, DbType.Int32);
                mobileLookups = await c.QueryAsync<MobileLookupModel>(sql: "uspSelectMobileTypes", param: p, commandType: CommandType.StoredProcedure);
                return mobileLookups;
            });

            return response;
        }

        #endregion
    }
}

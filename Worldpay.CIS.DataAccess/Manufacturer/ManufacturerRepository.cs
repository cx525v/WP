using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.Manufacturer
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public ManufacturerRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IManufacturerRepository Implementation

        public async Task<IEnumerable<ManufacturerModel>> GetAllManufacturersAsync()
        {
            var retVal = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ManufacturerModel> response = null;

                var p = new DynamicParameters();

                p.Add("DUMMY", 0, DbType.Int32);
                response = await c.QueryAsync<ManufacturerModel>(sql: "USP_CISBIS_ProdComp_GetAllManufacturers", param: p, commandType: CommandType.StoredProcedure);
                return response;

            });

            return retVal;
        }

        #endregion
    }
}

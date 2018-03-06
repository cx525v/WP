using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.Brand
{
    public class BrandRepository : IBrandRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public BrandRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IBrandRepository Implementation

        public async Task<IEnumerable<ProductBrandModel>> GetProductBrandsAsync()
        {
            var retVal = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ProductBrandModel> response = null;

                var p = new DynamicParameters();

                p.Add("Lid", 0, DbType.Int32);
                //response = await c.QueryAsync<ProductBrandModel>(sql: "uspGetProductBrandId", param: p, commandType: CommandType.StoredProcedure);
                response = await c.QueryAsync<ProductBrandModel>(sql: "uspSelectBrandIds", param: p, commandType: CommandType.StoredProcedure);
                return response;

            });

            return retVal;
        }

        #endregion

    }
}

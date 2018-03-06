using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.ProductType
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        #region Private Fields

        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        public ProductTypeRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IProductTypeRepository Implementation

        public async Task<IEnumerable<ProductTypeModel>> GetAllProductTypesAsync()
        {
             var response =  await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ProductTypeModel> productTypes = null;

                var p = new DynamicParameters();

                p.Add("DUMMY", 0, DbType.Int32);
                productTypes = await c.QueryAsync<ProductTypeModel>(sql: "USP_CISBIS_ProdComp_GetProductTypes", param: p, commandType: CommandType.StoredProcedure);
                return productTypes;
            });

            return response;
        }

        #endregion
    }
}

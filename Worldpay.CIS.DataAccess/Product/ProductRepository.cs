using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.Product
{
    public class ProductRepository : IProductRepository
    {
        #region Private Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly IDatabaseConnectionFactory _connectionFactory;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="connectionFactory"></param>
        public ProductRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        #endregion

        #region IProductRepository Implementation

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var response = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ProductModel> productModels = null;

                var p = new DynamicParameters();

                p.Add("DUMMY", 0, DbType.Int32);
                productModels = await c.QueryAsync<ProductModel>(sql: "USP_CISBIS_ProdComp_GetAllProducts", param: p, commandType: CommandType.StoredProcedure);
                return productModels;
            });

            return response;
        }


        /// <summary>
        /// This is used to retrieve a page of product records.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductModel>> GetProductsWithPagingAsync(int firstRecordNumber, int pageSize,
            string sortField,
            SortOrderEnum sortOrder)
        {
            var response = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ProductModel> productModels = null;

                var p = new DynamicParameters();

                p.Add("FirstRecordNumber", firstRecordNumber, DbType.Int32);
                p.Add("PageSize", pageSize, DbType.Int32);
                p.Add("SortField", sortField, DbType.String);
                p.Add("SortOrder", (int)sortOrder, DbType.Int32);

                productModels = await c.QueryAsync<ProductModel>(sql: "[CISPlus].[USP_CISBIS_ProdComp_GetALLProductsWithPaging]", param: p, commandType: CommandType.StoredProcedure);
                return productModels;
            });

            return response;
        }



        /// <summary>
        /// Gets the records associated with a product description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductModel>> GetProductsByDescriptionAsync(string description)
        {
            var response = await this._connectionFactory.GetConnection(async c =>
            {
                IEnumerable<ProductModel> productModels = null;

                var p = new DynamicParameters();

                p.Add("Description", description, DbType.Int32);
                productModels = await c.QueryAsync<ProductModel>(sql: "USP_CISBIS_ProdComp_GetProdCode", param: p, commandType: CommandType.StoredProcedure);
                return productModels;
            });

            return response;
        }

        #endregion

    }
}

using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Product;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services.Administrative
{
    public class ProductApi : IProductApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IProductRepository _productRepository;

        #endregion

        /// <summary>
        /// Constructor. This initializes the class state.
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public ProductApi(IOptions<Settings> optionsAccessor, IProductRepository productRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._productRepository = productRepository;
        }

        #region IProductApi Implementation

        /// <summary>
        /// This is used to retrieve all of the products.
        /// </summary>
        /// <returns>This is a list of all of the products.</returns>
        public async Task<IEnumerable<ProductModel>> GetAllProductsAsync()
        {
            var response = await this._productRepository
                                        .GetAllProductsAsync();

            return response;

        }

        /// <summary>
        /// This is used to retrieve a page of product records.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductModel>> GetProductsWithPagingAsync(int firstRecordNumber, 
            int pageSize,
            string sortField,
            SortOrderEnum sortOrder)
        {
            if(true == string.IsNullOrEmpty(sortField) 
                || true == string.Equals(sortField, "undefined", StringComparison.OrdinalIgnoreCase))
            {
                sortField = "description";
            }

            if(SortOrderEnum.Asc != sortOrder && SortOrderEnum.Desc != sortOrder)
            {
                sortOrder = SortOrderEnum.Asc;
            }

            var response = await this._productRepository
                                        .GetProductsWithPagingAsync(firstRecordNumber, 
                                        pageSize,
                                        sortField,
                                        sortOrder);

            return response;
        }

        /// <summary>
        /// Gets the records associated with a product description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public async Task<ProductModel> GetFirstProductByDescriptionAsync(string description)
        {
            ProductModel response = null;

            var products = await this._productRepository
                                        .GetProductsByDescriptionAsync(description);

            var productsList = new List<ProductModel>(products);

            if(productsList.Count > 0)
            {
                response = productsList[0];
            }

            return response;
        }

        #endregion
    }

}

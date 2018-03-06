using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Interfaces
{
    /// <summary>
    /// This is used to define the functionality for the product maintenance
    /// web api.
    /// </summary>
    public interface IProductApi
    {
        /// <summary>
        /// This is used to retrieve all the products.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();

        /// <summary>
        /// This is used to retrieve a page of product records.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductModel>> GetProductsWithPagingAsync(int firstRecordNumber, 
            int pageSize,
            string sortField,
            SortOrderEnum sortOrder);

        /// <summary>
        /// Gets the records associated with a product description.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<ProductModel> GetFirstProductByDescriptionAsync(string description);
    }
}

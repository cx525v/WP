using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();

        /// <summary>
        /// This is used to retrieve a page of product records.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ProductModel>> GetProductsWithPagingAsync(int firstRecordNumber, 
            int pageSize,
            string sortField,
            SortOrderEnum sortOrder);

        Task<IEnumerable<ProductModel>> GetProductsByDescriptionAsync(string description);
    }
}

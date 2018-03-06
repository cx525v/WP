using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ProductType;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class ProductTypesApi : IProductTypesApi
    {

        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IProductTypeRepository _productTypeRepository;

        #endregion

        #region Public Constructors

        public ProductTypesApi(IOptions<Settings> optionsAccessor, IProductTypeRepository productTypeRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._productTypeRepository = productTypeRepository;
        }

        #endregion

        #region IDownloadTimesApi Implementation

        public async Task<IEnumerable<ProductTypeModel>> GetAllProductTypesAsync()
        {
            var response = await this._productTypeRepository
                                    .GetAllProductTypesAsync();

            return response;
        }

        #endregion
    }
}

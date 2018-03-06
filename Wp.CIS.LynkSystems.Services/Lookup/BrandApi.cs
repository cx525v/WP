using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Brand;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class BrandApi : IBrandApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IBrandRepository _brandRepository;

        #endregion

        #region Public Constructors

        public BrandApi(IOptions<Settings> optionsAccessor,
                                IBrandRepository brandRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._brandRepository = brandRepository;
        }

        #endregion

        #region IBrandApi Implementation

        public async Task<IEnumerable<ProductBrandModel>> GetProductBrandsAsync()
        {
            var response = await this._brandRepository
                                    .GetProductBrandsAsync();

            return response;
        }

        #endregion
    }
}

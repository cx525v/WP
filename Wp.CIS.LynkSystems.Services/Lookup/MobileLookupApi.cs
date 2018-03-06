using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MobileLookup;
using Worldpay.CIS.DataAccess.ProductType;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class MobileLookupApi : IMobileLookupApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IMobileLookupRepository _mobileLookupRepository;

        #endregion

        #region Public Constructors

        public MobileLookupApi(IOptions<Settings> optionsAccessor,
            IMobileLookupRepository mobileLookupRepository
            )
        {
            this._optionsAccessor = optionsAccessor;

            this._mobileLookupRepository = mobileLookupRepository;
        }

        //public MobileLookupApi(IOptions<Settings> optionsAccessor)
        //{
        //    var i = 0;
        //    i++;
        //}

        #endregion

        #region IMobileLookupApi Implementation

        public async Task<IEnumerable<MobileLookupModel>> GetAllMobileLookupsAsync()
        {
            var response = await this._mobileLookupRepository
                                        .GetAllMobileLookupsAsync();

            return response;
        }

        #endregion
    }

}

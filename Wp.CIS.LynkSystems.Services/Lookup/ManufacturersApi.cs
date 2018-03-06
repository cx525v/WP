using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Manufacturer;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class ManufacturersApi : IManufacturersApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IManufacturerRepository _manufacturerRepository;

        #endregion

        #region Public Constructors

        public ManufacturersApi(IOptions<Settings> optionsAccessor, IManufacturerRepository manufacturerRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._manufacturerRepository = manufacturerRepository;
        }

        #endregion

        #region IManufacturersApi Implementation

        public async Task<IEnumerable<ManufacturerModel>> GetAllManufacturersAsync()
        {
            var response = await this._manufacturerRepository.GetAllManufacturersAsync();

            return response;
        }

        #endregion
    }
}

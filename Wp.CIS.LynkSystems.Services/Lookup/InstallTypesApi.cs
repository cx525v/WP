using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Lookup;
using Dapper;
using System.Data;
using Worldpay.CIS.DataAccess.InstallType;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class InstallTypesApi : IInstallTypesApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IInstallTypeRepository _installTypeRepository;

        #endregion

        #region Public Constructors

        public InstallTypesApi(IOptions<Settings> optionsAccessor,
                                IInstallTypeRepository installTypeRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._installTypeRepository = installTypeRepository;
        }

        #endregion

        #region IInstallTypesApi Implementation

        public async Task<IEnumerable<InstallTypeModel>> GetAllInstallTypesAsync()
        {
            var response = await this._installTypeRepository
                                    .GetAllInstallTypesAsync();

            return response;
        }

        #endregion
    }
}

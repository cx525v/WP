using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DownloadTime;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services.Lookup
{
    public class DownloadTimesApi : IDownloadTimesApi
    {
        #region Private Fields

        private IOptions<Settings> _optionsAccessor;

        private IDownloadTimeRepository _downloadtimeRepository;

        #endregion

        #region Public Constructors

        public DownloadTimesApi(IOptions<Settings> optionsAccessor, 
            IDownloadTimeRepository downloadtimeRepository)
        {
            this._optionsAccessor = optionsAccessor;

            this._downloadtimeRepository = downloadtimeRepository;
        }

        #endregion

        #region IDownloadTimesApi Implementation

        public async Task<IEnumerable<DownloadTimeModel>> GetAllDownloadTimesAsync()
        {
            var response = await this._downloadtimeRepository
                                        .GetAllDownloadTimesAsync();

            return response;
        }

        #endregion
    }
}

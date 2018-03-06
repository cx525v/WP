
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.ActiveServicesInfo;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class ActiveServicesApi : IActiveServicesApi
    {
        private IActiveServicesRepository _activeServicesRepository;
        public ActiveServicesApi(IOptions<Settings> optionsAccessor, IActiveServicesRepository activeServicesRepository)
        {
            _activeServicesRepository = activeServicesRepository;
        }
        public async Task<ApiResult<ICollection<ActiveServices>>> GetActiveServices(int LIDType, int LID)
        {
            ApiResult<ICollection<ActiveServices>> response = new ApiResult<ICollection<ActiveServices>>();
           
            try
            {
                response.Result = await _activeServicesRepository.GetActiveServices(LIDType, LID);
            }
            catch (System.Exception)
            {

                throw;
            }
            return response;
        }

    }
}


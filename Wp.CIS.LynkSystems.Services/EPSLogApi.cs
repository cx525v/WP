using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsLog;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class EPSLogApi : IEPSLogApi
    {
        //ToDO: Need to load connection string from app settings
        public IEPSLogRepository _epsLogRepository;
        
        public EPSLogApi(IOptions<Settings> optionsAccessor, IEPSLogRepository epsLogRepository)
        {
            _epsLogRepository = epsLogRepository;
        }

        public async Task<ApiResult<ICollection<EPSLog>>> GetEPSLogAsync(string startDate, string endDate, int? LidType, string Lid)
        {
            ApiResult<ICollection<EPSLog>> response = new ApiResult<ICollection<EPSLog>>();
            var errorkey = EPSLogErrorCodes.Succeeded;
            try
            {
                if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    errorkey = EPSLogErrorCodes.EPSLogDateRangeError;
                }
                else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    DateTime start = Convert.ToDateTime(startDate);
                    DateTime end = Convert.ToDateTime(endDate);
                    if ((end - start).TotalDays > 62)
                    {
                        errorkey = EPSLogErrorCodes.EPSLogDateRangeError;
                    }
                }


                if (errorkey == EPSLogErrorCodes.Succeeded)
                    response.Result = await _epsLogRepository.GetEPSLogAsync(startDate, endDate, LidType, Lid);
                else
                {
                    response.AddErrorMessage(errorkey.ToString());
                }

            }
            catch
            {
                throw;
            }
           
            return response;
        }
    }
}

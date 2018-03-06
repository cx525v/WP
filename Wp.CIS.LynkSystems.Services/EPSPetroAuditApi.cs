using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsPetroAudit;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class EPSPetroAuditApi : IEPSPetroAuditApi
    {
        public IEPSPetroAuditRepository _epsPetroAuditRepository;
        public EPSPetroAuditApi(IOptions<Settings> optionsAccessor, IEPSPetroAuditRepository epsPetroAuditRepository)
        {
            _epsPetroAuditRepository = epsPetroAuditRepository;
        }
        public async Task<ApiResult<ICollection<EPSPetroAudit>>> GetEPSPetroAuditByVersion(int versionID, string startDate, string endDate)
        {
            ApiResult<ICollection<EPSPetroAudit>> response = new ApiResult<ICollection<EPSPetroAudit>>();

            try
            {
                var errorkey = EPSPetroAuditErrorCodes.Succeeded;
                if (versionID <= 0)
                {
                    errorkey = EPSPetroAuditErrorCodes.InValidVersionIdError;
                }
                else if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                {
                    errorkey = EPSPetroAuditErrorCodes.DatesErrorMsg;
                }
                else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    DateTime start;
                    DateTime.TryParse(startDate, out start);
                    DateTime end;
                    DateTime.TryParse(endDate, out end);
                    if ((end - start).TotalDays > 30)
                    {
                        errorkey = EPSPetroAuditErrorCodes.DateRangeError;
                    }
                }

                if (errorkey == EPSPetroAuditErrorCodes.Succeeded)
                    response.Result = await _epsPetroAuditRepository.GetEPSPetroAuditsAsync(versionID, startDate, endDate);
                else
                    response.AddErrorMessage(errorkey.ToString());
            }
            catch(Exception)
            {
                throw;
            }
            
            return response;
        }

        public async Task<ApiResult<ICollection<EPSPetroAuditDetails>>> GetEPSPetroAuditDetails(int auditID)
        {
            ApiResult<ICollection<EPSPetroAuditDetails>> response = new ApiResult<ICollection<EPSPetroAuditDetails>>();
            try
            {
                if (auditID <= 0)
                    response.AddErrorMessage(EPSPetroAuditErrorCodes.InValidAuditIdError.ToString());
                else
                    response.Result = await _epsPetroAuditRepository.GetEPSPetroAuditDetailsAsync(auditID);

            }
            catch (Exception)
            {

                throw;
            }
          
            return response;
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsMapping;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class EPSMappingApi : IEPSMappingApi
    {
        public IEPSMappingRepository _epsMappingRepository;
        public EPSMappingApi(IOptions<Settings> optionsAccessor, IEPSMappingRepository epsMappingRepository)
        {
            _epsMappingRepository = epsMappingRepository;
        }

        public async Task<ApiResult<ICollection<EPSMapping>>> RetrieveEPSMappingAsync(int versionID)
        {
            ApiResult<ICollection<EPSMapping>> response = new ApiResult<ICollection<EPSMapping>>();

            try
            {
                response.Result = await _epsMappingRepository.RetrieveEPSMappingAsync(versionID);
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResult<EPSMappingErrorCodes>> UpdateEPSMappingAsync(EPSMapping mapping)
        {
            ApiResult<EPSMappingErrorCodes> response = new ApiResult<EPSMappingErrorCodes>();

            try
            {
                var errorResponse = ValidateMapping(mapping);
                if (errorResponse == EPSMappingErrorCodes.Succeeded)
                    await _epsMappingRepository.UpdateEPSMappingAsync(mapping);

                response.Result = errorResponse;

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<bool> BulkInsertEPSMappingAsync(int versionId, string strFormatFileName, string strDataFileName)
        {
            bool result = false;

            try
            {
                result = await _epsMappingRepository.BulkInsertEPSMappingAsync(versionId, strFormatFileName, strDataFileName);
            }
            catch(Exception)
            {
                throw;
            }


            return result;
        }

        public async Task<ApiResult<bool>> CopyEpsMappingAsync(int fromVersionId, int toVersionId, string userName)
        {
            ApiResult<bool> response = new ApiResult<bool>();

            try
            {
                response.Result = await _epsMappingRepository.CopyEpsMappingAsync(fromVersionId, toVersionId, userName);
            }
            catch(Exception)
            {
                throw;
            }
            
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        public async Task<ApiResult<EPSMappingErrorCodes>> InsertEPSMappingAsync(EPSMapping mapping)
        {
            ApiResult<EPSMappingErrorCodes> res = new ApiResult<EPSMappingErrorCodes>();

            try
            {
                var response = ValidateMapping(mapping);
                if (response == EPSMappingErrorCodes.Succeeded)
                {
                    var insertResponse = await _epsMappingRepository.InsertEPSMappingAsync(mapping);

                    if (false == insertResponse)
                    {
                        response = EPSMappingErrorCodes.EPSMappingAllErrorMsg;
                    }
                }
                res.Result = response;

            }
            catch (Exception)
            {
                throw;
            }

            return res;
        }

        private EPSMappingErrorCodes ValidateMapping(EPSMapping mapping)
        {
            var response = EPSMappingErrorCodes.Succeeded;

            try
            {
                if (mapping.pdlFlag)
                {
                    if (string.IsNullOrEmpty(mapping.paramName))
                    {
                        response = EPSMappingErrorCodes.EPSMappingParamNameErrorMsg;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(mapping.worldPayFieldName) || string.IsNullOrEmpty(mapping.worldPayTableName))
                    {
                        response = EPSMappingErrorCodes.EPSMappingTable_FieldNameErrorMsg;
                    }
                }

                if ((string.IsNullOrEmpty(mapping.viperFieldName) || string.IsNullOrEmpty(mapping.viperTableName)) && response == EPSMappingErrorCodes.Succeeded)
                {
                    response = EPSMappingErrorCodes.EPSMappingViperFieldsErrorMsg;

                }

                if ((mapping.effectiveBeginDate.HasValue == false || mapping.effectiveEndDate.HasValue == false) && response == EPSMappingErrorCodes.Succeeded)
                {
                    response = EPSMappingErrorCodes.EPSMappingDatesErrorMsg;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IEPSMappingApi
    {

        Task<ApiResult<ICollection<EPSMapping>>> RetrieveEPSMappingAsync(int versionID);
        Task<ApiResult<EPSMappingErrorCodes>> UpdateEPSMappingAsync(EPSMapping epsMapping);
        Task<ApiResult<EPSMappingErrorCodes>> InsertEPSMappingAsync(EPSMapping mapping);
        Task<bool> BulkInsertEPSMappingAsync(int versionId, string strFormatFileName, string strDataFileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromVersionID"></param>
        /// <param name="toVersionID"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> CopyEpsMappingAsync(int fromVersionID, int toVersionID, string userName);
    }
}

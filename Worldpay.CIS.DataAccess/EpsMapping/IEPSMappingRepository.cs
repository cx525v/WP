using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsMapping
{
    public interface IEPSMappingRepository
    {
        Task<ICollection<EPSMapping>> RetrieveEPSMappingAsync(int versionID);

        Task<bool> UpdateEPSMappingAsync(EPSMapping epsMapping);

        Task<bool> InsertEPSMappingAsync(EPSMapping mapping);

        Task<bool> BulkInsertEPSMappingAsync(int versionId, string strFormatFileName, string strDataFileName);

        Task<bool> CopyEpsMappingAsync(int fromVersionID, int toVersionID, string userName);
    }
}

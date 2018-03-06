using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IEPSPetroAuditApi
    {
        Task<ApiResult<ICollection<EPSPetroAudit>>> GetEPSPetroAuditByVersion(int versionID, string startDate, string endDate);

        Task<ApiResult<ICollection<EPSPetroAuditDetails>>> GetEPSPetroAuditDetails(int auditId);
    }
}

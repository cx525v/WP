using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsPetroAudit
{
    public interface IEPSPetroAuditRepository
    {
        Task<ICollection<EPSPetroAudit>> GetEPSPetroAuditsAsync(int versionID, string startDate, string endDate);

        Task<ICollection<EPSPetroAuditDetails>> GetEPSPetroAuditDetailsAsync(int auditID);
    }
}

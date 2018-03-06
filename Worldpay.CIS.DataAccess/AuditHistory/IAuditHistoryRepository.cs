using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.AuditHistory
{
    public interface IAuditHistoryRepository
    {
        Task<IEnumerable<AuditHistoryModel>> GetAuditHistoryAsync(LidTypeEnum lidType, int lid, ActionTypeEnum actionType);
    }
}

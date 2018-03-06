using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Interfaces.Administrative
{
    public interface IAuditHistoryApi
    {
        Task<AuditHistoryModel> GetLatestAuditHistoryRecordAsync(LidTypeEnum lidType, int lid, ActionTypeEnum actionType);
    }
}

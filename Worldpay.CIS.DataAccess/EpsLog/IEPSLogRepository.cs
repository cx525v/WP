using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsLog
{
    public interface IEPSLogRepository
    {
        Task<ICollection<EPSLog>> GetEPSLogAsync(string startDate, string endDate, int? LidType, string Lid);
       // Task<ICollection<EPSLog>> GetValuesAsync(DynamicParameters p);
    }
}

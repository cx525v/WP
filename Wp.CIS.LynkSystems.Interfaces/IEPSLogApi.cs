using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;


namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IEPSLogApi
    {
        Task<ApiResult<ICollection<EPSLog>>> GetEPSLogAsync(string startDate, string endDate, int? LidType, string Lid);

    }
}

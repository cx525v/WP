using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IRecentStatementApi
    {
        Task<ApiResult<ICollection<RecentStatement>>> GetRecentStatementAsync(string merchantNbr);
    }
}

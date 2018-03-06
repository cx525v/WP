using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ITerminalDetailsApi
    {
        ApiResult<EAndPData> GetTerminalDetails(int termNbr);

        Task<ApiResult<TerminalSettlementInfo>> GetTerminalSettlementInfo(int termNbr);
    }
}

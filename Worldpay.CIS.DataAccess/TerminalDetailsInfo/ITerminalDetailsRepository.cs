using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.TerminalDetailsInfo
{
    public interface ITerminalDetailsRepository
    {
        Task<EAndPData> GetTerminalDetails(int termNbr);

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo
{
    public interface ITerminalDetailsSettlementInfoRepository
    {
        Task<TerminalSettlementInfo> GetTerminalSettlementInfo(int termNbr);
    }
}

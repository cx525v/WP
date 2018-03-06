using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.BankingInfo
{
    public interface IBankingInfoRepository
    {
        Task<ICollection<Wp.CIS.LynkSystems.Model.BankingInformation>> GetBankingInfo(Helper.LIDTypes LIDType, string LID);
    }
}

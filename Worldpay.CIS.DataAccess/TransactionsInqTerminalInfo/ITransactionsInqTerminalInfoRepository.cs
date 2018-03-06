using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo
{
    public interface ITransactionsInqTerminalInfoRepository
    {
        Task<TransactionsInquiryGeneralInfo> GetTransactionInquiryTerminalInfo(int? TerminalNbr, string TerminalId);
    }
}

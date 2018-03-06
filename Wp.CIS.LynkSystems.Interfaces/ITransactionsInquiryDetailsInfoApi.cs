using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ITransactionsInquiryDetailsInfoApi
    {
        //string startDate, string endDate
        Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryResults(TransactionsInquiryGeneralInfo terminalgeneralinfo, int terminalnbr, int? SearchId, string startDate, string endDate, int? BatchNo, string CardNo, int SkipRecords, int PageSize);
    }

}

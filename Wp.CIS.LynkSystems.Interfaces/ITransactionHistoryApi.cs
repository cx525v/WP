using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ITransactionHistoryApi
    {
        Task<ApiResult<GenericPaginationResponse<TransactionHistory>>>
            GetTransactionHistoryAsync(string terminalID, PaginationTransactionHistory Page);
    }
}

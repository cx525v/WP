using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Worldpay.CIS.DataAccess.TransactionHistory
{
    public interface ITransactionHistoryRepository
    {
        Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> GetTransactionHistoryAsync(string terminalId, PaginationTransactionHistory Page);
    }
}

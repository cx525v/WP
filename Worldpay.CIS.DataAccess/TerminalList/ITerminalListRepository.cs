using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Worldpay.CIS.DataAccess.TerminalList
{
    public interface ITerminalListRepository
    {
        Task<ICollection<Terminal>> GetTerminalListAsync(int merchantId);
        Task<GenericPaginationResponse<Terminal>> GetTerminalListAsync(int merchantId, PaginationTerminal Page);
    }
}

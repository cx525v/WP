using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ITerminalListApi
    {
        Task<ApiResult<ICollection<Terminal>>> GetTerminalListAsync(int terminalID);

        Task<ApiResult<GenericPaginationResponse<Terminal>>> 
            GetTerminalListAsync(int terminalID, PaginationTerminal Page);
    }
}

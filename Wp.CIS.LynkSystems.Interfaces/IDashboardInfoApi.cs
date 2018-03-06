using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Dashboard;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IDashboardInfoApi
    {
        Task<ApiResult<DashboardInfo>> GetDashboardSearchResults(Helper.LIDTypes LIDtype, int LID);

        Task<DashboardPrimaryKeysModel> GetDashboardSearchPrimaryKeys(LidTypeEnum lidtype, string lid, int? lidPk);
        Task<ApiResult<TerminalDetails>> GetTerminalDetails(int termNbr);
        Task<ApiResult<DashboardInfo>> GetDashboardSearchResultsPagination(Helper.LIDTypes LIDtype, int LID);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Dashboard;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.DashboardInfo
{
    public interface IDashboardInfoRepository
    {
        Task<DashboardPrimaryKeysModel> GetDashboardSearchPrimaryKeys(LidTypeEnum LIDtype, string lid, int? lidPk);
        Task<Wp.CIS.LynkSystems.Model.DashboardInfo> GetDashboardSearchResults(Helper.LIDTypes LIDtype, int LID, int maxNumberOfRecordsToReturn);
        Task<TerminalDetails> GetTerminalDetails(int termNbr);
        Task<Wp.CIS.LynkSystems.Model.DashboardInfo> GetDashboardSearchResultsPagination(Helper.LIDTypes LIDtype, int LID, int maxRecordCount);
    }
}

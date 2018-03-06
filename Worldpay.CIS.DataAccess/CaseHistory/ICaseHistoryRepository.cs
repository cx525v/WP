using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Worldpay.CIS.DataAccess.CaseHistory
{
    public interface ICaseHistoryRepository
    {
        Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> GetCaseHistoryInfo(LidTypeEnum liDtype, string LID, string ExtraId, PaginationCaseHistory page);
    }
}

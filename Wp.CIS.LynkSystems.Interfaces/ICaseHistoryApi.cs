using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ICaseHistoryApi
    {
        //Task<ICollection<CaseHistory>> GetCaseHistory(Helper.LIDTypes liDtype, string lid, int? extraID, int? pagesize, int? skiprecords, string sortfield,
        //                                              int? sortorder, int? filtercaseid, string filtercasedesc, string filterorgdeptname, string filtercaselevel);
        Task<ApiResult<GenericPaginationResponse<CaseHistory>>> GetCaseHistory(LidTypeEnum liDtype, string LID, string ExtraId, PaginationCaseHistory page);
    }
}



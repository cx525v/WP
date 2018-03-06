using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IContactListApi
    {
        Task<ApiResult<GenericPaginationResponse<Demographics>>> 
            GetContactListAsync(LidTypeEnum LIDType, string LID, PaginationDemographics page);

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Worldpay.CIS.DataAccess.ContactList
{
    public interface IContactListRepository
    {
        Task<GenericPaginationResponse<Demographics>> GetContactListAsync(LidTypeEnum LIDType, string LID, PaginationDemographics page);
    }
}

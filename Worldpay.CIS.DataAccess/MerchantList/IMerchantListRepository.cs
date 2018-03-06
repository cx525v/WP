using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Worldpay.CIS.DataAccess.MerchantList
{
    public interface IMerchantListRepository
    {
        Task<GenericPaginationResponse<Merchant>> GetMerchantListAsync(int custId, PaginationMerchant page);
    }
}

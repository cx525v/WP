using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Lookup;

namespace Worldpay.CIS.DataAccess.MobileLookup
{
    public interface IMobileLookupRepository
    {
        Task<IEnumerable<MobileLookupModel>> GetAllMobileLookupsAsync();
    }
}

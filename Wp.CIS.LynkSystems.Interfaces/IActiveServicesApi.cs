using Wp.CIS.LynkSystems.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IActiveServicesApi
    {
        Task<ApiResult<ICollection<ActiveServices>>> GetActiveServices(int LIDtype, int LID);
    }
}

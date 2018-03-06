using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.ActiveServicesInfo
{
    public interface IActiveServicesRepository
    {
        Task<ICollection<Wp.CIS.LynkSystems.Model.ActiveServices>> GetActiveServices(int LIDType, int LID);
    }
}

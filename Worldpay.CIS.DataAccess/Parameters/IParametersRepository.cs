using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Worldpay.CIS.DataAccess.Parameters
{
   public interface IParametersRepository
    {
        Task<ICollection<Wp.CIS.LynkSystems.Model.Parameters>> GetParametersAsync(int? parameterId = null);
    }
}

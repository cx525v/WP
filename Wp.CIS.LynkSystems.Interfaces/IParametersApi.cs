using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IParametersApi
    {
        Task<ApiResult<ICollection<Parameters>>> GetParameters(int? parameterId = null);
    }
}

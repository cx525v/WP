using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.Parameters;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class ParametersApi : IParametersApi
    {
        public IParametersRepository _parametersRepository;
        public ParametersApi(IOptions<Settings> optionsAccessor, IParametersRepository parametersRepository)
        {
            _parametersRepository = parametersRepository;
        }
        public async Task<ApiResult<ICollection<Parameters>>> GetParameters(int? parameterId = null)
        {
            ApiResult<ICollection<Parameters>> response = new ApiResult<ICollection<Parameters>>();
            
            try
            {
                response.Result = await _parametersRepository.GetParametersAsync(parameterId);
            }
            catch (System.Exception)
            {
                throw;
            }
            return response;
        }

    }
}

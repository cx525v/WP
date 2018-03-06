using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.BankingInfo;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class BankingApi: IBankingApi
    {
        public IBankingInfoRepository _bankingRepository;
        public BankingApi(IOptions<Settings> optionsAccessor , IBankingInfoRepository bankingRepository )
        {
            _bankingRepository = bankingRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LIDType"></param>
        /// <param name="LID"></param>
        /// <returns></returns>
        public async Task<ApiResult<ICollection<BankingInformation>>> GetBankingInfo(Helper.LIDTypes LIDType, string LID)
        {
            ApiResult<ICollection<BankingInformation>> response = new ApiResult<ICollection<BankingInformation>>();
           
            try
            {
                response.Result = await _bankingRepository.GetBankingInfo(LIDType, LID);
            }
            catch (System.Exception)
            {
 
                throw;
            }
            return response;
        }
    }
}

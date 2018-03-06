using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MerchantList;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class MerchantListApi : IMerchantListApi
    {
        public IMerchantListRepository _merchantRepository;
        private readonly ILoggingFacade _loggingFacade;
        public MerchantListApi(IOptions<Settings> optionsAccessor, IMerchantListRepository merchantRepository,
                                         ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Merchant List API Service",
                                    "MerchantListApi.cs", "MerchantListApi"), CancellationToken.None);
            _merchantRepository = merchantRepository;            
        }
        /// <summary>
        /// CustomerId is input to retrieve the List of Merchant.
        /// </summary>
        /// <param name="CustId"></param>
        /// <returns></returns>
        
        public async Task<ApiResult<GenericPaginationResponse<Merchant>>> GetMerchantListAsync(int CustId, PaginationMerchant page)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Merchant List GetMerchantListAsync for CustomerID -" + CustId,
                                   "MerchantListApi.cs", "GetMerchantListAsync"), CancellationToken.None);
            ApiResult<GenericPaginationResponse<Merchant>> response = new ApiResult<GenericPaginationResponse<Merchant>>();
            
            try
            {
                response.Result = await _merchantRepository.GetMerchantListAsync(CustId, page);

                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the  Merchant List resultset from DB for CustomerID -" + CustId,
                                   "MerchantListApi.cs", "GetMerchantListAsync"), CancellationToken.None);
            }
            catch(Exception)
            {
                throw; 
            }
            return response;
        }
    }
}

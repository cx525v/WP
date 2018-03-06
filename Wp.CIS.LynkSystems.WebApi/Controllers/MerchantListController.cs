using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Microsoft.Extensions.Configuration;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>

    [Produces("application/json")]
    [Route("api/MerchantList")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class MerchantListController : Controller
    {

        #region Constructor MerchantListController
        private readonly IDistributedCache _cache;
        private readonly IMerchantListApi _merchantListApi;
        private readonly IStringLocalizer<MerchantListController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="merchantListApi"></param>
        /// /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>

        public MerchantListController(IDistributedCache cache, 
                             IMerchantListApi merchantListApi, 
                             IStringLocalizer<MerchantListController> localizer,
                             IOperation operation,
                             ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Merchant List Controller",
                      "MerchantListController.cs", "MerchantListController"), CancellationToken.None);
            _cache = cache;
            _merchantListApi = merchantListApi;
            _localizer = localizer;
            _operation = operation;
           
        }

        #endregion Constructor 

        #region MerchantListController API call
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageinput"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        
        public async Task<IActionResult> GetMerchantList([FromBody] MerchantListInput pageinput)
        {
            try
            {
                int custId = Convert.ToInt32(pageinput.LIDValue);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "start of calling the Merchant List controller for CustomerID - " + custId +"  resultset",
                                                "MerchantListController.cs", "GetMerchantList"), CancellationToken.None);

                
                PaginationMerchant page = pageinput.Page;

                var key = UniqueCachingKey(pageinput);
                                
                if (!ModelState.IsValid)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "MerchantListController.cs",
                                                               "GetMerchantList"), CancellationToken.None);
                    return BadRequest(ModelState);
                }
                                
                //first check if the data is in cache..
                var data = _operation.RetrieveCache(key, new GenericPaginationResponse<Merchant>());

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "calling the service(GetMerchantListAsync) for Merchant List resultset",
                                                "MerchantListController.cs", "GetMerchantList"), CancellationToken.None);

                    var result = await _merchantListApi.GetMerchantListAsync(custId, page);

                    if (result.ErrorMessages.Count == 0)
                    {
                        if (result.Result != null && result.Result.TotalNumberOfRecords > 0)
                        {
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Merchant List resultset",
                                                "MerchantListController.cs", "GetMerchantList"), CancellationToken.None);
                            await _operation.AddCacheAsync(key, result.Result);
                            return Ok(result.Result);
                        }
                        else
                        {
                            var msg = this._localizer["NoDataFound"]?.Value;
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, msg + "while Fetching the Merchant List resultset",
                                                        "MerchantListController.cs", "GetMerchantList"), CancellationToken.None);
                            result.Result.ModelMessage = msg;
                            return Ok(result.Result);
                        }

                    
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Merchant List resultset from Cache key for CustomerID - " + key,
                                             "MerchantListController.cs", "GetMerchantList"), CancellationToken.None);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetMerchantList()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }



        }

        //Forming the Unique ChacheId
        private string UniqueCachingKey(MerchantListInput pageinput)
        {            
            int custId = Convert.ToInt32(pageinput.LIDValue);
            PaginationMerchant page = pageinput.Page;
            var key = _localizer["UniqueKeyMerchantList"] + "_" + custId;
            if (page.PageSize > 0)
            {
                key = key + "_PageSize_" + page.PageSize;
            }
            if (page.SkipRecordNumber > 0)
            {
                key = key + "_SkipRecord_" + page.SkipRecordNumber;
            }
            if (page.SortField != null)
            {
                key = key + "_" + page.SortField + "_" + page.SortFieldByAsc;
            }
            if (page.FilterMID != null)
            {
                key = key + "_FilterMID_" + page.FilterMID;
            }

            if (page.FilterName != null)
            {
                key = key + "_FilterName_" + page.FilterName;
            }

            if (page.FilterState != null)
            {
                key = key + "_FilterState_" + page.FilterState;
            }

            if (page.FilterStatusIndicator != null)
            {
                key = key + "_FilterStatusIndicator_" + page.FilterStatusIndicator;
            }

            if (page.FilterZipCode != null)
            {
                key = key + "_FilterZipCode_" + page.FilterZipCode;
            }


            return key;
        }
        #endregion
    }
}

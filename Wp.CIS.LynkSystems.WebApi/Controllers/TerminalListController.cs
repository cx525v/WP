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
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Wp.CIS.LynkSystems.Model.Pagination;
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
    [Route("api/TerminalList")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class TerminalListController : Controller
    {

        #region Constructor TerminalListController
        private readonly IDistributedCache _cache;
        private readonly ITerminalListApi _terminalListApi;
        private readonly IStringLocalizer<TerminalListController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="terminalListApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>

        public TerminalListController(IDistributedCache cache,
                             ITerminalListApi terminalListApi,
                             IStringLocalizer<TerminalListController> localizer,
                             IOperation operation,
                             ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List Controller",
                      "TerminalListController.cs", "TerminalListController"), CancellationToken.None);
            _cache = cache;
            _terminalListApi = terminalListApi;
            _localizer = localizer;
            _operation = operation;
            
        }
        #endregion Constructor 


        #region TerminalListController API call
        #region ---TODO Cleanup of below code once pagination from the clientside is done. 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>
        /// 
        [HttpGet("{merchantId}", Name = "GetTerminalList")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetTerminalList(int merchantId)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List Controller for MerchantID - " + merchantId,
                      "TerminalListController.cs", "TerminalListController"), CancellationToken.None);
                if (!ModelState.IsValid)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "TerminalListController.cs",
                                                               "GetTerminalList"), CancellationToken.None);
                    return BadRequest(ModelState);
                }

                //first check if the data is in cache..
                var data = _operation.RetrieveCache(merchantId.ToString(), new List<Terminal>());

                if (data == null)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Calling the Services(GetTerminalListAsync) for Terminal List for MerchantID - " + merchantId,
                                                 "TerminalListController.cs", "GetTerminalList"), CancellationToken.None);
                    var result = await _terminalListApi.GetTerminalListAsync(merchantId);
                    if (result.ErrorMessages.Count == 0)
                    {
                        if (result.Result != null && result.Result.Count > 0)
                        {
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Terminal List resultset for MerchantID - " + merchantId,
                                                "TerminalListController.cs", "GetTerminalList"), CancellationToken.None);
                            await _operation.AddCacheAsync(merchantId.ToString(), result.Result);

                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Added to Cache for the Terminal List resultset for MerchantID - " + merchantId,
                                                "TerminalListController.cs", "GetTerminalList"), CancellationToken.None);
                            return Ok(result.Result);
                        }
                        else
                        {
                            var msg = this._localizer?["NoDataFound"]?.Value;
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, msg + "while Fetching the Terminal List resultset for MerchantID - " + merchantId,
                                                        "TerminalListController.cs", "GetTerminalList"), CancellationToken.None);
                            return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                        }
                    }
                    else
                    {
                        var msg = this._localizer?["InternalServerError"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "TerminalListController.cs for MerchantID - " + merchantId,
                                                               "GetTerminalList"), CancellationToken.None);
                        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
                    }

                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Terminal List resultset from Cache for MerchantID - " + merchantId,
                                             "TerminalListController.cs", "GetTerminalList"), CancellationToken.None);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Error occured  for MerchantID - " + merchantId + " " + msg + ex.Message, "TerminalListController.cs",
                                                               "GetTerminalList"), CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
        #endregion ---TODO
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetTerminalList([FromBody] TerminalListInput pageinput)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List Controller",
                      "TerminalListController.cs", "TerminalListController_HttpPost"), CancellationToken.None);
                int merchantId = Convert.ToInt32(pageinput.LIDValue);
                PaginationTerminal page = pageinput.Page;
                var key = UniqueCachingKey(pageinput);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //first check if the data is in cache..
                var data = _operation.RetrieveCache(key, new GenericPaginationResponse<Terminal>());

                if (data == null)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Calling the Services(GetTerminalListAsync) the Terminal List for MerchantID - " + merchantId,
                                                 "TerminalListController.cs", "GetTerminalList_HttpPost"), CancellationToken.None);
                    var result = await _terminalListApi.GetTerminalListAsync(merchantId, page);

                    if (result.ErrorMessages.Count == 0)
                    {
                        if (result.Result != null && result.Result.TotalNumberOfRecords > 0)
                        {
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Terminal List resultset for MerchantID - " + merchantId,
                                                "TerminalListController.cs", "GetTerminalList_HttpPost"), CancellationToken.None);

                            await _operation.AddCacheAsync(key, result.Result);

                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Added to Cache for the Terminal List resultset for MerchantID - " + merchantId,
                                                "TerminalListController.cs", "GetTerminalList_HttpPost"), CancellationToken.None);
                            return Ok(result.Result);
                        }
                        else
                        {
                            var msg = this._localizer["NoDataFound"]?.Value;
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, msg + "while Fetching the Terminal List resultset for MerchantID - " + merchantId,
                                                        "TerminalListController.cs", "GetTerminalList_HttpPost"), CancellationToken.None);
                            result.Result.ModelMessage = msg;
                            return Ok(result.Result);
                        }
                    }
                    else
                    {
                        var msg = this._localizer?["InternalServerError"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Error occured for MerchantID - " + merchantId +", "+ msg, "TerminalListController.cs",
                                                               "GetTerminalList_HttpPost"), CancellationToken.None);
                        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
                    }

                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Terminal List resultset from Cache key for MerchantID - " + key,
                                             "TerminalListController.cs", "GetTerminalList_HttpPost"), CancellationToken.None);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetTerminalList()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        //Forming the Unique ChacheId
        private string UniqueCachingKey(TerminalListInput pageinput)
        {
            
            int merchantId = Convert.ToInt32(pageinput.LIDValue);
            PaginationTerminal page = pageinput.Page;
            var key = _localizer["UniqueKeyTerminalList"] + "_" + merchantId;
            if (page.PageSize > 0)
            {
                key = key + "_PageSize_" + page.PageSize;
            }
            if (page.SkipRecordNumber > 0)
            {
                key = key + "_SkipRecord_" + page.SkipRecordNumber;
            }
            if(page.SortField != null)
            {
                key = key + "_" + page.SortField + "_" + page.SortFieldByAsc;                
            }
            if(page.FilterDate != null)
            {
                key = key + "_FilterDate_" + page.FilterDate;
            }

            if (page.FilterSoftware != null)
            {
                key = key + "_FilterSoftware_" + page.FilterSoftware;
            }

            if (page.FilterStatus != null)
            {
                key = key + "_FilterStatus_" + page.FilterStatus;
            }

            if (page.FilterStatusEquipment != null)
            {
                key = key + "_FilterStatusEquipment_" + page.FilterStatusEquipment;
            }

            if (page.FilterTID != null)
            {
                key = key + "_FilterTID_" + page.FilterTID;
            }
            

            return key;
        }
        #endregion
    }
}
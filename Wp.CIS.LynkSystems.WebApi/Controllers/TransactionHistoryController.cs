using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/TransactionHistory")]
    public class TransactionHistoryController : Controller
    {
        #region Constructor
        private readonly IDistributedCache _cache;
        private readonly ITransactionHistoryApi _transactionHistoryApi;
        private readonly IStringLocalizer<TransactionHistoryController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="transactionHistoryApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public TransactionHistoryController(IDistributedCache cache,
                             ITransactionHistoryApi transactionHistoryApi,
                             IStringLocalizer<TransactionHistoryController> localizer,
                             IOperation operation,
                             ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Transaction History Controller",
                      "TransactionHistoryController.cs", "TransactionHistoryController"), CancellationToken.None);
            _cache = cache;
            _transactionHistoryApi = transactionHistoryApi;
            _localizer = localizer;
            _operation = operation;            
        }
        #endregion

        #region Transaction API Implementation
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageinput"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetTransactionHistory([FromBody] TransactionHistoryInput pageinput)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Web Api call for Transaction History Controller " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue, "TransactionHistoryController.cs", "GetTransactionHistory"),
                                           CancellationToken.None);
                
                string terminalId = pageinput.LIDValue;
                PaginationTransactionHistory page = pageinput.Page;

                var key = UniqueCachingKey(pageinput);
                
                if (!ModelState.IsValid)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "TransactionHistoryController.cs",
                                                               "GetTransactionHistory"), CancellationToken.None);
                    return BadRequest(ModelState);
                }

                //first check if the data is in cache..
                var data = _operation.RetrieveCache(key, new GenericPaginationResponse<TransactionHistory>());

                if (data == null)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "calling the GetTransactionHistoryAsync for resultset(Transaction Hostory)  " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue,
                                                    "TransactionHistoryController.cs", "GetTransactionHistory"), CancellationToken.None);
                    var result = await _transactionHistoryApi.GetTransactionHistoryAsync(terminalId, page);

                    if (result.ErrorMessages.Count == 0)
                    {

                        if (result.Result != null && result.Result.TotalNumberOfRecords > 0)
                        {
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "got the resultset for the GetTransactionHistoryAsync  " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue, "TransactionHistoryController.cs",
                                                                   "GetTransactionHistory"), CancellationToken.None);
                            await _operation.AddCacheAsync(key, result.Result);

                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Added to Cache for the Terminal List resultset  " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue,
                                                "TransactionHistoryController.cs", "GetTransactionHistory"), CancellationToken.None);
                            return Ok(result.Result);
                        }
                        else
                        {
                            var msg = this._localizer["NoDataFound"]?.Value;
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetTransactionHistoryAsync No Data Found  " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue, "TransactionHistoryController.cs",
                                                                   "GetTransactionHistory"), CancellationToken.None);
                            result.Result.ModelMessage = msg;
                            return Ok(result.Result);
                        }
                    }else
                    {
                        var msg = this._localizer?["InternalServerError"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Error Occured  for " + pageinput.lidTypeEnum.ToString() + ", Value - "
                                           + pageinput.LIDValue + " "+ msg, "TransactionHistoryController.cs",
                                                               "GetTransactionHistory"), CancellationToken.None);
                        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
                    }

                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetTransaction History Got resultset from Cache  " + key + ", Value - "
                                           + pageinput.LIDValue, "TransactionHistoryController.cs",
                                                               "GetTransactionHistory"), CancellationToken.None);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetTransactionHistory()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

        }

        //Forming the Unique ChacheId
        private string UniqueCachingKey(TransactionHistoryInput pageinput)
        {
            
            string terminalId = pageinput.LIDValue;
            PaginationTransactionHistory page = pageinput.Page;
            TransactionTypeEnum transactionType = page.TransactionType;

            var key = _localizer["UniqueKeyTerminalList"] + "_" + transactionType.ToString() + "_" + terminalId;
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
            if (page.FilterByAmount != null)
            {
                key = key + "_FilterByAmount_" + page.FilterByAmount;
            }

            if (page.FilterByDate != null)
            {
                key = key + "_FilterByDate_" + page.FilterByDate;
            }

            if (page.FilterByLast4 != null)
            {
                key = key + "_FilterByLast4_" + page.FilterByLast4;
            }

            if (page.FilterByNetworkCD != null)
            {
                key = key + "_FilterByNetworkCD_" + page.FilterByNetworkCD;
            }

            if (page.FilterByTranType != null)
            {
                key = key + "_FilterByTranType_" + page.FilterByTranType;
            }


            return key;
        }
        #endregion
    }
}

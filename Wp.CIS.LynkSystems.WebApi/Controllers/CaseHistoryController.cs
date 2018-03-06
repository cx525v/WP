using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// CaseHistoryController
    /// </summary>
    [Produces("application/json")]
    [Route("api/casehistory")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class CaseHistoryController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ICaseHistoryApi _caseHistory;
        private readonly IStringLocalizer<CaseHistoryController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="caseHistory"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public CaseHistoryController(IDistributedCache cache, 
                                     ICaseHistoryApi caseHistory,
                                     IStringLocalizer<CaseHistoryController> localizer,
                                     IOperation operation,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Case History Controller",
                      "CaseHistoryController.cs", "CaseHistoryController"), CancellationToken.None);

            _caseHistory = caseHistory;
            _cache = cache;
            _localizer = localizer;
            _operation = operation;

        }
        /// <param name="pageinput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] CaseHistoryInput pageinput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                LidTypeEnum LIDType = pageinput.lidTypeEnum;
                string LID = pageinput.LIDValue;
                string ExtraId = pageinput.ExtraID;
                PaginationCaseHistory page = pageinput.Page;
                var key = _localizer["UniqueKey"] + "_" + LID;

                if (page.SkipRecordNumber > 0)
                {
                    key = key + "_" + page.SkipRecordNumber;
                }

                var result = (await _caseHistory.GetCaseHistory(LIDType, LID, ExtraId, page));

                if (result.Result != null && result.Result.TotalNumberOfRecords > 0)
                {
                    await _operation.AddCacheAsync(key, result.Result);
                    return Ok(result.Result);
                }
                else
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    result.Result.ModelMessage = msg;
                    return Ok(result.Result);
                }
            }
            catch (Exception ex)
            {
               
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in CaseHistoryGet()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}

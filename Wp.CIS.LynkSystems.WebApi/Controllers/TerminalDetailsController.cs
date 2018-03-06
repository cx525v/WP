using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Worldpay.Logging.Providers.Log4Net.Facade;
using System.Threading;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/TerminalDetails")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class TerminalDetailsController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ITerminalDetailsApi _terminalDetailsApi;
        private readonly IStringLocalizer<TerminalDetailsController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="terminalDetailsApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public TerminalDetailsController(IDistributedCache cache, ITerminalDetailsApi terminalDetailsApi, IStringLocalizer<TerminalDetailsController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal Details Controller", "TerminalDetailsController.cs", "TerminalDetailsController"), CancellationToken.None);
            _terminalDetailsApi = terminalDetailsApi;
            _cache = cache;
            _localizer = localizer;
            _operation = operation;
        }

        /// <summary>
        /// GetTerminalDetails
        /// </summary>
        /// <param name="termNbr"></param>
        /// <returns></returns>
        [Route("GetTerminalDetails")]
        public async Task<IActionResult> GetTerminalDetails(int termNbr)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetTerminalDetails " + termNbr, "TerminalDetailsController.cs", "GetTerminalDetails"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "TerminalDetailsController.cs", "GetTerminalDetails"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "terminalDetails_" + termNbr.ToString();
                var data = _operation.RetrieveCache(key, new EAndPData());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = _terminalDetailsApi.GetTerminalDetails(termNbr);


                        data = result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "TerminalDetailsController.cs", "GetTerminalDetails"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetTerminalDetails Successful", "TerminalDetailsController.cs", "GetTerminalDetails"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {

                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetTerminalDetails()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="termNbr"></param>
        /// <returns></returns>       
        [Route("GetSettlementInfo")]
        public async Task<IActionResult> GetTerminalSettlementInfo(int termNbr)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetTerminalSettlementInfo " + termNbr, "TerminalDetailsController.cs", "GetTerminalDetails"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "TerminalDetailsController.cs", "GetTerminalSettlementInfo"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "getTerminalSettlementInfo_" + termNbr.ToString();
                var data = _operation.RetrieveCache(key, new TerminalSettlementInfo());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _terminalDetailsApi.GetTerminalSettlementInfo(termNbr);


                      data = result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "TerminalDetailsController.cs", "GetTerminalSettlementInfo"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetTerminalSettlementInfo Successful", "TerminalDetailsController.cs", "GetTerminalSettlementInfo"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetTerminalSettlementInfo()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
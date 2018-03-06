using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.WebApi.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/recentstatement")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class RecentStatementController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IRecentStatementApi _recentStatementApi;
        private readonly IStringLocalizer<RecentStatementController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="recentStatementApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public RecentStatementController(IDistributedCache cache,
            IRecentStatementApi recentStatementApi,
            IStringLocalizer<RecentStatementController> localizer,
            IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;

            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Recent Statement Controller", "RecentStatementController.cs", "TerminalDetailsController"), CancellationToken.None);

            this._cache = cache;

            this._recentStatementApi = recentStatementApi;

            this._localizer = localizer;

            this._operation = operation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantNbr"></param>
        /// <returns></returns>
        [HttpGet("{merchantNbr}")]
        public async Task<IActionResult> GetRecentStatement(string merchantNbr)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetRecentStatement " + merchantNbr, "RecentStatementController.cs", "GetRecentStatement"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "RecentStatementController.cs", "GetRecentStatement"), CancellationToken.None);
                return BadRequest(ModelState);
            }
            try
            {
                string key = "recentStatement_" + merchantNbr;
                var data = _operation.RetrieveCache(key, new List<Model.RecentStatement>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _recentStatementApi.GetRecentStatementAsync(merchantNbr);
                    if(result.Result != null && result.Result.Count > 0)
                    {
                        await _operation.AddCacheAsync(key, result.Result);
                        return Ok(result.Result);
                    }
                    else
                    {
                        var msg = this._localizer["NoDataFound"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + "GetRecentStatement Unsuccessful", "RecentStatementController.cs", "GetRecentStatement"), CancellationToken.None);
                        return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                    }
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetRecentStatement Successful", "RecentStatementController.cs", "GetRecentStatement"), CancellationToken.None);
                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetRecentStatement()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}

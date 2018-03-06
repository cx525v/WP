using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.WebApi.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/memoinfo")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class MemoInfoController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IMemoInfoApi _memoInfo;
        private readonly IStringLocalizer<MemoInfoController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="memoInfo"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public MemoInfoController(IDistributedCache cache, IMemoInfoApi memoInfo, IStringLocalizer<MemoInfoController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Memo Info Controller", "MemoInfoController.cs", "MemoInfoController"), CancellationToken.None);
            _memoInfo = memoInfo;
            _cache = cache;
            _localizer = localizer;
            _operation = operation;
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="LIDType"></param>
		/// <param name="LID"></param>
		/// <returns></returns>        
		[HttpGet]
        public async Task<IActionResult> Get(Model.Helper.LIDTypes LIDType, int LID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Memo Info Get " + LIDType + ", " + LID, "MemoInfoController.cs", "Get"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "MemoInfoController.cs", "Get"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "memoList_" + LID.ToString();
                var data = _operation.RetrieveCache(key, new MemoList());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _memoInfo.GetMemoResults(LIDType, LID);


                        data = result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "MemoInfoController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Memo Info Get Successful", "MemoInfoController.cs", "Get"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["GetMemoInfoErrorMessage"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in MemoInfoGet()", CancellationToken.None);

                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}

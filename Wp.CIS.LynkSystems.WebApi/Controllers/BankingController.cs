using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Banking")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class BankingController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IBankingApi _bankingApi;
        private readonly IStringLocalizer<BankingController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="bankingApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public BankingController(IDistributedCache cache, IBankingApi bankingApi, IStringLocalizer<BankingController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Banking Controller", "BankingController.cs", "BankingController"), CancellationToken.None);
            _bankingApi = bankingApi;
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
        [HttpGet("{LIDType}/{LID}")]
        [Route("GetBankingInfo")]
        public async Task<IActionResult> GetBankingInfo(Helper.LIDTypes LIDType, string LID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetBankingInfo " + LIDType + ", " + LID, "BankingController.cs", "GetBankingInfo"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "BankingController.cs", "GetBankingInfo"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "bankingInfo_" + "_" + LIDType + "_" + LID.ToString();
                var data = _operation.RetrieveCache(key, new List<Model.BankingInformation>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _bankingApi.GetBankingInfo(LIDType, LID);

                        data = (List<BankingInformation>)result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);
                   
                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "BankingController.cs", "GetBankingInfo"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetBankingInfo Successful", "BankingController.cs", "GetBankingInfo"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {
                
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetBankingInfo()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
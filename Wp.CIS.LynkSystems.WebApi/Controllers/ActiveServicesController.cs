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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// ActiveServices
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class ActiveServicesController : Controller
    {
        private readonly IDistributedCache _cache;

        private readonly IActiveServicesApi _activeServicesApi;
        private readonly IStringLocalizer<ActiveServicesController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="activeServicesApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public ActiveServicesController(IDistributedCache cache, IActiveServicesApi activeServicesApi
            , IStringLocalizer<ActiveServicesController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting ActiveServices Controller", "ActiveServicesController.cs", "ActiveServicesController"), CancellationToken.None);
            _cache = cache;
            _activeServicesApi = activeServicesApi;
            _localizer = localizer;
            _operation = operation;
        }

        /// <summary>
        /// GetActiveServices for a LID
        /// </summary>
        /// <param name="LIDType"></param>
        /// <param name="LID"></param>
        /// <returns></returns>
        [Route("{LIDtype}/{LID}")]
        public async Task<IActionResult> Get(int LIDType, int LID)
        {

            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetActiveServices " + LIDType + ", " + LID, "ActiveServicesController.cs", "GetActiveServices"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "ActiveServicesController.cs", "GetActiveServices"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "activeservices_" + LID.ToString();
                var data = _operation.RetrieveCache(key, new List<ActiveServices>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _activeServicesApi.GetActiveServices(LIDType, LID);

                        data = (List<ActiveServices>)result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);
                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "ActiveServicesController.cs", "GetActiveServices"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetActiveServices Successful", "ActiveServicesController.cs", "GetActiveServices"), CancellationToken.None);
                return Ok(data);
            }

            catch (Exception ex)
            {
                
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetActiveServices()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

    }
}

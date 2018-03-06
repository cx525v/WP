using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.WebApi.Common;
using System.Collections.Generic;
using System;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Facade;
using System.Threading;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// ParametersController
    /// </summary>
    [Produces("application/json")]
    [Route("api/Parameters")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class ParametersController : Controller
    {
        private readonly IDistributedCache _cache;

        private readonly IParametersApi _parametersApi;
        private readonly IStringLocalizer<ParametersController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// ParametersController
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="parametersApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public ParametersController(IDistributedCache cache, IParametersApi parametersApi
            , IStringLocalizer<ParametersController> localizer
            , IOperation operation
            , ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting ParametersController", "ParametersController.cs", "ParametersController"), CancellationToken.None);

            _cache = cache;
            _parametersApi = parametersApi;
            _localizer = localizer;
            _operation = operation;
        }

        /// <summary>
        /// GetParameters
        /// </summary>
        /// <param name="parameterId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetParameters")]
        public async Task<IActionResult> GetParameters(int? parameterId=null)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetParameters " + parameterId??"", "ParametersController.cs", "GetParameters"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "ParametersController.cs", "GetParameters"), CancellationToken.None);

                return BadRequest(ModelState);
            }

            try
            {
                string key = "parameters_" + Convert.ToString(parameterId);
                var data = _operation.RetrieveCache(key, new List<Model.Parameters>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _parametersApi.GetParameters(parameterId);

                    if (result.IsSuccess)
                    {
                        data = (List<Model.Parameters>)result.Result;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "ParametersController GetParameters Successful", "GetParametersController.cs", "GetParameters"), CancellationToken.None);

                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);
                    }
                    else
                    {
                        var msg = this._localizer?["GenericError"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "ParametersController.cs", "Get"), CancellationToken.None);

                        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
                    }
                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                //return BadRequest(ex);
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in ParametersController GetParameters( " + parameterId ?? ""+")", CancellationToken.None);

                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
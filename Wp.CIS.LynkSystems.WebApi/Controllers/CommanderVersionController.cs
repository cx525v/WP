using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/CommanderVersion")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]

    public class CommanderVersionController : Controller
    {

        private readonly IDistributedCache _cache;

        private readonly ICommanderVersionApi _commanderVersionApi;

        private readonly IStringLocalizer<CommanderVersionController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="commanderVersionApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public CommanderVersionController(IDistributedCache cache, ICommanderVersionApi commanderVersionApi, IStringLocalizer<CommanderVersionController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _cache = cache;
            _commanderVersionApi = commanderVersionApi;
            _localizer = localizer;
            _operation = operation;
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting CommanderVersionController",
                      "CommanderVersionController.cs", "CommanderVersionController"), CancellationToken.None);
        }

        /// <summary>
        /// GetBaseVersions
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetBaseVersions")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetBaseVersions()
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Web Api call for GetBaseVersions", "CommanderVersionController.cs", "GetBaseVersions"),
                                           CancellationToken.None);
                var response = await _commanderVersionApi.GetBaseVersions();
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["CommanderGetBaseVersionsErrorMsg"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetBaseVersions()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        /// <summary>
        /// GetVersions
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetVersions")]
        public async Task<IActionResult> GetVersions()
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Web Api call for GetVersions", "CommanderVersionController.cs", "GetVersions"),
                                           CancellationToken.None);
                return Ok(await _commanderVersionApi.GetVersions());
            }
            catch (System.Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetVersions()", CancellationToken.None);
                return BadRequest(ex);
            }
        }
        /// <summary>
        /// CreateVersion
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("CreateVersion")]
        public async Task<IActionResult> CreateVersion([FromBody]CommanderVersion version)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Web Api call for CreateVersion", "CommanderVersionController.cs", "CreateVersion"),
                                           CancellationToken.None);

                var response = await _commanderVersionApi.CreateVersion(version);
                if (response.IsSuccess)
                    return Ok(response.Result);
                else
                {
                    var msg = this._localizer?[response.ErrorMessages.FirstOrDefault().ToString()]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Web Api call for CreateVersion " + msg, "CommanderVersionController.cs", "CreateVersion"),
                                           CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.BadRequest, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = this._localizer?[CommanderVersionErrorCodes.CommanderCreateversionsErrorMsg.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in Create Version()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpDelete("{versionID}/{userName}")]
        public async Task<IActionResult> DeleteVersion(int versionID, string userName)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Web api call for DeleteVersion versionID: " + versionID + "userName: " + userName, "CommanderVersionController.cs", "DeleteVersion"), CancellationToken.None);
                var response = await _commanderVersionApi.DeleteVersion(versionID, userName);
                return Ok(response.Result);
            }
            catch (System.Exception ex)
            {
                var msg = this._localizer?[CommanderVersionErrorCodes.CommanderVersionDeleteErrorMsg.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in Delete Version()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
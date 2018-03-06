using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using System.Linq;
using Wp.CIS.LynkSystems.Model.Error;
using Worldpay.Logging.Providers.Log4Net.Facade;
using System.Threading;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// EPSPetroAuditController
    /// </summary>
    [Route("api/epspetroaudit")]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class EPSPetroAuditController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IEPSPetroAuditApi _epsPetroAuditApi;
        private readonly IStringLocalizer<EPSPetroAuditController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="cache"></param>
       /// <param name="epsPetroAuditApi"></param>
       /// <param name="localizer"></param>
       /// <param name="operation"></param>
       /// <param name="loggingFacade"></param>
        public EPSPetroAuditController(IDistributedCache cache, IEPSPetroAuditApi epsPetroAuditApi, IStringLocalizer<EPSPetroAuditController> localizer, IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSPetroAudit Controller", "EPS PetroAuditController.cs", "EPSPetroAuditController"), CancellationToken.None);
            this._cache = cache;
            this._epsPetroAuditApi = epsPetroAuditApi;
            this._localizer = localizer;
            _operation = operation;
        }

        /// <summary>
        /// GetEPSPetroAuditByVersion
        /// </summary>
        /// <param name="versionID"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("{versionID}/{startDate}/{endDate}")]
        public async Task<IActionResult> Get(int versionID, string startDate, string endDate)
        {
            try
            {
                var response = await _epsPetroAuditApi.GetEPSPetroAuditByVersion(versionID, startDate, endDate);
                if (response.IsSuccess)
                    return Ok(response.Result);
                else
                {
                    var msg = this._localizer?[response.ErrorMessages?.FirstOrDefault()?.ToString()]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "EPSPetroAuditController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.BadRequest, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = this._localizer?[EPSPetroAuditErrorCodes.GenericError.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetEPSPetroAuditByVersion()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        /// <summary>
        /// GetEPSPetroAuditDetails
        /// </summary>
        /// <param name="auditID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEPSPetroAuditDetails")]
        public async Task<IActionResult> GetEPSPetroAuditDetails(int auditID)
        {
            try
            {
                var response = await _epsPetroAuditApi.GetEPSPetroAuditDetails(auditID);
                if (response.IsSuccess)
                    return Ok(response.Result);
                else
                {
                    var msg = this._localizer?[response.ErrorMessages?.FirstOrDefault()?.ToString()]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "EPSPetroAuditController.cs", "GetEPSPetroAuditDetails"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.BadRequest, msg);
                }
            }
            catch (Exception ex)
            {
                var msg = this._localizer?[EPSPetroAuditErrorCodes.GenericError.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetEPSPetroAuditDetails()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
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
    [Route("api/EPSTable")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class EPSTableController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IEPSTableApi _epsTableApi;
        private readonly IStringLocalizer<EPSTableController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="epsTableApi"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public EPSTableController(IDistributedCache cache, IEPSTableApi epsTableApi, IStringLocalizer<EPSTableController> localizer
            , IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSTable Controller", "EPSTableController.cs", "EPSTableController"), CancellationToken.None);
            _epsTableApi = epsTableApi;
            _cache = cache;
            _localizer = localizer;
            _operation = operation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="versionID"></param>
        /// <returns></returns>
        [Route("GetAllPetroTablesByVersion")]
        public async Task<IActionResult> GetAllPetroTablesByVersion(int versionID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting GetAllPetroTablesByVersion " + versionID + ", ", "EPSTableController.cs", "GetAllPetroTablesByVersion"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSTableController.cs", "GetAllPetroTablesByVersion"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "epsTable_" + versionID;
                var data = _operation.RetrieveCache(key, new List<Model.PetroTable>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _epsTableApi.EPSGetAllPetroTablesByVersion(versionID);


                        data = (List<PetroTable>)result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "EPSTableController.cs", "GetAllPetroTablesByVersion"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetAllPetroTablesByVersion Successful", "EPSTableController.cs", "GetAllPetroTablesByVersion"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["EPSTableGetAllPetroTableErrorMsg"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetAllPetroTablesByVersion()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }


        /// <summary>
        /// EPSUpsertPetroTable
        /// </summary>
        /// <param name="petroTable"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EPSUpsertPetroTable")]
        public async Task<IActionResult> EPSUpsertPetroTable([FromBody] PetroTable petroTable)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSUpsertPetroTable HttpPost request", "EPSTableController.cs", "EPSUpsertPetroTable"), CancellationToken.None);
                var serviceResponse = await _epsTableApi.EPSUpsertPetroTable(petroTable);
                if (serviceResponse.Result == Model.Error.EPSTableErrorCodes.Succeeded)
                {
                    return Ok(true);
                }
                else
                {
                    var errorMessage = this._localizer[serviceResponse.Result.ToString()]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, errorMessage + "EPSUpsertPetroTable Unsuccessful", "EPSUpsertPetroTable.cs", "EPSUpsertPetroTable"), CancellationToken.None);
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                
                var msg = this._localizer?[Model.Error.EPSTableErrorCodes.EPSTableUpsertErrorMsg.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSUpsertPetroTable()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
    }
}
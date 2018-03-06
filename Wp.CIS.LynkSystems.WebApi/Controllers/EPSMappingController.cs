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
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.WebApi.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/epsmapping")]
    [Produces("application/json")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class EPSMappingController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IEPSMappingApi _epsmappingApi;
        private readonly IStringLocalizer<EPSMappingController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="cache"></param>
       /// <param name="epsmappingApi"></param>
       /// <param name="localizer"></param>
       /// <param name="operation"></param>
       /// <param name="loggingFacade"></param>
        public EPSMappingController(IDistributedCache cache, IEPSMappingApi epsmappingApi, IStringLocalizer<EPSMappingController> localizer, IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSMapping Controller", "EPSMappingController.cs", "EPSMappingController"), CancellationToken.None);
            this._cache = cache;
            this._epsmappingApi = epsmappingApi;
            this._localizer = localizer;
            this._operation = operation;
        }


        /// <summary>
        /// Retrieve all EPS table mapping
        /// </summary>
        /// <returns></returns>
        //[HttpGet("{versionID}")]
        //[ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        //public async Task<IActionResult> Get(int versionID)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    try
        //    {
        //        var response = await _epsmappingApi.RetrieveEPSMappingAsync(versionID);
        //        return Ok(response.Result);
        //    }
        //    catch (Exception)
        //    {
        //        var msg = this._localizer?["EPSMappingRetrieveErrorMsg"]?.Value;
        //        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
        //    }
        //}
        [HttpGet("{versionID}")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> Get(int versionID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPS Mapping Get " + versionID , "EPSMappingController.cs", "Get"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSMappingController.cs", "Get"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "epsMapping_" + versionID.ToString();
               var data = _operation.RetrieveCache(key, new List<Model.EPSMapping>());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _epsmappingApi.RetrieveEPSMappingAsync(versionID);


                        data = (List<EPSMapping>)result.Result;
                        //Now add data to cache..
                       await _operation.AddCacheAsync(key, data);
 
                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "EPSMappingController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetEPSMapping Successful", "EPSMappingController.cs", "Get"), CancellationToken.None);

                return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["EPSMappingRetrieveErrorMsg"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSMappingGet()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}


        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<IActionResult> Post([FromBody]EPSMapping mapping)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPS Mapping Post ", "EPSMappingController.cs", "Post"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSMappingController.cs", "Post"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                var serviceResponse = await _epsmappingApi.InsertEPSMappingAsync(mapping);
                if (serviceResponse.Result == Model.Error.EPSMappingErrorCodes.Succeeded)
                {
                    return Ok(true);
                }
                else
                {
                    var errorKey = serviceResponse.Result.ToString();
                    var errorMessage = this._localizer[errorKey]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, $"{errorMessage} User: {this.HttpContext?.Request?.Headers["UserName"]}  Post Unsuccessful", "EPSMappingController.cs", "Post"), CancellationToken.None);
                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                
                var msg = this._localizer?[Model.Error.EPSMappingErrorCodes.EPSMappingAllErrorMsg.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSMappingPost()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
      

        /// <summary>
        /// update EPS mapping
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]EPSMapping mapping)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPS Mapping Update ", "EPSMappingController.cs", "Update"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSMappingController.cs", "Update"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                var serviceResponse = await _epsmappingApi.UpdateEPSMappingAsync(mapping);
                if (serviceResponse.Result == Model.Error.EPSMappingErrorCodes.Succeeded)
                {
                    return Ok(true);
                }
                else
                {
                    var errorKey = serviceResponse.Result.ToString();
                    var errorMessage = this._localizer[errorKey]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, errorMessage + "Update Unsuccessful", "EPSMappingController.cs", "Update"), CancellationToken.None);

                    return BadRequest(errorMessage);
                }
            }
            catch (Exception ex)
            {
                
                var msg = this._localizer?["EPSMappingUpdateErrorMsg"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSMappingUpdate()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapping"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("copy")]
        public async Task<IActionResult> Copy([FromBody]EPSCopyMapping mapping)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPS Mapping Copy ", "EPSMappingController.cs", "Copy"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSMappingController.cs", "Copy"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _epsmappingApi.CopyEpsMappingAsync(mapping.FromVersionID, mapping.ToVersionID, mapping.UserName);
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["EPSMappingCopyErrorMsg"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSCopyMapping()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

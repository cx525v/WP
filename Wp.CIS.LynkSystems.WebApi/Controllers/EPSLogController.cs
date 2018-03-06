using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Error;
using Wp.CIS.LynkSystems.WebApi.Common;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// The API to get EPS log
    /// </summary>
    /// 

    [Produces("application/json")]
    [Route("api/epslog")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class EPSLogController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IEPSLogApi _epslogApi;
        private readonly IStringLocalizer<EPSLogController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="cache"></param>
      /// <param name="epslogApi"></param>
      /// <param name="localizer"></param>
      /// <param name="operation"></param>
      /// <param name="loggingFacade"></param>
        public EPSLogController(IDistributedCache cache, IEPSLogApi epslogApi, IStringLocalizer<EPSLogController> localizer, IOperation operation, ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPSLog Controller", "EPSLogController.cs", "EPSLogController"), CancellationToken.None);
            this._cache = cache;
            this._epslogApi = epslogApi;
            this._localizer = localizer;
            this._operation = operation;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="LidType"></param>
        /// <param name="Lid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string startDate, string endDate, int? LidType, string Lid)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting EPS Log Get " + startDate + ", " + endDate + "," + LidType +"," + Lid, "EPSLogController.cs", "Get"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "EPSLogController.cs", "Get"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _epslogApi.GetEPSLogAsync(startDate, endDate, LidType, Lid);

                if (!result.IsSuccess)
                {
                    var msg = this._localizer?[result.ErrorMessages?.FirstOrDefault()?.ToString()]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + "GetEPSLog Unsuccessful", "EPSLogController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.BadRequest, msg);
                }
                if (result.Result == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "EPSLogController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "GetEPSLog Successful", "EPSLogController.cs", "Get"), CancellationToken.None);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                
                var msg = this._localizer?[EPSLogErrorCodes.EPSLogError.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in EPSLogGet()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }
        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

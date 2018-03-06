using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Worldpay.CIS.Utilities;
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
    [Route("api/MerchantProfile")]

    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class MerchantProfileController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IMerchantProfileApi _mprofileApi;
        private readonly ILoggingFacade _loggingFacade;
        // private MyOptions  _settings;

        /// <summary>
        /// 
        /// </summary>

        /// <param name="cache"></param>
        /// <param name="mprofileApi"></param>
        /// <param name="loggingFacade"></param>
        public MerchantProfileController(IDistributedCache cache, IMerchantProfileApi mprofileApi, ILoggingFacade loggingFacade)
        {
            //settings = settings.Value;
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting MerchantProfileController", "MerchantProfileController.cs", "MerchantProfileController"), CancellationToken.None);

            _cache = cache;
            _mprofileApi = mprofileApi;
        }
       
        /// <summary>
        /// GET: api/MerchantProfile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }
        /// <summary>
        /// GET: api/MerchantProfile/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> Get(int id)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting MerchantProfileController id: " + id, "MerchantProfileController.cs", "Get"), CancellationToken.None);
            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "MerchantProfileController.cs", "Get"), CancellationToken.None);
                return BadRequest(ModelState);
            }
            try
            {
                //first check if the data is in cache..
                var data = new Operation(_cache).RetrieveCache(id.ToString(), new MerchantProfile());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    data = await _mprofileApi.GetMerchantProfileGeneralInfoAsync(id);
                    if (data != null)
                    {
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Get MerchantProfile Successful", "MerchantProfileController.cs", "Get"), CancellationToken.None);

                        //Now add data to cache..
                        await new Operation(_cache).AddCacheAsync(id.ToString(), data);
                    }
                }
                if (data == null)
                {
                    return Ok(new string[] { "value1", "Not Found" });
                }
                return Ok(data);
              

            }
            catch (Exception ex)
            {
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in MerchantProfileController Get(" + id +")", CancellationToken.None);

                return BadRequest(ex);
            }
        }

        
    }
}

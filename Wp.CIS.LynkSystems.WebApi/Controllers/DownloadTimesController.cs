using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Localization;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// This handles the operations for the download times
    /// </summary>
    [Produces("application/json")]
    [Route("api/DownloadTimes")]
    public class DownloadTimesController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IDownloadTimesApi _downloadTimesApi;
        private readonly IStringLocalizer<DownloadTimesController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="downloadTimesApi"></param>
        /// <param name="localizer"></param>
        public DownloadTimesController(IDistributedCache cache,
            IDownloadTimesApi downloadTimesApi,
            IStringLocalizer<DownloadTimesController> localizer)
        {
            this._cache = cache;

            this._downloadTimesApi = downloadTimesApi;

            this._localizer = localizer;
        }

        #endregion

        /// <summary>
        /// This retrieve all of the download times.
        /// </summary>
        /// <returns></returns>
        // GET: api/DownloadTimes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var dowmloadTimes = await this._downloadTimesApi
                                              .GetAllDownloadTimesAsync();

                response = this.Ok(dowmloadTimes);
            }
            catch (Exception)
            {
                var msg = this._localizer?["DownloadTimesErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

    }
}

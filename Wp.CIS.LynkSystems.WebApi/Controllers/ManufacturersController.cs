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
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/Manufacturers")]
    public class ManufacturersController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IManufacturersApi _manufacturersApi;
        private readonly IStringLocalizer<ManufacturersController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="manufacturersApi"></param>
        /// <param name="localizer"></param>
        public ManufacturersController(IDistributedCache cache,
            IManufacturersApi manufacturersApi,
            IStringLocalizer<ManufacturersController> localizer)
        {
            this._cache = cache;

            this._manufacturersApi = manufacturersApi;

            this._localizer = localizer;
        }

        #endregion

        #region Actions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Manufacturers
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var manufacturers = await this._manufacturersApi
                                              .GetAllManufacturersAsync();

                response = this.Ok(manufacturers);
            }
            catch (Exception)
            {
                var msg = this._localizer?["ManufacturersErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Microsoft.Extensions.Localization;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/MobileLookup")]
    public class MobileLookupController : Controller
    {

        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IMobileLookupApi _mobileLookupApi;
        private readonly IStringLocalizer<MobileLookupController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="mobileLookupApi"></param>
        /// <param name="localizer"></param>
        public MobileLookupController(IDistributedCache cache,
            IMobileLookupApi mobileLookupApi,
            IStringLocalizer<MobileLookupController> localizer)
        {
            this._cache = cache;

            this._mobileLookupApi = mobileLookupApi;

            this._localizer = localizer;
        }

        #endregion

        #region Public Actions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/MobileLookup
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var products = await this._mobileLookupApi
                                              .GetAllMobileLookupsAsync();

                products = products
                            .OrderBy(currentItem => currentItem.Description)
                            .ToList();

                response = this.Ok(products);
            }
            catch (Exception)
            {
                var msg = this._localizer?["MobileErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, "Error while retrieving the mobile lookup records");
            }

            return response;
        }

        #endregion

    }
}

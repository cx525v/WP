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
    //[Route("api/Brand")]
    public class BrandController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IBrandApi _brandApi;
        private readonly IStringLocalizer<BrandController> _localizer;

        #endregion


        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="brandApi"></param>
        /// <param name="localizer"></param>
        public BrandController(IDistributedCache cache,
            IBrandApi brandApi,
            IStringLocalizer<BrandController> localizer)
        {
            this._cache = cache;

            this._brandApi = brandApi;

            this._localizer = localizer;
        }

        #endregion

        #region Public Actions


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/Brand
        [Route("api/ProductBrands")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var installTypes = await this._brandApi
                                              .GetProductBrandsAsync();

                response = this.Ok(installTypes);
            }
            catch (Exception)
            {
                var msg = this._localizer?["RetrievingBrandsErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        #endregion
    }
}

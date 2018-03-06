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
    /// This handles the product types
    /// </summary>
    [Produces("application/json")]
    [Route("api/ProductTypes")]
    public class ProductTypesController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IProductTypesApi _productTypesApi;
        private readonly IStringLocalizer<ProductTypesController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="productTypesApi"></param>
        /// <param name="localizer"></param>
        public ProductTypesController(IDistributedCache cache,
            IProductTypesApi productTypesApi,
            IStringLocalizer<ProductTypesController> localizer)
        {
            this._cache = cache;

            this._productTypesApi = productTypesApi;

            this._localizer = localizer;
        }

        #endregion

        #region Public Actions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/ProductTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var productTypes = await this._productTypesApi
                                              .GetAllProductTypesAsync();

                response = this.Ok(productTypes);
            }
            catch (Exception)
            {
                var msg = this._localizer?["ProductTypesErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        #endregion
    }
}

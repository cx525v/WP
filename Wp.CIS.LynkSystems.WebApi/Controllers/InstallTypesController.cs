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
    [Route("api/InstallTypes")]
    public class InstallTypesController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IInstallTypesApi _installTypesApi;
        private readonly IStringLocalizer<InstallTypesController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="installTypesApi"></param>
        /// <param name="localizer"></param>
        public InstallTypesController(IDistributedCache cache,
            IInstallTypesApi installTypesApi,
            IStringLocalizer<InstallTypesController> localizer)
        {
            this._cache = cache;

            this._installTypesApi = installTypesApi;

            this._localizer = localizer;
        }

        #endregion

        #region Actions

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/InstallTypes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IActionResult response = null;

            try
            {
                var installTypes = await this._installTypesApi
                                              .GetAllInstallTypesAsync();

                response = this.Ok(installTypes);
            }
            catch (Exception)
            {
                var msg = this._localizer?["InstallTypeError"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        #endregion
    }
}

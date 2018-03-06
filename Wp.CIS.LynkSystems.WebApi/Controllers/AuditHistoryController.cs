using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wp.CIS.LynkSystems.Interfaces.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;
using Microsoft.Extensions.Localization;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/AuditHistory")]
    public class AuditHistoryController : Controller
    {
        #region Private Fields

        private readonly IDistributedCache _cache;
        private readonly IAuditHistoryApi _auditHistoryApi;
        private readonly IStringLocalizer<AuditHistoryController> _localizer;

        #endregion

        #region Public Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="auditHistoryApi"></param>
        /// <param name="localizer"></param>
        public AuditHistoryController(IDistributedCache cache,
            IAuditHistoryApi auditHistoryApi,
            IStringLocalizer<AuditHistoryController> localizer)
        {
            this._cache = cache;

            this._auditHistoryApi = auditHistoryApi;

            this._localizer = localizer;
        }

        #endregion

        #region Public Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lidType"></param>
        /// <param name="lid"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LidTypeEnum lidType, [FromQuery] int lid, [FromQuery] ActionTypeEnum actionType)
        {
            IActionResult response = null;

            try
            {
                var auditHistory = await this._auditHistoryApi
                                              .GetLatestAuditHistoryRecordAsync(lidType, lid, actionType);

                response = this.Ok(auditHistory);
            }
            catch (Exception)
            {
                var msg = this._localizer?["AuditHistoryError"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        #endregion


    }
}

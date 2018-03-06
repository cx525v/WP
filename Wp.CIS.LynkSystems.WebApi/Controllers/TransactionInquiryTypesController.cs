using System;
using System.Linq;
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

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// The API to get Transaction Inquiry
    /// </summary>
    ///
    [Produces("application/json")]
    [Route("api/TransactionInquiryTypes")]
    public class TransactionInquiryTypesController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ITransactionInquiryTypes _transinqtypes;

        /// <param name="cache"></param>
        /// <param name="transinqtypes"></param>
        public TransactionInquiryTypesController(IDistributedCache cache, ITransactionInquiryTypes transinqtypes)
        {
            _cache = cache;
            _transinqtypes = transinqtypes;
        }

        /// <summary>
        /// Gets all active transaction inquiry types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _transinqtypes.GetTransactionInquiryTypes());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving the transaction inquiry types");
            }
                        
        }

    }
}

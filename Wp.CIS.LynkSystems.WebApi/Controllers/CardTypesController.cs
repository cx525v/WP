using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{

    /// <summary>
    /// The API to get Card Types
    /// </summary>
    ///
    [Produces("application/json")]
    [Route("api/CardTypes")]
    public class CardTypesController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ICardTypes _cardtypes;

        /// <param name="cache"></param>
        /// <param name="cardtypes"></param>
        public CardTypesController(IDistributedCache cache, ICardTypes cardtypes)
        {
            _cache = cache;
            _cardtypes = cardtypes;
        }

        /// <summary>
        /// Gets all active card types
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
                return Ok(await _cardtypes.GetCISCardTypes());
            }
            catch (Exception)
            {
                return BadRequest("Error retrieving the transaction inquiry card types");
            }

        }
    }
}
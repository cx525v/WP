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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// The API to get Transaction Inquiry
    /// </summary>
    ///
    [Produces("application/json")]
    [Route("api/TransactionsInquiryTerminalInfo")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class TransactionsInquiryTerminalInfoController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ITransactionsInquiryTerminalInfoApi _transinqterminalinfo;

        private readonly IStringLocalizer<Resources.SharedResource> _localizer;      

        /// <param name="cache"></param>
        /// <param name="transinqterminalinfo"></param>
        /// <param name="localizer"></param>
        public TransactionsInquiryTerminalInfoController(IDistributedCache cache, ITransactionsInquiryTerminalInfoApi transinqterminalinfo,
            IStringLocalizer<Resources.SharedResource> localizer)
        {
            _cache = cache;
            _transinqterminalinfo = transinqterminalinfo;

            _localizer = localizer;
        }
        
        // GET: api/values
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult response = null;

            try
            {
                var data = new Operation(_cache).RetrieveCache(id.ToString(), new TransactionsInquiryGeneralInfo());

                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    data = await _transinqterminalinfo.TransactionInquiryGetTerminalInfo(id);
                    if (data != null)
                    {
                        //Now add data to cache..
                        await new Operation(_cache).AddCacheAsync(id.ToString(), data);
                    }
                }

                //var data = await _transinqterminalinfo.TransactionInquiryGetTerminalInfo(id);

                if(null == data)
                {
                    response =  NoContent();
                }
                else
                {
                    response =  Ok(data);
                }


            }
            catch (Exception)
            {
                var msg = this._localizer?["GetTerminalInfoError"]?.Value;
                response = this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
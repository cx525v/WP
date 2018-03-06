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

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// The API to get Transaction Inquiry
    /// </summary>
    ///
    [Produces("application/json")]
    [Route("api/TransactionsInquiryDetailsInfo")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class TransactionsInquiryDetailsInfoController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ITransactionsInquiryDetailsInfoApi _transinqApi;
        private readonly ITransactionsInquiryDetailsInfoTierApi _transinqTierApi;

        private readonly IStringLocalizer<TransactionsInquiryDetailsInfoController> _localizer;

        /// <param name="cache"></param>
        /// <param name="transinqApi"></param>
        /// <param name="transinqTierApi"></param>
        /// <param name="localizer"></param>
        public TransactionsInquiryDetailsInfoController(IDistributedCache cache, ITransactionsInquiryDetailsInfoApi transinqApi,
                                             ITransactionsInquiryDetailsInfoTierApi transinqTierApi,
                                             IStringLocalizer<TransactionsInquiryDetailsInfoController> localizer)
        {
            _cache = cache;
            _transinqApi    = transinqApi;
            _transinqTierApi = transinqTierApi;
            _localizer = localizer;
        }


        // GET: api/values
        /// <summary>
        /// 
        /// </summary>
        /// <param name="terminalgeneralinfo"></param>
        /// <param name="TerminalNbr"></param>
        /// <param name="SearchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        ///  <param name="CardNo"></param>
        ///  <param name="BatchNo"></param>
        ///  <param name="SkipRecords"></param>
        /// <param name="PageSize"></param> 
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> Get(TransactionsInquiryGeneralInfo terminalgeneralinfo, int TerminalNbr, int? SearchId, string startDate, string endDate, int? BatchNo, string CardNo, int SkipRecords, int PageSize)
        {
            bool _IsTopTier = terminalgeneralinfo.istoptier;

            var response = new GenericPaginationResponse<TransactionsInquiry>
            {
                SkipRecords = 0,
                PageSize = 0
            };

            //_IsTopTier = true;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string cachekey = TerminalNbr + "|" + SearchId + "|" + startDate + "|" + endDate + "|" + BatchNo + "|" + CardNo + "|"+ _IsTopTier;

                var data = new Operation(_cache).RetrieveCache(cachekey.ToString(), new TransactionsInquiry());

                if (data == null)
                {
                    if (!(_IsTopTier))
                    {
                        response = await _transinqApi.GetTransactionInquiryResults(terminalgeneralinfo, TerminalNbr, SearchId, startDate, endDate, BatchNo, CardNo, SkipRecords, PageSize);
                    }
                    else
                    {
                        response = await _transinqTierApi.GetTransactionInquiryResults(terminalgeneralinfo, TerminalNbr, SearchId, startDate, endDate, BatchNo, CardNo, SkipRecords, PageSize);
                    }

                    if (response != null)
                    {
                        //Now add data to cache..
                        await new Operation(_cache).AddCacheAsync(cachekey.ToString(), response);
                    }
                }

                return Ok(response);

                //if (!(_IsTopTier))
                //{
                //    return Ok(await _transinqApi.GetTransactionInquiryResults(terminalgeneralinfo, TerminalNbr, SearchId, startDate, endDate, BatchNo, CardNo, SkipRecords, PageSize));
                //}
                //else
                //{
                //    return Ok(await _transinqTierApi.GetTransactionInquiryResults(terminalgeneralinfo, TerminalNbr, SearchId, startDate, endDate, BatchNo, CardNo, SkipRecords, PageSize));
                //}


            }
            catch (Exception)
            {
                var msg = this._localizer?["TransactionSearchErrorMsg"]?.Value;
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.WebApiInput;

using Wp.CIS.LynkSystems.Services;

using Microsoft.Extensions.Options;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using System.Threading;
using Worldpay.Logging.Contracts.Enums;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/ContactList")]
    
    public class ContactListController : Controller
    {
        #region Constructor ContactListController
        
        private readonly IDistributedCache _cache;
        private readonly IContactListApi _contactList;
        private readonly IStringLocalizer<ContactListController> _localizer;
        private readonly IOperation _operation;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="contactList"></param>
        /// <param name="localizer"></param>
        /// <param name="operation"></param>
        /// <param name="loggingFacade"></param>
        public ContactListController(IDistributedCache cache,
                                     IContactListApi contactList,
                                     IStringLocalizer<ContactListController> localizer,
                                     IOperation operation,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting ContactList Controller Demographics Detail",
                      "ContactListController.cs", "ContactListController"), CancellationToken.None);
            _cache = cache;
            _contactList = contactList;
            _operation = operation;
            _localizer = localizer;            
        }


        #endregion

        #region Web API call methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageinput"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        public async Task<IActionResult> GetContactList([FromBody] ContactListInput pageinput )
        {
            
            try
            {
                LidTypeEnum LIDType = pageinput.lidTypeEnum;
                string LID = pageinput.LIDValue;
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "start calling the HttpPost method for the Contact List controller for input - " + LIDType +", Value - " + LID,
                                            "ContactListController.cs", "GetContactList"), CancellationToken.None);
                                                
                PaginationDemographics page = pageinput.Page;

                var key = UniqueCachingKey(pageinput);
                
                if (!ModelState.IsValid)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "ContactListController.cs",
                                                               "GetContactList"), CancellationToken.None);
                    return BadRequest(ModelState);
                }
                var data = _operation.RetrieveCache(key, new GenericPaginationResponse<Demographics>());

                if (data == null)
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "calling service for getting Contact List resultset from DB",
                                            "ContactListController.cs", "GetContactList"), CancellationToken.None); 

                    //since no data in cache, now get data from DB
                    var result = await _contactList.GetContactListAsync(LIDType, LID, page);

                    if(result.ErrorMessages.Count == 0)
                    {
                        if (result.Result != null && result.Result.TotalNumberOfRecords > 0)
                        {
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, " Fetched the Contact List resultset",
                                            "ContactListController.cs", "GetContactList"), CancellationToken.None);
                            //Now add data to cache..
                            await _operation.AddCacheAsync(key, data);
                            return Ok(result.Result);
                        }
                        else
                        {
                            var msg = this._localizer["NoDataFound"]?.Value;
                            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, msg + " while Fetching the Contact List resultset",
                                                    "ContactListController.cs", "GetContactList"), CancellationToken.None);
                            result.Result.ModelMessage = msg;                           
                            return Ok(result.Result);
                        }
                    }
                    else
                    {
                        var msg = this._localizer?["InternalServerError"]?.Value;
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "ContactListController.cs",
                                                               "GetContactList"), CancellationToken.None);
                        return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
                    }
                    
                }
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Contact List resultset from Cache key - " + key,
                                             "ContactListController.cs", "GetContactList"), CancellationToken.None);
                return Ok(data);
            }
            catch(Exception ex)
            {
                var msg = this._localizer?["InternalServerError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetContactList()", CancellationToken.None);
                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }

        }

        //Forming the Unique ChacheId
        private string UniqueCachingKey(ContactListInput pageinput)
        {
            
            int merchantId = Convert.ToInt32(pageinput.LIDValue);
            PaginationDemographics page = pageinput.Page;
            var key = _localizer["UniqueKey"] + "_" + merchantId;
            if (page.PageSize > 0)
            {
                key = key + "_PageSize_" + page.PageSize;
            }
            if (page.SkipRecordNumber > 0)
            {
                key = key + "_SkipRecord_" + page.SkipRecordNumber;
            }
            if (page.SortField != null)
            {
                key = key + "_" + page.SortField + "_" + page.SortFieldByAsc;
            }
            if (page.FilterContact != null)
            {
                key = key + "_FilterContact_" + page.FilterContact;
            }

            if (page.FilterLast4 != null)
            {
                key = key + "_FilterLast4_" + page.FilterLast4;
            }

            if (page.FilterRole != null)
            {
                key = key + "_FilterRole_" + page.FilterRole;
            }            

            return key;
        }
        #endregion

    }
}
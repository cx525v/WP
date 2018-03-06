using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace Wp.CIS.LynkSystems.WebApi.Controllers
{
    /// <summary>
    /// DashboardInfoController
    /// </summary>
    [Produces("application/json")]
    [Route("api/dashboardinfo")]
    [EnableCors("CorsPolicy")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 30, Location = ResponseCacheLocation.Client)]
    public class DashboardInfoController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IDashboardInfoApi _dashboardInfo;
        private readonly IStringLocalizer<DashboardInfoController> _localizer;
        private readonly IOperation _operation;
	    private readonly ILoggingFacade _loggingFacade;

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cache"></param>
		/// <param name="dashboardInfo"></param>
		/// <param name="localizer"></param>
		/// <param name="operation"></param>
		/// <param name="loggingFacade"></param>
		public DashboardInfoController(IDistributedCache cache, IDashboardInfoApi dashboardInfo, IStringLocalizer<DashboardInfoController> localizer
		    , IOperation operation, ILoggingFacade loggingFacade)
		{
			_loggingFacade = loggingFacade;
			_loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Dashboard Info Controller", "DashboardInfoController.cs", "DashboardInfoController"), CancellationToken.None);
			_dashboardInfo = dashboardInfo;
		    _cache = cache;
		    _localizer = localizer;
		    _operation = operation;
	    }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LIDType"></param>
        /// <param name="LID"></param>
        /// <returns></returns>        
        [HttpGet]
        public async Task<IActionResult> Get(Model.Helper.LIDTypes LIDType, int LID)
        {
	       await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Dashboard Info Get " + LIDType + ", " + LID, "DashboardInfoController.cs", "Get"), CancellationToken.None);

			if (!ModelState.IsValid)
            {
				await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "DashboardInfoController.cs", "Get"), CancellationToken.None);
				return BadRequest(ModelState);
            }

            try
            {
                string key = "dashboardInfo_" + LID.ToString();
                var data = _operation.RetrieveCache(key, new DashboardInfo());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _dashboardInfo.GetDashboardSearchResults(LIDType, LID);

                        data = result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
	                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg , "DashboardInfoController.cs", "Get"), CancellationToken.None);
					return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
	            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dashboard Info Get Successful", "DashboardInfoController.cs", "Get"), CancellationToken.None);

				return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["GetDashboardInfoErrorMessage"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetDashBoardInfoGet()", CancellationToken.None);

                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        /// <summary>
		/// GetDashBoardInfoSearch
		/// </summary>
		/// <param name="LIDType"></param>
		/// <param name="LID"></param>
		/// <returns></returns>       
        /// 
        [Route("GetDashBoardInfoSearch")]
        [HttpGet]
        public async Task<IActionResult> GetDashBoardInfoSearch(Model.Helper.LIDTypes LIDType, string LID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Dashboard Info GetDashBoardInfo LIDType : " + LIDType
                                                   + ", LID Value: " + LID, "DashboardInfoController.cs", "GetDashBoardInfo"), CancellationToken.None);

            if (!ModelState.IsValid)
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, ModelState.ToString(), "DashboardInfoController.cs", "GetDashBoardInfo"), CancellationToken.None);
                return BadRequest(ModelState);
            }

            try
            {
                string key = "dashboardInfo_" + "_" + LIDType + "_" + LID.ToString();
                var data = _operation.RetrieveCache(key, new DashboardInfo());

                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Before Database call of Service's method (GetDashboardSearchResultsPagination) for LIDType "
                                                + LIDType + " LID Value " + LID, "DashboardInfoController.cs", "Get"), CancellationToken.None);
                    //call the Services for Data Retrieval from Database.
                    var result = await _dashboardInfo.GetDashboardSearchResultsPagination(LIDType, Convert.ToInt32(LID));

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "After Database call of Service's method (GetDashboardSearchResultsPagination) for LIDType "
                                                + LIDType + " LID Value " + LID, "DashboardInfoController.cs", "Get"), CancellationToken.None);


                        data = result.Result;
                        //Now add data to cache..
                        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Adding into the Cache with Cache Key " + key,
                                                                     "DashboardInfoController.cs", "Get"), CancellationToken.None);
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg, "DashboardInfoController.cs", "Get"), CancellationToken.None);
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
                else
                {
                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Retrieved the Data from the Cache with Cache Key " + key,
                                                                     "DashboardInfoController.cs", "Get"), CancellationToken.None);

                    return Ok(data);
                }
                
            }
            catch (Exception ex)
            {
 

                var msg = this._localizer?["GetDashboardInfoErrorMessage"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetDashBoardInfoSearch()", CancellationToken.None );

                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
        }

        /// <summary>
        /// GetDashboardSearchResults
        /// </summary>
        /// <param name="lidType"></param>
        /// <param name="lid"></param>
        /// <returns></returns>  
        [Route("GetSearchPrimaryKeys")]
        [HttpGet]
        public async Task<IActionResult> GetSearchPrimaryKeys(LidTypeEnum lidType, string lid)
        {
	        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Dashboard Info GetSearchPrimaryKeys ", "DashboardInfoController.cs", "GetSearchPrimaryKeys"), CancellationToken.None);

			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IActionResult response = null;

            try
            {

                //since no data in cache, now get data from DB
                var data = await _dashboardInfo.GetDashboardSearchPrimaryKeys(lidType, lid, null);
                response = Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?[DashboardInfoErrorKeys.GetPkErrors.ToString()]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in GetSearchPrimaryKeys()", CancellationToken.None );
	            
				response = this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);
            }
	        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dashboard Info GetSearchPrimaryKeys Successful ", "DashboardInfoController.cs", "GetSearchPrimaryKeys"), CancellationToken.None);

			return response;

        }

        /// <summary>
        /// GetTerminalDetails
        /// </summary>
        /// <param name="termNbr"></param>
        /// <returns></returns>
        [Route("GetTerminalDetails")]
        [HttpGet]
        public async Task<IActionResult> GetTerminalDetails(int termNbr)
        {
	        await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Dashboard Info GetTerminalDetails ", "DashboardInfoController.cs", "GetTerminalDetails"), CancellationToken.None);

			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string key = "terminalDetails_" + termNbr.ToString();
                var data = _operation.RetrieveCache(key, new TerminalDetails());
                if (data == null)
                {
                    //since no data in cache, now get data from DB
                    var result = await _dashboardInfo.GetTerminalDetails(termNbr);

                        data = result.Result;
                        //Now add data to cache..
                        await _operation.AddCacheAsync(key, data);

                }
                if (data == null)
                {
                    var msg = this._localizer["NoDataFound"]?.Value;
                    return this.StatusCode((int)System.Net.HttpStatusCode.OK, msg);
                }
	            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dashboard Info GetTerminalDetails Successful ", "DashboardInfoController.cs", "GetTerminalDetails"), CancellationToken.None);

				return Ok(data);
            }
            catch (Exception ex)
            {
                var msg = this._localizer?["GenericError"]?.Value;
                await _loggingFacade.LogExceptionAsync(ex, this.HttpContext?.Request?.Headers["UserName"], LogLevels.Error, "Error in DashboardInfoGetTerminalDetails()", CancellationToken.None);

                return this.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, msg);

            }
        }
    }
}
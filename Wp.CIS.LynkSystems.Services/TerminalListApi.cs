using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TerminalList;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace Wp.CIS.LynkSystems.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class TerminalListApi : ITerminalListApi
    {

        #region TerminalList Constructor
        public ITerminalListRepository _terminalRepository;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="terminalRepository"></param>
        public TerminalListApi(IOptions<Settings> optionsAccessor, 
                               ITerminalListRepository terminalRepository,
                               ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List Controller",
                                    "TerminalListApi.cs", "TerminalListApi"), CancellationToken.None);
            _terminalRepository = terminalRepository;            
        }
        #endregion

        #region Service calls from the API
        #region ---TODO Cleanup of below code once pagination from the clientside is done.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TerminalID"></param>
        /// <returns></returns>
        public async Task<ApiResult<GenericPaginationResponse<Terminal>>> GetTerminalListAsync(int merchantID, PaginationTerminal Page)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List GetTerminalListAsync for MerchantID - " + merchantID,
                                    "TerminalListApi.cs", "GetTerminalListAsync"), CancellationToken.None);

            ApiResult<GenericPaginationResponse<Terminal>> response = new ApiResult<GenericPaginationResponse<Terminal>>();
            var errorkey = GlobalErrorCode.Succeeded;
            try
            {
                response.Result = await _terminalRepository.GetTerminalListAsync(merchantID, Page);

                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "fteched Terminal List resultset from DB for MerchantID - " + merchantID ,
                                    "TerminalListApi.cs", "GetTerminalListAsync"), CancellationToken.None);
                return response;
            }
            catch (Exception ex)
            {
                errorkey = GlobalErrorCode.InternalServerError;
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Error Occured  for MerchantID - " + merchantID + " " + errorkey.ToString() + ex.Message, "TerminalListApi.cs",
                                                               "GetTerminalListAsync"), CancellationToken.None);
                response.AddErrorMessage(errorkey.ToString());
                return response;
            }

        }
        #endregion ---TODO
        public async Task<ApiResult<ICollection<Terminal>>> GetTerminalListAsync(int merchantID)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List GetTerminalListAsync for MerchantID - " + merchantID,
                                    "TerminalListApi.cs", "GetTerminalListAsync"), CancellationToken.None);
            ApiResult<ICollection<Terminal>> response = new ApiResult<ICollection<Terminal>>();
           
            try
            {
                response.Result = await _terminalRepository.GetTerminalListAsync(merchantID);

                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "fteched Terminal List resultset from DB for MerchantID - " + merchantID,
                                    "TerminalListApi.cs", "GetTerminalListAsync"), CancellationToken.None);
            }
            catch (Exception)
            {
               
                throw;
               
            }
            return response;
        }

        #endregion
    }
}

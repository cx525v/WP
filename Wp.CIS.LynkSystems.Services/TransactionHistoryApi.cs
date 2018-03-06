using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TransactionHistory;
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
    public class TransactionHistoryApi : ITransactionHistoryApi
    {

        #region Constructor
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly ILoggingFacade _loggingFacade;
        public TransactionHistoryApi(IOptions<Settings> optionsAccessor,
                                         ITransactionHistoryRepository transactionHistoryRepository,
                                         ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Transaction History API Service",
                                    "TransactionHistoryApi.cs", "TransactionHistoryApi"), CancellationToken.None);
            _transactionHistoryRepository = transactionHistoryRepository;
        }
        #endregion
        #region TransactionHistoryApi API call method
        public async Task<ApiResult<GenericPaginationResponse<TransactionHistory>>> GetTransactionHistoryAsync(string terminalID, PaginationTransactionHistory Page)
        {
            await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Transaction History API Service for TerminalID - " + terminalID ,
                                    "TransactionHistoryApi.cs", "TransactionHistoryApi"), CancellationToken.None);
            ApiResult<GenericPaginationResponse<TransactionHistory>> response = new ApiResult<GenericPaginationResponse<TransactionHistory>>();
            
            try
            {
                response.Result = await _transactionHistoryRepository.GetTransactionHistoryAsync(terminalID, Page);

                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Fetched the Transaction History resultset from DB for TerminalID - " + terminalID,
                                    "TransactionHistoryApi.cs", "GetTransactionHistoryAsync"), CancellationToken.None);
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

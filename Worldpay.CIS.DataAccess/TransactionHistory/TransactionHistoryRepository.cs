using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Worldpay.CIS.DataAccess.TransactionHistory
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        #region Constructor TerminalListRepository
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly int maxRecordCount;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="connectionFactory"></param>
        public TransactionHistoryRepository(IOptions<DataContext> optionsAccessor,
                                     IDatabaseConnectionFactory connectionFactory,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Transaction History Repository",
                                    "TransactionHistoryApi.cs", "TransactionHistoryApi"), CancellationToken.None);
            this._connectionFactory = new BaseRepository(optionsAccessor.Value.TranHistoryConnectionString, optionsAccessor.Value.CommandTimeout);
            maxRecordCount = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }
        #endregion Constructor 

        #region TerminalListRepository database call for getting the data.
        /// <summary>
        /// Model - Terminal
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>

        public async Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> GetTransactionHistoryAsync(string terminalId, PaginationTransactionHistory page)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Transaction History GetTransactionHistoryAsync for TerminalID " + terminalId,
                                    "TransactionHistoryRepository.cs", "GetTransactionHistoryAsync()"), CancellationToken.None);
                var response = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>
                {
                    SkipRecords = page.SkipRecordNumber                    
                };
                return await this._connectionFactory.GetConnection(async c =>
               {
                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Open Dapper Connection of SQL server for Transaction History Repository for TerminalID " + terminalId,
                                    "TransactionHistoryRepository.cs", "GetTransactionHistoryAsync()"), CancellationToken.None);
                   var p = new DynamicParameters();
                   p.Add("TerminalID", terminalId, DbType.String);
                   if (!string.IsNullOrEmpty(page.SortField))
                   {
                       p.Add("SortField", page.SortField, DbType.String);
                       p.Add("SortOrder", page.SortFieldByAsc, DbType.Boolean);
                   }
                                                         
                   if (!string.IsNullOrEmpty(page.FilterByDate))
                   {
                       p.Add("FilterByDate", page.FilterByDate, DbType.String);
                   }
                   if (!string.IsNullOrEmpty(page.FilterByAmount))
                   {
                       p.Add("FilterByAmount", page.FilterByAmount, DbType.String);
                   }
                   if (!string.IsNullOrEmpty(page.FilterByLast4))
                   {
                       p.Add("FilterByLast4", page.FilterByLast4, DbType.String);
                   }
                   if (!string.IsNullOrEmpty(page.FilterByTranType))
                   {
                       p.Add("FilterByTranType", page.FilterByTranType, DbType.String);
                   }

                   if (!string.IsNullOrEmpty(page.FilterByDesc))
                   {
                       p.Add("FilterByDesc", page.FilterByDesc, DbType.String);
                   }

                   if (!string.IsNullOrEmpty(page.FilterByNetworkCD))
                   {
                       p.Add("FilterByNetworkCD", page.FilterByNetworkCD, DbType.String);
                   }

                   if (page.PageSize > 0)
                   {
                       p.Add("PageSize", page.PageSize, DbType.Int16);
                   }
                   if (page.SkipRecordNumber > 0)
                   {
                       p.Add("SkipRecords", page.SkipRecordNumber, DbType.Int16);
                   }

                   p.Add("MaximumRecsLimit", maxRecordCount, DbType.Int16);
                   p.Add("TotalRecordsCount", DbType.Int32, direction: ParameterDirection.Output);

                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dapper Connection parameterized Query for Transaction History Repository for TerminalID " + terminalId,
                                    "TransactionHistoryRepository.cs", "GetTransactionHistoryAsync()"), CancellationToken.None);

                   if (page.TransactionType == TransactionTypeEnum.Settled)
                   {
                       response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionHistory>(sql: "[CISPlus].[uspCISPlusGetTerminalTransactionsSettled]", param: p, commandType: CommandType.StoredProcedure);
                       response.TotalNumberOfRecords = p.Get<int>("TotalRecordsCount");
                   }
                   else if (page.TransactionType == TransactionTypeEnum.Acquired)
                   {
                       response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionHistory>(sql: "[CISPlus].[uspCISPlusGetTerminalTransactionsAcquired]", param: p, commandType: CommandType.StoredProcedure);
                       response.TotalNumberOfRecords = p.Get<int>("TotalRecordsCount");
                   }

                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Successful DB call for Transaction History Repository Query result for TerminalID " + terminalId,
                                    "TransactionHistoryRepository.cs", "GetTransactionHistoryAsync()"), CancellationToken.None);
                   return response;
               });
            }
            catch(Exception ex)
            {
                var msg = String.Format("{0}.GetTransactionHistoryAsync() experienced an exception (not a timeout) for TerminalID " + terminalId, GetType().FullName);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + ex.ToString() + ex.Message, "GetTransactionHistoryAsync.cs",
                                                               "GetTransactionHistoryAsync()"), CancellationToken.None);
                throw;
                    
            }
        }
        #endregion
    }
}

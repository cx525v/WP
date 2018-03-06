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

namespace Worldpay.CIS.DataAccess.TerminalList
{
    /// <summary>
    /// 
    /// </summary>
    public class TerminalListRepository : ITerminalListRepository
    {
        #region Constructor TerminalListRepository
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private int maxRecordCount;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="connectionFactory"></param>
        public TerminalListRepository(IOptions<DataContext> optionsAccessor,
                                     IDatabaseConnectionFactory connectionFactory,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List Repository",
                                    "TerminalListRepository.cs", "TerminalListRepository"), CancellationToken.None);
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
            maxRecordCount = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }
        #endregion Constructor 

        #region TerminalListRepository database call for getting the data.
        #region ---TODO Cleanup of below code once pagination from the clientside is done.
        public async Task<ICollection<Terminal>> GetTerminalListAsync(int merchantId)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List GetTerminalListAsync for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync"), CancellationToken.None);

                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("MerchantID", merchantId, DbType.Int32);

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dapper Connection parameterized Query for Terminal List Repository  for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync"), CancellationToken.None);

                    var terminalList = await c.QueryAsync<Terminal>(sql: "[CISPlus].[uspCISPlusGetTerminalListByMerchant]", param: p, commandType: CommandType.StoredProcedure);

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Successful DB call for Terminal List Repository Query result for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync"), CancellationToken.None);

                    return terminalList.ToList();
                });
            }
            catch (Exception ex)
            {
                var msg = String.Format("{0}.GetTerminalListAsync() experienced an exception (not a timeout) for MerchantID " + merchantId, GetType().FullName);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + ex.ToString() + ex.Message, "TerminalListRepository.cs",
                                                               "GetTerminalListAsync"), CancellationToken.None);
                throw new Exception(String.Format("{0}.GetTerminalListAsync() experienced an exception (not a timeout)", GetType().FullName), ex);
            }
        }
        #endregion ---TODO
        /// <summary>
        /// Model - Terminal
        /// </summary>
        /// <param name="merchantId"></param>
        /// <returns></returns>

        public async Task<GenericPaginationResponse<Terminal>> GetTerminalListAsync(int merchantId, PaginationTerminal page)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Terminal List GetTransactionHistoryAsync for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync"), CancellationToken.None);
                var response = new GenericPaginationResponse<Terminal>
                {
                    SkipRecords = page.SkipRecordNumber,
                    PageSize = page.PageSize
                };
                return await this._connectionFactory.GetConnection(async c =>
               {
                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Open Dapper Connection of SQL server for Terminal List Repository for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync"), CancellationToken.None);

                   var p = new DynamicParameters();
                   p.Add("MerchantID", merchantId, DbType.Int32);
                   if (page.SortField != null)
                   {
                       p.Add("SortField", page.SortField, DbType.String);
                       p.Add("SortByAsc", page.SortFieldByAsc, DbType.Boolean);
                   }
                   

                   if (page.FilterTID != null)
                   {
                       p.Add("FilterByTID", page.FilterTID, DbType.String);
                   }
                   if (page.FilterDate != null)
                   {
                       p.Add("FilterByDate", page.FilterDate, DbType.String);
                   }
                   if (page.FilterSoftware != null)
                   {
                       p.Add("FilterBySoftware", page.FilterSoftware, DbType.String);
                   }
                   if (page.FilterStatus != null)
                   {
                       p.Add("FilterByStatus", page.FilterStatus, DbType.String);
                   }
                   if (page.FilterStatusEquipment != null)
                   {
                       p.Add("FilterByEquipment", page.FilterStatusEquipment, DbType.String);
                   }

                   if (page.PageSize != 0)
                   {
                       p.Add("PageSize", page.PageSize, DbType.Int16);
                   }
                   if (page.SkipRecordNumber != 0)
                   {
                       p.Add("SkipRecordNumber", page.SkipRecordNumber, DbType.Int16);
                   }

                   p.Add("MaximumRecsLimit", maxRecordCount, DbType.Int16);
                   p.Add("TotalRecordsCount", DbType.Int32, direction: ParameterDirection.Output);

                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dapper Connection parameterized Query for Terminal List Repository for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync()"), CancellationToken.None);

                   response.ReturnedRecords = await c.QueryAsync<Terminal>(sql: "[CISPlus].[uspCISPlusGetTerminalListByMerchant]", param: p, commandType: CommandType.StoredProcedure);
                   response.TotalNumberOfRecords = p.Get<int>("TotalRecordsCount");

                   await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Successful DB call for Terminal List Repository Query result for MerchantID " + merchantId,
                                    "TerminalListRepository.cs", "GetTerminalListAsync()"), CancellationToken.None);
                   return response;
               });
            }
            catch(Exception ex)
            {
                var msg = String.Format("{0}.GetTerminalListAsync() experienced an exception (not a timeout) for MerchantID " + merchantId, GetType().FullName);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + ex.ToString() + ex.Message, "TerminalListRepository.cs",
                                                               "GetTerminalListAsync()"), CancellationToken.None);
                throw;
                   
            }
        }

        


        #endregion
    }
}

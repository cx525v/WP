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

namespace Worldpay.CIS.DataAccess.MerchantList
{
    /// <summary>
    /// 
    /// </summary>
    public class MerchantListRepository : IMerchantListRepository
    {

        #region Constructor MerchantListRepository
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private int maxRecordCount;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="connectionFactory"></param>
        public MerchantListRepository(IOptions<DataContext> optionsAccessor,
                                     IDatabaseConnectionFactory connectionFactory,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Merchant List Repository",
                                    "MerchantListRepository.cs", "MerchantListRepository"), CancellationToken.None);
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
            maxRecordCount = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }
        #endregion Constructor


        #region MerchantListRepository database call for getting the data.
        /// <summary>
        /// Model - Merchant
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>

        public async Task<GenericPaginationResponse<Merchant>> GetMerchantListAsync(int custId, PaginationMerchant page)
        {
            try
            {
                var response = new GenericPaginationResponse<Merchant>
                {
                    SkipRecords = page.SkipRecordNumber,
                    PageSize = page.PageSize
                };
                return await this._connectionFactory.GetConnection(async c => {
                  //  await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Open Dapper Connection of SQL server for Merchant List Repository for CustomerID - " + custId,
                  //                  "MerchantListRepository.cs", "GetMerchantListAsync()"), CancellationToken.None);
                    var p = new DynamicParameters();
                    p.Add("CustomerID", custId, DbType.String);
                    if (page.SortField != null)
                    {
                        p.Add("SortField", page.SortField, DbType.String);
                        p.Add("@SortByAsc", page.SortFieldByAsc, DbType.Boolean);
                    }                    

                    if (page.FilterMID != null)
                    {
                        p.Add("FilterByMID", page.FilterMID, DbType.String);
                    }
                    if (page.FilterName != null)
                    {
                        p.Add("FilterByName", page.FilterName, DbType.String);
                    }
                    if (page.FilterState != null)
                    {
                        p.Add("FilterByState", page.FilterState, DbType.String);
                    }
                    if (page.FilterStatusIndicator != null)
                    {
                        p.Add("FilterByStatusIndicator", page.FilterStatusIndicator, DbType.String);
                    }
                    if (page.FilterZipCode != null)
                    {
                        p.Add("FilterByZipCode", page.FilterZipCode, DbType.String);
                    }

                    if (page.PageSize != 0)
                    {
                        p.Add("PageSize", page.PageSize, DbType.Int16);
                    }
                    if (page.SkipRecordNumber != 0)
                    {
                        p.Add("SkipRecordNumber", page.SkipRecordNumber, DbType.Int16);
                    }
	                 p.Add("TotalRecordsCount", DbType.Int32, direction: ParameterDirection.Output);
					p.Add("MaximumRecsLimit", maxRecordCount, DbType.Int16);
                  

					await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dapper Connection parameterized Query for Merchant List Repository for CustomerID - " + custId,
                                    "MerchantListRepository.cs", "GetMerchantListAsync()"), CancellationToken.None);

                    response.ReturnedRecords = await c.QueryAsync<Merchant>(sql: "[CISPlus].[uspCISPlusGetMerchantListByCustomer]", param: p, commandType: CommandType.StoredProcedure);
                    response.TotalNumberOfRecords = p.Get<int>("TotalRecordsCount");

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Successful DB call for Merchant List Repository Query result for CustomerID - " + custId,
                                   "MerchantListRepository.cs", "GetMerchantListAsync()"), CancellationToken.None);

                    return response;
                });
            }

            catch(Exception ex)
            {
                var msg = String.Format("{0}.GetMerchantListAsync() experienced an exception (not a timeout) for CustomerId " + custId, GetType().FullName);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, msg + ex.ToString() + ex.Message, "MerchantListRepository.cs",
                                                               "GetMerchantListAsync()"), CancellationToken.None);
                throw; 
                   
            }

        }
        #endregion
    }
}

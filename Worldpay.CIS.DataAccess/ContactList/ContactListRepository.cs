using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Models;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace Worldpay.CIS.DataAccess.ContactList
{
    /// <summary>
    /// 
    /// </summary>
    public class ContactListRepository : IContactListRepository
    {

        #region Constructor ContactListRepository
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private int maxRecordCount;
        private readonly ILoggingFacade _loggingFacade;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAccessor"></param>
        /// <param name="connectionFactory"></param>
        public ContactListRepository(IOptions<DataContext> optionsAccessor,
                                     IDatabaseConnectionFactory connectionFactory,
                                     ILoggingFacade loggingFacade)
        {
            _loggingFacade = loggingFacade;
            _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Contact List Repository",
                                    "ContactListRepository.cs", "ContactListRepository"), CancellationToken.None);
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
            maxRecordCount = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }
        #endregion Constructor


        #region ContactListRepository database call for getting the data.
        /// <summary>
        /// Model - Merchant
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>

        public async Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.Demographics>> GetContactListAsync(LidTypeEnum LIDType, string LID, PaginationDemographics page)
        {
            try
            {
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Starting Contact List Repository GetContactListAsync for " + LIDType.ToString() + " Value - " + LID ,
                                    "ContactListRepository.cs", "GetContactListAsync"), CancellationToken.None);

                var response = new GenericPaginationResponse<Demographics>
                {
                    SkipRecords = page.SkipRecordNumber,
                    PageSize = page.PageSize
                };
                return await this._connectionFactory.GetConnection(async c => {

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Open Dapper Connection of SQL server for Contact List Repository  for " + LIDType.ToString() + " Value - " + LID,
                                    "ContactListRepository.cs", "GetContactListAsync"), CancellationToken.None);

                    var p = new DynamicParameters();
                    p.Add("iLIDType", LIDType, DbType.Int32);
                    p.Add("iLID", LID, DbType.String);
                    if(page.SortField != null )
                    {
                        p.Add("SortField", page.SortField, DbType.String);                        
                        p.Add("@SortByAsc", page.SortFieldByAsc, DbType.Boolean);                        
                    }
                    

                    if (page.FilterContact != null)
                    {
                        p.Add("FilterByValueForContact", page.FilterContact, DbType.String);
                    }
                    if (page.FilterLast4 != null)
                    {
                        p.Add("FilterByValueForSSN", page.FilterLast4, DbType.String);
                    }
                    if (page.FilterRole != null)
                    {
                        p.Add("FilterByValueForTitle", page.FilterRole, DbType.String);
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

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Dapper Connection parameterized Query for Contact List Repository for " + LIDType.ToString() + " Value - " + LID,
                                    "ContactListRepository.cs", "GetContactListAsync"), CancellationToken.None);

                    response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.Demographics>(sql: "[CISPlus].[uspCISPlusGetDemographicsByLIdType]", param: p, commandType: CommandType.StoredProcedure);
                    response.TotalNumberOfRecords = p.Get<int>("TotalRecordsCount");

                    await _loggingFacade.LogAsync(new LogEntry(LogLevels.Info, "Successful DB call for Contact List Repository Query result for " + LIDType.ToString() + " Value - " + LID,
                                   "ContactListRepository.cs", "GetContactListAsync"), CancellationToken.None);

                    return response;
                });
            }
            catch(Exception ex)
            {
                var msg = String.Format("{0}.GetContactListAsync() experienced an exception (not a timeout)", GetType().FullName);
                await _loggingFacade.LogAsync(new LogEntry(LogLevels.Error, "Error Occurred  for " + LIDType.ToString() + " Value - " + LID +" "+ msg + ex.ToString() + ex.Message, "ContactListRepository.cs",
                                                               "GetContactListAsync"), CancellationToken.None);
                throw;
                
            }

        }
        #endregion
    }
}

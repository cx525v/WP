using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Model;
using Worldpay.CIS.DataAccess.Connection;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using System;

namespace Worldpay.CIS.DataAccess.CaseHistory
{
    public class CaseHistoryRepository : ICaseHistoryRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private int maxrecordslimit;

        public CaseHistoryRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            
            this._connectionFactory = new BaseRepository(optionsAccessor.Value.StarV3ConnectionString, optionsAccessor.Value.CommandTimeout);
            maxrecordslimit = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }

        public async Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> GetCaseHistoryInfo(LidTypeEnum lidtype, string lid, string extraId, PaginationCaseHistory page)
        {
            try
            {
                var response = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>();


                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();

                    p.Add("LIDType", lidtype, DbType.Int32);
                    p.Add("LID", lid, DbType.String);
                    p.Add("MaximumRecsLimit", maxrecordslimit, DbType.String);

                    if (extraId != null)
                    {
                        p.Add("ExtraID", extraId, DbType.String);
                    }

                    p.Add("PageSize", page.PageSize, DbType.Int32);

                    p.Add("SkipRecords", page.SkipRecordNumber, DbType.Int32);

                    if (page.SortField != null)
                    {
                        p.Add("SortField", page.SortField, DbType.String);
                    }

                    p.Add("SortOrder", page.SortFieldByAsc, DbType.Boolean);

                    if (page.FilterCaseId != null)
                    {
                        p.Add("FilterCaseId", page.FilterCaseId, DbType.String);
                    }

                    if (page.FilterCaseDesc != null)
                    {
                        p.Add("FilterCaseDesc", page.FilterCaseDesc, DbType.String);
                    }

                    if (page.FilterOrgDeptName != null)
                    {
                        p.Add("FilterOrgDeptName", page.FilterOrgDeptName, DbType.String);
                    }

                    if (page.FilterCaseLevel != null)
                    {
                        p.Add("FilterCaseLevel", page.FilterCaseLevel, DbType.String);
                    }

                    if (true == page.FilterCreateDate.HasValue)
                    {
                        var beginCreateDate = new DateTime(page.FilterCreateDate.Value.Year, page.FilterCreateDate.Value.Month, page.FilterCreateDate.Value.Day);
                        var endCreateDate = new DateTime(page.FilterCreateDate.Value.Year, page.FilterCreateDate.Value.Month, page.FilterCreateDate.Value.Day, 23, 59, 59);
                        p.Add("FilterBeginCreateDate", beginCreateDate, DbType.Date);
                        p.Add("FilterEndCreateDate", endCreateDate, DbType.Date);
                    }

                    p.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);

                    response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.CaseHistory>(sql: "CISPlus.uspCISPlusGetCaseHistoryRecords", param: p,
                        commandType: CommandType.StoredProcedure);

                    response.TotalNumberOfRecords = p.Get<int>("TotalRecords");


                    return response;
                });
            }
            catch (System.Exception)
            {
                throw;
            }
        }





    }
}

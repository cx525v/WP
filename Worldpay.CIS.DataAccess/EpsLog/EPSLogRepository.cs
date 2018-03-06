using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsLog
{
    public class EPSLogRepository : IEPSLogRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public EPSLogRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<ICollection<EPSLog>> GetEPSLogAsync(string startDate, string endDate, int? LidType, string Lid)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("StartDate", startDate, DbType.DateTime);
                p.Add("EndDate", endDate, DbType.DateTime);
                p.Add("LIDType", LidType, DbType.Int32);
                p.Add("LID", Lid, DbType.Int32);
                var result = await GetValuesAsync(p);
                return await Task.FromResult(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public virtual async Task<ICollection<EPSLog>> GetValuesAsync(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<EPSLog>(sql: "CISPlus.uspEPSViewSoapLog", param: p, commandType: CommandType.StoredProcedure);

                return result.ToList();              
            });
        }

    }
}

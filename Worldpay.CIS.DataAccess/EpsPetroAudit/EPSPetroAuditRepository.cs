using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.EpsPetroAudit
{
    public class EPSPetroAuditRepository : IEPSPetroAuditRepository
    {

        private readonly IDatabaseConnectionFactory _connectionFactory;
        public EPSPetroAuditRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<ICollection<EPSPetroAudit>> GetEPSPetroAuditsAsync(int versionID, string startDate, string endDate)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("VersionID", versionID, DbType.Int32);
                p.Add("StartDate", startDate, DbType.DateTime);
                p.Add("EndDate", endDate, DbType.DateTime);
                var result = await GetEPSPetroAudit(p);
                return await Task.FromResult(result);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public virtual async Task<ICollection<EPSPetroAudit>> GetEPSPetroAudit(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<EPSPetroAudit>(sql: "CISPlus.uspCISPlusGeEPSPetroAuditByVersion", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }

        public async Task<ICollection<EPSPetroAuditDetails>> GetEPSPetroAuditDetailsAsync(int auditID)
        {
            try
            {              
                var p = new DynamicParameters();
                p.Add("AuditID", auditID, DbType.Int32);
                var result = await GetEPSPetroAuditDetails(p);
                return await Task.FromResult(result);
            }
            catch (System.Exception)
            {
                throw;
            }
        }


        public virtual async Task<ICollection<EPSPetroAuditDetails>> GetEPSPetroAuditDetails(DynamicParameters p)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var result = await c.QueryAsync<EPSPetroAuditDetails>(sql: "CISPlus.uspCISPlusGeEPSPetroAuditDetails", param: p, commandType: CommandType.StoredProcedure);
                return result.ToList();
            });
        }
    }
}

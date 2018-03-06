using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;

namespace Worldpay.CIS.DataAccess.RecentStatement
{
    public class RecentStatementRepository : IRecentStatementRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public RecentStatementRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            
            this._connectionFactory = new BaseRepository(optionsAccessor.Value.StaticReportsConnectionString, optionsAccessor.Value.CommandTimeout);
            
        }

        public async Task<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> GetRecentStatementAsync(string merchantNbr)
        {
            try
            {
                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("MerchantNbr", merchantNbr, DbType.String);
                    var recentStatementList = await c.QueryAsync<Wp.CIS.LynkSystems.Model.RecentStatement>(sql: "[dbo].[uspCISPlusGetRecentStatement]", param: p, commandType: CommandType.StoredProcedure);
                    return recentStatementList.ToList();
                });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
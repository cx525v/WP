using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo
{

    public class TerminalDetailsSettlementInfoRepository : ITerminalDetailsSettlementInfoRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public TerminalDetailsSettlementInfoRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {

            this._connectionFactory = new BaseRepository(optionsAccessor.Value.TranHistSumConnectionString, optionsAccessor.Value.CommandTimeout);

        }


        public Task<TerminalSettlementInfo> GetTerminalSettlementInfo(int termNbr)
        {
            try
            {
                return this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("TerminalNbr", termNbr, DbType.Int32);
                    var terminalSettlement = await c.QueryFirstOrDefaultAsync<TerminalSettlementInfo>(sql: "CISPlus.uspCISPlusGetTerminalSettlementInfo ", param: p, commandType: CommandType.StoredProcedure);
                    return terminalSettlement;
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

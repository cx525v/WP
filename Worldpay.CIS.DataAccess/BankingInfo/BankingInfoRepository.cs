using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.BankingInfo
{
    public class BankingInfoRepository : IBankingInfoRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public BankingInfoRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<ICollection<BankingInformation>> GetBankingInfo(Helper.LIDTypes LidType, string Lid)
        {
            try
            {
                return await this._connectionFactory.GetConnection(async c =>
                {
                    var p = new DynamicParameters();
                    p.Add("LIDType", LidType, DbType.Int32);
                    p.Add("LID", Lid, DbType.String);
                    var bankingInformation = await c.QueryAsync<BankingInformation>(sql: "CISPlus.uspCISPlusGetBankingInfo", param: p, commandType: CommandType.StoredProcedure);
                    return bankingInformation.ToList();
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

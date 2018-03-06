using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Model;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Worldpay.CIS.DataAccess.Connection;

namespace Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo
{
    public class TransactionsInqTerminalInfoRepository : ITransactionsInqTerminalInfoRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public TransactionsInqTerminalInfoRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;
        }

        public async Task<TransactionsInquiryGeneralInfo> GetTransactionInquiryTerminalInfo(int? terminalnbr, string terminalid)
        {
            return await this._connectionFactory.GetConnection(async c =>
            {
                var p = new DynamicParameters();
                p.Add("TerminalNbr", terminalnbr, DbType.Int32);
                p.Add("TerminalId", terminalid, DbType.String);
                var traninq = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo>(sql: "CISPlus.uspTransactionInquiryGetTerminalInfo", param: p, commandType: CommandType.StoredProcedure);
                return traninq.SingleOrDefault();
            });
        }






    }
}

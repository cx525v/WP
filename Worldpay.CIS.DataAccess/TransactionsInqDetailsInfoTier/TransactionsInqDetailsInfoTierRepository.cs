using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Model;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Worldpay.CIS.DataAccess.Connection;


namespace Worldpay.CIS.DataAccess.TransactionsInqDetailsInfoTier
{
    public class TransactionsInqDetailsInfoTierRepository : ITransactionsInqDetailsInfoTierRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        public TransactionsInqDetailsInfoTierRepository(IOptions<DataContext> optionsAccessor, IDatabaseConnectionFactory connectionFactory)
        {
            if (_connectionFactory == null)
                this._connectionFactory = new BaseRepository(optionsAccessor);
            else
                this._connectionFactory = connectionFactory;

        }

        public async Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryDetailResults(int terminalnbr, int? batchno, int customerid, string startdate, string enddate, int? searchId, int cardtype, int skiprecords, int pagesize)
        {
            var response = new GenericPaginationResponse<TransactionsInquiry>
            {
                SkipRecords = skiprecords,
                PageSize = pagesize
            };

            return await this._connectionFactory.GetConnection(async c =>
            {
                TransactionsInquiry dbinfo = new TransactionsInquiry();

                var p = new DynamicParameters();

                p.Add("TerminalNbr", terminalnbr, DbType.Int32);
                p.Add("BeginDate", startdate, DbType.DateTime);
                p.Add("EndDate", enddate, DbType.DateTime);
                p.Add("SearchId", searchId, DbType.Int32);
                p.Add("CardType", cardtype, DbType.Int32);
                p.Add("SkipRecords", skiprecords, DbType.Int32);
                p.Add("PageSize", pagesize, DbType.Int32);
                p.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
                response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(sql: "CISNOW_HistoryViewerTransactionsByBatch", param: p, commandType: CommandType.StoredProcedure);
                response.TotalNumberOfRecords = p.Get<int>("TotalRecords");
                return response;
            });
        }

        public async Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryBatchResults(int terminalnbr, int? batchno, int customerid, string startdate, string enddate, int? searchId, int cardtype, int skiprecords, int pagesize)
        {
            var response = new GenericPaginationResponse<TransactionsInquiry>
            {
                SkipRecords = skiprecords,
                PageSize = pagesize
            };

            return await this._connectionFactory.GetConnection(async c =>
            {
                TransactionsInquiry dbinfo = new TransactionsInquiry();

                var p = new DynamicParameters();

                p.Add("TerminalNbr", terminalnbr, DbType.Int32);
                p.Add("BatchNo", batchno, DbType.Int32);
                p.Add("CustomerID", customerid, DbType.Int32);
                p.Add("BeginDate", startdate, DbType.DateTime);
                p.Add("EndDate", enddate, DbType.DateTime);
                p.Add("SearchId", searchId, DbType.Int32);
                p.Add("CardType", cardtype, DbType.Int32);
                p.Add("SkipRecords", skiprecords, DbType.Int32);
                p.Add("PageSize", pagesize, DbType.Int32);
                p.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
                response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(sql: "CISNOW_HistoryViewerTransactionsByBatch", param: p, commandType: CommandType.StoredProcedure);
                response.TotalNumberOfRecords = p.Get<int>("TotalRecords");
                return response;
            });
        }


        public async Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryCardNoResults(int terminalnbr, string cardno, int customerid, string startdate, string enddate, int? searchId, int cardtype, int skiprecords, int pagesize)
        {
            var response = new GenericPaginationResponse<TransactionsInquiry>
            {
                SkipRecords = skiprecords,
                PageSize = pagesize
            };

            return await this._connectionFactory.GetConnection(async c =>
            {
                TransactionsInquiry dbinfo = new TransactionsInquiry();

                var p = new DynamicParameters();

                p.Add("LID", terminalnbr, DbType.Int32);
                p.Add("PAN", cardno.ToString(), DbType.Int32);
                p.Add("CustomerID", customerid, DbType.Int32);
                p.Add("BeginDate", startdate, DbType.DateTime);
                p.Add("EndDate", enddate, DbType.DateTime);
                p.Add("SearchId", searchId, DbType.Int32);
                p.Add("CardType", cardtype, DbType.Int32);
                p.Add("SkipRecords", skiprecords, DbType.Int32);
                p.Add("PageSize", pagesize, DbType.Int32);
                p.Add("TotalRecords", DbType.Int32, direction: ParameterDirection.Output);
                response.ReturnedRecords = await c.QueryAsync<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(sql: "CISNOW_HistoryViewerTransactionsByPan", param: p, commandType: CommandType.StoredProcedure);
                response.TotalNumberOfRecords = p.Get<int>("TotalRecords");
                return response;
            });
        }
    }
}

using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Services.DapperConnection;

namespace Wp.CIS.LynkSystems.Services
{
    public class TransactionInquiryTypesApi : ConnectionManager, ITransactionInquiryTypes
    {
        //ToDO: Need to load connection string from app settings
        public TransactionInquiryTypesApi(IOptions<Settings> optionsAccessor) : base(optionsAccessor.Value.CISStageConnectionString)
        {

        }
        public async Task<ICollection<TransactionInquiryTypes>> GetTransactionInquiryTypes()
        {
            return await WithConnection(async c =>
            {
                var transtype = await c.QueryAsync<Model.TransactionInquiryTypes>(sql: "[CISNOW].HistoryViewerTransactionsTypes", commandType: CommandType.StoredProcedure);
                return transtype.ToList();
            });
        }

    }
}

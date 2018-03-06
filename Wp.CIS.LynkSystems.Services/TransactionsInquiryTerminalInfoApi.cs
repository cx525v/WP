using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo;

namespace Wp.CIS.LynkSystems.Services
{
    public class TransactionsInquiryTerminalInfoApi: ITransactionsInquiryTerminalInfoApi
    {
        public static int recordcnt = 0;

        private const string termkey = "LK";

        private ITransactionsInqTerminalInfoRepository _transactionsinqterminalinfoRepository;

        public TransactionsInquiryTerminalInfoApi(IOptions<Settings> optionsAccessor, ITransactionsInqTerminalInfoRepository transactionsinqterminalinfoRepository)
        {
            _transactionsinqterminalinfoRepository = transactionsinqterminalinfoRepository;
        }
        
        //Call: CIS database server
        public async Task<TransactionsInquiryGeneralInfo> TransactionInquiryGetTerminalInfo(string id)
        {
            int? terminalNumber = null;
            string theTerminalId = null;

            //var isTerminalId = id.StartsWith("LK");

            if (id.StartsWith(termkey))
            { theTerminalId = id; }
            else
            { terminalNumber = int.Parse(id); }


            //if (true == isTerminalId)
            //{
            //    theTerminalId = id;
            //}
            //else
            //{
            //    terminalNumber = int.Parse(id);
            //}

            return await _transactionsinqterminalinfoRepository.GetTransactionInquiryTerminalInfo(terminalNumber, theTerminalId);
            
        }

        
    }
}

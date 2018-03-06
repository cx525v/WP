using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services.DapperConnection;

using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Data;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;

namespace Wp.CIS.LynkSystems.Services
{
    public class TransactionInquiryDetailsInfoApi : ITransactionsInquiryDetailsInfoApi
    {
       private ITransactionsInqDetailsInfoRepository _transactionsinqRepository;
     
       public TransactionInquiryDetailsInfoApi(IOptions<Settings> optionsAccessor, ITransactionsInqDetailsInfoRepository transactionsinqRepository)
       {
          _transactionsinqRepository = transactionsinqRepository;
       }


       public async Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryResults(TransactionsInquiryGeneralInfo terminalgeneralinfo, int terminalnbr, int? searchId, string startDate, string endDate, int? BatchNo, string CardNo, int skiprecords, int pagesize)
        {

            int _lid = terminalgeneralinfo.terminalNbr;
            int _lidType = terminalgeneralinfo.lidType;
            int _customerId = terminalgeneralinfo.customerId;
            int _cardtype = terminalgeneralinfo.selectedCardType;
           

            if (BatchNo > 0)
            {

                return await _transactionsinqRepository.GetTransactionInquiryBatchResults(terminalnbr, BatchNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);
            
            }
            else if (!(CardNo is null))
            {
                return await _transactionsinqRepository.GetTransactionInquiryCardNoResults(terminalnbr, CardNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);
            }
            else
            {
                return await _transactionsinqRepository.GetTransactionInquiryDetailResults(terminalnbr, BatchNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);
            }
            
        }
    }
}

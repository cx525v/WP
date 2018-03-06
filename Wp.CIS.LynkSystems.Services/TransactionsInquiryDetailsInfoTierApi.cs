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
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfoTier;

namespace Wp.CIS.LynkSystems.Services
{
    public class TransactionsInquiryDetailsTierApi : ITransactionsInquiryDetailsInfoTierApi
    {
    
        private ITransactionsInqDetailsInfoTierRepository _transactionsinqtierRepository;
        
        public TransactionsInquiryDetailsTierApi(IOptions<Settings> optionsAccessor, ITransactionsInqDetailsInfoTierRepository transactionsinqtierRepository)
        {
            _transactionsinqtierRepository = transactionsinqtierRepository;
        }
        
        public async Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryResults(TransactionsInquiryGeneralInfo terminalgeneralinfo, int terminalnbr, int? searchId, string startDate, string endDate, int? BatchNo, string CardNo, int skiprecords, int pagesize)
        {
            int _lid = terminalgeneralinfo.terminalNbr;
            int _lidType = terminalgeneralinfo.lidType;
            int _customerId = terminalgeneralinfo.customerId;
            int _cardtype = terminalgeneralinfo.selectedCardType;

            if (BatchNo > 0)
            {
                return await _transactionsinqtierRepository.GetTransactionInquiryBatchResults(terminalnbr, BatchNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);

            }
            else if (!(CardNo is null))
            {
                return await _transactionsinqtierRepository.GetTransactionInquiryCardNoResults(terminalnbr, CardNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);
            }
            else
            {
                return await _transactionsinqtierRepository.GetTransactionInquiryDetailResults(terminalnbr, BatchNo, _customerId, startDate, endDate, searchId, _cardtype, skiprecords, pagesize);
            }
        }
    }
}

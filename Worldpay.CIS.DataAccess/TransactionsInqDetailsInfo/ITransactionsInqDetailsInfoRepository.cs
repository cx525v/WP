﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo
{
    public interface ITransactionsInqDetailsInfoRepository
    {

        Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryDetailResults(int terminalnbr, int? BatchNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize);

        Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryBatchResults(int terminalnbr, int? BatchNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize);

        Task<GenericPaginationResponse<TransactionsInquiry>> GetTransactionInquiryCardNoResults(int terminalnbr, string CardNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize);
    }
}

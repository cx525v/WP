using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.TransactionsInqDetailsInfo
{
    public class MockTransactionsInqDetailsInfoRepository : ITransactionsInqDetailsInfoRepository
    {
        List<Wp.CIS.LynkSystems.Model.TransactionsInquiry> transinq;
        
        
        public bool FailGet { get; set; }

        public MockTransactionsInqDetailsInfoRepository()
        {
            transinq = new List<Wp.CIS.LynkSystems.Model.TransactionsInquiry>()
            {
                new Wp.CIS.LynkSystems.Model.TransactionsInquiry
                {
                    ARN    =   "24224435199101000064217",
                    ProcessingDate  =   "2015-07-17 00:00:00",
                    BatchNo =   717267,
                    SeqNo   =   68,
                    CardName    =   "Fleet One",
                    TranDesc    =   "Credit Sale",
                    TranDateTime    =   "2015-07-17 00:00:00",
                    AuthOnly    =    false,
                    AuthDateTime    =   "2015-07-17 10:30:42",
                    SettledAmount   =   15.00,
                    DispensedAmount =   0.00,
                    CashBackAmount  =   0.00,
                    SurchargeAmount =   0.00,
                    OriginalAuthAmount  =   15.00,
                    TotalAuthAmount =   15.00,
                    CompleteCode    =   0,
                    PAN =   "501486XHFOGNPUD0297",
                    ExpirationDate  =   null,
                    AuthNetwork =   54,
                    AuthCode    =   "749067",
                    AuthType    =   '0',
                    AuthRespCode    =   0,
                    AuthSourceCode  =   " ",
                    VisaTranRefNo   =   "000000000000000",
                    AVSResponseCode =   " ",
                    CommTypeDesc    =   "Frame",
                    CaptureTypeDesc =   "TERMINAL",
                    POSEntryModeDesc    =   "Swiped",
                    TieredQualificationType =   0,
                    CardQualificationType   =   0,
                    GrossTranAmount =   15.00,
                    GrossTranAmountPaid =   15.00,
                    PaidDate    =   "2015-07-21 00:00:00",
                    ACHOriginDate   =   "2015-07-17 00:00:00",
                    BankRTNbr   =   "121000248",
                    BankAcctType    =   1,
                    BankAcctNbr =   "654654",
                    TieredDesc  =   "Transaction Fees",
                    TranType    =   16,
                    TermID  =   "LK807325",
                    CardPaymentDate =   " ",
                    CardType    =   18,
                    ErrorCode   =   " ",
                    ReasonCD1   =   " ",
                    CardQualDesc    =   "Normal",
                    ExternalID  =   null,
                    NetworkRefNbr   =   " ",
                    RTCIndicator    = " ",
                    DecryptData =   " ",
                    CVMDescription  =   " ",
                    NetID  =   " ",
                    FullDeviceID    = " ",
                },
            };
        }

        public Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionsInquiry>> GetTransactionInquiryBatchResults(int terminalnbr, int? BatchNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionsInquiry>> GetTransactionInquiryCardNoResults(int terminalnbr, string CardNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize)
        {
            //throw new NotImplementedException();
            return await Task.FromResult(new GenericPaginationResponse<TransactionsInquiry>
            {
                ReturnedRecords = new List<TransactionsInquiry>()
                {
                    new TransactionsInquiry
                    {
                        ARN = "Test ARN Code",
                        CompleteCode = 555555
                    }
                },
                TotalNumberOfRecords = 1
            });
        }

        public Task<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionsInquiry>> GetTransactionInquiryDetailResults(int terminalnbr, int? BatchNo, int CustomerId, string startDate, string endDate, int? SearchId, int CardType, int SkipRecords, int PageSize)
        {
            throw new NotImplementedException();
        }

    }
}

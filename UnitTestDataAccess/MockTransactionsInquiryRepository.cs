using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model;

namespace UnitTestDataAccess
{
    public class MockTransactionsInquiryRepository
    {
        List<TransactionsInquiry> transinq;

        public bool FailGet { get; set; }

        public MockTransactionsInquiryRepository()
        {
            transinq = new List<TransactionsInquiry>
            {
              new TransactionsInquiry
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
                FullDeviceID    = " "
              },

              new TransactionsInquiry
              {
                ARN   =   "27777775199101000066223",
                ProcessingDate  =   "2015-07-17 00:00:00",
                BatchNo =   717267,
                SeqNo   =   938,
                CardName    =   "Pulse",
                TranDesc    =   "Denied Debit Transaction",
                TranDateTime    =   "2015-07-17 15:46:47",
                AuthOnly    =   true,
                AuthDateTime    =   "2015-07-17 15:46:47",
                SettledAmount   =   0.00,
                DispensedAmount =   0.00,
                CashBackAmount  =   0.00,
                SurchargeAmount =   0.00,
                OriginalAuthAmount  =   5.00,
                TotalAuthAmount =   5.00,
                CompleteCode    =   110,
                PAN =   "508148JIJTNO1235",
                ExpirationDate  =   null,
                AuthNetwork =   9,
                AuthCode    =   "30",
                AuthType    =   ' ',
                AuthRespCode    =   0,
                AuthSourceCode  =   null,
                VisaTranRefNo   =   "000000000",
                AVSResponseCode =   null,
                CommTypeDesc    =   "Frame",
                CaptureTypeDesc =   "TERMINAL",
                POSEntryModeDesc    =   null,
                TieredQualificationType =   0,
                CardQualificationType   =   602,
                GrossTranAmount =   0.00,
                GrossTranAmountPaid =   0.00,
                PaidDate    =   "2015-07-21 00:00:00",
                ACHOriginDate   =   "2015-07-17 00:00:00",
                BankRTNbr   =   "121000248",
                BankAcctType    =   1,
                BankAcctNbr =   "654654",
                TieredDesc  =   "Transaction Fees",
                TranType    =   51,
                TermID  =   "LK807325",
                CardPaymentDate =   "2015-07-21 00:00:00",
                CardType    =   39,
                ErrorCode   =   null,
                ReasonCD1   =   null,
                CardQualDesc    =  "Qual Petro Smtk",
                ExternalID  =   null,
                NetworkRefNbr   =   null,
                RTCIndicator    =   null,
                DecryptData =   null,
                CVMDescription  =   null,
                NetID  =   null,
                FullDeviceID    =   null,
              },
            };
        }

        public List<TransactionsInquiry> GetTransactionsInquiryAsync()
        {
            return transinq;
        }


    }
}

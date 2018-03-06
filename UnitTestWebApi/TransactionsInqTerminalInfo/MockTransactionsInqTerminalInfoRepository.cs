using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo;
using Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.TransactionsInqTerminalInfo
{
    public class MockTransactionsInqTerminalInfoRepository : ITransactionsInqTerminalInfoRepository 
    {
        List<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo> transinq;
        public bool FailGet { get; set; }

        public MockTransactionsInqTerminalInfoRepository()
        {
            transinq = new List<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo>()
            {
                new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo()
                {
                    customerId  =   548985,
                    customerNbr  =   "1000548985",
                    merchantId =   887770,
                    merchantNbr   =   "542929803206234",
                    address    =   "78 Eight St",
                    city    =   "Atlanta",
                    state    =   "GA",
                    zipcode    =    "54654",
                    sicCode    =   5651,
                    sicDesc   =   "CLOTHING (GENERAL)",
                    name =   "Regional/LLC II",
                    services  =   null,
                    statusDesc =   "Active",
                    businessDesc  =   "Payment",
                    lastDepositDate =   new DateTime(2015,06,25),
                    consolidation    =   null,
                    sensitivitylevel =   0,
                    istoptier  =   false,
                    terminalID =   "LK807325",
                    terminalNbr    =   10006144,
                    lidType    =   1,
                    selectedCardType    =   0,
                    selectedCostPlusType  =   0
                },
                 new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo() {
                    customerId  =   698809,
                    customerNbr  =   "1000698809",
                    merchantId =   10000773,
                    merchantNbr   =   "542929803217801",
                    address    =   "600 Morgan falls",
                    city    =   "Atlanta",
                    state    =   "GA",
                    zipcode    =    "30350",
                    sicCode    =   5947,
                    sicDesc   =   "GIFT, NOVELTY STORES",
                    name =   "AMEX Retail ACI",
                    services  =   null,
                    statusDesc =   "Active",
                    businessDesc  =   "Payment",
                    lastDepositDate =   new DateTime(2014,03,06),
                    consolidation    =   null,
                    sensitivitylevel =   0,
                    istoptier  =   false,
                    terminalID =   "LK100813",
                    terminalNbr    =   10007759,
                    lidType    =   1,
                    selectedCardType    =   0,
                    selectedCostPlusType  =   0

                },
                new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo() {
                    customerId  =   548984,
                    customerNbr  =   "1000548984",
                    merchantId =   887769,
                    merchantNbr   =   "542929803206226",
                    address    =   "7 Seventh St",
                    city    =   "Duluth",
                    state    =   "GA",
                    zipcode    =    "54654",
                    sicCode    =   5651,
                    sicDesc   =   "CLOTHING (GENERAL)",
                    name =   "Regional/Corp Public IiI",
                    services  =   null,
                    statusDesc =   "Active",
                    businessDesc  =   "Payment",
                    lastDepositDate =   new DateTime(2014,03,06),
                    consolidation    =   null,
                    sensitivitylevel =   0,
                    istoptier  =   false,
                    terminalID =   "LK807328",
                    terminalNbr    =   10006143,
                    lidType    =   1,
                    selectedCardType    =   0,
                    selectedCostPlusType  =   0

                },
            };
        }
        
        public List<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo> GetTerminalInfo()
        {
            return transinq;
        }
        
        Task<TransactionsInquiryGeneralInfo> ITransactionsInqTerminalInfoRepository.GetTransactionInquiryTerminalInfo(int? TerminalNbr, string TerminalId)
        {
            return Task.Run(() =>
            {

                return transinq.Find(io => io.terminalNbr == TerminalNbr);
            });

        }
    }
}

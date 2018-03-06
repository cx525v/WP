using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model;

namespace UnitTestDataAccess
{
    public class MockTransactionsInquiryTerminalInfoRepository
    {
        List<TransactionsInquiryGeneralInfo> generalinfo;

        public bool FailGet { get; set; }


        public MockTransactionsInquiryTerminalInfoRepository()
        {
            generalinfo = new List<TransactionsInquiryGeneralInfo>
            {
              new TransactionsInquiryGeneralInfo
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

            //  new TransactionsInquiryGeneralInfo
            //    {
            //    customerId  =   "24224435199101000064217",
            //    customerNbr  =   "2015-07-17 00:00:00",
            //    merchantId =   717267,
            //    merchantNbr   =   68,
            //    address    =   "Fleet One",
            //    city    =   "Credit Sale",
            //    state    =   "2015-07-17 00:00:00",
            //    zipcode    =    false,
            //    sicCode    =   "2015-07-17 10:30:42",
            //    sicDesc   =   15.00,
            //    name =   0.00,
            //    services  =   0.00,
            //    statusDesc =   0.00,
            //    businessDesc  =   15.00,
            //    lastDepositDate =   15.00,
            //    consolidation    =   0,
            //    sensitivitylevel =   "501486XHFOGNPUD0297",
            //    istoptier  =   null,
            //    terminalID =   54,
            //    terminalNbr    =   "749067",
            //    lidType    =   '0',
            //    selectedCardType    =   0,
            //    selectedCostPlusType  =   " "
            //   },
              
            };
        }

        public List<TransactionsInquiryGeneralInfo> GetTransactionsInquiryGeneralInfoAsync()
        {
            return generalinfo;
        }





    }
}

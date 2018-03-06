using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class TransactionsInquiryGeneralInfo
    {
        public int customerId { get; set; }
        public string customerNbr { get; set; }
        public int merchantId { get; set; }
        public string merchantNbr { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public int sicCode { get; set; }
        public string sicDesc { get; set; }
        public string name { get; set; }
        public string services { get; set; }
        public string statusDesc { get; set; }
        public string businessDesc { get; set; }
        public DateTime lastDepositDate { get; set; }
        public string consolidation { get; set; }
        public int sensitivitylevel { get; set; }
        public bool istoptier { get; set; }
        public string terminalID { get; set; }
        public int terminalNbr { get; set; }
        public int lidType { get; set; }
        public int selectedCardType { get; set; }
        public int selectedCostPlusType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class MerchantInfo
    {
        public int merchantId { get; set; }
        public int customerID { get; set; }
        public DateTime activationDt { get; set; }
        public int sicCode { get; set; }
        public int industryType { get; set; }
        public string merchantNbr { get; set; }
        public int acquiringBankId { get; set; }
        public int programType { get; set; }
        public int statusIndicator { get; set; }
        public string fnsNbr { get; set; }
        public int benefitType { get; set; }
        public int riskLevelID { get; set; }
        public int merchantType { get; set; }
        public string internetURL { get; set; }
        public DateTime deactivationDt { get; set; }
        public DateTime incrementalDt { get; set; }
        public DateTime thresholdDt { get; set; }
        public int brandID { get; set; }
        public string sicDesc { get; set; }
        public char? merchantClass { get; set; }
        public string riskLevel { get; set; }
        public string statDesc { get; set; }
        public string indTypeDesc { get; set; }
        public string mchName { get; set; }
        public string mchAddress { get; set; }
        public string mchCity { get; set; }
        public string mchState { get; set; }
        public string mchZipCode { get; set; }
        public string mchPhone { get; set; }
        public string mchContact { get; set; }
        public string acquiringBank { get; set; }
        public string benefitTypeDesc { get; set; }
        public string merchFedTaxID { get; set; }
    }
}

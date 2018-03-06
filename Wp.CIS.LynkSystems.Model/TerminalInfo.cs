using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class TerminalInfo
    {
        public int customerID { get; set; }
        public int merchantId { get; set; }
        public string terminalId { get; set; }
        public int businessType { get; set;}
        public int programType { get; set; }
        public DateTime activationDt { get; set; }
        public DateTime? downLoadDate { get; set; }
        public DateTime? sentToStratusDate { get; set; }
        public string cspStatusInterval { get; set; }
        public int commType { get; set; }
        public int statusIndicator { get; set; }
        public string cutOffTime { get; set; }
        public int captureType { get; set; }
        public int defaultNetwork { get; set; }
        public DateTime? deactivationDt { get; set; }
        public int originalSO { get; set; }
        public DateTime? installDate { get; set; }
        public DateTime? forcedBillingDate { get; set; }
        public DateTime incrementalDt { get; set; }
        public string busTypeDesc { get; set; }
        public int cashAdv { get; set; }
        public int checkSvc { get; set; }
        public int credit { get; set; }
        public int debit { get; set; }
        public int ebt { get; set; }
        public int fleet { get; set; }
        public int pob { get; set; }
        public int suppLA { get; set; }
        public string merchantName { get; set; }
        public string statDesc { get; set; }
    }
}

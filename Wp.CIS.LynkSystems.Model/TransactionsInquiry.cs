using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Model
{
    public class TransactionsInquiry
    {
        public string ARN { get; set; }
        public string ProcessingDate { get; set; }
        public int BatchNo { get; set; }
        public int SeqNo { get; set; }
        public string CardName { get; set; }
        public string TranDesc { get; set; }
        public string TranDateTime { get; set; }
        public bool AuthOnly { get; set; }
        public string AuthDateTime { get; set; }
        public double SettledAmount { get; set; }
        public double DispensedAmount { get; set; }
        public double CashBackAmount { get; set; }
        public double SurchargeAmount { get; set; }
        public double OriginalAuthAmount { get; set; }
        public double TotalAuthAmount { get; set; }
        public int CompleteCode { get; set; }
        public string PAN { get; set; }
        public string ExpirationDate { get; set; }
        public int AuthNetwork { get; set; }
        public string AuthCode { get; set; }
        public char? AuthType { get; set; }
        public int AuthRespCode { get; set; }
        public string AuthSourceCode { get; set; }
        public string VisaTranRefNo { get; set; }
        public string AVSResponseCode { get; set; }
        public string CommTypeDesc { get; set; }
        public string CaptureTypeDesc { get; set; }
        public string POSEntryModeDesc { get; set; }
        public int TieredQualificationType { get; set; }
        public int CardQualificationType { get; set; }
        public double GrossTranAmount { get; set; }
        public double GrossTranAmountPaid { get; set; }
        public string PaidDate { get; set; }
        public string ACHOriginDate { get; set; }
        public string BankRTNbr { get; set; }
        public int BankAcctType { get; set; }
        public string BankAcctNbr { get; set; }
        public string TieredDesc { get; set; }
        public int TranType { get; set; }
        public string TermID { get; set; }
        public string CardPaymentDate { get; set; }
        public int CardType { get; set; }
        public string ErrorCode { get; set; }
        public string ReasonCD1 { get; set; }
        public string CardQualDesc { get; set; }
        public string ExternalID { get; set; }
        public string NetworkRefNbr { get; set; }
        public string RTCIndicator { get; set; }
        public string DecryptData { get; set; }
        public string CVMDescription { get; set; }
        public string NetID { get; set; }
        public string FullDeviceID { get; set; }
        
    }
}

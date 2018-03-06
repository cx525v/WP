using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class DashboardInfo
    {
        public CustomerProfile CustProfile { get; set; }

        public ActiveServices ActvServices { get; set; }

        public Group GroupInfo { get; set; }

        public TerminalProfile TermProfile { get; set; }

        public TerminalInfo TermInfo { get; set; }

        public MerchantInfo MerchInfo { get; set; }

        public List<Demographics> DemographicsInfo { get; set; }

        public List<Demographics> DemographicsInfoCust { get; set; }

        public List<Demographics> DemographicsInfoMerch { get; set; }

        public List<Demographics> DemographicsInfoTerm { get; set; }

        public List<Merchant> MerchantsList { get; set; }

        public List<CaseHistory> CaseHistorysList { get; set; }
        public int TotalNumberOfCaseHistoryRecords { get; set; }
        public int TotalMerchantRecords { get; set; }
        public int TotalDemographicsRecords { get; set; }
        public int TotalDemographicsInfoCustRecords { get; set; }
        public int TotalDemographicsInfoMerchRecords { get; set; }

    }
}

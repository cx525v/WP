using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Model
{
    [Serializable]
    public class MerchantProfile
    {
        public int MerchantId { get; set; }
        public int CardType { get; set; }
        public int AccountNbr { get; set; }
        public double DiscountRate { get; set; }

        public int CustomerID { get; set; }
        public DateTime ActivationDt { get; set; }
        public int SicCode { get; set; }
        public int IndustryType { get; set; }
        public string MerchantNbr { get; set; }
        public int AcquiringBankId { get; set; }
        public int ProgramType { get; set; }
        public int StatusIndicator { get; set; }
        public int FNSNbr { get; set; }
        public int BenefitType { get; set; }
        public int RiskLevelID { get; set; }
        public int MerchantType { get; set; }
        public string InternetURL { get; set; }
    public DateTime DeactivationDt { get; set; }
    public DateTime IncrementalDt { get; set; }
    public DateTime ThresholdDt { get; set; }

    public int BrandID { get; set; }
    public string SicCodeDesc { get; set; }
    public int AcqBankNameAddressID { get; set; }
    public string AcqBankDesc { get; set; }
    public string IndustryTypeDesc { get; set; }
    public string ProgramTypeDesc { get; set; }
    public int DescriptorCd { get; set; }
    public int VisaIndustryType { get; set; }
    public int HighRiskInd { get; set; }
    public string BeneTypeDesc { get; set; }
    public string  StatDesc { get; set; }
    public string MerchantClass { get; set; }
    public int RiskLevel { get; set; }
    public string RiskLevelDesc { get; set; }
        public int ChkAcctNbr { get; set; }
        public int FederalTaxID { get; set; }
        public int StateTaxCode { get; set; }
        public string MerchTypeDesc { get; set; }
        public int SubIndGrpID{ get; set; }
    public string SubIndGrpDesc { get; set; }
    public string MVV1 { get; set; }
    public string MVV2 { get; set; }
    public int StoreNbr { get; set; }
    public int IRSVerificationStatus { get; set; }

    //            OldMerchantNbr: ko.observable(),
    //            //Status:ko.observable(),
    //            ajaxLoaded: ko.observable(false),
    //            IndustryTypes: ko.observableArray(), selectedIndustryType: ko.observable(),
    //            BenefitTypes: ko.observableArray(), selectedBenefitType: ko.observable(),
    //            SicCodes: ko.observableArray(), selectedSicCode: ko.observable(),
    //            MerchantId: ko.observable(),
    //            ActivationDt: ko.observable(), CustomerID: ko.observable(), ActivationDt: ko.observable(), SicCode: ko.observable(), IndustryType: ko.observable(),
    //            MerchantNbr: ko.observable(), AcquiringBankId: ko.observable(), ProgramType: ko.observable(), StatusIndicator: ko.observable(),
    //            FNSNbr: ko.observable(), BenefitType: ko.observable(), RiskLevelID: ko.observable(), MerchantType: ko.observable(), InternetURL: ko.observable(),
    //            DeactivationDt: ko.observable(), IncrementalDt: ko.observable(), ThresholdDt: ko.observable(), BrandID: ko.observable(), SicCodeDesc: ko.observable(),
    //            AcqBankNameAddressID: ko.observable(), AcqBankDesc: ko.observable(), IndustryTypeDesc: ko.observable(), ProgramTypeDesc: ko.observable(),
    //            DescriptorCd: ko.observable(), VisaIndustryType: ko.observable(), HighRiskInd: ko.observable(), BeneTypeDesc: ko.observable(),
    //            StatDesc: ko.observable(), MerchantClass: ko.observable(), RiskLevel: ko.observable(), RiskLevelDesc: ko.observable(), ChkAcctNbr: ko.observable(),
    //            FederalTaxID: ko.observable(), StateTaxCode: ko.observable(), MerchTypeDesc: ko.observable(), SubIndGrpID: ko.observable(),
    //            SubIndGrpDesc: ko.observable(), MVV1: ko.observable(), MVV2: ko.observable(), StoreNbr: ko.observable(), IRSVerificationStatus: ko.observable(),
}
}

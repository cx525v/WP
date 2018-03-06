using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess;
using Worldpay.CIS.DataAccess.MerchantProfile;

namespace CIS.WebApi.UnitTests.MerchantProfile
{
    public class MockMerchantRepository : IMerchantProfileRepository
    {
       

        List<Wp.CIS.LynkSystems.Model.MerchantProfile> merchants;

        public bool FailGet { get; set; }

        public MockMerchantRepository()
        {
            merchants = new List<Wp.CIS.LynkSystems.Model.MerchantProfile>() {
            new Wp.CIS.LynkSystems.Model.MerchantProfile(){
                  MerchantId  = 191807 ,CardType  = 0,AccountNbr  = 0,DiscountRate  = 0.0,CustomerID  = 17070,
             ActivationDt=new DateTime(2000,08,08),SicCode  = 5921,IndustryType  = 1,MerchantNbr = "007" ,AcquiringBankId  = 1,
             ProgramType  = 14,StatusIndicator  = 7,FNSNbr  = 0,BenefitType  = 0,RiskLevelID  = 1,MerchantType  = 1,
             InternetURL = " " ,DeactivationDt=new DateTime(2001,12,12) ,IncrementalDt =new DateTime(2000,08,09),ThresholdDt=DateTime.Now ,
             BrandID  = 0,SicCodeDesc = " ",AcqBankNameAddressID  = 0,AcqBankDesc =" ",IndustryTypeDesc=" " ,
             ProgramTypeDesc =" " ,DescriptorCd  = 0,VisaIndustryType  = 0,HighRiskInd  = 0,BeneTypeDesc =" " ,
            StatDesc =" ",MerchantClass =" ",RiskLevel = 0 ,RiskLevelDesc =" ",ChkAcctNbr  = 0,
             FederalTaxID = 0 ,StateTaxCode  = 0,MerchTypeDesc= " " ,SubIndGrpID = 0,
             SubIndGrpDesc =" ",MVV1 =" ",MVV2 =" ",StoreNbr = 0 ,IRSVerificationStatus = 0
            }, 
            new Wp.CIS.LynkSystems.Model.MerchantProfile()
            {
                MerchantId  = 445709 ,CardType  = 0,AccountNbr  = 0,DiscountRate  = 0.0,CustomerID  = 299794,
                ActivationDt=new DateTime(2003,09,02),SicCode  = 4214,IndustryType  = 1,MerchantNbr = "542929800750721" ,AcquiringBankId  = 12,
                ProgramType  = 13,StatusIndicator  = 1,FNSNbr  = 0,BenefitType  = 0,RiskLevelID  = 4,MerchantType  = 1,
                InternetURL = " " ,DeactivationDt=DateTime.Now ,IncrementalDt =new DateTime(2003,09,30),ThresholdDt=DateTime.Now ,
                BrandID  = 1,SicCodeDesc = " ",AcqBankNameAddressID  = 0,AcqBankDesc =" ",IndustryTypeDesc=" " ,
                ProgramTypeDesc =" " ,DescriptorCd  = 0,VisaIndustryType  = 0,HighRiskInd  = 0,BeneTypeDesc =" " ,
                StatDesc =" ",MerchantClass =" ",RiskLevel = 0 ,RiskLevelDesc =" ",ChkAcctNbr  = 0,
                FederalTaxID = 0 ,StateTaxCode  = 0,MerchTypeDesc= " " ,SubIndGrpID = 0,
                SubIndGrpDesc =" ",MVV1 =" ",MVV2 =" ",StoreNbr = 0 ,IRSVerificationStatus = 0
            },
                new Wp.CIS.LynkSystems.Model.MerchantProfile()
                {
                MerchantId  = 445710 ,CardType  = 0,AccountNbr  = 0,DiscountRate  = 0.0,CustomerID  = 300025,
                ActivationDt=new DateTime(2003,09,02),SicCode  = 6011,IndustryType  = 1,MerchantNbr = "999999000175250" ,AcquiringBankId  = 1,
                ProgramType  = 13,StatusIndicator  = 7,FNSNbr  = 0,BenefitType  = 0,RiskLevelID  = 1,MerchantType  = 1,
                InternetURL = " " ,DeactivationDt=new DateTime(2007,02,15) ,IncrementalDt =new DateTime(2003,09,03),ThresholdDt=DateTime.Now ,
                BrandID  = 1,SicCodeDesc = " ",AcqBankNameAddressID  = 0,AcqBankDesc =" ",IndustryTypeDesc=" " ,
                ProgramTypeDesc =" " ,DescriptorCd  = 0,VisaIndustryType  = 0,HighRiskInd  = 0,BeneTypeDesc =" " ,
                StatDesc =" ",MerchantClass =" ",RiskLevel = 0 ,RiskLevelDesc =" ",ChkAcctNbr  = 0,
                FederalTaxID = 0 ,StateTaxCode  = 0,MerchTypeDesc= " " ,SubIndGrpID = 0,
                SubIndGrpDesc =" ",MVV1 =" ",MVV2 =" ",StoreNbr = 0 ,IRSVerificationStatus = 0
            }
                ,
                new Wp.CIS.LynkSystems.Model.MerchantProfile()
                {
                    MerchantId  = 445711 ,CardType  = 0,AccountNbr  = 0,DiscountRate  = 0.0,CustomerID  = 299849,
                    ActivationDt=new DateTime(2003,09,02),SicCode  = 5499,IndustryType  = 1,MerchantNbr = "542929800751968" ,AcquiringBankId  = 1,
                    ProgramType  = 1,StatusIndicator  = 7,FNSNbr  = 0,BenefitType  = 0,RiskLevelID  = 1,MerchantType  = 1,
                    InternetURL = " " ,DeactivationDt=new DateTime(2005,01,24) ,IncrementalDt =new DateTime(2003,09,09),ThresholdDt=DateTime.Now ,
                    BrandID  = 1,SicCodeDesc = " ",AcqBankNameAddressID  = 0,AcqBankDesc =" ",IndustryTypeDesc=" " ,
                    ProgramTypeDesc =" " ,DescriptorCd  = 0,VisaIndustryType  = 0,HighRiskInd  = 0,BeneTypeDesc =" " ,
                    StatDesc =" ",MerchantClass =" ",RiskLevel = 0 ,RiskLevelDesc =" ",ChkAcctNbr  = 0,
                    FederalTaxID = 0 ,StateTaxCode  = 0,MerchTypeDesc= " " ,SubIndGrpID = 0,
                    SubIndGrpDesc =" ",MVV1 =" ",MVV2 =" ",StoreNbr = 0 ,IRSVerificationStatus = 0
                }
            };

        }
        public List<Wp.CIS.LynkSystems.Model.MerchantProfile> GetMerchantProfiles()
        {
            return merchants;
        }
      

        Task<Wp.CIS.LynkSystems.Model.MerchantProfile> IMerchantProfileRepository.GetMerchantProfileGeneralInfoAsync(int mid)
        {
            return Task.Run(() =>
            {

                return merchants.Find(io => io.MerchantId == mid);
            });
           
        }
    }
}

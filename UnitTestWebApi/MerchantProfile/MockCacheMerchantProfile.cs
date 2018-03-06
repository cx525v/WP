using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CIS.WebApi.UnitTests.MerchantProfile
{
    public class MockCacheMerchantProfile : IDistributedCache
    {
       
        Dictionary<string,Wp.CIS.LynkSystems.Model.MerchantProfile> _dict = new Dictionary<string,Wp.CIS.LynkSystems.Model.MerchantProfile>()
        {
            { "191808",  new Wp.CIS.LynkSystems.Model.MerchantProfile(){
                MerchantId  = 191807 ,CardType  = 0,AccountNbr  = 0,DiscountRate  = 0.0,CustomerID  = 17070,
                ActivationDt=new DateTime(2000,08,08),SicCode  = 5921,IndustryType  = 1,MerchantNbr = "007" ,AcquiringBankId  = 1,
                ProgramType  = 14,StatusIndicator  = 7,FNSNbr  = 0,BenefitType  = 0,RiskLevelID  = 1,MerchantType  = 1,
                InternetURL = " " ,DeactivationDt=new DateTime(2001,12,12) ,IncrementalDt =new DateTime(2000,08,09),ThresholdDt=DateTime.Now ,
                BrandID  = 0,SicCodeDesc = " ",AcqBankNameAddressID  = 0,AcqBankDesc =" ",IndustryTypeDesc=" " ,
                ProgramTypeDesc =" " ,DescriptorCd  = 0,VisaIndustryType  = 0,HighRiskInd  = 0,BeneTypeDesc =" " ,
                StatDesc =" ",MerchantClass =" ",RiskLevel = 0 ,RiskLevelDesc =" ",ChkAcctNbr  = 0,
                FederalTaxID = 0 ,StateTaxCode  = 0,MerchTypeDesc= " " ,SubIndGrpID = 0,
                SubIndGrpDesc =" ",MVV1 =" ",MVV2 =" ",StoreNbr = 0 ,IRSVerificationStatus = 0
            }},
            {"445709",  new Wp.CIS.LynkSystems.Model.MerchantProfile()
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
                }},
                
        };

        public byte[] Get(string key)
        {
            object mprofile= _dict.FirstOrDefault(e => e.Key == key).Value;
            
            if (mprofile == null)
                return null;
            else
            {
                var val = JsonConvert.SerializeObject(mprofile);
                // bf.Serialize(ms, mprofile);
                byte[] cacheArr = Encoding.ASCII.GetBytes(val);

                return cacheArr;
            }
           

        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                object mprofile = _dict.FirstOrDefault(e => e.Key == key).Value;

                if (mprofile == null)
                    return null;
                else
                {
                    var val = JsonConvert.SerializeObject(mprofile);
                    // bf.Serialize(ms, mprofile);
                    byte[] cacheArr = Encoding.ASCII.GetBytes(val);

                    return cacheArr;
                }
            });
        }

        public void Refresh(string key)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string key, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
           
            try
            {
                var bytesAsString = Encoding.ASCII.GetString(value);
                var mprofile = (Wp.CIS.LynkSystems.Model.MerchantProfile)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.MerchantProfile>(bytesAsString);
                _dict.Add(key, mprofile);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {

            return Task.Run(() =>
            {
               
                try
                {
                    var bytesAsString = Encoding.ASCII.GetString(value);
                    var mprofile = (Wp.CIS.LynkSystems.Model.MerchantProfile)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.MerchantProfile>(bytesAsString);
                    _dict.Add(key, mprofile);
                }
                catch (Exception e)
                {
                    throw e;
                }

            });

        }
    }
}

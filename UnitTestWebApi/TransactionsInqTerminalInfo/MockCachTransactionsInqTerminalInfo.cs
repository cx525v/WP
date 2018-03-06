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

namespace CIS.WebApi.UnitTests.TransactionsInqTerminalInfo
{
    public class MockCachTransactionsInqTerminalInfo : IDistributedCache
    {
        Dictionary<string, Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo> _dict = new Dictionary<string, Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo>()
        {
            //{ new Wp.CIS.LynkSystems.Model.TransactionsInquiry()
            { "10006144",  new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo()
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
                selectedCostPlusType  =   0,
                }
            },
        };


        public byte[] Get(string key)
        {
            object transinq = _dict.FirstOrDefault(e => e.Key == key).Value;

            if (transinq == null)
                return null;
            else
            {
                var val = JsonConvert.SerializeObject(transinq);
                // bf.Serialize(ms, mprofile);
                byte[] cacheArr = Encoding.ASCII.GetBytes(val);

                return cacheArr;
            }


        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() =>
            {
                object transinqfile = _dict.FirstOrDefault(e => e.Key == key).Value;

                if (transinqfile == null)
                    return null;
                else
                {
                    var val = JsonConvert.SerializeObject(transinqfile);
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
                var transinq = (Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo>(bytesAsString);
                _dict.Add(key, transinq);
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
                    var transinq = (Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo>(bytesAsString);
                    _dict.Add(key, transinq);
                }
                catch (Exception e)
                {
                    throw e;
                }

            });

        }







    }
}

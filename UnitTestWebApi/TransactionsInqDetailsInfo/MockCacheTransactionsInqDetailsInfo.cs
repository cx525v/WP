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

namespace CIS.WebApi.UnitTests.TransactionsInqDetailsInfo
{
    public class MockCacheTransactionsInqDetailsInfoTier : IDistributedCache
    {
        Dictionary<string, Wp.CIS.LynkSystems.Model.TransactionsInquiry> _dict = new Dictionary<string, Wp.CIS.LynkSystems.Model.TransactionsInquiry>()
        {
            //{ new Wp.CIS.LynkSystems.Model.TransactionsInquiry()
            { "191808",  new Wp.CIS.LynkSystems.Model.TransactionsInquiry()
               {
                    ARN    =   "24224435199101000064217",
                    ProcessingDate  =   "2015-07-17 00:00:00",
                    BatchNo =   717267,
                    SeqNo   =   68,
                    CardName    =   "Fleet One",
                    TranDesc    =   "Credit Sale",
                    TranDateTime    =   "2015-07-17 00:00:00",
                    AuthOnly    =    false,
                    AuthDateTime    =   "2015-07-17 10:30:42",
                    SettledAmount   =   15.00,
                    DispensedAmount =   0.00,
                    CashBackAmount  =   0.00,
                    SurchargeAmount =   0.00,
                    OriginalAuthAmount  =   15.00,
                    TotalAuthAmount =   15.00,
                    CompleteCode    =   0,
                    PAN =   "501486XHFOGNPUD0297",
                    ExpirationDate  =   null,
                    AuthNetwork =   54,
                    AuthCode    =   "749067",
                    AuthType    =   '0',
                    AuthRespCode    =   0,
                    AuthSourceCode  =   " ",
                    VisaTranRefNo   =   "000000000000000",
                    AVSResponseCode =   " ",
                    CommTypeDesc    =   "Frame",
                    CaptureTypeDesc =   "TERMINAL",
                    POSEntryModeDesc    =   "Swiped",
                    TieredQualificationType =   0,
                    CardQualificationType   =   0,
                    GrossTranAmount =   15.00,
                    GrossTranAmountPaid =   15.00,
                    PaidDate    =   "2015-07-21 00:00:00",
                    ACHOriginDate   =   "2015-07-17 00:00:00",
                    BankRTNbr   =   "121000248",
                    BankAcctType    =   1,
                    BankAcctNbr =   "654654",
                    TieredDesc  =   "Transaction Fees",
                    TranType    =   16,
                    TermID  =   "LK807325",
                    CardPaymentDate =   " ",
                    CardType    =   18,
                    ErrorCode   =   " ",
                    ReasonCD1   =   " ",
                    CardQualDesc    =   "Normal",
                    ExternalID  =   null,
                    NetworkRefNbr   =   " ",
                    RTCIndicator    = " ",
                    DecryptData =   " ",
                    CVMDescription  =   " ",
                    NetID  =   " ",
                    FullDeviceID    = " ",
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
                var transinq = (Wp.CIS.LynkSystems.Model.TransactionsInquiry)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(bytesAsString);
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
                    var transinq = (Wp.CIS.LynkSystems.Model.TransactionsInquiry)JsonConvert.DeserializeObject<Wp.CIS.LynkSystems.Model.TransactionsInquiry>(bytesAsString);
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



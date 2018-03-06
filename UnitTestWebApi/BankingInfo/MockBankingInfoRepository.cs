using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model;

namespace UnitTestDataAccess.BankingInfo
{
    public class MockBankingInfoRepository 
    {
        List<BankingInformation> bankingInfo;
        public bool FailGet { get; set; }
        public MockBankingInfoRepository()
        {
            bankingInfo = new List<BankingInformation>();
            bankingInfo.Add(new BankingInformation()
            {
                ActivityAcctTypeDescription = "Settlement",
                BankAcctNbr = "0000000000",
                BankRTNbr = "0000000000",
                BankName = "US Bank"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LidType"></param>
        /// <param name="Lid"></param>
        /// <returns></returns>
        public ApiResult<ICollection<BankingInformation>> GetMockBankingInfo()
        {
            ApiResult<ICollection<BankingInformation>> expected = new ApiResult<ICollection<BankingInformation>>()
            {
                Result = bankingInfo
            };
            return expected;
        }
    }
}

using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.Memo
{
    public class MockMemoInfoRepository
    {
        public ApiResult<MemoList> GetMockMemoInfo()
        {
            List<MemoInfo> customerMemo = new List<MemoInfo>()
            {
                new MemoInfo(){
                    lidtype = 2,
                    lid = 10000037,
                    categoryID = 16,
                    memo = "IRS ALERT: Please verify IRS information with merchant.  Update is required",
                    enabled = true,
                    categoryDesc = "IRS"
                }
            };
            List<MemoInfo> merchMemo = new List<MemoInfo>()
            {
                new MemoInfo(){
                lidtype =   3,
                lid =   479198,
                categoryID  =   13,
                memo    =   "PS Test for non IRS and non Multi Merch Customer",
                enabled = true,
                categoryDesc = "Other"
                },
                new MemoInfo(){
                lidtype =   3,
                lid =   479198,
                categoryID  =   13,
                memo    =   "This is a Multi Merchant",
                enabled = true,
                categoryDesc = "Multi Merchant"
                }
            };
            List<MemoInfo> groupMemo = new List<MemoInfo>()
            {
                new MemoInfo(){
                    groupID = 10000037,
                    categoryID = 16,
                    memo = "Non IRS & Multi Merchant Test",
                    enabled = true,
                    categoryDesc = "IRS"
                }
            };

            MemoList memoList = new MemoList();
            memoList.customerMemo = customerMemo;
            memoList.merchMemo = merchMemo;
            memoList.groupMemo = groupMemo;
            ApiResult<MemoList> expected = new ApiResult<MemoList>()
            {
                Result = memoList
            };
            return expected;
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MemoInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class MemoInfoApi : IMemoInfoApi
    {
        private IMemoInfoRepository _memoRepository;
        public MemoInfoApi(IOptions<Settings> optionsAccessor, IMemoInfoRepository memoRepository)
        {
            _memoRepository = memoRepository;
        }
        public async Task<ApiResult<MemoList>> GetMemoResults(Helper.LIDTypes LIDtype, int LID)
        {
            ApiResult<MemoList> response = new ApiResult<MemoList>();
            
            try
            {
                response.Result = await _memoRepository.GetMemoResults(LIDtype, LID);
            }
            catch (Exception)
            {
             
                throw;
            }
            return response;
        }
    }
}

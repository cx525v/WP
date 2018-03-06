using System;
using Wp.CIS.LynkSystems.Interfaces;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Options;
using Worldpay.CIS.DataAccess.MerchantProfile;

namespace Wp.CIS.LynkSystems.Services
{
    public class MerchantProfileApi :  IMerchantProfileApi
    {
        private IMerchantProfileRepository _merchprofilerepository;
        public MerchantProfileApi(IOptions<Settings> optionsAccessor, IMerchantProfileRepository merchprofilerepository) 
        {
            
            _merchprofilerepository = merchprofilerepository;
           
        }
        public Task<ApiResult<string>> GerMerchantProfileGeneralInfoTerminalGridAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<Model.MerchantProfile> GetMerchantProfileGeneralInfoAsync(int mid)
        {
            try
            {

                return await _merchprofilerepository.GetMerchantProfileGeneralInfoAsync(mid);
            }
            catch
            {
                throw;
            }
        }


        public Task<ApiResult<string>> GetMerchantProfileGeneralInfoDropdownInfoAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetMerchantProfileGetAuditHistoryAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetMerchantProfileMFCAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetMerchantProfilePCIInformationAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetMerchantProfileThirdPartyAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetMerchantProfileWithholdingAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> GetSeasonalMerchantGetAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> MerchantProfileAddNewMFCAsync(string merchantId, string mfcMerchNumber, string effectiveBeginDate, string closeDate, string groupId, string rate, string balance, string caption)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> MerchantProfileGenerateMerchantNumberAsync(string merchantId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> MerchantProfileMerchantInfoAsync(string merchantId)
        {
            throw new NotImplementedException();
        }


    }
}

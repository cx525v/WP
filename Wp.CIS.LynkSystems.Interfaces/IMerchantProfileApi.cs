using Wp.CIS.LynkSystems.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IMerchantProfileApi
    {

       // Task<Model.MerchantProfile> SaveAsync(MerchantProfilePCI mprofilePCI);
        Task<ApiResult<string>>  GetMerchantProfileGeneralInfoDropdownInfoAsync(string MerchantID);

        Task<MerchantProfile> GetMerchantProfileGeneralInfoAsync(int MID);

        //GerMerchantProfileGeneralInfoTerminalGrid

        Task<ApiResult<string>> GerMerchantProfileGeneralInfoTerminalGridAsync(string MerchantID);

        //GetMerchantProfileThirdParty

        Task<ApiResult<string>> GetMerchantProfileThirdPartyAsync(string MerchantID);


        Task<ApiResult<string>> GetMerchantProfilePCIInformationAsync(string MerchantID);


        Task<ApiResult<string>> GetMerchantProfileMFCAsync(string MerchantID);


        Task<ApiResult<string>> GetMerchantProfileWithholdingAsync(string MerchantID);

        //GetMerchantProfileGetAuditHistory

        Task<ApiResult<string>> GetMerchantProfileGetAuditHistoryAsync(string MerchantID);

        //MerchantProfileGenerateMerchantNumber

        Task<ApiResult<string>> MerchantProfileGenerateMerchantNumberAsync(string MerchantID);



        Task<ApiResult<string>> MerchantProfileMerchantInfoAsync(string MerchantID);




        Task<ApiResult<string>> MerchantProfileAddNewMFCAsync(string MerchantID, string MFCMerchNumber, string EffectiveBeginDate, string CloseDate, string GroupId, string rate, string Balance, string caption);

        //GetSeasonalMerchantGet

        Task<ApiResult<string>> GetSeasonalMerchantGetAsync(string MerchantID);
        Task<User> GetAsync(int id);
        //Task SaveAsync(int id);
    }
}

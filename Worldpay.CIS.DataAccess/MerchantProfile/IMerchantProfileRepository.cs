using System.Threading.Tasks;

namespace Worldpay.CIS.DataAccess.MerchantProfile
{
   public interface IMerchantProfileRepository
    {
        Task<Wp.CIS.LynkSystems.Model.MerchantProfile> GetMerchantProfileGeneralInfoAsync(int mid);
       

    }
}

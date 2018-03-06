namespace Wp.CIS.LynkSystems.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Wp.CIS.LynkSystems.Model;
    public interface IBankingApi
    {
        Task<ApiResult<ICollection<BankingInformation>>> GetBankingInfo(Helper.LIDTypes LIDType, string LID);
    }
}

using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Worldpay.CIS.DataAccess.MemoInfo
{
    public interface IMemoInfoRepository
    {
        Task<MemoList> GetMemoResults(Helper.LIDTypes LIDtype, int LID);
    }
}

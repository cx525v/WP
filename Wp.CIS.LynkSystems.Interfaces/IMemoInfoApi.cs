using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IMemoInfoApi
    {
        Task<ApiResult<MemoList>> GetMemoResults(Helper.LIDTypes LIDtype, int LID);
    }
}

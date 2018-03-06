using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;


namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface ICommanderVersionApi
    {
        Task<ICollection<CommanderVersion>> GetVersions();
        Task<ApiResult<ICollection<BaseVersion>>> GetBaseVersions();
        Task<ApiResult<bool>> CreateVersion(CommanderVersion commanderVersion);
        Task<ApiResult<bool>> DeleteVersion(int versionID,string userName);
    }
}

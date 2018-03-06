using System.Collections.Generic;
using System.Threading.Tasks;

namespace Worldpay.CIS.DataAccess.CommanderVersion
{
    public  interface ICommanderVersionRepository
    {
        Task<ICollection<Wp.CIS.LynkSystems.Model.CommanderVersion>> GetVersionsAsync();
        Task<ICollection<Wp.CIS.LynkSystems.Model.BaseVersion>> GetBaseVersionsAsync();
        Task<bool> CreateVersionAsync(Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion);
        Task<bool> DeleteVersionAsync(int versionID, string userName);
    }
}

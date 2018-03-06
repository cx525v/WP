using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IEPSTableApi
    {
        Task<ApiResult<ICollection<PetroTable>>> EPSGetAllPetroTablesByVersion(int versionID);
        Task<ApiResult<EPSTableErrorCodes>> EPSUpsertPetroTable(PetroTable petroTable);
    }
}

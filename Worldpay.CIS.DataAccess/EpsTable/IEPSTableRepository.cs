using System.Collections.Generic;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Worldpay.CIS.DataAccess.EpsTable
{
    public interface IEPSTableRepository
    {
        Task<ICollection<PetroTable>> EPSGetAllPetroTablesByVersionAsync(int versionID);
        Task<EPSTableErrorCodes> EPSUpsertPetroTableAsync(PetroTable petroTable);
    }
}

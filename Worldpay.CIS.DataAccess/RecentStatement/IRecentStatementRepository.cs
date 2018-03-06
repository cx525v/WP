using System.Collections.Generic;
using System.Threading.Tasks;

namespace Worldpay.CIS.DataAccess.RecentStatement
{
    public interface IRecentStatementRepository
    {
        Task<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> GetRecentStatementAsync(string merchantNbr);
    }
}

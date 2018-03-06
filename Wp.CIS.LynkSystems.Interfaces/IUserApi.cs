using Wp.CIS.LynkSystems.Model;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Interfaces
{
    public interface IUserApi
    {
        Task<User> GetAsync(int id);

        Task<ApiResult<string>> SaveAsync(User user);
    }
}
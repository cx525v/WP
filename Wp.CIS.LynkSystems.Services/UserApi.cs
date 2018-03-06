
using Wp.CIS.LynkSystems.Interfaces;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using System;

namespace Wp.CIS.LynkSystems.Services
{
    public class UserApi : IUserApi
    {
        public UserApi()
        {
        }

        public Task<User> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> SaveAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
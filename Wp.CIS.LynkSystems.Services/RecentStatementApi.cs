using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Worldpay.CIS.DataAccess.RecentStatement;
using Wp.CIS.LynkSystems.Interfaces;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class RecentStatementApi : IRecentStatementApi
    {
        private IRecentStatementRepository _recentStatementRepository;
        public RecentStatementApi(IOptions<Settings> optionsAccessor, IRecentStatementRepository recentStatementRepository)
        {
            _recentStatementRepository = recentStatementRepository;
        }

        public async Task<ApiResult<ICollection<RecentStatement>>> GetRecentStatementAsync(string merchantNbr)
        {
            ApiResult<ICollection<RecentStatement>> response = new ApiResult<ICollection<RecentStatement>>();
            
            try
            {
                response.Result = await _recentStatementRepository.GetRecentStatementAsync(merchantNbr);
            }
            catch (System.Exception)
            {
                throw;
            }
            return response;
        }
    }
}
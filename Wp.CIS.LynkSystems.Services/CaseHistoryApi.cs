using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Wp.CIS.LynkSystems.Services.DapperConnection;
using Wp.CIS.LynkSystems.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Model;
using Dapper;
using Worldpay.CIS.DataAccess.CaseHistory;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class CaseHistoryApi : ICaseHistoryApi
    {
        private ICaseHistoryRepository _casehistoryrepository;

        public CaseHistoryApi(IOptions<Settings> optionsAccessor, ICaseHistoryRepository casehistoryRepository)
        {
            _casehistoryrepository = casehistoryRepository;
            //_maxresultset = optionsAccessor.Value.MaxNumberOfRecordsToReturn;
        }

        public async Task<ApiResult<GenericPaginationResponse<CaseHistory>>> GetCaseHistory(LidTypeEnum lidtype, string lid, string extraId, PaginationCaseHistory page)
        {
            ApiResult<GenericPaginationResponse<CaseHistory>> response = new ApiResult<GenericPaginationResponse<CaseHistory>>();
           
            try
            {
                response.Result = await _casehistoryrepository.GetCaseHistoryInfo(lidtype, lid, extraId, page);
            }
            catch (Exception)
            {
 
                throw;
            }
            return response;
        }
    }
}

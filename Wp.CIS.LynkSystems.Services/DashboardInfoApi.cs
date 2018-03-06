using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Dashboard;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class DashboardInfoApi : IDashboardInfoApi
    {
        private IDashboardInfoRepository _dashboardRepository;

        private IOptions<Settings> _optionsAccessor;

        public DashboardInfoApi(IOptions<Settings> optionsAccessor, IDashboardInfoRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;

            this._optionsAccessor = optionsAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LIDtype"></param>
        /// <param name="LID"></param>
        /// <returns></returns>
        async Task<ApiResult<DashboardInfo>> IDashboardInfoApi.GetDashboardSearchResults(Helper.LIDTypes LIDtype, int LID)
        {
            ApiResult<DashboardInfo> response = new ApiResult<DashboardInfo>();
            
            try
            {
                response.Result = await _dashboardRepository.GetDashboardSearchResults(LIDtype, LID, this._optionsAccessor.Value.MaxNumberOfRecordsToReturn);
            }
            catch (System.Exception)
            {
              
                throw;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lidtype"></param>
        /// <param name="lid"></param>
        /// <param name="lidPk"></param>
        /// <returns></returns>
        public async Task<DashboardPrimaryKeysModel> GetDashboardSearchPrimaryKeys(LidTypeEnum lidtype, string lid, int? lidPk)
        {
          
            DashboardPrimaryKeysModel response = null;

            try
            {
                response = await this._dashboardRepository
                                        .GetDashboardSearchPrimaryKeys(lidtype, lid, lidPk);

                switch (response.LidType)
                {
                    case LidTypeEnum.Customer:
                        if (true == response.CustomerID.HasValue)
                        {
                            response.RecordFound = true;
                            response.ConvertedLidPk = response.CustomerID;
                        }
                        else
                        {
                            response.RecordFound = false;
                        }
                        break;

                    case LidTypeEnum.Merchant:
                        if (true == response.MerchantID.HasValue)
                        {
                            response.RecordFound = true;
                            response.ConvertedLidPk = response.MerchantID;
                        }
                        else
                        {
                            response.RecordFound = false;
                        }
                        break;

                    case LidTypeEnum.Terminal:
                        if (true == response.TerminalNbr.HasValue)
                        {
                            response.RecordFound = true;
                            response.ConvertedLidPk = response.TerminalNbr;
                        }
                        else
                        {
                            response.RecordFound = false;
                        }
                        break;

                    default:
                        response.RecordFound = false;
                        break;
                }
            }
            catch(Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ApiResult<TerminalDetails>> GetTerminalDetails(int termNbr) {

            ApiResult<TerminalDetails> response = new ApiResult<TerminalDetails>();
            
            try
            {
                response.Result = await _dashboardRepository.GetTerminalDetails(termNbr);
            }
            catch (System.Exception)
            {
                throw;
            }
            return response;
        }

        public async Task<ApiResult<DashboardInfo>> GetDashboardSearchResultsPagination(Helper.LIDTypes LIDtype, int LID)
        {
            ApiResult<DashboardInfo> response = new ApiResult<DashboardInfo>();
            
            try
            {
                response.Result = await _dashboardRepository.GetDashboardSearchResultsPagination(LIDtype, LID, this._optionsAccessor.Value.MaxNumberOfRecordsToReturn);
            }
            catch (System.Exception)
            {
                throw;
            }
            return response;
        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Error;

namespace Wp.CIS.LynkSystems.Services
{
    public class TerminalDetailsApi : ITerminalDetailsApi
    {
       
        private ITerminalDetailsRepository _terminalDetailsRepository;
        private ITerminalDetailsSettlementInfoRepository _terminalDetailsSettlementInfoRepository;

        public TerminalDetailsApi(IOptions<Settings> optionsAccessor, ITerminalDetailsRepository terminalDetailsRepository, ITerminalDetailsSettlementInfoRepository terminalDetailsSettlementInfoRepository)
        {
            _terminalDetailsRepository = terminalDetailsRepository;
            _terminalDetailsSettlementInfoRepository = terminalDetailsSettlementInfoRepository;
        }

        public ApiResult<EAndPData> GetTerminalDetails(int termNbr)
        {

            ApiResult<EAndPData> response = new ApiResult<EAndPData>();

            try
            {
                var result = _terminalDetailsRepository.GetTerminalDetails(termNbr);
                response.Result = result.Result;
            }
            catch (System.Exception)
            {

                throw;
            }
            return response;
        }

        public async Task<ApiResult<TerminalSettlementInfo>> GetTerminalSettlementInfo(int termNbr)
        {
            ApiResult<TerminalSettlementInfo> response = new ApiResult<TerminalSettlementInfo>();
           
            try
            {
                response.Result = await _terminalDetailsSettlementInfoRepository.GetTerminalSettlementInfo(termNbr);
            }
            catch (System.Exception)
            {
             
                throw;
            }
            return response;
        }
    }
}

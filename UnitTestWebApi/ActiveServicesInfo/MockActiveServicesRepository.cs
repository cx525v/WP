using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.ActiveServicesInfo
{
    public class MockActiveServicesRepository
    {
        public List<ActiveServices> activeServices;

        public MockActiveServicesRepository()
        {
            activeServices = new List<ActiveServices>();
            activeServices.Add(new ActiveServices()
            {
                LIDType = 1,
                LID = 757365,
                BillingMethodType = 6,
                BillMtdDesc = "Cost Plus",
                //ChkName = NULL,
                GiftLynk_ON = true,
                RewardsLynk_ON = true,
                LastProcessingDt = System.DateTime.Now,
                Amex_ON = true,
                Discover_ON = false,
                Discover_CT21_ON = true,
                Diner_ON = false,
                JCB_ON = false,
                OpenCase = 2,
                TerminalRental_ON = false,
                PrinterRental_ON = false,
                PINPadRental_ON = true,
                SoftDesc = "VAR Software",
                CreditST_ON = true,
                DebitST_ON = true,
                CheckST_ON = false,
                ACHST_ON = false,
                LynkAdvantage_ON = false,
                SICDesc = "5541 SVC STA",
                //WITH / WITHOUT OTH SVC,
                //LADesc =       ,
                //Equipment = "SEPS",
            });
        }

        public ApiResult<ICollection<ActiveServices>> GetMockData()
        {
            ApiResult<ICollection<ActiveServices>> expected = new ApiResult<ICollection<ActiveServices>>()
            {
                Result = activeServices
            };
            return expected;
        }
    }
}

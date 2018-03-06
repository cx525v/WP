using System;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.TerminalDetailsInfo
{
    public class MockTerminalDetailsInfoRepository
    {
        public ApiResult<EAndPData> GetMockTerminalDetails()
        {
            TerminalDetails terminalDetails = new TerminalDetails()
            {
                credit = 1,
                debit = 1,
                autoSettleOverride = "0",
                autoSettleTime = "0000",
                autoSettleIndicator = "1",
                timeZone = "EDT",
                terminalDescription = "Terminal - Omni 396 POS",
                terminalType = "XPNT",
            };

            var activeServices = new ActiveServices()
            {
                LIDType = 1,
                LID = 757365,
                BillingMethodType = 6,
                BillMtdDesc = "Cost Plus",
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
            };

            var terminalInfo = new TerminalInfo();
            terminalInfo.customerID = 393727;
            terminalInfo.merchantId = 570343;
            terminalInfo.terminalId = "LK429221";
            terminalInfo.businessType = 2;
            terminalInfo.programType = 0;
            terminalInfo.activationDt = DateTime.Now;
            terminalInfo.downLoadDate = DateTime.Now;
            terminalInfo.sentToStratusDate = DateTime.Now;
            terminalInfo.cspStatusInterval = "2";
            terminalInfo.commType = 2;
            terminalInfo.statusIndicator = 1;
            terminalInfo.cutOffTime = "400";
            terminalInfo.captureType = 0;
            terminalInfo.defaultNetwork = 0;
            terminalInfo.originalSO = 838033;
            terminalInfo.incrementalDt = DateTime.Now;
            terminalInfo.busTypeDesc = "Payment";
            terminalInfo.cashAdv = 0;
            terminalInfo.checkSvc = 0;
            terminalInfo.credit = 1;
            terminalInfo.debit = 0;
            terminalInfo.ebt = 0;
            terminalInfo.fleet = 0;
            terminalInfo.pob = 0;
            terminalInfo.suppLA = 0;
            terminalInfo.merchantName = "Golden Corral 919";
            terminalInfo.statDesc = "Active";

            var sensitivityInfo = new SensitivityInfo()
            {
                senLevelDesc = "Mid Market",
                sensitivityLevel = 4,
            };

            var eandpData = new EAndPData();
            eandpData.activeServices = activeServices;
            eandpData.terminalInfo = terminalInfo;
            eandpData.terminalDetails = terminalDetails;
          //  eandpData.terminalSettlementInfo = GetMockTerminalSettlementInfo().Result;
            eandpData.sensitivityInfo = sensitivityInfo;

            ApiResult<EAndPData> expected = new ApiResult<EAndPData>()
            {
                Result = eandpData
            };
            return expected;
        }

        public ApiResult<TerminalSettlementInfo> GetMockTerminalSettlementInfo()
        {
            var terminalSettlementInfo = new TerminalSettlementInfo() { grossAmt = 1290, nbrOfTrans = 1243 };
            ApiResult<TerminalSettlementInfo> expected = new ApiResult<TerminalSettlementInfo>()
            {
                Result = terminalSettlementInfo
            };
            return expected;
        }
    }
}

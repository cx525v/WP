using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Dashboard;
using Wp.CIS.LynkSystems.Model.Enums;

namespace CIS.WebApi.UnitTests.DashboardInfo
{
    public class MockDashboardInfoRepository
    {
        public Wp.CIS.LynkSystems.Model.DashboardInfo dashboardInfo;

        MerchantInfo merchantInfo = new MerchantInfo();
        CustomerProfile customerProfile = new CustomerProfile();
        TerminalInfo terminalInfo = new TerminalInfo();
        public List<Demographics> custDemographicsList = new List<Demographics>();
        public List<Demographics> termDemographics = new List<Demographics>();
        public List<Demographics> merchDemographicsList = new List<Demographics>();
        public MockDashboardInfoRepository()
        {
            dashboardInfo = new Wp.CIS.LynkSystems.Model.DashboardInfo();
            merchantInfo.merchantId = 570343;
            merchantInfo.customerID = 393727;
            merchantInfo.activationDt = DateTime.Now;
            merchantInfo.sicCode = 5812;
            merchantInfo.industryType = 2;
            merchantInfo.merchantNbr = "232333";
            merchantInfo.acquiringBankId = 13;
            merchantInfo.programType = 40;
            merchantInfo.statusIndicator = 6;
            merchantInfo.fnsNbr = "0";
            merchantInfo.benefitType = 0;
            merchantInfo.riskLevelID = 1;
            merchantInfo.merchantType = 1;
            merchantInfo.incrementalDt = DateTime.Now;
            merchantInfo.thresholdDt = DateTime.Now;
            merchantInfo.brandID = 1;
            merchantInfo.sicDesc = "RESTAURANTS";
            merchantInfo.merchantClass = 'A';
            merchantInfo.riskLevel = "1";
            merchantInfo.statDesc = "New Account";
            merchantInfo.indTypeDesc = "Restaurant";
            merchantInfo.mchName = "Golden Corral 919";
            merchantInfo.mchAddress = "2701 Coors Blvd NW";
            merchantInfo.mchCity = "Albuquerque";
            merchantInfo.mchState = "NM";
            merchantInfo.mchZipCode = "87120";
            merchantInfo.mchPhone = "5058314607";
            merchantInfo.mchContact = "Store Manager";
            merchantInfo.acquiringBank = "Citizens Trust Tier 1 Tier 2";
            merchantInfo.benefitTypeDesc = "None";
            merchantInfo.merchFedTaxID = "561005071";

            customerProfile.customerID = 393727;
            customerProfile.description = "Golden Corral Corporation";
            customerProfile.activationDt = DateTime.Now;
            customerProfile.statusIndicator = 1;
            customerProfile.legalType = 3;
            customerProfile.customerNbr = "1000393727";
            customerProfile.classCode = 0;
            customerProfile.sensitivityLevel = 6;
            customerProfile.stmtTollFreeNumber = "18772827362";
            customerProfile.legalDesc = "Corporation";
            customerProfile.senseLvlDesc = "Tier 2";
            customerProfile.statDesc = "Active";
            customerProfile.lynkAdvantage = 0;
            customerProfile.pinPadPlus = 0;
            customerProfile.giftLynk = 1;
            customerProfile.rewardsLynk = 1;
            customerProfile.demoID = 393727;
            customerProfile.custName = "Golden Corral Corporation";
            customerProfile.custContact = "Terri Warren";
            customerProfile.prinID = 393727;
            customerProfile.prinName = "Theodore Fowler";
            customerProfile.prinAddress = "5151 Glenwood Ave";
            customerProfile.prinCity = "Raleigh";
            customerProfile.prinState = "NC";
            customerProfile.prinZipcode = "27612";
            customerProfile.prinSSN = "561005071";

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

            custDemographicsList.Add(new Demographics()
            {
                Level = "Customer",
                Name = "Golden Corral Corporation",
                Address = "5151 Glenwood Ave. ",
                City = "Raleigh",
                State = "NC",
                ZipCode = "27612",
                Phone = "9197819310",
                Contact = "Terri Warren",
                AddressType = "Customer",
                AddressTypeID = 11,
                County = "Wake",
                NameAddressID = 3301636,
            });
            custDemographicsList.Add(new Demographics()
            {
                Level = "Customer",
                Name = "Theodore Fowler",
                Address = "5151 Glenwood Ave",
                City = "Raleigh",
                State = "NC",
                ZipCode = "27612",
                Phone = "9197819310",
                Contact = "Theodore Fowler",
                SSN = "561005071",
                AddressType = "Principal",
                AddressTypeID = 17,
                County = "Wake",
                NameAddressID = 3302202,
            });

            merchDemographicsList.Add(new Demographics()
            {
                Level = "Merchant",
                Name = "Metropolitan Investment GRP 3",
                Address = "4500 N 32nd Street Suite 200",
                City = "Phoenix",
                State = "AZ",
                ZipCode = "85018",
                Phone = "6029129000",
                Fax = "6029129478",
                Contact = "Trish Don Francesco",
                Title = "Owner",
                SSN = "503602306",
                AddressType = "IRS 1099",
                AddressTypeID = 68,
                County = "Maricopa",
                NameAddressID = 6491283
            });
            merchDemographicsList.Add(new Demographics()
            {
                Level = "Merchant",
                Name = " Investment GRP 3",
                Address = "N 32nd Street Suite 200",
                City = "Phoenix",
                State = "AZ",
                ZipCode = "85018",
                Phone = "6029129000",
                Fax = "6029129478",
                Contact = "Trish Don Francesco",
                Title = "Owner",
                SSN = "503602306",
                AddressType = "IRS 1099",
                AddressTypeID = 68,
                County = "Maricopa",
                NameAddressID = 6491283
            });
            merchDemographicsList.Add(new Demographics()
            {
                Level = "Merchant",
                Name = " Investment GRP 3",
                Address = "N 32nd Street Suite 200",
                City = "Phoenix",
                State = "AK",
                ZipCode = "85018",
                Phone = "6029129000",
                Fax = "6029129478",
                Contact = "Trish Don Francesco",
                Title = "Owner",
                SSN = "503602306",
                AddressType = "IRS 1099",
                AddressTypeID = 68,
                County = "Maricopa",
                NameAddressID = 6491283
            });

            termDemographics.Add(new Demographics()
            {
                Level = "Terminal",
                Name = "Theodore Fowler",
                Address = "5151 Glenwood Ave",
                City = "Raleigh",
                State = "NC",
                ZipCode = "27612",
                Phone = "9197819310",
                Contact = "Theodore Fowler",
                SSN = "561005071",
                AddressType = "Principal",
                AddressTypeID = 17,
                County = "Wake",
                NameAddressID = 3302202,
            });
            termDemographics.Add(new Demographics()
            {
                Level = "Terminal",
                Name = "Metropolitan Investment GRP 3",
                Address = "4500 N 32nd Street Suite 200",
                City = "Phoenix",
                State = "AZ",
                ZipCode = "85018",
                Phone = "6029129000",
                Fax = "6029129478",
                Contact = "Trish Don Francesco",
                Title = "Owner",
                SSN = "503602306",
                AddressType = "IRS 1099",
                AddressTypeID = 68,
                County = "Maricopa",
                NameAddressID = 6491283
            });
            termDemographics.Add(new Demographics()
            {
                Level = "Terminal",
                Name = " Investment GRP 3",
                Address = "N 32nd Street Suite 200",
                City = "Phoenix",
                State = "AZ",
                ZipCode = "85018",
                Phone = "6029129000",
                Fax = "6029129478",
                Contact = "Trish Don Francesco",
                Title = "Owner",
                SSN = "503602306",
                AddressType = "IRS 1099",
                AddressTypeID = 68,
                County = "Maricopa",
                NameAddressID = 6491283
            });
        }

        public ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> GetMockTerminalData()
        {
            dashboardInfo.MerchInfo = merchantInfo;
            dashboardInfo.CustProfile = customerProfile;
            dashboardInfo.TermInfo = terminalInfo;
            dashboardInfo.DemographicsInfoMerch = merchDemographicsList;
            dashboardInfo.DemographicsInfoCust = custDemographicsList;
            dashboardInfo.DemographicsInfoTerm = termDemographics;
            ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> expected = new ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo>()
            {
                Result = dashboardInfo
            };
            return expected;
        }

        public ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> GetMockMerchantData()
        {
            dashboardInfo.MerchInfo = merchantInfo;
            dashboardInfo.CustProfile = customerProfile;
            dashboardInfo.DemographicsInfoMerch = merchDemographicsList;
            dashboardInfo.DemographicsInfoCust = custDemographicsList;
            ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> expected = new ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo>()
            {
                Result = dashboardInfo
            };
            return expected;
        }

        public ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> GetMockCustomerData()
        {
            dashboardInfo.CustProfile = customerProfile;
            dashboardInfo.DemographicsInfoCust = custDemographicsList;
            ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> expected = new ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo>()
            {
                Result = dashboardInfo
            };
            return expected;
        }

        public ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> GetMockCacheCustomerData()
        {
            customerProfile.customerID = 123;
            dashboardInfo.CustProfile = customerProfile;
            dashboardInfo.DemographicsInfoCust = custDemographicsList;
            ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo> expected = new ApiResult<Wp.CIS.LynkSystems.Model.DashboardInfo>()
            {
                Result = dashboardInfo
            };
            return expected;
        }

        public ApiResult<TerminalDetails> GetMockTerminalDetails()
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
            };

            ApiResult<TerminalDetails> expected = new ApiResult<TerminalDetails>()
            {
                Result = terminalDetails
            };
            return expected;
        }
    }
}

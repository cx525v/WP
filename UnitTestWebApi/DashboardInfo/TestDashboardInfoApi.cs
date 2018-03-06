using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.DashboardInfo
{
    public class TestDashboardInfoApi
    {
        private const int maxRecordsToReturn = 500;

        [Fact]
        public void DashboardInfoApiTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;

            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi api = Substitute.For<IDashboardInfoApi>();

            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> options = Options.Create(appSettings);


            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData().Result;
            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult);


            api = new DashboardInfoApi(options, mockRepo);

            // Act
            var actualRecord = (api.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
            Assert.Equal((actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo.customerID, 393727);
        }

        [Fact]
        public void DashboardInfoApiTest_MerchantSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi api = Substitute.For<IDashboardInfoApi>();

            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> options = Options.Create(appSettings);

            var expectedResult = repository.GetMockMerchantData().Result;

            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult);

            api = new DashboardInfoApi(options, mockRepo);

            // Act
            var actualRecord = (api.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
            Assert.Equal((actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo, null);
        }

        [Fact]
        public void DashboardInfoApiTest_CustomerSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();

            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi api = Substitute.For<IDashboardInfoApi>();


            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> options = Options.Create(appSettings);

            var expectedResult = repository.GetMockCustomerData().Result;

            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult);

            api = new DashboardInfoApi(options, mockRepo);

            // Act
            var actualRecord = (api.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
            Assert.Equal((actualRecord).MerchInfo, null);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo, null);
        }

        [Fact]
        public async Task DashboardInfoApiTest_Exception()
        {
            // Arrange
            int CustomerID = 191809;

            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> optionsAccessor = Options.Create(appSettings);
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi dashboardInfoApi = Substitute.For<IDashboardInfoApi>();

            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID, maxRecordsToReturn).ThrowsForAnyArgs(new Exception());
            dashboardInfoApi = new DashboardInfoApi(optionsAccessor, mockRepo);


            // Assert
            await Assert.ThrowsAsync<Exception>(() => dashboardInfoApi.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID));

        }
        [Fact]
        public void DashboardInfoApiTest_TerminalDetailsSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();

            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi api = Substitute.For<IDashboardInfoApi>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var expectedResult = repository.GetMockTerminalDetails().Result;

            mockRepo.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult);
            api = new DashboardInfoApi(appSettings, mockRepo);

            // Act
            var actualRecord = (api.GetTerminalDetails(lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
        }

        [Fact]
        public async Task DashboardInfoApiTest_TerminalDetailsException()
        {
            // Arrange
            int CustomerID = 191809;

           // IOptions<Settings> optionsAccessor = Options.Create(appSettings);
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi dashboardInfoApi = Substitute.For<IDashboardInfoApi>();

            mockRepo.GetTerminalDetails(CustomerID).ThrowsForAnyArgs(new Exception());
            dashboardInfoApi = new DashboardInfoApi(optionsAccessor, mockRepo);


            // Assert
            
            await Assert.ThrowsAsync<Exception>(() => dashboardInfoApi.GetTerminalDetails(CustomerID));
 
        }

        #region Unit Test GetDashboardSearchResultsPagination

        [Fact]
        public void DashboardInfoApiTest_GetDashboardSearchResultsPagination_Success()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi api = Substitute.For<IDashboardInfoApi>();

            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> options = Options.Create(appSettings);

            var expectedResult = repository.GetMockMerchantData().Result;

            mockRepo.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult);

            api = new DashboardInfoApi(options, mockRepo);

            // Act
            var actualRecord = (api.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
            Assert.Equal((actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo, null);
        }


        [Fact]
        public async Task DashboardInfoApiTest_GetDashboardSearchResultsPagination_Exception()
        {
            // Arrange
            int CustomerID = 191809;
            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> optionsAccessor = Options.Create(appSettings);

            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            IDashboardInfoApi dashboardInfoApi = Substitute.For<IDashboardInfoApi>();
            

            mockRepo.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID, maxRecordsToReturn).ThrowsForAnyArgs(new Exception());
            dashboardInfoApi = new DashboardInfoApi(optionsAccessor, mockRepo);


            // Assert
            await Assert.ThrowsAsync<Exception>(() => dashboardInfoApi.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID));
            
        }
        #endregion
    }
}

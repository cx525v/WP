using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Xunit;

namespace CIS.WebApi.UnitTests.DashboardInfo
{
    public class TestDashboardInfoRepository
    {
        private const int maxRecordsToReturn = 500;

        [Fact]

        //Would be revisiting to modify the actual way of call method.
        public void DashboardInfoTest_TerminalSuccess()
        {
            // Arrange
            int lid = 191809;

            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid, maxRecordsToReturn).Result;

            // Assert

            Assert.Equal((actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo.customerID, 393727);
        }

        [Fact]
        public void DashboardInfoTest_MerchantSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockMerchantData();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();


            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid, maxRecordsToReturn).Result;

            // Assert
            Assert.Equal((actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo, null);
        }

        [Fact]
        public void DashboardInfoTest_CustomerSuccess()
        {
            // Arrange
            int lid = 589547;

            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockCustomerData();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid, maxRecordsToReturn).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid, maxRecordsToReturn).Result;

            // Assert
            Assert.Equal((actualRecord).MerchInfo, null);
            Assert.Equal((actualRecord).CustProfile.customerID, 393727);
            Assert.Equal((actualRecord).TermInfo, null);
        }
        [Fact]
        public void DashboardInfoTest_TerminalDetailsSuccess()
        {
            // Arrange
            int lid = 589547;

            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();
            IDashboardInfoRepository mockRepo = Substitute.For<IDashboardInfoRepository>();
            mockRepo.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetTerminalDetails(lid).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult.Result);
        }
    }
}

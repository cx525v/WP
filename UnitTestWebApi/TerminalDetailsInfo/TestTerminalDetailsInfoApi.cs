using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.TerminalDetailsInfo
{
    public class TestTerminalDetailsInfoApi
    {
        [Fact]
        public void TerminalDetailsInfoApiTest_TerminalDetailsSuccess()
        {
            // Arrange
            int lid = 589547;
            MockTerminalDetailsInfoRepository repository = new MockTerminalDetailsInfoRepository();

            ITerminalDetailsRepository mockRepo = Substitute.For<ITerminalDetailsRepository>();
            ITerminalDetailsSettlementInfoRepository mockSettlementRepo = Substitute.For<ITerminalDetailsSettlementInfoRepository>();
            ITerminalDetailsApi api = Substitute.For<ITerminalDetailsApi>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var expectedResult = repository.GetMockTerminalDetails().Result;
            var expectedSettlementResult = repository.GetMockTerminalSettlementInfo().Result;

            mockRepo.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult);
            mockSettlementRepo.GetTerminalSettlementInfo(lid).ReturnsForAnyArgs(expectedSettlementResult);
            api = new TerminalDetailsApi(appSettings, mockRepo, mockSettlementRepo);

            // Act
            var actualRecord = (api.GetTerminalDetails(lid).Result);

            // Assert
            Assert.Equal((actualRecord), expectedResult);
        }

        [Fact]
        public void TerminalDetailsInfoApiTest_TerminalDetailsException()
        {
            // Arrange
            int CustomerID = 191809;

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            ITerminalDetailsRepository mockRepo = Substitute.For<ITerminalDetailsRepository>();
            ITerminalDetailsApi dashboardInfoApi = Substitute.For<ITerminalDetailsApi>();

            mockRepo.GetTerminalDetails(CustomerID).ThrowsForAnyArgs(new Exception());
            dashboardInfoApi = new TerminalDetailsApi(optionsAccessor, mockRepo,null);


            // Assert
            Assert.Throws<Exception>(() => dashboardInfoApi.GetTerminalDetails(CustomerID));
           
        }
    }
}

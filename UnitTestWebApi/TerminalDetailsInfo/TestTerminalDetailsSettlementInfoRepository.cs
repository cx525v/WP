using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo;
using Xunit;

namespace CIS.WebApi.UnitTests.TerminalDetailsInfo
{
    public class TestTerminalDetailsSettlementInfoRepository
    {
        [Fact]

        //Would be revisiting to modify the actual way of call method.
        public void TerminalDetailsSettlementTest_TerminalSuccess()
        {
            // Arrange
            int lid = 191809;

            MockTerminalDetailsInfoRepository repository = new MockTerminalDetailsInfoRepository();
            var expectedResult = repository.GetMockTerminalSettlementInfo();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            ITerminalDetailsSettlementInfoRepository mockRepo = Substitute.For<ITerminalDetailsSettlementInfoRepository>();
            mockRepo.GetTerminalSettlementInfo(lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetTerminalSettlementInfo(lid).Result;

            // Assert

            Assert.Equal(actualRecord, expectedResult.Result);
        }
    }
}

using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Xunit;

namespace CIS.WebApi.UnitTests.TerminalDetailsInfo
{
    public class TestTerminalDetailsInfoRepository
    {
        [Fact]
        //Would be revisiting to modify the actual way of call method.
        public void TerminalDetailsInfoTest_TerminalSuccess()
        {
            // Arrange
            int lid = 191809;

            MockTerminalDetailsInfoRepository repository = new MockTerminalDetailsInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            ITerminalDetailsRepository mockRepo = Substitute.For<ITerminalDetailsRepository>();
            mockRepo.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetTerminalDetails(lid).Result;

            // Assert

            Assert.Equal(actualRecord, expectedResult.Result);
        }
    }
}

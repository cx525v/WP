using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.MemoInfo;
using Xunit;

namespace CIS.WebApi.UnitTests.Memo
{
    public class TestMemoInfoRepository
    {
        [Fact]
        //Would be revisiting to modify the actual way of call method.
        public void MemoInfoTest_TerminalSuccess()
        {
            // Arrange
            int lid = 191809;

            MockMemoInfoRepository repository = new MockMemoInfoRepository();
            var expectedResult = repository.GetMockMemoInfo();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IMemoInfoRepository mockRepo = Substitute.For<IMemoInfoRepository>();
            mockRepo.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result;

            // Assert

            Assert.Equal(actualRecord, expectedResult.Result);
        }
    }
}

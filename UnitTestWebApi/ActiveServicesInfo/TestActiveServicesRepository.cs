using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ActiveServicesInfo;
using Worldpay.CIS.DataAccess.Connection;
using Xunit;

namespace CIS.WebApi.UnitTests.ActiveServicesInfo
{
    public class TestActiveServicesRepository
    {
        [Fact]

        //Would be revisiting to modify the actual way of call method.
        public void ActiveServicesTest_Success()
        {
            // Arrange
            int lid = 191809;

            MockActiveServicesRepository repository = new MockActiveServicesRepository();
            var expectedResult = repository.GetMockData();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IActiveServicesRepository mockRepo = Substitute.For<IActiveServicesRepository>();
            mockRepo.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult.Result);
        }
    }
}

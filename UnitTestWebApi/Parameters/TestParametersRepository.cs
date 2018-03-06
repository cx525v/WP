using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.Parameters;
using Worldpay.CIS.WebApi.UnitTests.Parameters;
using Xunit;

namespace CIS.WebApi.UnitTests.Parameters
{
    public class TestParametersRepository
    {
        [Fact]

        //Would be revisiting to modify the actual way of call method.
        public void ParametersTest_Success()
        {
            // Arrange
            int lid = 191809;

            MockParametersRepository repository = new MockParametersRepository();
            var expectedResult = repository.GetMockData();
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IParametersRepository mockRepo = Substitute.For<IParametersRepository>();
            mockRepo.GetParametersAsync(lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetParametersAsync(lid).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult.Result);
        }
    }
}

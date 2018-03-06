using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Parameters;
using Worldpay.CIS.WebApi.UnitTests.Parameters;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.Parameters
{
    public class TestParametersApi
    {
        [Fact]
        public void ParametersApiTest_Success()
        {
            // Arrange
            int parameterId = 589547;
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IParametersRepository mockRepo = Substitute.For<IParametersRepository>();
            IParametersApi api = Substitute.For<IParametersApi>();

            MockParametersRepository repository = new MockParametersRepository();
            var expectedResult = repository.GetMockData().Result;
            mockRepo.GetParametersAsync(parameterId).ReturnsForAnyArgs(expectedResult);

            api = new ParametersApi(optionsAccessor, mockRepo);

            // Act
            var actualRecord = (api.GetParameters(parameterId).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
        }

        [Fact]
        public async Task ParametersApiTest_Exception()
        {
            // Arrange
            int parameterId = 191809;

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IParametersRepository mockRepo = Substitute.For<IParametersRepository>();
            IParametersApi parametersApi = Substitute.For<IParametersApi>();

            mockRepo.GetParametersAsync(parameterId).ThrowsForAnyArgs(new Exception());
            parametersApi = new ParametersApi(optionsAccessor, mockRepo);


            // Assert
            await Assert.ThrowsAsync<Exception>(() => parametersApi.GetParameters(parameterId));

        }
    }
}

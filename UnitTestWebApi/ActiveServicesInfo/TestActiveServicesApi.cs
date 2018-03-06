using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ActiveServicesInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.ActiveServicesInfo
{
    public class TestActiveServicesApi
    {
        [Fact]
        public void ActiveServicesApiTest_Success()
        {
            // Arrange
            int lid = 589547;
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IActiveServicesRepository mockRepo = Substitute.For<IActiveServicesRepository>();
            IActiveServicesApi api = Substitute.For<IActiveServicesApi>();

            MockActiveServicesRepository repository = new MockActiveServicesRepository();
            var expectedResult = repository.GetMockData().Result;
            mockRepo.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);

            api = new ActiveServicesApi(optionsAccessor, mockRepo);

            // Act
            var actualRecord = (api.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
        }

        [Fact]
        public async Task ActiveServicesApiTest_Exception()
        {
            // Arrange
            int CustomerID = 191809;

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IActiveServicesRepository mockRepo = Substitute.For<IActiveServicesRepository>();
            IActiveServicesApi activeServicesApi = Substitute.For<IActiveServicesApi>();

            mockRepo.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID).ThrowsForAnyArgs(new Exception());
            activeServicesApi = new ActiveServicesApi(optionsAccessor, mockRepo);

            // Act
            //var result = activeServicesApi.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID).Result;
            await Assert.ThrowsAsync<Exception>(() => activeServicesApi.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerID, CustomerID));

            // Assert
            //Assert.Equal(((IList<string>)result.ErrorMessages).First(), "InternalServerError");
        }
    }
}

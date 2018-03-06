using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NSubstitute;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.Parameters;
using Worldpay.CIS.WebApi.UnitTests.Parameters;
using Wp.CIS.LynkSystems.WebApi.Common;
using Microsoft.Extensions.Localization;
using NSubstitute.ExceptionExtensions;
using CIS.WebApi.UnitTests.Common;
using Worldpay.Logging.Providers.Log4Net.Facade;
namespace CIS.WebApi.UnitTests.Parameters
{
    public class TestParametersController
    {
        [Fact]
        public void ParametersControllerTest_ModelState_Invalid()
        {
            //Arrange
            int lid = 589547;
            MockParametersRepository repository = new MockParametersRepository();
            var expectedResult = repository.GetMockData();

            IParametersRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);

            IParametersApi dAPI = Substitute.For<IParametersApi>();
            dAPI.GetParameters(lid).ReturnsForAnyArgs(expectedResult);
            ParametersController controller = FakeController(_cache, dAPI, null, fakeOperation, FakeLogger());

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.GetParameters(lid).Result;


            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task ParametersControllerTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;
            MockParametersRepository repository = new MockParametersRepository();
            var expectedResult = repository.GetMockData();

            IParametersRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);

            IParametersApi dAPI = Substitute.For<IParametersApi>();
            dAPI.GetParameters(lid).ReturnsForAnyArgs(expectedResult);
            ParametersController controller = FakeController(_cache, dAPI, null, fakeOperation, FakeLogger());

            // Act
            var dinfo = await controller.GetParameters(lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(actualRecord, expectedResult.Result);
        }

        [Fact]
        public async Task ParametersControllerTerminalTest_GetAnException()
        {
            // Arrange
            MockParametersRepository repository = new MockParametersRepository();
            var expectedResult = repository.GetMockData();
            IParametersRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IStringLocalizer<ParametersController> localizer
                        = Substitute.For<IStringLocalizer<ParametersController>>();
            string key = "GenericError";
            string value = "Error occured";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IParametersApi dAPI = new ParametersApi(appSettings, mockRepo);
            dAPI.GetParameters(null).ThrowsForAnyArgs(new System.Exception());
            ParametersController controller = FakeController(_cache, dAPI, localizer, fakeOperation,FakeLogger());


            // Act
            var dinfo = await controller.GetParameters(null);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private IParametersRepository FakeRepository()
        {
            return Substitute.For<IParametersRepository>();
        }

        private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<List<Wp.CIS.LynkSystems.Model.Parameters>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<List<Wp.CIS.LynkSystems.Model.Parameters>>())).DoNotCallBase();
            return fakeOperation;
        }

        private ParametersController FakeController(IDistributedCache mockCache, IParametersApi Parameters, IStringLocalizer<ParametersController> localizer,IOperation fakeOperation, ILoggingFacade loggingFacade)
        {            
            return new ParametersController(mockCache, Parameters, localizer, fakeOperation, loggingFacade);
        }

        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }
    }
}

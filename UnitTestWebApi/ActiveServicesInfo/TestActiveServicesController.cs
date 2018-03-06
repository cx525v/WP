using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ActiveServicesInfo;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.ActiveServicesInfo
{
    public class TestActiveServicesController
    {
        private const int maxRecordsToReturn = 500;
        [Fact]
        public void ActiveServicesControllerTest_ModelState_InvalidAsync()
        {
            //Arrange
            int lid = 589547;
            MockActiveServicesRepository repository = new MockActiveServicesRepository();
            var expectedResult = repository.GetMockData();

            IActiveServicesRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IActiveServicesApi dAPI = Substitute.For<IActiveServicesApi>();
            dAPI.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            ActiveServicesController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.Get((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;
            MockActiveServicesRepository repository = new MockActiveServicesRepository();
            var expectedResult = repository.GetMockData();

            IActiveServicesRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IActiveServicesApi dAPI = Substitute.For<IActiveServicesApi>();
            dAPI.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            ActiveServicesController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(actualRecord, expectedResult.Result);
        }
 
        [Fact]
        public async Task ActiveServicesControllerTerminalTest_GetAnException()
        {
           // Arrange
           MockActiveServicesRepository repository = new MockActiveServicesRepository();
            var expectedResult = repository.GetMockData();
            IActiveServicesRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            ILoggingFacade fakeLogger = FakeLogger();
            var dAPI = Substitute.For<IActiveServicesApi>();

            var appSettings = new Settings()
            {
                MaxNumberOfRecordsToReturn = maxRecordsToReturn
            };
            IOptions<Settings> options = Options.Create(appSettings);

           IDistributedCache mockCache = Substitute.For<IDistributedCache>();
           IStringLocalizer<ActiveServicesController> localizer
                      = Substitute.For<IStringLocalizer<ActiveServicesController>>();
            string key = "GenericError";
            string value = "Error occured";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);    
           
            dAPI.GetActiveServices((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, 0).ThrowsForAnyArgs(new System.Exception());
           
            ActiveServicesController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);


            // Act
            var dinfo = await controller.Get((int)Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, 0);
         
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private IActiveServicesRepository FakeRepository()
        {
            return Substitute.For<IActiveServicesRepository>();
        }

        private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<List<Wp.CIS.LynkSystems.Model.ActiveServices>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<List<Wp.CIS.LynkSystems.Model.ActiveServices>>())).DoNotCallBase();
            return fakeOperation;
        }

        private ActiveServicesController FakeController(IActiveServicesApi activeServices, IDistributedCache mockCache, IOperation fakeOperation, ILoggingFacade loggingFacade)
        {
            var localizer = new MockStringLocalizer<ActiveServicesController>();
            localizer[0] = new LocalizedString("GenericError", "Error occured");
            return new ActiveServicesController(mockCache, activeServices, localizer, fakeOperation, loggingFacade);
        }

        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }
    }
}

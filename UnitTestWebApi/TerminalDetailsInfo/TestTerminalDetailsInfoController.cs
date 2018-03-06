using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TerminalDetailsInfo;
using Worldpay.CIS.DataAccess.TerminalDetailsSettlementInfo;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.TerminalDetailsInfo
{
    public class TestTerminalDetailsInfoController
    {
        [Fact]
        public void TerminalDetailsControllerTest_ModelState_Invalid()
        {
            //Arrange
            int lid = 589547;
            MockTerminalDetailsInfoRepository repository = new MockTerminalDetailsInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();

            ITerminalDetailsRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            ITerminalDetailsApi dAPI = Substitute.For<ITerminalDetailsApi>();
            dAPI.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult);
            TerminalDetailsController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.GetTerminalDetails(lid).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task TerminalDetailsControllerTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;
            MockTerminalDetailsInfoRepository repository = new MockTerminalDetailsInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();

            ITerminalDetailsRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            ITerminalDetailsApi dAPI = Substitute.For<ITerminalDetailsApi>();
            dAPI.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult);
            TerminalDetailsController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.GetTerminalDetails(lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.EAndPData)actualRecord), expectedResult.Result);
        }

        [Fact]
        public async Task TerminalDetailsControllerTerminalTest_GetAnException()
        {
            // Arrange
            ITerminalDetailsRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            ILoggingFacade fakeLogger = FakeLogger();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IStringLocalizer<TerminalDetailsController> localizer
                        = Substitute.For<IStringLocalizer<TerminalDetailsController>>();
            string key = "GenericError";
            string value = "Error occured";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ITerminalDetailsApi dAPI = new TerminalDetailsApi(appSettings, mockRepo, null);
            dAPI.GetTerminalDetails(0).ThrowsForAnyArgs(new System.Exception());
            TerminalDetailsController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.GetTerminalDetails(0);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private ITerminalDetailsRepository FakeRepository()
        {
            return Substitute.For<ITerminalDetailsRepository>();
        }

        private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.EAndPData>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.EAndPData>())).DoNotCallBase();
            return fakeOperation;
        }

        private IOperation FakeTerminalSettlementInfoOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.TerminalSettlementInfo>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.TerminalSettlementInfo>())).DoNotCallBase();
            return fakeOperation;
        }

        private ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private TerminalDetailsController FakeController(ITerminalDetailsApi dashboardInfo, IDistributedCache mockCache, IOperation fakeOperation, ILoggingFacade fakeLogger)
        {
            var localizer = new MockStringLocalizer<TerminalDetailsController>();
            localizer[0] = new LocalizedString("GenericError", "Error occured");
            return new TerminalDetailsController(mockCache, dashboardInfo, localizer, fakeOperation, fakeLogger);
        }
    }
}

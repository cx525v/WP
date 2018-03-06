using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using System.Threading.Tasks;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.EpsLog
{
    public class TestEPSLogApiController
    {
        [Fact]
        public async Task EPSLogControllerTest_Success()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSLogRepository fakeRepo = new MockEPSLogRepository();
            IDistributedCache _cache = FakeCache();
            IEPSLogApi epslogAPI = new EPSLogApi(appSettings,fakeRepo);
            EPSLogController controller = FakeController(_cache, epslogAPI);
            var epslogs = await controller.Get("07/05/2017", "07/05/2017", null, null);
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epslogs).Value;
            var expected = JsonConvert.SerializeObject(fakeRepo.epslogs);
            var actual = JsonConvert.SerializeObject(actualResult);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EPSLogControllerTest_Fail()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSLogRepository fakeRepo = new MockEPSLogRepository();
            IDistributedCache _cache = FakeCache();
            IEPSLogApi epslogAPI = new EPSLogApi(appSettings, fakeRepo);
            var localizer = new MockStringLocalizer<EPSLogController>();
            localizer[0] = new LocalizedString("EPSLogDateRangeError", "Start or End date not provided");
            EPSLogController controller = FakeController(_cache, epslogAPI, localizer);
            

            var epslogs = await controller.Get(null, null, null, null);
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epslogs);

            Assert.Equal(actualResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal((actualResult.Value), "Start or End date not provided");
        }

        [Fact]
        public async Task EPSLogControllerTest_FailDaterange()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSLogRepository fakeRepo = new MockEPSLogRepository();
            IDistributedCache _cache = FakeCache();
            IEPSLogApi epslogAPI = new EPSLogApi(appSettings, fakeRepo);
            var localizer = new MockStringLocalizer<EPSLogController>();
            localizer[0] = new LocalizedString("EPSLogDateRangeError", "Date range should not be greater than 62 days");
            EPSLogController controller = FakeController(_cache, epslogAPI, localizer);
            
            var epslogs = await controller.Get("2017-04-01", "2017-07-01", null, null);
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epslogs);

            Assert.Equal(actualResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualResult.Value, "Date range should not be greater than 62 days");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private MockEPSLogRepository FakeRepository()
        {
            return Substitute.For<MockEPSLogRepository>();
        }
        private static EPSLogController FakeController(IDistributedCache cache, IEPSLogApi service, MockStringLocalizer<EPSLogController> localizer = null)
        {
            
            if (localizer == null)
                localizer = new MockStringLocalizer<EPSLogController>();
            IOperation fakeOperation = FakeOperation(cache);
            ILoggingFacade fakeLogger = FakeLogger();

            var controller = new EPSLogController(cache, service, localizer, fakeOperation, fakeLogger)
            {
            };
            return controller;
        }
        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private static IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            return fakeOperation;
        }
    }
}

using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using System.Linq;
using System;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.WebApi.Common;

namespace CIS.WebApi.UnitTests.EpsPetroAudit
{
    public class TestEPSPetroAuditApiController
    {
        [Fact]
        public async Task EPSPetroAuditControllerTest_SuccessAuditByVersion()
        {
            int versionId = 901;
            string start = "2017-09-10 13:53:28.710";
            string end = "2017-10-10 13:53:28.710";
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSPetroAuditRepository fakeRepo = new MockEPSPetroAuditRepository();
            IDistributedCache _cache = FakeCache();
            IEPSPetroAuditApi epsPetroAuditAPI = new EPSPetroAuditApi(appSettings, fakeRepo);
            EPSPetroAuditController controller = FakeController(_cache, epsPetroAuditAPI);

            var epsPetroAudits = await controller.Get(versionId, start, end); 
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epsPetroAudits).Value;
            
            var audits = fakeRepo.epsPetroAudits.Where(s => s.versionId == versionId && Convert.ToDateTime(start) <= s.auditDate 
            && Convert.ToDateTime(end) >= s.auditDate).ToList();
            var expected = JsonConvert.SerializeObject(audits);
            var actual = JsonConvert.SerializeObject(actualResult);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EPSPetroAuditControllerTest_FailAuditByVersion()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSPetroAuditRepository fakeRepo = new MockEPSPetroAuditRepository();
            IDistributedCache _cache = FakeCache();
            var localizer = new MockStringLocalizer<EPSPetroAuditController>();
            localizer[0] = new LocalizedString("DatesErrorMsg", "Start and end dates are mandatory");
            IEPSPetroAuditApi epsPetroAuditAPI = new EPSPetroAuditApi(appSettings, fakeRepo);
            EPSPetroAuditController controller = FakeController(_cache, epsPetroAuditAPI, localizer);

            var epsPetroAudits = await controller.Get(901, null, null); 
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epsPetroAudits);

            Assert.Equal(actualResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal((actualResult.Value), "Start and end dates are mandatory");
        }

        [Fact]
        public async Task EPSPetroAuditControllerTest_FailOnDateRangeAuditByVersion()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSPetroAuditRepository fakeRepo = new MockEPSPetroAuditRepository();
            IDistributedCache _cache = FakeCache();
            var localizer = new MockStringLocalizer<EPSPetroAuditController>();
            localizer[0] = new LocalizedString("DateRangeError", "Date range should not be greater than 30 days");
            IEPSPetroAuditApi epsPetroAuditAPI = new EPSPetroAuditApi(appSettings, fakeRepo);
            EPSPetroAuditController controller = FakeController(_cache, epsPetroAuditAPI, localizer);

            var epsPetroAudits = await controller.Get(901, "2017-09-01 13:53:28.710", "2017-10-10 13:53:28.710"); 
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epsPetroAudits);

            Assert.Equal(actualResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal((actualResult.Value), "Date range should not be greater than 30 days");
        }

        [Fact]
        public async Task EPSPetroAuditControllerTest_SuccessByAuditId()
        {
            int auditID = 2471;
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSPetroAuditRepository fakeRepo = new MockEPSPetroAuditRepository();
            IDistributedCache _cache = FakeCache();
            IEPSPetroAuditApi epsPetroAuditAPI = new EPSPetroAuditApi(appSettings, fakeRepo);
            EPSPetroAuditController controller = FakeController(_cache, epsPetroAuditAPI);

            var epsPetroAudits = await controller.GetEPSPetroAuditDetails(auditID);
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epsPetroAudits).Value;
            var expected = JsonConvert.SerializeObject(fakeRepo.epsPetroAuditDetails.Where(s => s.auditId == auditID));
            var actual = JsonConvert.SerializeObject(actualResult);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task EPSPetroAuditControllerTest_FailByAuditId()
        {
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            MockEPSPetroAuditRepository fakeRepo = new MockEPSPetroAuditRepository();
            IDistributedCache _cache = FakeCache();
            var localizer = new MockStringLocalizer<EPSPetroAuditController>();
            localizer[0] = new LocalizedString("InValidAuditIdError", "Invalid AuditId");
            IEPSPetroAuditApi epsPetroAuditAPI = new EPSPetroAuditApi(appSettings, fakeRepo);
            EPSPetroAuditController controller = FakeController(_cache, epsPetroAuditAPI, localizer);

            var epsPetroAudits = await controller.GetEPSPetroAuditDetails(0); 
            var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)epsPetroAudits);

            Assert.Equal(actualResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal((actualResult.Value), "Invalid AuditId");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private MockEPSPetroAuditRepository FakeRepository()
        {
            return Substitute.For<MockEPSPetroAuditRepository>();

        }

        private static IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            return fakeOperation;
        }
        private static EPSPetroAuditController FakeController(IDistributedCache cache, IEPSPetroAuditApi service, MockStringLocalizer<EPSPetroAuditController> localizer = null)
        {

            if (localizer == null)
                localizer = new MockStringLocalizer<EPSPetroAuditController>();
            IOperation fakeOperation = FakeOperation(cache);
            ILoggingFacade fakeLogger = FakeLogger();

            var controller = new EPSPetroAuditController(cache, service, localizer, fakeOperation, fakeLogger)
            {
            };
            return controller;
        }

    }
}

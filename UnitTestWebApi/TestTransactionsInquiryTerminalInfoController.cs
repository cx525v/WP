using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using UnitTestDataAccess;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Wp.CIS.LynkSystems.WebApi.Resources;
using Xunit;

namespace CIS.WebApi.UnitTests
{
    public class TestTransactionsInquiryTerminalInfoController
    {
        //[Fact]
        //public async Task TransactionsInquiryTerminalInfoControllerTest_Success()
        //{
        //    MockTransactionsInquiryTerminalInfoRepository fakeRepo = new MockTransactionsInquiryTerminalInfoRepository();
        //    IDistributedCache _cache = FakeCache();
        //    Settings appSettings = new Settings() { CISStageConnectionString = "Data Source=DSPADWSTAR;Initial Catalog=CIS;Integrated Security=False;User Id=CISPlusUser;Password=!Savannah123;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;" };
        //    IOptions<Settings> options = Options.Create(appSettings);

        //    ITransactionsInquiryTerminalInfoApi generalinfoapi = new TransactionsInquiryTerminalInfoApi(options);

        //    TransactionsInquiryGeneralInfo transinqgeninfo = new TransactionsInquiryGeneralInfo();

        //    TransactionsInquiryGeneralInfoController controller = FakeController(_cache, generalinfoapi, null);
        //    var transinq = await controller.Get("10006144");
        //    var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)transinq).Value;
        //    var expected = JsonConvert.SerializeObject(fakeRepo.GetTransactionsInquiryGeneralInfoAsync());
        //    var actual = JsonConvert.SerializeObject(actualResult);
        //    Assert.Equal(expected, actual);
        //}

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private MockTransactionsInquiryTerminalInfoRepository FakeRepository()
        {
            return Substitute.For<MockTransactionsInquiryTerminalInfoRepository>();
        }

        private static TransactionsInquiryTerminalInfoController FakeController(IDistributedCache cache, ITransactionsInquiryTerminalInfoApi service1, IStringLocalizer<SharedResource> resource1)
        {
            var controller = new TransactionsInquiryTerminalInfoController(cache, service1, resource1)
            {
            };
            return controller;
        }





    }
}

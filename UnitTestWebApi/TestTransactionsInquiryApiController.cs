using System.Threading.Tasks;
using UnitTestDataAccess;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Wp.CIS.LynkSystems.Model;
using Xunit;
using Microsoft.Extensions.Localization;



namespace UnitTestWebApi
{
    public class TestTransactionsInquiryApiController
    {
        //[Fact]
        //public async Task TransactionsInquiryControllerTest_Success()
        //{
        //    MockTransactionsInquiryRepository fakeRepo = new MockTransactionsInquiryRepository();
        //    IDistributedCache _cache = FakeCache();
        //    Settings appSettings = new Settings() { CISConnectionString = "Data Source=DSPADWHIST;Initial Catalog=TRANHISTMAIN;Integrated Security=False;User Id=CISPlusUser;Password=!Savannah123;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;" };
        //    IOptions<Settings> options = Options.Create(appSettings);

        //    ITransactionsInquiryApi transinqAPI = new TransactionInquiryApi(options);
        //    ITransactionsInquiryTierApi transinqTierAPI = new TransactionsInquiryTierApi(options);
        //    TransactionsInquiryGeneralInfo transinqgeninfo = new TransactionsInquiryGeneralInfo();

        //    TransactionsInquiryController controller = FakeController(_cache, transinqAPI, transinqTierAPI, null);
        //    var transinq = await controller.Get(transinqgeninfo, 1, 10006144, "6/18/2015", "7/18/2015", 0, "0", 10, 5);
        //    var actualResult = ((Microsoft.AspNetCore.Mvc.ObjectResult)transinq).Value;
        //    var expected = JsonConvert.SerializeObject(fakeRepo.GetTransactionsInquiryAsync());
        //    var actual = JsonConvert.SerializeObject(actualResult);
        //    Assert.Equal(expected, actual);
        //}

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private MockTransactionsInquiryRepository FakeRepository()
        {
            return Substitute.For<MockTransactionsInquiryRepository>();
        }

        private static TransactionsInquiryDetailsInfoController FakeController(IDistributedCache cache, ITransactionsInquiryDetailsInfoApi service2, ITransactionsInquiryDetailsInfoTierApi service3, IStringLocalizer<TransactionsInquiryDetailsInfoController> localiz)
        {
            var controller = new TransactionsInquiryDetailsInfoController(cache, service2, service3, localiz)
            {
            };
            return controller;
       }
    }
}

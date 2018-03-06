using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Worldpay.CIS.DataAccess;
using Worldpay.CIS.DataAccess.TransactionsInqTerminalInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.WebApi.Resources;

namespace CIS.WebApi.UnitTests.TransactionsInqTerminalInfo
{
    public class TestTransactionsInqTerminalInfoApiController
    {
        //private IStringLocalizer<SharedResource> localizer;

        [Fact]
        public async Task TransactionsInqTerminalInfoControllerTest_Success()
        {
            string id = "10006144";
            // Arrange
            ITransactionsInqTerminalInfoRepository mockRepo = Substitute.For<ITransactionsInqTerminalInfoRepository>();
            mockRepo = new MockTransactionsInqTerminalInfoRepository();
            
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            ITransactionsInquiryTerminalInfoApi terminfo = new TransactionsInquiryTerminalInfoApi(appSettings, mockRepo);

            TransactionsInquiryTerminalInfoController controller = new TransactionsInquiryTerminalInfoController(mockCache, terminfo,null);
            
            // Act
            var terminalinfo = await controller.Get(id);

            // Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalinfo).Value;

            Assert.Equal(((Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo)actualRecord).terminalNbr.ToString(), "10006144");
        }
        

        [Fact]
        public async Task TransactionsInqTerminalRetrievalFromCache_NotAvailableInCache()
        {
                string id = "10006143";
                IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
                ITransactionsInqTerminalInfoRepository mockRepo = Substitute.For<ITransactionsInqTerminalInfoRepository>();
                //var mockRepo = new MockTransactionsInqTerminalInfoRepository();
                mockRepo = new MockTransactionsInqTerminalInfoRepository();
            
                IDistributedCache mockCache = FakeCache();
                mockCache = new MockCachTransactionsInqTerminalInfo();
                ITransactionsInquiryTerminalInfoApi terminalinfo = new TransactionsInquiryTerminalInfoApi(appSettings, mockRepo);

                //Retrieving from Cache first. 
                var cacheRetrievedData
                    = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());

                //Does not exist in mock Cache. Hence null
                Assert.Equal(cacheRetrievedData, null);

                //since no data in cache, now call the controller. The controller retrieves data and also adds to cache 
                TransactionsInquiryTerminalInfoController controller = new TransactionsInquiryTerminalInfoController(mockCache, terminalinfo, null);
            
                //Retrieve the data from controller and also check for the data in the cache. 
                var terminaldata = await controller.Get(id);
                       

                var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminaldata).Value;
                //var cacheretrieveddata = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());

                // Check the retrieved data
                Assert.Equal(((Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo)actualRecord).terminalNbr.ToString(), "10006143");
            
                // Make sure the data retrieved from controller is same as the data from the cache
                //Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(cacheretrieveddata));
        }

        [Fact]
        public async Task TransactionsInqTerminalRetrievalFromCache_AvailableInCache()
        {
            try
            {
                string id = "10006144";

                IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
                ITransactionsInqTerminalInfoRepository mockRepo = Substitute.For<ITransactionsInqTerminalInfoRepository>();

                IDistributedCache mockCache = FakeCache();
                mockCache = new MockCachTransactionsInqTerminalInfo();
                ITransactionsInquiryTerminalInfoApi terminalinfo = new TransactionsInquiryTerminalInfoApi(appSettings, mockRepo);

                var cacheRetrievedData
                    = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());

                Assert.NotNull(cacheRetrievedData);
                //since no data in cache, now get data from DB
                TransactionsInquiryTerminalInfoController controller = new TransactionsInquiryTerminalInfoController(mockCache, terminalinfo, null);

                var terminaldata = await controller.Get(id);
                var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminaldata).Value;
                var cacheretrieveddata = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());
                Assert.Equal(((Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo)actualRecord).terminalNbr.ToString(), "10006144");

                Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(cacheretrieveddata));
            }
            catch(System.Exception exc)
            {
              string msg =   exc.Message.ToString();

            }
        }
                
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITransactionsInqTerminalInfoRepository FakeRepository()
        {
            return Substitute.For<ITransactionsInqTerminalInfoRepository>();

        }

        private static TransactionsInquiryTerminalInfoController FakeController(IDistributedCache cache, ITransactionsInquiryTerminalInfoApi transinqterminalinfo,
            IStringLocalizer<SharedResource> localizer)
        {
            var controller = new TransactionsInquiryTerminalInfoController(cache, transinqterminalinfo, localizer)
            {
            };
            return controller;

        }


        private static IConfigurationRoot getConfigurationBuilder()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

    }
}

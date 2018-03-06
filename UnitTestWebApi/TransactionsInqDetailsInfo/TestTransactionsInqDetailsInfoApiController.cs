using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Worldpay.CIS.DataAccess;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Wp.CIS.LynkSystems.Model;
using Microsoft.Extensions.Localization;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfoTier;
using CIS.WebApi.UnitTests.TransactionsInqDetailInfoTier;
using System.Linq;

namespace CIS.WebApi.UnitTests.TransactionsInqDetailsInfo
{
    public class TestTransactionsInqDetailsInfoApiController
    {
        [Fact]
        public async Task TransactionsInqControllerTest_Success()
        {
            // Arrange
            ITransactionsInqDetailsInfoRepository mockRepo = Substitute.For<ITransactionsInqDetailsInfoRepository>();
            mockRepo = new MockTransactionsInqDetailsInfoRepository();


            ITransactionsInqDetailsInfoTierRepository mockRepo2 = Substitute.For<ITransactionsInqDetailsInfoTierRepository>();
            
            mockRepo2 = new MockTransactionsInqDetailsInfoTierRepository();


            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            ITransactionsInquiryDetailsInfoApi transInqApi = new TransactionInquiryDetailsInfoApi(appSettings, mockRepo);
            ITransactionsInquiryDetailsInfoTierApi transInqTierApi = new TransactionsInquiryDetailsTierApi(appSettings, mockRepo2);
            TransactionsInquiryGeneralInfo transinqgeninfo = new TransactionsInquiryGeneralInfo();

            TransactionsInquiryDetailsInfoController controller = new TransactionsInquiryDetailsInfoController(mockCache, transInqApi, transInqTierApi, null);

            // Act
            var transinquiries = await controller.Get(transinqgeninfo, 1, 10006144, "6/18/2015", "7/18/2015", 0, "0", 10, 5);

            // Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)transinquiries).Value;
            ////Assert.Equal(((Wp.CIS.LynkSystems.Model.MerchantProfile)actualRecord).MerchantNbr.ToString(), "007");
        }

        [Fact]
        public async Task TransactionsInqDetailsInfoRetrievalFromCache_NotAvailableInCache()
        {
            string id = "10006143";

            var expectedRecord = new TransactionsInquiry
            {
                ARN = "Test ARN Code",
                CompleteCode = 555555
            };

            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ITransactionsInqDetailsInfoRepository mockRepo = Substitute.For<ITransactionsInqDetailsInfoRepository>();
            mockRepo.GetTransactionInquiryCardNoResults(1, "", 2, "", "", null, 3, 4, 5).ReturnsForAnyArgs(new GenericPaginationResponse<TransactionsInquiry>());
            //var mockRepo = new MockTransactionsInqTerminalInfoRepository();
            mockRepo = new MockTransactionsInqDetailsInfoRepository();

            ITransactionsInqDetailsInfoTierRepository mockRepo2 = Substitute.For<ITransactionsInqDetailsInfoTierRepository>();
            mockRepo2 = new MockTransactionsInqDetailsInfoTierRepository();



            IDistributedCache mockCache = FakeCache();
            mockCache = new MockCacheTransactionsInqDetailsInfoTier();

            ITransactionsInquiryDetailsInfoApi detailsinfo = new TransactionInquiryDetailsInfoApi(appSettings, mockRepo);
            ITransactionsInquiryDetailsInfoTierApi detailstierinfo = new TransactionsInquiryDetailsTierApi(appSettings, mockRepo2);
            TransactionsInquiryGeneralInfo transinqgeninfo = new TransactionsInquiryGeneralInfo();

            //Retrieving from Cache first. 
            var cacheRetrievedData
                = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());

            //Does not exist in mock Cache. Hence null
            Assert.Equal(cacheRetrievedData, null);

            //since no data in cache, now call the controller. The controller retrieves data and also adds to cache 
            TransactionsInquiryDetailsInfoController controller = new TransactionsInquiryDetailsInfoController(mockCache, detailsinfo, detailstierinfo, null);

            //Retrieve the data from controller and also check for the data in the cache. 
            var detailsdata = await controller.Get(transinqgeninfo, 1, 10006144, "6/18/2015", "7/18/2015", 0, "0", 10, 5);


            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)detailsdata).Value;
            var cacheretrieveddata = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.TransactionsInquiryGeneralInfo());

            var firstRecord = ((GenericPaginationResponse<TransactionsInquiry>)actualRecord).ReturnedRecords.FirstOrDefault();

            // Check the retrieved data
            Assert.Equal(firstRecord.ARN, "Test ARN Code");

            // Make sure the data retrieved from controller is same as the data from the cache
            //Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(cacheretrieveddata));
            Assert.Equal(JsonConvert.SerializeObject(firstRecord), JsonConvert.SerializeObject(expectedRecord));
        }
        
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITransactionsInqDetailsInfoRepository FakeRepository()
        {
            return Substitute.For<ITransactionsInqDetailsInfoRepository>();

        }

        private static TransactionsInquiryDetailsInfoController FakeController(IDistributedCache cache, ITransactionsInquiryDetailsInfoApi transinqApi, ITransactionsInquiryDetailsInfoTierApi transinqTierApi, IStringLocalizer<TransactionsInquiryDetailsInfoController> localizer)
        {
            var controller = new TransactionsInquiryDetailsInfoController(cache, transinqApi, transinqTierApi, localizer)
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

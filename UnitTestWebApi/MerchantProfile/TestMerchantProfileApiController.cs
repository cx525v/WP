using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using Worldpay.CIS.DataAccess;
using Worldpay.CIS.DataAccess.MerchantProfile;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.Logging.Providers.Log4Net.Facade;
using System.Threading;
using Worldpay.Logging.Contracts.Enums;
using Worldpay.Logging.Contracts.Models;
namespace CIS.WebApi.UnitTests.MerchantProfile
{
    public class TestMerchantProfileApiController
    {
        [Fact]
        public async Task MerchantProfileControllerTest_Success()
        {
            // Arrange
            IMerchantProfileRepository mockRepo = Substitute.For<IMerchantProfileRepository>();
             mockRepo = new MockMerchantRepository();
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            
            IMerchantProfileApi merchantProfileApi = new MerchantProfileApi(appSettings, mockRepo);
            MerchantProfileController controller = new MerchantProfileController(mockCache, merchantProfileApi, FakeLogger());
            
            // Act
            var merchProfiles = await controller.Get(191807);

            // Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchProfiles).Value;
            Assert.Equal(((Wp.CIS.LynkSystems.Model.MerchantProfile)actualRecord).MerchantNbr.ToString(), "007");


        }
       
        [Fact]
        public async Task MerchantProfileRetrievalFromCache_NotAvailableInCache()
        {
            int id = 191807;
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IMerchantProfileRepository mockRepo = Substitute.For<IMerchantProfileRepository>();
            mockRepo = new MockMerchantRepository();
            IDistributedCache mockCache = FakeCache();
            mockCache = new MockCacheMerchantProfile();
            IMerchantProfileApi merchantProfileApi = new MerchantProfileApi(appSettings, mockRepo);
            //Retrieving from Cache first. 
            var cacheRetrievedData
                = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.MerchantProfile());


            //Does not exist in mock Cache. Hence null
            Assert.Equal(cacheRetrievedData, null);

            //since no data in cache, now call the controller. The controller retrieves data and also adds to cache 
            MerchantProfileController controller = new MerchantProfileController(mockCache, merchantProfileApi, FakeLogger());
            //Retrieve the data from controller and also check for the data in the cache. 
            var merchProfile = await controller.Get(id);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchProfile).Value;
            var cacheretrieveddata = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.MerchantProfile());
            // Check the retrieved data
            Assert.Equal(((Wp.CIS.LynkSystems.Model.MerchantProfile)actualRecord).MerchantNbr.ToString(), "007");
            // Make sure the data retrieved from controller is same as the data from the cache
           // Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(cacheretrieveddata));
        }
        [Fact]
        public async Task MerchantProfileRetrievalFromCache_AvailableInCache()
        {
            int id = 191807;
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IMerchantProfileRepository mockRepo = Substitute.For<IMerchantProfileRepository>();
            mockRepo = new MockMerchantRepository();
            IDistributedCache mockCache = FakeCache();
            mockCache = new MockCacheMerchantProfile();
            IMerchantProfileApi merchantProfileApi = new MerchantProfileApi(appSettings, mockRepo);
            var cacheRetrievedData
                = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.MerchantProfile());

            Assert.Equal(cacheRetrievedData, null);
            //since no data in cache, now get data from DB
            MerchantProfileController controller = new MerchantProfileController(mockCache, merchantProfileApi, FakeLogger());

            var merchProfile = await controller.Get(id);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchProfile).Value;
            var cacheretrieveddata = new Operation(mockCache).RetrieveCache(id.ToString(), new Wp.CIS.LynkSystems.Model.MerchantProfile());
            Assert.Equal(((Wp.CIS.LynkSystems.Model.MerchantProfile)actualRecord).MerchantNbr.ToString(), "007");

            //Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(cacheretrieveddata));
        }
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IMerchantProfileRepository FakeRepository()
        {
            return Substitute.For<IMerchantProfileRepository>();

        }
        private static MerchantProfileController FakeController(IDistributedCache cache, IMerchantProfileApi mprofile, ILoggingFacade loggingFacade)
        {
             return new MerchantProfileController(cache, mprofile, loggingFacade);

        }
        private static IConfigurationRoot getConfigurationBuilder()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        private ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

    }
}

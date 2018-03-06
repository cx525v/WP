using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.RecentStatement;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;
using Newtonsoft.Json;
using System;
using CIS.WebApi.UnitTests.Common;
using Worldpay.Logging.Providers.Log4Net.Facade;

namespace Worldpay.CIS.WebApi.UnitTests.RecentStatement
{
    public class TestRecentStatementController
    {
        [Fact]
        public void RecentStatementControllerTest_ModelState_Invalid()
        {
            //Arrange
            string merchantNbr = "542929801430265";
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IStringLocalizer<RecentStatementController> localizer
                            = Substitute.For<IStringLocalizer<RecentStatementController>>();

            IRecentStatementApi recentStatementApi = Substitute.For<IRecentStatementApi>();

            IDistributedCache mockCache = FakeCache();
            ILoggingFacade fakeLogger = FakeLogger();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);



            RecentStatementController controller =
                new RecentStatementController(mockCache, recentStatementApi, localizer, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetRecentStatement(merchantNbr);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");
        }

        [Fact]
        //Mock API Call and unit test for the API call with returning mock RecentStatementList.
        public async Task RecentStatementControllerTest_Success()
        {
            // Arrange
            string merchantNbr = "542929801430265";
            
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ILoggingFacade fakeLogger = FakeLogger();
            IRecentStatementRepository mockRepo = Substitute.For<IRecentStatementRepository>();
            IStringLocalizer<RecentStatementController> localizer
                       = Substitute.For<IStringLocalizer<RecentStatementController>>();
            IRecentStatementApi mockRecentStatementApi = Substitute.For<IRecentStatementApi>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>())).DoNotCallBase();
            RecentStatementController controller = new RecentStatementController(mockCache, mockRecentStatementApi, localizer, fakeOperation, fakeLogger);
            mockRecentStatementApi.GetRecentStatementAsync(merchantNbr).ReturnsForAnyArgs(expectedResult);
            // Act
            var recentStatementList = await controller.GetRecentStatement(merchantNbr);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)recentStatementList).Value;

            // Assert
            Assert.Equal(((IList<Wp.CIS.LynkSystems.Model.RecentStatement>)actualRecord).Count, 1);
            Assert.Equal(actualRecord, expectedResult.Result);
        }


        //[Fact]
        //public async Task RecentStatementControllerTest_GetAnException()
        //{
        //    Arrange
        //    string merchantNbr = "542929801430265";

        //    MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
        //    ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

        //    IDistributedCache mockCache = Substitute.For<IDistributedCache>();
        //    ILoggingFacade fakeLogger = FakeLogger();
        //    IStringLocalizer<RecentStatementController> localizer
        //              = Substitute.For<IStringLocalizer<RecentStatementController>>();
        //    string key = "GenericError";
        //    string value = "Error Occurred";
        //    var localizedString = new LocalizedString(key, value);
        //    localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
        //    IRecentStatementApi recentStatementApi = Substitute.For<IRecentStatementApi>();
        //    IOperation fakeOperation = Substitute.For<Operation>(mockCache);
        //    fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>())).DoNotCallBase();



        //    RecentStatementController controller = new RecentStatementController(mockCache, recentStatementApi, localizer, fakeOperation, null);
        //    recentStatementApi.GetRecentStatementAsync(merchantNbr).ReturnsForAnyArgs(expectedResult);


        //    recentStatementApi.GetRecentStatementAsync(merchantNbr).Throws(new Exception());
        //    Act
        //   var dinfo = await controller.GetRecentStatement(merchantNbr);

        //    var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

        //    Assert

        //    Assert.Equal(actualRecord.StatusCode, 500);

        //    Assert.Equal(actualRecord.Value, "Error Occurred");

        //}

        [Fact]
        public async Task RecentStatementControllerTest_RetrievalFromCache_AvailableInCache()
        {
            // Arrange
            string merchantNbr = "542929801430265";

            string key = merchantNbr;
            MockRecentStatementRepository repository = new MockRecentStatementRepository();
            var expectedResult = repository.GetMockData();

            IDistributedCache mockCache = FakeCache();
            ILoggingFacade fakeLogger = FakeLogger();
            IRecentStatementRepository mockRepo = Substitute.For<IRecentStatementRepository>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IRecentStatementApi dAPI = new RecentStatementApi(appSettings, mockRepo);

            var localizer = new MockStringLocalizer<RecentStatementController>();
            localizer[0] = new LocalizedString("GenericError", "Error occured");
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.RetrieveCache(key, Arg.Any<List<Wp.CIS.LynkSystems.Model.RecentStatement>>()).ReturnsForAnyArgs(expectedResult.Result);

            //since no data in cache, now call the controller. The controller retrieves data and also adds to cache 
            RecentStatementController controller = new RecentStatementController(mockCache, dAPI, localizer, fakeOperation,fakeLogger);
            // Act
            //Retrieve the data from controller and also check for the data in the cache. 
            var merchantNumber = await controller.GetRecentStatement(merchantNbr);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchantNumber).Value;

            // Assert
            // Check the retrieved data
            // Make sure the data retrieved from controller is same as the data from the cache
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }

        [Fact]
        public async Task RecentStatementControllerTest_NoDataFound()
        {
            // Arrange
            string merchantNbr = "542929801430265";
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();

            IStringLocalizer<RecentStatementController> localizer
                       = Substitute.For<IStringLocalizer<RecentStatementController>>();
            string key = "NoDataFound";
            string value = "No data found for provided lid.";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            IRecentStatementApi mockRecentStatementApi = Substitute.For<IRecentStatementApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>())).DoNotCallBase();
            RecentStatementController controller = new RecentStatementController(mockCache, mockRecentStatementApi, localizer, fakeOperation, FakeLogger());
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> result = new ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>>();
           
            mockRecentStatementApi.GetRecentStatementAsync(merchantNbr).ReturnsForAnyArgs(result);

            // Act
            var recentStatementList = await controller.GetRecentStatement(merchantNbr);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)recentStatementList);

            // Assert

            Assert.Equal(actualRecord.StatusCode, 200);

            Assert.Equal(actualRecord.Value, "No data found for provided lid.");

        }
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IRecentStatementRepository FakeRepository()
        {
            return Substitute.For<IRecentStatementRepository>();
        }

        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private RecentStatementController FakeController(IDistributedCache cache,
                                                           IRecentStatementApi recentStatementApi,
                                                           IStringLocalizer<RecentStatementController> localizer,
                                                           IOperation operation, ILoggingFacade fakeLogger)
        {
            return new RecentStatementController(cache, recentStatementApi, localizer, operation, fakeLogger);
        }
    }
}

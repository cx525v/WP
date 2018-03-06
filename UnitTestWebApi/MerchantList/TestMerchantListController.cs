using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NSubstitute;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.MerchantList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Microsoft.Extensions.Configuration;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.MerchantList
{
    public class TestContactListController : Controller
    {

        [Fact]
        public void MerchantListControllerTest_ModelState_Invalid()
        {
            //Arrange
            int CustomerID = 191807;
            
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            PaginationMerchant page = mockMerchantListRepository.GetPagination();
            MerchantListInput pageinput = new MerchantListInput();
            pageinput.LIDValue = CustomerID.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;


            IStringLocalizer<MerchantListController> localizer
                            = Substitute.For<IStringLocalizer<MerchantListController>>();

            IMerchantListApi merchantListApi = Substitute.For<IMerchantListApi>();
            
            IDistributedCache mockCache = FakeCache();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);
            


            MerchantListController controller = 
                new MerchantListController( mockCache, merchantListApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetMerchantList(pageinput);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        //[Fact]
        /////Unit Test for the RetrieveCache()
        //public async Task MerchantListControllerTest_GetDataFromCache()
        //{
        //    int CustomerID = 191809;
            
        //    IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
        //    configurationRoot = GetConfiguration(configurationRoot);
        //    MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
        //    ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);

        //    PaginationMerchant page = mockMerchantListRepository.GetPagination();
        //    MerchantListInput pageinput = new MerchantListInput();
        //    pageinput.LIDValue = CustomerID.ToString();
        //    pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
        //    pageinput.Page = page;

        //    IDistributedCache mockCache = Substitute.For<IDistributedCache>();
        //    IMerchantListRepository mockRepo = Substitute.For<IMerchantListRepository>();
        //    IStringLocalizer<MerchantListController> localizer
        //               = Substitute.For<IStringLocalizer<MerchantListController>>();
        //    IMerchantListApi mockMerchantListApi = Substitute.For<IMerchantListApi>();
        //    ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

        //    loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

        //    IOperation fakeOperation = Substitute.For<Operation>(mockCache);
        //    fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Merchant>>())).DoNotCallBase();

        //    fakeOperation.RetrieveCache("_FakeStringID", new GenericPaginationResponse<Merchant>()).ReturnsForAnyArgs(expectedResult.Result);
        //    MerchantListController controller = new MerchantListController(mockCache, mockMerchantListApi, localizer, fakeOperation, loggingFacade);

            
        //    //ACT

        //    var merchList = await controller.GetMerchantList(pageinput);
        //    var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).Value;

        //    //Assert
        //    Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        //}



        [Fact]
        //Mock API Call and unit test for the API call with returning mock MerchantList.
        public async Task MerchantListControllerTest_Success()
        {
            // Arrange
            int CustomerID = 191809;
            string mid = "191807";
            
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();
            MerchantListInput pageinput = new MerchantListInput();
            pageinput.LIDValue = CustomerID.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IMerchantListRepository mockRepo = Substitute.For<IMerchantListRepository>();
            IStringLocalizer<MerchantListController> localizer
                       = Substitute.For<IStringLocalizer<MerchantListController>>();
            IMerchantListApi mockMerchantListApi = Substitute.For<IMerchantListApi>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Merchant>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Merchant>>())).DoNotCallBase();
            MerchantListController controller = new MerchantListController(mockCache, mockMerchantListApi, localizer, fakeOperation, loggingFacade);
            mockMerchantListApi.GetMerchantListAsync(CustomerID, page).ReturnsForAnyArgs(expectedResult);
            // Act
            var merchList = await controller.GetMerchantList(pageinput);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).Value;
            string merchInfo = ((IList<Merchant>)((GenericPaginationResponse<Merchant>)actualRecord).ReturnedRecords).Where(x => x.MID == mid).FirstOrDefault().Name;


            // Assert
            var recordCount = ((GenericPaginationResponse<Merchant>)actualRecord).ReturnedRecords;
            Assert.Equal(recordCount.ToList().Count, 2);
            

            Assert.Equal(merchInfo, "ABC Corp");
        }
        

        [Fact]
        public async Task MerchantListControllerTest_NoDataFound()
        {
            // Arrange
            int CustomerID = 191809;
            
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();
            MerchantListInput pageinput = new MerchantListInput();
            pageinput.LIDValue = CustomerID.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();

            IStringLocalizer<MerchantListController> localizer
                       = Substitute.For<IStringLocalizer<MerchantListController>>();
            string key = "NoDataFound";
            string value = "No data found for provided ID";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();
            IMerchantListApi merchantListApi = Substitute.For<IMerchantListApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Merchant>>())).DoNotCallBase();            

            ApiResult<GenericPaginationResponse<Merchant>> response = new ApiResult<GenericPaginationResponse<Merchant>>();
            response.Result = new GenericPaginationResponse<Merchant>() ;
           

            merchantListApi.GetMerchantListAsync(CustomerID, page).ReturnsForAnyArgs(response);
            MerchantListController controller
                       = new MerchantListController(mockCache, merchantListApi, localizer, fakeOperation, loggingFacade);


            // Act
            var merchList = await controller.GetMerchantList(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).StatusCode, 200);
            var actualMerchantList = ((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).Value;

            Assert.Equal(((GenericPaginationResponse<Merchant>)actualMerchantList).ModelMessage, localizer["NoDataFound"].Value);

        }

        [Fact]
        public async Task MerchantListControllerTest_GetAnException()
        {
            // Arrange
            int CustomerID = 191809;
            
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();
            MerchantListInput pageinput = new MerchantListInput();
            pageinput.LIDValue = CustomerID.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Merchant>>())).DoNotCallBase();

            IStringLocalizer<MerchantListController> localizer
                       = Substitute.For<IStringLocalizer<MerchantListController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IMerchantListApi merchantListApi = Substitute.For<IMerchantListApi>();

            MerchantListController controller
                       = new MerchantListController(mockCache, merchantListApi, localizer, fakeOperation, loggingFacade);
            
            merchantListApi.GetMerchantListAsync(CustomerID, page).Throws(new Exception());
            // Act
            var merchList = await controller.GetMerchantList(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)merchList).Value, localizer["InternalServerError"].Value);

        }


        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IMerchantListRepository FakeRepository()
        {
            return Substitute.For<IMerchantListRepository>();

        }
        

        private static IConfigurationRoot GetConfiguration(IConfigurationRoot configurationRoot)
        {
            configurationRoot["DatabaseDefaults:MaxNumberOfRecordsToReturn"] = "500";
            return configurationRoot;
        }

    }
}

using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.TerminalList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Microsoft.Extensions.Configuration;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.TerminalList
{
    public class TestTerminalListController
    {

        [Fact]
        //UnitTest for validating the Invalid Model Data.
        public void TerminalListController_ModelState_Invalid()
        {
            //Arrange
            int TerminalNbr = 589587;
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            TerminalListInput pageinput = new TerminalListInput();
            pageinput.LIDValue = TerminalNbr.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
       
            IStringLocalizer<TerminalListController> localizer
                            = Substitute.For<IStringLocalizer<TerminalListController>>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = FakeCache();
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            
            TerminalListController controller = new TerminalListController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetTerminalList(pageinput);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        [Fact]
        ///Unit Test for the RetrieveCache()
        public async Task MerchantListControllerTest_GetDataFromCache()
        {
            int TerminalNbr = 589587;
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
           
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            TerminalListInput pageinput = new TerminalListInput();
            pageinput.LIDValue = TerminalNbr.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();
            IStringLocalizer<TerminalListController> localizer
                       = Substitute.For<IStringLocalizer<TerminalListController>>();
            ITerminalListApi mockTerminalListApi = Substitute.For<ITerminalListApi>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();

            fakeOperation.RetrieveCache("FakeStringID", new GenericPaginationResponse<Terminal>()).ReturnsForAnyArgs(expectedResult.Result);

            TerminalListController controller = new TerminalListController(mockCache, mockTerminalListApi, localizer, fakeOperation, loggingFacade);

            
            //ACT
            var terminalList = await controller.GetTerminalList(pageinput);

            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            //Assert
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }

        [Fact]
        public async Task TerminalListControllerTest_Success()
        {
            // Arrange
            int TerminalNbr = 589587;
            string TerminalID = "LK429486";
          
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            TerminalListInput pageinput = new TerminalListInput();
            pageinput.LIDValue = TerminalNbr.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IStringLocalizer<TerminalListController> localizer
                       = Substitute.For<IStringLocalizer<TerminalListController>>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            TerminalListController controller
                       = new TerminalListController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTerminalListAsync(TerminalNbr, page).ReturnsForAnyArgs(expectedResult);
            // Act
            var terminalList = await controller.GetTerminalList(pageinput);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            string terminalInfo = ((IList<Terminal>)((GenericPaginationResponse<Terminal>)actualRecord).ReturnedRecords).Where(x => x.TerminalID == TerminalID).FirstOrDefault().Software;


            // Assert
            var recordCount = ((GenericPaginationResponse<Terminal>)actualRecord).ReturnedRecords;
            Assert.Equal(recordCount.ToList().Count, 1);
            //Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(terminalInfo, "LSPR3271");
        }

        
        [Fact]
        public async Task TerminalListControllerTest_NoDataFound()
        {
            // Arrange
            int TerminalNbr = 589587;
            
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            TerminalListInput pageinput = new TerminalListInput();
            pageinput.LIDValue = TerminalNbr.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IStringLocalizer<TerminalListController> localizer = Substitute.For<IStringLocalizer<TerminalListController>>();
            string key = "NoDataFound";
            string value = "No data found for provided ID";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);


            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();


            ApiResult<GenericPaginationResponse<Terminal>> response = new ApiResult<GenericPaginationResponse<Terminal>>();
            response.Result = new GenericPaginationResponse<Terminal>();

            terminalListApi.GetTerminalListAsync(TerminalNbr, page).ReturnsForAnyArgs(response);
            TerminalListController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);


            // Act
            var terminalList = await fakecontroller.GetTerminalList(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 200);
            var actualTerminalList = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;

            Assert.Equal(((GenericPaginationResponse<Terminal>)actualTerminalList).ModelMessage, localizer["NoDataFound"].Value);
            

        }

        [Fact]
        public async Task TerminalListControllerTest_GetAnException()
        {
            // Arrange
            int TerminalNbr = 589587;
          
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            TerminalListInput pageinput = new TerminalListInput();
            pageinput.LIDValue = TerminalNbr.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<TerminalListController> localizer = Substitute.For<IStringLocalizer<TerminalListController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();

            TerminalListController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTerminalListAsync(TerminalNbr, page).Throws(new Exception());
            // Act
            var terminalList = await fakecontroller.GetTerminalList(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value, localizer["InternalServerError"].Value);

        }

        #region Old Test Cases
        [Fact]
        //UnitTest for validating the Invalid Model Data.
        public void TerminalListController_ModelState_Invalid_Old()
        {
            //Arrange
            string TerminalNbr = "589587";

            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);

            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<TerminalListController> localizer
                            = Substitute.For<IStringLocalizer<TerminalListController>>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = FakeCache();
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);

            TerminalListController controller = new TerminalListController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetTerminalList(Convert.ToInt32(TerminalNbr));

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        [Fact]
        ///Unit Test for the RetrieveCache()
        public async Task MerchantListControllerTest_GetDataFromCache_Old()
        {
            string TerminalNbr = "589587";
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();

            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();
            IStringLocalizer<TerminalListController> localizer
                       = Substitute.For<IStringLocalizer<TerminalListController>>();
            ITerminalListApi mockTerminalListApi = Substitute.For<ITerminalListApi>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();

            fakeOperation.RetrieveCache("FakeStringID", new List<Terminal>()).ReturnsForAnyArgs(expectedResult.Result);

            TerminalListController controller = new TerminalListController(mockCache, mockTerminalListApi, localizer, fakeOperation, loggingFacade);


            //ACT
            var terminalList = await controller.GetTerminalList(Convert.ToInt32(TerminalNbr));

            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            //Assert
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }

        [Fact]
        public async Task TerminalListControllerTest_Success_Old()
        {
            // Arrange
            string TerminalNbr = "589587";
            string TerminalID = "LK429486";
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IStringLocalizer<TerminalListController> localizer
                       = Substitute.For<IStringLocalizer<TerminalListController>>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<ICollection<Terminal>>())).DoNotCallBase();
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            TerminalListController controller
                       = new TerminalListController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).ReturnsForAnyArgs(expectedResult);
            // Act
            var terminalList = await controller.GetTerminalList(Convert.ToInt32(TerminalNbr));
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            string terminalInfo = ((IList<Terminal>)((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value).Where(x => x.TerminalID == TerminalID).FirstOrDefault().Software;


            // Assert

            Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(terminalInfo, "LSPR3271");
        }


        [Fact]
        public async Task TerminalListControllerTest_FailToRetrieveData_Old()
        {
            // Arrange
            string TerminalNbr = "589587";

            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);

            IStringLocalizer<TerminalListController> localizer = Substitute.For<IStringLocalizer<TerminalListController>>();
            string key = "NoDataFound";
            string value = "No data found for provided ID";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);


            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ApiResult<ICollection<Terminal>> response = new ApiResult<ICollection<Terminal>>();


            terminalListApi.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).ReturnsForAnyArgs(response);
            TerminalListController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);


            // Act
            var terminalList = await fakecontroller.GetTerminalList(Convert.ToInt32(TerminalNbr));

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 200);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value, localizer["NoDataFound"].Value);

        }

        [Fact]
        public async Task TerminalListControllerTest_GetAnException_Old()
        {
            // Arrange
            string TerminalNbr = "589587";

            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<TerminalListController> localizer = Substitute.For<IStringLocalizer<TerminalListController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);


            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            TerminalListController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).Throws(new Exception());
            // Act
            var terminalList = await fakecontroller.GetTerminalList(Convert.ToInt32(TerminalNbr));

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value, localizer["InternalServerError"].Value);

        }
        #endregion
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITerminalListRepository FakeRepository()
        {
            return Substitute.For<ITerminalListRepository>();

        }

        private static TerminalListController FakeController(
                                             IDistributedCache cache,
                                             ITerminalListApi terminalApi,
                                             IStringLocalizer<TerminalListController> localizer,
                                             IOperation operation,
                                             ILoggingFacade loggingFacade)
        {
            return new TerminalListController(cache, terminalApi, localizer, operation, loggingFacade);

        }
        
    }
}

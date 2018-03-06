using System.Threading;
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
using Worldpay.CIS.DataAccess.TransactionHistory;
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
using Worldpay.Logging.Contracts.Models;
using Worldpay.LogicFacade.Contracts.Interfaces;

namespace CIS.WebApi.UnitTests.TransactionHistoryList
{
    public class TestTransactionHistoryController
    {

        [Fact]
        //UnitTest for validating the Invalid Model Data.
        public void TransactionHistoryController_ModelState_Invalid()
        {
            //Arrange
            string terminalId = "LK429486";
            string transactionType = "Credit";


            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            TransactionHistoryInput pageinput = new TransactionHistoryInput();
            pageinput.LIDValue = terminalId.ToString();
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
       
            IStringLocalizer<TransactionHistoryController> localizer
                            = Substitute.For<IStringLocalizer<TransactionHistoryController>>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IDistributedCache mockCache = FakeCache();
            ITransactionHistoryApi terminalListApi = Substitute.For<ITransactionHistoryApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            TransactionHistoryController controller = new TransactionHistoryController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetTransactionHistory(pageinput);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        [Fact]
        ///Unit Test for the RetrieveCache()
        public async Task TransactionHistoryControllerTest_GetDataFromCache()
        {
            string terminalId = "LK429486";
            string transactionType = "Credit";

            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
           
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            TransactionHistoryInput pageinput = new TransactionHistoryInput();
            pageinput.LIDValue = terminalId;
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ITransactionHistoryRepository mockRepo = Substitute.For<ITransactionHistoryRepository>();
            IStringLocalizer<TransactionHistoryController> localizer
                       = Substitute.For<IStringLocalizer<TransactionHistoryController>>();
            ITransactionHistoryApi mockTransactionHistoryApi = Substitute.For<ITransactionHistoryApi>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.TransactionHistory>>())).DoNotCallBase();

            fakeOperation.RetrieveCache("FakeStringID", new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>()).ReturnsForAnyArgs(expectedResult.Result);

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            TransactionHistoryController controller = new TransactionHistoryController(mockCache, mockTransactionHistoryApi, localizer, fakeOperation, loggingFacade);

            
            //ACT
            var terminalList = await controller.GetTransactionHistory(pageinput);

            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            //Assert
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }

        [Fact]
        public async Task TransactionHistoryControllerTest_Success()
        {
            // Arrange

            string terminalId = "LK429486";
            string transactionType = "Credit";


            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            TransactionHistoryInput pageinput = new TransactionHistoryInput();
            pageinput.LIDValue = terminalId;
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IStringLocalizer<TransactionHistoryController> localizer
                       = Substitute.For<IStringLocalizer<TransactionHistoryController>>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.TransactionHistory>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.TransactionHistory>>())).DoNotCallBase();
            ITransactionHistoryApi terminalListApi = Substitute.For<ITransactionHistoryApi>();

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            TransactionHistoryController controller
                       = new TransactionHistoryController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTransactionHistoryAsync(terminalId, page).ReturnsForAnyArgs(expectedResult);
            // Act
            var terminalList = await controller.GetTransactionHistory(pageinput);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;
            string terminalInfo = ((IList<TransactionHistory>)((GenericPaginationResponse<TransactionHistory>)actualRecord).ReturnedRecords)
                                     .Where(x => x.REQ_TRAN_TYPE == transactionType).FirstOrDefault().REQ_AMT;


            // Assert
            var recordCount = ((GenericPaginationResponse<TransactionHistory>)actualRecord).ReturnedRecords;
            Assert.Equal(recordCount.ToList().Count, 1);
            //Assert.Equal(((IList<Wp.CIS.LynkSystems.Model.TransactionHistory>)actualRecord).Count, 1);

            Assert.Equal(terminalInfo, "589587");
        }

        
        [Fact]
        public async Task TransactionHistoryControllerTest_NoDataFound()
        {
            // Arrange
            string terminalId = "LK429486";
            string transactionType = "Credit";


            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            TransactionHistoryInput pageinput = new TransactionHistoryInput();
            pageinput.LIDValue = terminalId;
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IStringLocalizer<TransactionHistoryController> localizer = Substitute.For<IStringLocalizer<TransactionHistoryController>>();
            string key = "NoDataFound";
            string value = "No data found for provided ID";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ITransactionHistoryApi terminalListApi = Substitute.For<ITransactionHistoryApi>();


            ApiResult<GenericPaginationResponse<TransactionHistory>> response = new ApiResult<GenericPaginationResponse<TransactionHistory>>();
            response.Result = new GenericPaginationResponse<TransactionHistory>();

            terminalListApi.GetTransactionHistoryAsync(terminalId, page).ReturnsForAnyArgs(response);
            TransactionHistoryController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);


            // Act
            var terminalList = await fakecontroller.GetTransactionHistory(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 200);
            var actualTransactionHistory = ((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value;

            Assert.Equal(((GenericPaginationResponse<TransactionHistory>)actualTransactionHistory).ModelMessage, localizer["NoDataFound"].Value);
            

        }

        [Fact]
        public async Task TransactionHistoryControllerTest_GetAnException()
        {
            // Arrange
            string terminalId = "LK429486";
            string transactionType = "Credit";


            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            TransactionHistoryInput pageinput = new TransactionHistoryInput();
            pageinput.LIDValue = terminalId;
            pageinput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Customer;
            pageinput.Page = page;

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<TransactionHistoryController> localizer = Substitute.For<IStringLocalizer<TransactionHistoryController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            ITransactionHistoryApi terminalListApi = Substitute.For<ITransactionHistoryApi>();

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            TransactionHistoryController fakecontroller
                       = FakeController(mockCache, terminalListApi, localizer, fakeOperation, loggingFacade);

            terminalListApi.GetTransactionHistoryAsync(terminalId, page).Throws(new Exception());
            // Act
            var terminalList = await fakecontroller.GetTransactionHistory(pageinput);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)terminalList).Value, localizer["InternalServerError"].Value);

        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITransactionHistoryRepository FakeRepository()
        {
            return Substitute.For<ITransactionHistoryRepository>();

        }

        private static TransactionHistoryController FakeController(
                                             IDistributedCache cache,
                                             ITransactionHistoryApi terminalApi,
                                             IStringLocalizer<TransactionHistoryController> localizer,
                                             IOperation operation,
                                             ILoggingFacade loggingFacade)
        {
            return new TransactionHistoryController(cache, terminalApi, localizer, operation, loggingFacade);

        }
        
    }
}

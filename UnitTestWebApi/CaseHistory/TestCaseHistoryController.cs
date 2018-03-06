using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using NSubstitute;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.ContactList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.Error;
//using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Worldpay.CIS.DataAccess.CaseHistory;
using CIS.WebApi.UnitTests.CaseHistory;
using Microsoft.Extensions.Configuration;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace Worldpay.CIS.WebApi.UnitTests.CaseHistory
{
    public class TestCaseHistoryController : Controller
    {
        [Fact]
        public void CaseHistoryControllerTest_ModelState_Invalid()
        {
            //Arrange
            var page = GetCaseHistoryObject();
            
            int lid = 648988;

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);

            IStringLocalizer<CaseHistoryController> localizer
                            = Substitute.For<IStringLocalizer<CaseHistoryController>>();            

            IDistributedCache mockCache = FakeCache();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);
            ICaseHistoryApi caseHistoryApi = Substitute.For<ICaseHistoryApi>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            CaseHistoryController controller = new CaseHistoryController(mockCache, caseHistoryApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.Get(page);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        [Fact]
        //Unit Test for the RetrieveCache()
        public async Task CaseHistoryControllerTest_GetDataFromCache()
        {
            var page = GetCaseHistoryObject();
            page.lidTypeEnum = LidTypeEnum.Terminal;
            page.LIDValue = "FakeStringID";

            int lid = 648988;

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);

            IStringLocalizer<CaseHistoryController> localizer
                            = Substitute.For<IStringLocalizer<CaseHistoryController>>();

            ICaseHistoryApi caseHistoryApi = Substitute.For<ICaseHistoryApi>();
            caseHistoryApi
                .GetCaseHistory(Arg.Any<LidTypeEnum>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<PaginationCaseHistory>())
                .Returns(Task.FromResult<ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>>(expectedResult));

            IDistributedCache mockCache = FakeCache();
            

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            fakeOperation.RetrieveCache("_FakeStringID", new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>()).ReturnsForAnyArgs(expectedResult.Result);
            CaseHistoryController controller = new CaseHistoryController(mockCache, caseHistoryApi, localizer, fakeOperation, loggingFacade);

            //ACT

            var contactList = await controller.Get(page);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value;

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }


        [Fact]
        //Mock API Call and unit test for the API call with returning mock ContactList.
        public async Task CaseHistoryControllerTest_Success()
        {
            // Arrange
            var page = GetCaseHistoryObject();
            page.lidTypeEnum = LidTypeEnum.Terminal;
            page.LIDValue = "FakeStringID";

            int lid = 648988;
            int CaseID = 8715123;
            string ExtraId = null;

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);
                        
            
            ICaseHistoryRepository mockRepo = Substitute.For<ICaseHistoryRepository>();


            IStringLocalizer<CaseHistoryController> localizer
                            = Substitute.For<IStringLocalizer<CaseHistoryController>>();

            ICaseHistoryApi caseHistoryApi = Substitute.For<ICaseHistoryApi>();

            IDistributedCache mockCache = FakeCache();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);


            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            CaseHistoryController controller = new CaseHistoryController(mockCache, caseHistoryApi, localizer, fakeOperation, loggingFacade);

            caseHistoryApi.GetCaseHistory(page.lidTypeEnum, page.LIDValue, ExtraId, page.Page).ReturnsForAnyArgs(expectedResult);

            // Act
           
            var casehistory = await controller.Get(page);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)casehistory).Value;
            string casehistoryInfo = ((IList<Wp.CIS.LynkSystems.Model.CaseHistory>)
                ((GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>)actualRecord).ReturnedRecords).Where(x => x.caseId == CaseID).FirstOrDefault().orgDeptName;

            var recordCount = ((GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>)actualRecord).ReturnedRecords;


            // Assert
            Assert.Equal(recordCount.ToList().Count, 6);
           

            Assert.Equal(casehistoryInfo, "Customer Care");
        }


        [Fact]
        public async Task CaseHistoryControllerTest_FailToRetrieveData()
        {
            // Arrange
            var page = GetCaseHistoryObject(); 
            page.lidTypeEnum = LidTypeEnum.Terminal;

            int lid = 648988;
            string ExtraId = null;

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);

            IStringLocalizer<CaseHistoryController> localizer
                            = Substitute.For<IStringLocalizer<CaseHistoryController>>();

            IDistributedCache mockCache = FakeCache();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);



            string key = "NoDataFound";
            string value = "No data found for provided ID";

            var localizedString = new LocalizedString(key, value);

            ICaseHistoryApi casehistoryApi = Substitute.For<ICaseHistoryApi>();

            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();

            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> response = new ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>();
            response.Result = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            casehistoryApi.GetCaseHistory(page.lidTypeEnum, page.LIDValue, ExtraId, page.Page).ReturnsForAnyArgs(response);
            CaseHistoryController controller = new CaseHistoryController(mockCache, casehistoryApi, localizer, fakeOperation, loggingFacade);


            // Act
            var casehisotryList = await controller.Get(page);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)casehisotryList).StatusCode, 200);
            

        }


        [Fact]
        public async Task CaseHistoryControllerTest_APICallHasErrorMessage()
        {
            // Arrange
            var page = GetCaseHistoryObject();
            string ExtraId = null;



            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(648988);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();

            IStringLocalizer<CaseHistoryController> localizer = Substitute.For<IStringLocalizer<CaseHistoryController>>();

            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

          
            ICaseHistoryApi caseHistoryApi = Substitute.For<ICaseHistoryApi>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            
            CaseHistoryController controller = new CaseHistoryController(mockCache, caseHistoryApi, localizer, fakeOperation, loggingFacade);


            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> response = new ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>();
            response.Result = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>();

            var errorkey = GlobalErrorCode.InternalServerError;
            response.AddErrorMessage(errorkey.ToString());


            caseHistoryApi.GetCaseHistory(page.lidTypeEnum, page.LIDValue, ExtraId, page.Page).ThrowsForAnyArgs(new Exception());

            // Act
           
            var casehistoryList = await controller.Get(page);
            
            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)casehistoryList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)casehistoryList).Value, localizer["InternalServerError"].Value);
        }


        [Fact]
        public async Task CaseHistoryControllerTest_GetAnException()
        {
            var page = GetCaseHistoryObject();
            string ExtraId = null;


            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(648988);

            
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Wp.CIS.LynkSystems.Model.CaseHistory>>())).DoNotCallBase();

            IStringLocalizer<CaseHistoryController> localizer = Substitute.For<IStringLocalizer<CaseHistoryController>>();

            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);


            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ICaseHistoryApi casehistoryApi = Substitute.For<ICaseHistoryApi>();
            CaseHistoryController controller = new CaseHistoryController(mockCache, casehistoryApi, localizer, fakeOperation, loggingFacade);

            casehistoryApi.GetCaseHistory(page.lidTypeEnum, page.LIDValue, ExtraId,  page.Page).Throws(new Exception());

            // Act
            var casehistoryList = await controller.Get(page);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)casehistoryList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)casehistoryList).Value, localizer["InternalServerError"].Value);

        }
        
        private CaseHistoryInput GetCaseHistoryObject()
        {
            CaseHistoryInput pageInput = new CaseHistoryInput();
            pageInput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Terminal;
            pageInput.LIDValue = "123456";

            PaginationCaseHistory page = new PaginationCaseHistory();
            page.FilterCaseLevel = "Term";
            page.PageSize = 100;
            page.SortField = "CASEID";
            page.SortFieldByAsc = true;

            pageInput.Page = page;
            return pageInput;
        }


        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ICaseHistoryRepository FakeRepository()
        {
            return Substitute.For<ICaseHistoryRepository>();

        }
        



    }
}

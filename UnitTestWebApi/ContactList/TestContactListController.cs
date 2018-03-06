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
using Wp.CIS.LynkSystems.Model.WebApiInput;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Wp.CIS.LynkSystems.Services;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.ContactList
{
    public class TestContactListController : Controller
    {

        [Fact]
        public void ContactListControllerTest_ModelState_Invalid()
        {
            //Arrange

            var page = GetContactListObject();

            string ssn = "3425";
            
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);

            IStringLocalizer<ContactListController> localizer
                            = Substitute.For<IStringLocalizer<ContactListController>>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            IContactListApi contactListApi = Substitute.For<IContactListApi>();

            IDistributedCache mockCache = FakeCache();
            IOperation fakeOperation = Substitute.ForPartsOf<Operation>(mockCache);
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();


            ContactListController controller =
                new ContactListController(mockCache, contactListApi, localizer, fakeOperation, loggingFacade);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var result = controller.GetContactList(page);

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode.ToString(), "400");

        }

        [Fact]
        //Unit Test for the RetrieveCache()
        public async Task ContactListControllerTest_GetDataFromCache()
        {

            var page = GetContactListObject();
            page.lidTypeEnum = LidTypeEnum.Terminal;
            page.LIDValue = "123456";

            string ssn = "3425";
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IContactListRepository mockRepo = Substitute.For<IContactListRepository>();
            IStringLocalizer<ContactListController> localizer
                       = Substitute.For<IStringLocalizer<ContactListController>>();
            IContactListApi mockContactListApi = Substitute.For<IContactListApi>();

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Demographics>>())).DoNotCallBase();

            fakeOperation.RetrieveCache("_123456", new GenericPaginationResponse<Demographics>()).ReturnsForAnyArgs(expectedResult.Result);
            ContactListController controller = new ContactListController( mockCache, mockContactListApi, localizer, fakeOperation, loggingFacade);


            //ACT

            var contactList = await controller.GetContactList(page);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value;

            //Assert
            Assert.Equal(JsonConvert.SerializeObject(actualRecord), JsonConvert.SerializeObject(expectedResult.Result));
        }



        [Fact]
        //Mock API Call and unit test for the API call with returning mock ContactList.
        public async Task ContactListControllerTest_Success()
        {
            // Arrange
            
            var page = GetContactListObject();

            int NameAddressID = 3301636;
            string ssn = "3425";
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IContactListRepository mockRepo = Substitute.For<IContactListRepository>();
            IStringLocalizer<ContactListController> localizer
                       = Substitute.For<IStringLocalizer<ContactListController>>();
            IContactListApi mockContactListApi = Substitute.For<IContactListApi>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Demographics>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Demographics>>())).DoNotCallBase();
            ContactListController controller = new ContactListController( mockCache, mockContactListApi, localizer, fakeOperation, loggingFacade);
            mockContactListApi.GetContactListAsync(page.lidTypeEnum, page.LIDValue, page.Page).ReturnsForAnyArgs(expectedResult);
            // Act
            var contactList = await controller.GetContactList(page);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value;
            string contactInfo = ((IList<Demographics>)(((GenericPaginationResponse<Demographics>)actualRecord).ReturnedRecords)).Where(x => x.NameAddressID == NameAddressID).FirstOrDefault().Name;


            // Assert
            var recordCount = ((GenericPaginationResponse<Demographics>)actualRecord).ReturnedRecords;
            Assert.Equal(recordCount.ToList().Count, 3);

            Assert.Equal(contactInfo, "Golden Corral Corporation");
        }


        [Fact]
        public async Task ContactListControllerTest_NoDataFound()
        {
            // Arrange
            
            var page = GetContactListObject();
            page.lidTypeEnum = LidTypeEnum.Terminal;
                        
            string ssn = "3425";
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();

            IStringLocalizer<ContactListController> localizer
                       = Substitute.For<IStringLocalizer<ContactListController>>();
            string key = "NoDataFound";
            string value = "No data found for provided ID";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            IContactListApi contactListApi = Substitute.For<IContactListApi>();
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Demographics>>())).DoNotCallBase();

            ApiResult<GenericPaginationResponse<Demographics>> response = new ApiResult<GenericPaginationResponse<Demographics>>();
            response.Result = new GenericPaginationResponse<Demographics>();


            contactListApi.GetContactListAsync(page.lidTypeEnum, page.LIDValue, page.Page).ReturnsForAnyArgs(response);
            ContactListController controller
                       = new ContactListController(mockCache, contactListApi, localizer, fakeOperation, loggingFacade);


            // Act
            var contactList = await controller.GetContactList(page);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).StatusCode, 200);

            var actualContactList = ((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value;

            Assert.Equal(((GenericPaginationResponse<Demographics>)actualContactList).ModelMessage, localizer["NoDataFound"].Value);

        }

        [Fact]
        public async Task ContactListControllerTest_APICallHasErrorMessage()
        {
            // Arrange
            var page = GetContactListObject();

            string ssn = "3425";
            IConfigurationRoot configurationRoot = Substitute.For<IConfigurationRoot>();
            configurationRoot = GetConfiguration(configurationRoot);
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();

            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<ICollection<Demographics>>())).DoNotCallBase();

            IStringLocalizer<ContactListController> localizer
                       = Substitute.For<IStringLocalizer<ContactListController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            IContactListApi contactListApi = Substitute.For<IContactListApi>();

            ContactListController controller
                       = new ContactListController(mockCache, contactListApi, localizer, fakeOperation, loggingFacade);


            ApiResult<GenericPaginationResponse<Demographics>> response = new ApiResult<GenericPaginationResponse<Demographics>>();
            var errorkey = GlobalErrorCode.InternalServerError;
            response.AddErrorMessage(errorkey.ToString());
            contactListApi.GetContactListAsync(page.lidTypeEnum, page.LIDValue, page.Page).ReturnsForAnyArgs(response); ;



            // Act
            var contactList = await controller.GetContactList(page);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value, localizer["InternalServerError"].Value);

        }

        [Fact]
        public async Task ContactListControllerTest_GetAnException()
        {
            // Arrange
            var page = GetContactListObject();

            string ssn = "3425";
            
            IOptions<Settings> optionAccessor = Substitute.For<IOptions<Settings>>();
           

            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            
            IOperation fakeOperation = Substitute.For<Operation>(mockCache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<GenericPaginationResponse<Demographics>>())).DoNotCallBase();

            IStringLocalizer<ContactListController> localizer
                       = Substitute.For<IStringLocalizer<ContactListController>>();
            string key = "InternalServerError";
            string value = "Some Server Error Occurs while retrieving the data";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);

            IContactListApi contactListApi = Substitute.For<IContactListApi>();

            ContactListController controller
                       = new ContactListController(mockCache, contactListApi, localizer, fakeOperation, loggingFacade);
            
            contactListApi.GetContactListAsync(page.lidTypeEnum, page.LIDValue, page.Page).Throws(new Exception());
            // Act
            var contactList = await controller.GetContactList(page);

            // Assert

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).StatusCode, 500);

            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)contactList).Value, localizer["InternalServerError"].Value);

        }

        private ContactListInput GetContactListObject()
        {
            ContactListInput pageInput = new ContactListInput();
            pageInput.lidTypeEnum = Wp.CIS.LynkSystems.Model.Enums.LidTypeEnum.Terminal;
            pageInput.LIDValue = "123456";

            PaginationDemographics page = new PaginationDemographics();
            page.FilterContact = "wa";
            page.PageSize = 100;
            page.SortField = "Contact";
            page.SortFieldByAsc = true;

            pageInput.Page = page;
            return pageInput;
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IContactListRepository FakeRepository()
        {
            return Substitute.For<IContactListRepository>();

        }
        
        private static IConfigurationRoot GetConfiguration(IConfigurationRoot configurationRoot)
        {
            configurationRoot["DatabaseDefaults:MaxNumberOfRecordsToReturn"] = "500";
            return configurationRoot;
        }

       
    }
}

using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.ContactList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.ContactList
{
    public class SettingsTest
    {

    }
    public class TestContactListApi
    {

        [Fact]
        public void ContactListApiTest_Success()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";
            PaginationDemographics page = new PaginationDemographics();
            int NameAddressID = 3301636;
            string ssn = "3425";
            
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IContactListRepository mockRepo = Substitute.For<IContactListRepository>();

            IContactListApi contactListApi = FakeApi(optionsAccessor, mockRepo, loggingFacade);// Substitute.For<IContactListApi>();
           
            mockRepo.GetContactListAsync(LIDType, LID, page).ReturnsForAnyArgs(expectedResult.Result);

            contactListApi = new ContactListApi(optionsAccessor, mockRepo, loggingFacade);
            
            // Act
            var contactList = contactListApi.GetContactListAsync(LIDType, LID, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Demographics>)contactList.Result.ReturnedRecords;
            string contactInfo = actualRecord.Where(x => x.NameAddressID == NameAddressID).FirstOrDefault().Name;            


            //// Assert

            Assert.Equal(((IList<Demographics>)actualRecord).Count, 3);

            Assert.Equal(contactInfo, "Golden Corral Corporation");
        }

        private IContactListApi FakeApi(IOptions<Settings> optionsAccessor, IContactListRepository mockRepo, ILoggingFacade loggingFacade)
        {
            return new ContactListApi(optionsAccessor, mockRepo, loggingFacade);
        }

        [Fact]
        public async Task ContactListApiTest_Exception()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";
            PaginationDemographics page = new PaginationDemographics();
            
            string ssn = "3425";
            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            
            IContactListRepository mockRepo = Substitute.For<IContactListRepository>();
            IContactListApi contactListApi = Substitute.For<IContactListApi>();
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockRepo.GetContactListAsync(LIDType, LID, page).Throws(new Exception());

                       
            contactListApi = new ContactListApi(optionsAccessor, mockRepo, loggingFacade);
            
            //Assert   
            await Assert.ThrowsAsync<Exception>(() => contactListApi.GetContactListAsync(LIDType, LID, page));

        }


        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IContactListRepository FakeRepository()
        {
            return Substitute.For<IContactListRepository>();

        }

       
    }
}

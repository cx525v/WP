
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.ContactList;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.Enums;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.ContactList
{
    public class TestContactListRepository
    {

        [Fact]
        
        //Would be revisiting to modify the actual way of call method.
        public void ContactListRepositoryTest_Success()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";
            
            PaginationDemographics page = new PaginationDemographics();

            int NameAddressID = 3301636; 
            string ssn = "3425";

            MockContactListRepository mockContactListRepository = new MockContactListRepository();
            ApiResult<GenericPaginationResponse<Demographics>> expectedResult = mockContactListRepository.GetMockData(ssn);
            
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IContactListRepository mockRepo = Substitute.For<IContactListRepository>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockRepo.GetContactListAsync(LIDType, LID, page).ReturnsForAnyArgs(expectedResult.Result);           
            
            // Act
            var contactList =  mockRepo.GetContactListAsync(LIDType, LID, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Demographics>)contactList.ReturnedRecords;
            string contactInfo = actualRecord.Where(x => x.NameAddressID == NameAddressID).FirstOrDefault().Name;

                
            //// Assert

            Assert.Equal(((IList<Demographics>)actualRecord).Count, 3);

            Assert.Equal(contactInfo, "Golden Corral Corporation");
        }

       
        
    }
}


using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.MerchantList;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.MerchantList
{
    public class TestTerminalListRepository
    {

        [Fact]
        
        //Would be revisiting to modify the actual way of call method.
        public void MerchantListRepositoryTest_Success()
        {
            // Arrange
            int CustomerID = 191809;
            string mid = "191807";
            

            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IMerchantListRepository mockRepo = Substitute.For<IMerchantListRepository>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockRepo.GetMerchantListAsync(CustomerID, page).ReturnsForAnyArgs(expectedResult.Result);           
            
            // Act
            var merchList =  mockRepo.GetMerchantListAsync(CustomerID, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Merchant>)merchList.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.MID == mid).FirstOrDefault().Name;

                
            //// Assert

            Assert.Equal(((IList<Merchant>)actualRecord).Count, 2);

            Assert.Equal(merchInfo, "ABC Corp");
        }

       
        
    }
}

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
using Worldpay.CIS.DataAccess.MerchantList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.MerchantList
{
    public class TestMerchantListApi
    {

        [Fact]
        public void MerchantListApiTest_Success()
        {
            // Arrange
            int CustomerID = 191809;
            string mid = "191807";
            

            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            
            IMerchantListRepository mockRepo = Substitute.For<IMerchantListRepository>();

            IMerchantListApi merchantListApi = Substitute.For<IMerchantListApi>();

            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockRepo.GetMerchantListAsync(CustomerID, page).ReturnsForAnyArgs(expectedResult.Result);

            merchantListApi = new MerchantListApi(optionsAccessor, mockRepo, loggingFacade);
            
            // Act
            var merchList = merchantListApi.GetMerchantListAsync(CustomerID, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Merchant>)merchList.Result.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.MID == mid).FirstOrDefault().Name;            


            //// Assert

            Assert.Equal(((IList<Merchant>)actualRecord).Count, 2);

            Assert.Equal(merchInfo, "ABC Corp");
        }

        [Fact]
        public async Task MerchantListApiTest_Exception()
        {
            // Arrange
            int CustomerID = 191809;
            
            MockMerchantListRepository mockMerchantListRepository = new MockMerchantListRepository();
            ApiResult<GenericPaginationResponse<Merchant>> expectedResult = mockMerchantListRepository.GetMockData(CustomerID);
            PaginationMerchant page = mockMerchantListRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IMerchantListRepository mockRepo = Substitute.For<IMerchantListRepository>();
            IMerchantListApi merchantListApi = Substitute.For<IMerchantListApi>();
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockRepo.GetMerchantListAsync(CustomerID, page).Throws(new Exception());

                       
            merchantListApi = new MerchantListApi(optionsAccessor, mockRepo, loggingFacade);


            //Assert   
            await Assert.ThrowsAsync<Exception>(() => merchantListApi.GetMerchantListAsync(CustomerID, page));
            

        }


        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private IMerchantListRepository FakeRepository()
        {
            return Substitute.For<IMerchantListRepository>();

        }
           
    }
}

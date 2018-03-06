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
using Worldpay.CIS.DataAccess.TransactionHistory;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.TransactionHistoryList
{
    public class TestTransactionHistoryApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void TransactionHistoryApiTest_Success()
        {
            // Arrange
            
            string terminalId = "LK429486";
            string transactionType = "Credit";
            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            
            ITransactionHistoryRepository mockRepo = Substitute.For<ITransactionHistoryRepository>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();

            ITransactionHistoryApi mockTransactionHistoryApi = Substitute.For<ITransactionHistoryApi>();
            mockTransactionHistoryApi.WhenForAnyArgs(x => x.GetTransactionHistoryAsync(Arg.Any<string>(), Arg.Any<PaginationTransactionHistory>())).DoNotCallBase();
            mockRepo.GetTransactionHistoryAsync(terminalId, page).ReturnsForAnyArgs(expectedResult.Result);

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            mockTransactionHistoryApi = new TransactionHistoryApi(optionsAccessor, mockRepo, loggingFacade);
            
            // Act
            var terminalList = mockTransactionHistoryApi.GetTransactionHistoryAsync(terminalId, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.TransactionHistory>)terminalList.Result.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.REQ_TRAN_TYPE == transactionType).FirstOrDefault().REQ_AMT;            


            //// Assert

            Assert.Equal(((IList<TransactionHistory>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "589587");
        }

        [Fact]
        public async Task TransactionHistoryApiTest_GetAnException()
        {
            // Arrange
            string terminalId = "LK429486";
            string transactionType = "Credit";
            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            ITransactionHistoryRepository mockRepo = Substitute.For<ITransactionHistoryRepository>();
            
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            ITransactionHistoryApi terminalListApi = Substitute.For<ITransactionHistoryApi>();
            terminalListApi.WhenForAnyArgs(x => x.GetTransactionHistoryAsync(Arg.Any<string>(), Arg.Any<PaginationTransactionHistory>())).DoNotCallBase();
            mockRepo.GetTransactionHistoryAsync(terminalId, page).Throws(new Exception());

            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            terminalListApi = new TransactionHistoryApi(optionsAccessor, mockRepo, loggingFacade);


            // Assert
            
            await Assert.ThrowsAsync<Exception>(() => terminalListApi.GetTransactionHistoryAsync(terminalId, page));

        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITransactionHistoryRepository FakeRepository()
        {
            return Substitute.For<ITransactionHistoryRepository>();

        }
        

        
    }
}

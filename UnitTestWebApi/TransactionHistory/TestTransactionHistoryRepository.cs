using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.TransactionHistory;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using System;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.TransactionHistoryList
{
    public class TestTransactionHistoryRepository
    {

        [Fact]
        //Would be revisiting to modify the actual way of call method.
        public void TestTransactionHistoryRepositoryTest_Success()
        {
            // Arrange
            string terminalId = "LK429486";
            string transactionType = "Credit";

            MockTransactionHistoryRepository mockTransactionHistoryRepository = new MockTransactionHistoryRepository();
            ApiResult<GenericPaginationResponse<TransactionHistory>> expectedResult = mockTransactionHistoryRepository.GetMockData(transactionType);
            PaginationTransactionHistory page = mockTransactionHistoryRepository.GetPagination();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            ITransactionHistoryRepository mockRepo = Substitute.For<ITransactionHistoryRepository>();

            
            mockRepo.GetTransactionHistoryAsync(terminalId, page).ReturnsForAnyArgs(expectedResult.Result);

                       
            // Act
            var terminalList =  mockRepo.GetTransactionHistoryAsync(terminalId, page).Result;
            var actualRecord = (IList<TransactionHistory>)terminalList.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.REQ_TRAN_TYPE == transactionType).FirstOrDefault().REQ_AMT;


            //// Assert

            Assert.Equal(((IList<TransactionHistory>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "589587");
        }
      
    }
}

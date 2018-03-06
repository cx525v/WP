using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.TerminalList;
using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using System;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.TerminalList
{
    public class TestTerminalListRepository
    {

        [Fact]
        //Would be revisiting to modify the actual way of call method.
        public void TestTerminalListRepositoryTest_Success()
        {
            // Arrange
            int terminalNbr = 589587;
            string tid = "LK429486";
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(terminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();

            
            mockRepo.GetTerminalListAsync(terminalNbr, page).ReturnsForAnyArgs(expectedResult.Result);

                       
            // Act
            var terminalList =  mockRepo.GetTerminalListAsync(terminalNbr, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Terminal>)terminalList.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.TerminalID == tid).FirstOrDefault().Software;


            //// Assert

            Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "LSPR3271");
        }

        #region Old Test Cases
        [Fact]
        //Would be revisiting to modify the actual way of call method.
        public void TestTerminalListRepositoryTest_Success_Old()
        {
            // Arrange
            string terminalNbr = "589587";
            string tid = "LK429486";


            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(terminalNbr);
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();


            mockRepo.GetTerminalListAsync(Convert.ToInt32(terminalNbr)).ReturnsForAnyArgs(expectedResult.Result);


            // Act
            var merchList = mockRepo.GetTerminalListAsync(Convert.ToInt32(terminalNbr)).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Terminal>)merchList;
            string merchInfo = actualRecord.Where(x => x.TerminalID == tid).FirstOrDefault().Software;


            //// Assert

            Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "LSPR3271");
        }
        #endregion
    }
}

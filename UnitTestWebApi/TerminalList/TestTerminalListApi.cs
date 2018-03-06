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
using Worldpay.CIS.DataAccess.TerminalList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Worldpay.Logging.Contracts.Enums;
using System.Threading;

namespace CIS.WebApi.UnitTests.TerminalList
{
    public class TestTerminalListApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void TerminalListApiTest_Success()
        {
            // Arrange
            int TerminalNbr = 589587;
            string TerminalID = "LK429486";
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ITerminalListApi mockTerminalListApi = Substitute.For<ITerminalListApi>();
            mockTerminalListApi.WhenForAnyArgs(x => x.GetTerminalListAsync(Arg.Any<int>(), Arg.Any<PaginationTerminal>())).DoNotCallBase();
            mockRepo.GetTerminalListAsync(TerminalNbr, page).ReturnsForAnyArgs(expectedResult.Result);

            mockTerminalListApi = new TerminalListApi(optionsAccessor, mockRepo, loggingFacade);
            
            // Act
            var terminalList = mockTerminalListApi.GetTerminalListAsync(TerminalNbr, page).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Terminal>)terminalList.Result.ReturnedRecords;
            string merchInfo = actualRecord.Where(x => x.TerminalID == TerminalID).FirstOrDefault().Software;            


            //// Assert

            Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "LSPR3271");
        }

        [Fact]
        public async Task TerminalListApiTest_GetAnException()
        {
            // Arrange
            int TerminalNbr = 589587;
            
            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<GenericPaginationResponse<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            PaginationTerminal page = mockTerminalListRepository.GetPagination();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();
            
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            terminalListApi.WhenForAnyArgs(x => x.GetTerminalListAsync(Arg.Any<int>(), Arg.Any<PaginationTerminal>())).DoNotCallBase();
            mockRepo.GetTerminalListAsync(TerminalNbr, page).Throws(new Exception());

            terminalListApi = new TerminalListApi(optionsAccessor, mockRepo, loggingFacade);
            

            // Act
            var terminalList = await terminalListApi.GetTerminalListAsync(TerminalNbr, page);

            // Assert
            Assert.Equal(((IList<string>)terminalList.ErrorMessages).First(), "InternalServerError");     

        }

        #region Old Test Cases
        [Fact]
        public void TerminalListApiTest_Success_Old()
        {
            // Arrange
            string TerminalNbr = "589587";
            string TerminalID = "LK429486";

            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();

            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();
            
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ITerminalListApi mockTerminalListApi = Substitute.For<ITerminalListApi>();
            mockTerminalListApi.WhenForAnyArgs(x => x.GetTerminalListAsync(Arg.Any<int>())).DoNotCallBase();
            mockRepo.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).ReturnsForAnyArgs(expectedResult.Result);

            mockTerminalListApi = new TerminalListApi(optionsAccessor, mockRepo,loggingFacade);

            // Act
            var merchList = mockTerminalListApi.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.Terminal>)merchList.Result;
            string merchInfo = actualRecord.Where(x => x.TerminalID == TerminalID).FirstOrDefault().Software;


            //// Assert

            Assert.Equal(((IList<Terminal>)actualRecord).Count, 1);

            Assert.Equal(merchInfo, "LSPR3271");
        }

        [Fact]
        public async Task TerminalListApiTest_GetAnException_Old()
        {
            // Arrange
            string TerminalNbr = "589587";

            MockTerminalListRepository mockTerminalListRepository = new MockTerminalListRepository();
            ApiResult<ICollection<Terminal>> expectedResult = mockTerminalListRepository.GetMockData(TerminalNbr);
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            ITerminalListRepository mockRepo = Substitute.For<ITerminalListRepository>();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            ILoggingFacade loggingFacade = Substitute.For<ILoggingFacade>();
            loggingFacade.WhenForAnyArgs(x => x.LogAsync(Arg.Any<LogLevels>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).DoNotCallBase();

            ITerminalListApi terminalListApi = Substitute.For<ITerminalListApi>();
            terminalListApi.WhenForAnyArgs(x => x.GetTerminalListAsync(Arg.Any<int>())).DoNotCallBase();
            mockRepo.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)).Throws(new Exception());

            terminalListApi = new TerminalListApi(optionsAccessor, mockRepo,loggingFacade);

            //Assert
            await Assert.ThrowsAsync<Exception>(() => terminalListApi.GetTerminalListAsync(Convert.ToInt32(TerminalNbr)));


        }
        #endregion
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ITerminalListRepository FakeRepository()
        {
            return Substitute.For<ITerminalListRepository>();

        }
        

        
    }
}

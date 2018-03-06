using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NSubstitute;
using UnitTestDataAccess;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using UnitTestDataAccess.BankingInfo;
using Worldpay.CIS.DataAccess.BankingInfo;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.WebApi.Common;
using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Localization;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.BankingInfo
{
    public class TestBankingApiController
    {
        [Fact]
        public void BankingInfoControllerTest_ModelState_Invalid()
        {
            MockBankingInfoRepository repository = new MockBankingInfoRepository();
            var expectedResult = repository.GetMockBankingInfo();
            IDistributedCache _cache = FakeCache();

            string lid = "756122";
            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            IBankingApi dAPI = Substitute.For<IBankingApi>();
            ILoggingFacade fakeLogger = FakeLogger();

            IOperation fakeOperation = FakeOperation(_cache);

            dAPI.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            BankingController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }
        [Fact]
        public async Task BankingInfoControllerTest_Success()
        {
            //Arrange
            MockBankingInfoRepository repository = new MockBankingInfoRepository();
            var expectedResult = repository.GetMockBankingInfo();
            IDistributedCache _cache = FakeCache();

            string lid = "756122";
            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            IBankingApi dAPI = Substitute.For<IBankingApi>();
            ILoggingFacade fakeLogger = FakeLogger();

            IOperation fakeOperation = FakeOperation(_cache);

            dAPI.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            BankingController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            //Act
            var dinfo = await controller.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            //Assert
            Assert.Equal(actualRecord, expectedResult.Result);
        }

        [Fact]
        public async Task BankingInfoControllerTest_GetAnException()
        {
            //Arrange
            MockBankingInfoRepository repository = new MockBankingInfoRepository();
            var expectedResult = repository.GetMockBankingInfo();
            IDistributedCache _cache = FakeCache();

            string lid = "756122";
            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            IBankingApi dAPI = Substitute.For<IBankingApi>();
            ILoggingFacade fakeLogger = FakeLogger();

            IOperation fakeOperation = FakeOperation(_cache);

            dAPI.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ThrowsForAnyArgs(new System.Exception());
            BankingController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.GetBankingInfo(Helper.LIDTypes.TerminalNbr, "0");
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        private BankingController FakeController(IBankingApi api, IDistributedCache mockCache, IOperation fakeOperation, ILoggingFacade loggingFacade)
        {
            var localizer = new MockStringLocalizer<BankingController>();
            localizer[0] = new LocalizedString("GenericError", "Error occured");
            return new BankingController(mockCache, api, localizer, fakeOperation, loggingFacade);
        }

        private ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<List<BankingInformation>>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<List<BankingInformation>>())).DoNotCallBase();
            return fakeOperation;
        }
    }
}

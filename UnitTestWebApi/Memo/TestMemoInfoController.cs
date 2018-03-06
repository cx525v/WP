using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MemoInfo;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.Memo
{
    public class TestMemoInfoController
    {
        [Fact]
        public void MemoInfoControllerTest_ModelState_Invalid()
        {
            //Arrange
            int lid = 589547;
            MockMemoInfoRepository repository = new MockMemoInfoRepository();
            var expectedResult = repository.GetMockMemoInfo();

            IMemoInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IMemoInfoApi dAPI = Substitute.For<IMemoInfoApi>();
            dAPI.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            MemoInfoController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task MemoInfoControllerTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;
            MockMemoInfoRepository repository = new MockMemoInfoRepository();
            var expectedResult = repository.GetMockMemoInfo();

            IMemoInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IMemoInfoApi dAPI = Substitute.For<IMemoInfoApi>();
            dAPI.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr,lid).ReturnsForAnyArgs(expectedResult);
            MemoInfoController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr,lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.MemoList)actualRecord), expectedResult.Result);
        }

        [Fact]
        public async Task MemoInfoControllerTerminalTest_GetAnException()
        {
            // Arrange
            IMemoInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            ILoggingFacade fakeLogger = FakeLogger();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IStringLocalizer<MemoInfoController> localizer
                        = Substitute.For<IStringLocalizer<MemoInfoController>>();
            
            IOperation fakeOperation = FakeOperation(_cache);
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IMemoInfoApi dAPI = new MemoInfoApi(appSettings, mockRepo);
            dAPI.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr,0).ThrowsForAnyArgs(new System.Exception());
            MemoInfoController controller = FakeController(dAPI, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr,0);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private IMemoInfoRepository FakeRepository()
        {
            return Substitute.For<IMemoInfoRepository>();
        }

        private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.MemoList>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.MemoList>())).DoNotCallBase();
            return fakeOperation;
        }

        private IOperation FakeTerminalSettlementInfoOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.TerminalSettlementInfo>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.TerminalSettlementInfo>())).DoNotCallBase();
            return fakeOperation;
        }

        private ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private MemoInfoController FakeController(IMemoInfoApi dashboardInfo, IDistributedCache mockCache, IOperation fakeOperation, ILoggingFacade fakeLogger)
        {
            var localizer = new MockStringLocalizer<MemoInfoController>();
            localizer[0] = new LocalizedString("GetMemoInfoErrorMessage", "Error occured");
            return new MemoInfoController(mockCache, dashboardInfo, localizer, fakeOperation, fakeLogger);
        }
    }
}

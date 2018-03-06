using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MemoInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.Memo
{
    public class TestMemoInfoApi
    {
        [Fact]
        public void MemoInfoApiTest_Success()
        {
            // Arrange
            int lid = 589547;
            MockMemoInfoRepository repository = new MockMemoInfoRepository();

            IMemoInfoRepository mockRepo = Substitute.For<IMemoInfoRepository>();
            IMemoInfoApi api = Substitute.For<IMemoInfoApi>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var expectedResult = repository.GetMockMemoInfo().Result;

            mockRepo.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            api = new MemoInfoApi(appSettings, mockRepo);

            // Act
            var actualRecord = (api.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult);
        }

        [Fact]
        public async Task MemoInfoApiTest_Exception()
        {
            // Arrange
            int CustomerID = 191809;

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IMemoInfoRepository mockRepo = Substitute.For<IMemoInfoRepository>();
            IMemoInfoApi dashboardInfoApi = Substitute.For<IMemoInfoApi>();

            mockRepo.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr,CustomerID).ThrowsForAnyArgs(new Exception());
            dashboardInfoApi = new MemoInfoApi(optionsAccessor, mockRepo);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => dashboardInfoApi.GetMemoResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, CustomerID));

        }
    }
}

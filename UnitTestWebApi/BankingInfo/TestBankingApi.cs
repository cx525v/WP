using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitTestDataAccess.BankingInfo;
using Worldpay.CIS.DataAccess.BankingInfo;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace CIS.WebApi.UnitTests.BankingInfo
{
    public class TestBankingApi
    {
        [Fact]
        public void TestBankingInfoApi_Success()
        {
            //Arrange
            MockBankingInfoRepository repository = new MockBankingInfoRepository();
            var expectedResult = repository.GetMockBankingInfo();

            string lid = "756122";

            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            IBankingApi api = Substitute.For<IBankingApi>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            mockRepo.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult.Result);
            api = new BankingApi(appSettings, mockRepo);

            // Act
            var actualRecord = api.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).Result;

            //Assert
            Assert.Equal(((IList<BankingInformation>)actualRecord.Result), (IList<BankingInformation>)expectedResult.Result);
        }

        [Fact]
        public async Task BankingInfoApiTest_Exception()
        {
            // Arrange
            string lid = "191809";

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            IBankingApi bankingInfoApi = Substitute.For<IBankingApi>();

            mockRepo.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ThrowsForAnyArgs(new Exception());
            bankingInfoApi = new BankingApi(optionsAccessor, mockRepo);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => bankingInfoApi.GetBankingInfo(Helper.LIDTypes.TerminalNbr, "0"));
           
        }
    }
}

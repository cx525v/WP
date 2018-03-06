using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnitTestDataAccess.BankingInfo;
using Worldpay.CIS.DataAccess.BankingInfo;
using Wp.CIS.LynkSystems.Model;
using Xunit;

namespace Worldpay.CIS.WebApi.UnitTests.BankingInfo
{
    public class TestBankingInfoRepository
    {
        [Fact]
        public void TestBankingInfoRepository_Success()
        {
            MockBankingInfoRepository repository = new MockBankingInfoRepository();
            var expectedResult = repository.GetMockBankingInfo();


            string lid = "756122";
            IBankingInfoRepository mockRepo = Substitute.For<IBankingInfoRepository>();
            mockRepo.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var actualRecord = mockRepo.GetBankingInfo(Helper.LIDTypes.TerminalNbr, lid).Result;

            // Assert
            Assert.Equal((actualRecord), expectedResult.Result);
        }
    }
}

using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worldpay.CIS.DataAccess.RecentStatement;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Xunit;

namespace Worldpay.CIS.WebApi.UnitTests.RecentStatement
{
    public class TestRecentStatementApi
    {
        [Fact]
        public void RecentStatementApiTest_Success()
        {
            // Arrange
            string merchantNbr = "542929801430265";
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IRecentStatementRepository mockRepo = Substitute.For<IRecentStatementRepository>();

            IRecentStatementApi api = Substitute.For<IRecentStatementApi>();
            mockRepo.GetRecentStatementAsync(merchantNbr).ReturnsForAnyArgs(expectedResult.Result);

            api = new RecentStatementApi(optionsAccessor, mockRepo);

            // Act
            var recentList = api.GetRecentStatementAsync(merchantNbr).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.RecentStatement>)recentList.Result;

            //// Assert
            Assert.Equal((actualRecord).Count, 1);
            Assert.Equal(actualRecord, expectedResult.Result);
        }

        [Fact]
        public async System.Threading.Tasks.Task RecentStatementApiTest_ExceptionAsync()

        {
            // Arrange
            string merchantNbr = "542929801430265";
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();
            IRecentStatementRepository mockRepo = Substitute.For<IRecentStatementRepository>();

            IRecentStatementApi api = Substitute.For<IRecentStatementApi>();
            mockRepo.GetRecentStatementAsync(merchantNbr).Throws(new Exception());

            api = new RecentStatementApi(optionsAccessor, mockRepo);

            //Assert
            await Assert.ThrowsAsync<Exception>(() => api.GetRecentStatementAsync(merchantNbr));


        }
    }
}

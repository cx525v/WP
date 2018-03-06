using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worldpay.CIS.DataAccess.Connection;
using Worldpay.CIS.DataAccess.RecentStatement;
using Wp.CIS.LynkSystems.Model;
using Xunit;

namespace Worldpay.CIS.WebApi.UnitTests.RecentStatement
{
    public class TestRecentStatementRepository
    {
        [Fact]
        public void RecentStatementRepositoryTest_Success()
        {
            // Arrange
            string merchantNbr = "542929801430265";
            MockRecentStatementRepository mockRecentStatementRepository = new MockRecentStatementRepository();
            ApiResult<ICollection<Wp.CIS.LynkSystems.Model.RecentStatement>> expectedResult = mockRecentStatementRepository.GetMockData();

            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();
            IRecentStatementRepository mockRepo = Substitute.For<IRecentStatementRepository>();

            mockRepo.GetRecentStatementAsync(merchantNbr).ReturnsForAnyArgs(expectedResult.Result);

            // Act
            var recentStatementList = mockRepo.GetRecentStatementAsync(merchantNbr).Result;
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.RecentStatement>)recentStatementList;

            //// Assert
            Assert.Equal((actualRecord).Count, 1);
            Assert.Equal(actualRecord, expectedResult.Result);
        }
    }
}

using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.AuditHistory;
using Wp.CIS.LynkSystems.Interfaces.Administrative;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Administrative;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.AuditHistory
{
    public class TestAuditHistoryApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            const int expectedAuditId = 2;
            IAuditHistoryRepository mockRepo = Substitute.For<IAuditHistoryRepository>();
            
            var repositoryReturnValue = new List<AuditHistoryModel>()
            {
                new AuditHistoryModel()
                {
                    ActionDate = DateTime.Now.AddDays(-30),
                    AuditId = 1
                },
                new AuditHistoryModel()
                {
                    ActionDate = DateTime.Now,
                    AuditId = expectedAuditId
                }
            };

            mockRepo
                .GetAuditHistoryAsync(Arg.Any<LidTypeEnum>(), Arg.Any<int>(), Arg.Any<ActionTypeEnum>())
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<AuditHistoryModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<AuditHistoryController> mockLocalizer = Substitute.For<IStringLocalizer<AuditHistoryController>>();
            mockLocalizer = new MockStringLocalizer<AuditHistoryController>();

            IAuditHistoryApi auditHistoryApi = new AuditHistoryApi(appSettings, mockRepo);
            AuditHistoryController controller = new AuditHistoryController(mockCache, auditHistoryApi, mockLocalizer);

            //// Act
            var response = await controller.Get(LidTypeEnum.Terminal, 1, ActionTypeEnum.ProjectMaintenanceScreen);

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((AuditHistoryModel)actualRecord).AuditId, expectedAuditId);
        }

        [Fact]
        public async Task ExceptionThrownInDbTest()
        {
            // Arrange
            IAuditHistoryRepository mockRepo = Substitute.For<IAuditHistoryRepository>();

            mockRepo
                .GetAuditHistoryAsync(Arg.Any<LidTypeEnum>(), Arg.Any<int>(), Arg.Any<ActionTypeEnum>())
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<AuditHistoryController>();

            IAuditHistoryApi auditHistoryApi = new AuditHistoryApi(appSettings, mockRepo);
            var controller = new AuditHistoryController(mockCache, auditHistoryApi, mockLocalizer);

            //// Act
            var actionResult = await controller.Get(LidTypeEnum.Terminal, 1, ActionTypeEnum.ProjectMaintenanceScreen);
            var objectResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            ////// Assert
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.InternalServerError);
        }

        #endregion
    }
}

using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DownloadTime;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.DownloadTimes
{
    public class TestDownloadTimesApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IDownloadTimeRepository mockRepo = Substitute.For<IDownloadTimeRepository>();

            var repositoryReturnValue = new List<DownloadTimeModel>()
            {
                new DownloadTimeModel()
                {
                    Description = "Description 1",
                    DLTypeID = 1
                },
                new DownloadTimeModel()
                {
                    Description = "Description 2",
                    DLTypeID = 2
                }
            };

            mockRepo
                .GetAllDownloadTimesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<DownloadTimeModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<DownloadTimesController>();

            IDownloadTimesApi theApi = new DownloadTimesApi(appSettings, mockRepo);
            var controller = new DownloadTimesController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<DownloadTimeModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IDownloadTimeRepository mockRepo = Substitute.For<IDownloadTimeRepository>();

            mockRepo
                .GetAllDownloadTimesAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<DownloadTimesController>();

            IDownloadTimesApi theApi = new DownloadTimesApi(appSettings, mockRepo);
            var controller = new DownloadTimesController(mockCache, theApi, mockLocalizer);

            //// Act
            var actionResult = await controller.Get();
            var objectResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            ////// Assert
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.InternalServerError);
        }

        #endregion
    }
}

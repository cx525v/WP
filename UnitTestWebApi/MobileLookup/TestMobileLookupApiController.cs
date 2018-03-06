using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.MobileLookup;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.MobileLookup
{
    public class TestMobileLookupApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IMobileLookupRepository mockRepo = Substitute.For<IMobileLookupRepository>();

            var repositoryReturnValue = new List<MobileLookupModel>()
            {
                new MobileLookupModel()
                {
                    Description = "Description 1",
                    MobileType = 1
                },
                new MobileLookupModel()
                {
                    Description = "Description 2",
                    MobileType = 2
                }
            };

            mockRepo
                .GetAllMobileLookupsAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<MobileLookupModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<MobileLookupController>();

            IMobileLookupApi theApi = new MobileLookupApi(appSettings, mockRepo);
            var controller = new MobileLookupController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<MobileLookupModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IMobileLookupRepository mockRepo = Substitute.For<IMobileLookupRepository>();

            mockRepo
                .GetAllMobileLookupsAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<MobileLookupController>();

            IMobileLookupApi theApi = new MobileLookupApi(appSettings, mockRepo);
            var controller = new MobileLookupController(mockCache, theApi, mockLocalizer);

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

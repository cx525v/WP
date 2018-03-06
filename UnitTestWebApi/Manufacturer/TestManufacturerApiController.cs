using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Manufacturer;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.Manufacturer
{
    public class TestManufacturerApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IManufacturerRepository mockRepo = Substitute.For<IManufacturerRepository>();

            var repositoryReturnValue = new List<ManufacturerModel>()
            {
                new ManufacturerModel()
                {
                    Description = "Description 1",
                    MfgCode = 1
                },
                new ManufacturerModel()
                {
                    Description = "Description 2",
                    MfgCode = 2
                }
            };

            mockRepo
                .GetAllManufacturersAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ManufacturerModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<ManufacturersController>();

            IManufacturersApi theApi = new ManufacturersApi(appSettings, mockRepo);
            var controller = new ManufacturersController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<ManufacturerModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IManufacturerRepository mockRepo = Substitute.For<IManufacturerRepository>();

            mockRepo
                .GetAllManufacturersAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<ManufacturersController>();

            IManufacturersApi theApi = new ManufacturersApi(appSettings, mockRepo);
            var controller = new ManufacturersController(mockCache, theApi, mockLocalizer);

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

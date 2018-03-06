using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.Brand;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.Brand
{
    public class TestBrandApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IBrandRepository mockRepo = Substitute.For<IBrandRepository>();

            var repositoryReturnValue = new List<ProductBrandModel>()
            {
                new ProductBrandModel()
                {
                    Description = "Description 1",
                    ID = 1
                },
                new ProductBrandModel()
                {
                    Description = "Description 2",
                    ID = 2
                }
            };

            mockRepo
                .GetProductBrandsAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ProductBrandModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<BrandController> mockLocalizer = Substitute.For<IStringLocalizer<BrandController>>();
            mockLocalizer = new MockStringLocalizer<BrandController>();

            IBrandApi theApi = new BrandApi(appSettings, mockRepo);
            BrandController controller = new BrandController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<ProductBrandModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IBrandRepository mockRepo = Substitute.For<IBrandRepository>();

            mockRepo
                .GetProductBrandsAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<BrandController>();

            IBrandApi theApi = new BrandApi(appSettings, mockRepo);
            var controller = new BrandController(mockCache, theApi, mockLocalizer);

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

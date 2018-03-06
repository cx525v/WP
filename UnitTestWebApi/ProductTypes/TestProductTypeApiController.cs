using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.ProductType;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.ProductTypes
{
    public class TestProductTypeApiController
    {
        #region Tests

        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IProductTypeRepository mockRepo = Substitute.For<IProductTypeRepository>();

            var repositoryReturnValue = new List<ProductTypeModel>()
            {
                new ProductTypeModel()
                {
                    Description = "Description 1",
                    ProductTypeId = 1
                },
                new ProductTypeModel()
                {
                    Description = "Description 2",
                    ProductTypeId = 2
                }
            };

            mockRepo
                .GetAllProductTypesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ProductTypeModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<ProductTypesController>();

            IProductTypesApi theApi = new ProductTypesApi(appSettings, mockRepo);
            var controller = new ProductTypesController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<ProductTypeModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IProductTypeRepository mockRepo = Substitute.For<IProductTypeRepository>();

            mockRepo
                .GetAllProductTypesAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<ProductTypesController>();

            IProductTypesApi theApi = new ProductTypesApi(appSettings, mockRepo);
            var controller = new ProductTypesController(mockCache, theApi, mockLocalizer);

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

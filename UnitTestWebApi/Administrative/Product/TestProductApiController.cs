using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Collections.Generic;
using Worldpay.CIS.DataAccess.Product;
using Xunit;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Wp.CIS.LynkSystems.Services.Administrative;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model.Enums;
using CIS.WebApi.UnitTests.Common;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.WebApi.Models.Administrative.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Wp.CIS.LynkSystems.Model.Administrative;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CIS.WebApi.UnitTests.Administrative.Product
{
    public class TestProductApiController
    {
        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();
            var mockProductTypesApi = Substitute.For<IProductTypesApi>();
            var mockManufacturersApi = Substitute.For<IManufacturersApi>();
            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();
            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();
            var mockBrandApi = Substitute.For<IBrandApi>();


            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache, 
                productApi, 
                mockDownloadTimesApi, 
                mockProductTypesApi, 
                mockManufacturersApi, 
                mockInstallTypesApi, 
                mockMobileLookupApi, 
                mockBrandApi, 
                mockLocalizer);

            var request = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = 10,
                SortField = string.Empty,
                SortOrder = SortOrderEnum.Asc
            };

            //// Act
            var actionResult = await controller.GetProductsWithPaging(request);

            //// Assert
            var actualRecord = ((ObjectResult)actionResult).Value;
            Assert.Equal(((List<ProductModel>)actualRecord).Count, 10);
        }

        [Fact]
        public async Task CompareRecordsTest()
        {
            // Arrange
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();
            var mockProductTypesApi = Substitute.For<IProductTypesApi>();
            var mockManufacturersApi = Substitute.For<IManufacturersApi>();
            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();
            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();
            var mockBrandApi = Substitute.For<IBrandApi>();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache,
                productApi,
                mockDownloadTimesApi,
                mockProductTypesApi,
                mockManufacturersApi,
                mockInstallTypesApi,
                mockMobileLookupApi,
                mockBrandApi,
                mockLocalizer);

            var request = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = 10,
                SortField = string.Empty,
                SortOrder = SortOrderEnum.Asc
            };

            var testProducts = await mockRepo.GetAllProductsAsync();
            var firstProduct = new List<ProductModel>(testProducts)[0];

            //// Act
            var actionResult = await controller.GetProductsWithPaging(request);

            //// Assert
            var actualRecord = ((ObjectResult)actionResult).Value;
            Assert.Equal(((List<ProductModel>)actualRecord).Count, 10);
            var testProduct = ((List<ProductModel>)actualRecord)[0];

            Assert.Equal(JsonConvert.SerializeObject(firstProduct), JsonConvert.SerializeObject(testProduct));
        }

        [Fact]
        public async Task PagingRecordsTest()
        {
            // Arrange
            const int pageSize = 6;
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();
            var mockProductTypesApi = Substitute.For<IProductTypesApi>();
            var mockManufacturersApi = Substitute.For<IManufacturersApi>();
            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();
            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();
            var mockBrandApi = Substitute.For<IBrandApi>();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache,
                productApi,
                mockDownloadTimesApi,
                mockProductTypesApi,
                mockManufacturersApi,
                mockInstallTypesApi,
                mockMobileLookupApi,
                mockBrandApi,
                mockLocalizer);

            var request = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = pageSize,
                SortField = string.Empty,
                SortOrder = SortOrderEnum.Asc
            };


            //// Act
            var actionResult = await controller.GetProductsWithPaging(request);

            //// Assert
            var actualRecord = ((ObjectResult)actionResult).Value;
            Assert.Equal(pageSize, ((List<ProductModel>)actualRecord).Count);
        }

        [Fact]
        public async Task PagingRecordsExpectedRecordTest()
        {
            // Arrange
            const int pageSize = 6;
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();
            var mockProductTypesApi = Substitute.For<IProductTypesApi>();
            var mockManufacturersApi = Substitute.For<IManufacturersApi>();
            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();
            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();
            var mockBrandApi = Substitute.For<IBrandApi>();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache,
                productApi,
                mockDownloadTimesApi,
                mockProductTypesApi,
                mockManufacturersApi,
                mockInstallTypesApi,
                mockMobileLookupApi,
                mockBrandApi,
                mockLocalizer);

            var request = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 2,
                PageSize = pageSize,
                SortField = string.Empty,
                SortOrder = SortOrderEnum.Asc
            };

            var testProducts = await mockRepo.GetAllProductsAsync();
            var thirdRecord = new List<ProductModel>(testProducts)[2];

            //// Act
            var actionResult = await controller.GetProductsWithPaging(request);

            //// Assert
            var actualRecord = ((ObjectResult)actionResult).Value;
            var testProduct = ((List<ProductModel>)actualRecord)[0];

            Assert.Equal(JsonConvert.SerializeObject(thirdRecord), JsonConvert.SerializeObject(testProduct));
        }

        [Fact]
        public async Task BadRequestTest()
        {
            // Arrange
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();
            var mockProductTypesApi = Substitute.For<IProductTypesApi>();
            var mockManufacturersApi = Substitute.For<IManufacturersApi>();
            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();
            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();
            var mockBrandApi = Substitute.For<IBrandApi>();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache,
                productApi,
                mockDownloadTimesApi,
                mockProductTypesApi,
                mockManufacturersApi,
                mockInstallTypesApi,
                mockMobileLookupApi,
                mockBrandApi,
                mockLocalizer);

            var request = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = -1,
                PageSize = 0,
                SortField = string.Empty,
                SortOrder = SortOrderEnum.Asc
            };

            controller.ModelState.AddModelError("", "Error");


            //// Act
            var actionResult = await controller.GetProductsWithPaging(request);

            var badResponse = actionResult as BadRequestObjectResult;

            //// Assert
            Assert.NotNull(badResponse);
            Assert.Equal(badResponse.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetAllLookupsTest()
        {
            IProductRepository mockRepo = new MockProductRepository();

            var mockDownloadTimesApi = Substitute.For<IDownloadTimesApi>();

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

            mockDownloadTimesApi
                .GetAllDownloadTimesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<DownloadTimeModel>>(repositoryReturnValue));


            var mockProductTypesApi = Substitute.For<IProductTypesApi>();

            var productTypesRepositoryReturnValue = new List<ProductTypeModel>()
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

            mockProductTypesApi
                .GetAllProductTypesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ProductTypeModel>>(productTypesRepositoryReturnValue));


            var mockManufacturersApi = Substitute.For<IManufacturersApi>();

            var manufRepositoryReturnValue = new List<ManufacturerModel>()
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

            mockManufacturersApi
                .GetAllManufacturersAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ManufacturerModel>>(manufRepositoryReturnValue));


            var mockInstallTypesApi = Substitute.For<IInstallTypesApi>();


            var installTypesRepositoryReturnValue = new List<InstallTypeModel>()
            {
                new InstallTypeModel()
                {
                    Description = "Description 1",
                    InstallType = 1
                },
                new InstallTypeModel()
                {
                    Description = "Description 2",
                    InstallType = 2
                }
            };

            mockInstallTypesApi
                .GetAllInstallTypesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<InstallTypeModel>>(installTypesRepositoryReturnValue));


            var mockMobileLookupApi = Substitute.For<IMobileLookupApi>();

            var mobileRepositoryReturnValue = new List<MobileLookupModel>()
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

            mockMobileLookupApi
                .GetAllMobileLookupsAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<MobileLookupModel>>(mobileRepositoryReturnValue));

            var mockBrandApi = Substitute.For<IBrandApi>();

            var brandRepositoryReturnValue = new List<ProductBrandModel>()
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

            mockBrandApi
                .GetProductBrandsAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<ProductBrandModel>>(brandRepositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            IStringLocalizer<ProductsController> mockLocalizer = new MockStringLocalizer<ProductsController>();

            IProductApi productApi = new ProductApi(appSettings, mockRepo);
            var controller = new ProductsController(mockCache,
                productApi,
                mockDownloadTimesApi,
                mockProductTypesApi,
                mockManufacturersApi,
                mockInstallTypesApi,
                mockMobileLookupApi,
                mockBrandApi,
                mockLocalizer);

            //// Act
            var actionResult = await controller.GetLookupsForProducts();

            //// Assert
            var actualRecord = ((ObjectResult)actionResult).Value;
            Assert.Equal(((ProductLookupValuesModel)actualRecord).Brands.Count(), 2);
            Assert.Equal(((ProductLookupValuesModel)actualRecord).DownloadTimes.Count(), 2);
            Assert.Equal(((ProductLookupValuesModel)actualRecord).InstallTypes.Count(), 2);
            Assert.Equal(((ProductLookupValuesModel)actualRecord).Manufacturers.Count(), 2);
            Assert.Equal(((ProductLookupValuesModel)actualRecord).MobileLookups.Count(), 2);
            Assert.Equal(((ProductLookupValuesModel)actualRecord).ProductTypes.Count(), 2);
        }

        [Fact]
        public void PageSizeValidationSuccess()
        {
            var model = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = 10,
                SortField = null,
                SortOrder = SortOrderEnum.Asc
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.True(valid);

        }

        [Fact]
        public void PageSizeValidationFailure()
        {
            var model = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = 101,
                SortField = null,
                SortOrder = SortOrderEnum.Asc
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);

        }

        [Fact]
        public void FirstRecordNumberValidationSuccess()
        {
            var model = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = 0,
                PageSize = 10,
                SortField = null,
                SortOrder = SortOrderEnum.Asc
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.True(valid);

        }

        [Fact]
        public void FirstRecordNumberValidationFailure()
        {
            var model = new GetProductsWithPagingRequestModel
            {
                FirstRecordNumber = -1,
                PageSize = 101,
                SortField = null,
                SortOrder = SortOrderEnum.Asc
            };
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            Assert.False(valid);

        }
    }
}

using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.InstallType;
using Wp.CIS.LynkSystems.Interfaces.Lookup;
using Wp.CIS.LynkSystems.Model.Lookup;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.Services.Lookup;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using NSubstitute.ExceptionExtensions;

namespace CIS.WebApi.UnitTests.InstallTypes
{
    public class TestInstallTypesApiController
    {
        [Fact]
        public async Task SuccessTest()
        {
            // Arrange
            IInstallTypeRepository mockRepo = Substitute.For<IInstallTypeRepository>();

            var repositoryReturnValue = new List<InstallTypeModel>()
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

            mockRepo
                .GetAllInstallTypesAsync()
                .ReturnsForAnyArgs(Task.FromResult<IEnumerable<InstallTypeModel>>(repositoryReturnValue));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<InstallTypesController>();

            IInstallTypesApi theApi = new InstallTypesApi(appSettings, mockRepo);
            var controller = new InstallTypesController(mockCache, theApi, mockLocalizer);

            //// Act
            var response = await controller.Get();

            ////// Assert
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)response).Value;
            Assert.Equal(((List<InstallTypeModel>)actualRecord).Count, repositoryReturnValue.Count);
        }

        [Fact]
        public async Task ExceptionTest()
        {
            // Arrange
            IInstallTypeRepository mockRepo = Substitute.For<IInstallTypeRepository>();

            mockRepo
                .GetAllInstallTypesAsync()
                .ThrowsForAnyArgs(new Exception("Test Exception"));

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var mockLocalizer = new MockStringLocalizer<InstallTypesController>();

            IInstallTypesApi theApi = new InstallTypesApi(appSettings, mockRepo);
            var controller = new InstallTypesController(mockCache, theApi, mockLocalizer);

            //// Act
            var actionResult = await controller.Get();
            var objectResult = actionResult as Microsoft.AspNetCore.Mvc.ObjectResult;

            ////// Assert
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.InternalServerError);
        }
    }
}

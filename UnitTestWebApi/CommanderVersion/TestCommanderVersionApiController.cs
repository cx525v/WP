using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.CommanderVersion
{
    public class TestCommanderVersionApiController
    {
        [Fact]
        public async Task CommanderVersionControllerTestGetBaseVersions_Success()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi);

            //.. Act
            var dinfo = await controller.GetBaseVersions();
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            var activityVersionDescription = api.baseVersionInfo.ToList()[0].VersionDescription;

            //..Assert
            Assert.Equal(((List<BaseVersion>)actualRecord)[0].VersionDescription, activityVersionDescription);
            Assert.Equal(((List<BaseVersion>)actualRecord), api.baseVersionInfo.ToList());
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_Pattern1Success()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi);

            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = null,
                VersionDescription = "WPYPAK 06.02.06",
                CreatedByUser = "test"
            };
            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            int newVersionId = api.versionInfo.Max(s => Convert.ToInt32(s.VersionID));
            var currentVersion = api.versionInfo.Where(s => Convert.ToInt32(s.VersionID) == newVersionId).FirstOrDefault();

            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(commanderVersion, currentVersion);
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_Pattern2Success()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi);

            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = null,
                VersionDescription = "WPYPAK 6.02.06",
                CreatedByUser = "test"
            };
            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            int newVersionId = api.versionInfo.Max(s => Convert.ToInt32(s.VersionID));
            var currentVersion = api.versionInfo.Where(s => Convert.ToInt32(s.VersionID) == newVersionId).FirstOrDefault();

            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(commanderVersion, currentVersion);
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_FailInvalidFormat()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            var localizer = new MockStringLocalizer<CommanderVersionController>();
            localizer[0] = new LocalizedString("CommanderCreateversionsVersionFormatErrorMsg", "Invalid Version format");
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi, localizer);

            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = null,
                VersionDescription = "test 123"
            };
            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = (Microsoft.AspNetCore.Mvc.ObjectResult)dinfo;

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "Invalid Version format");
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_FailVersionAlreadyExist()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            var localizer = new MockStringLocalizer<CommanderVersionController>();
            localizer[0] = new LocalizedString("CommanderCreateversionsErrorMsg", "Same version already exists");
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi, localizer);


            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = null,
                VersionDescription = "WPYPAK 06.02.01"
            };
            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = (Microsoft.AspNetCore.Mvc.ObjectResult)dinfo;

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.InternalServerError);
            Assert.Equal((actualRecord.Value), "Same version already exists");
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_BaseVersionSuccess()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi);

            int baseversionId = 1;
            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = baseversionId,
                VersionDescription = "WPYPAK 6.02.07",
                CreatedByUser = "test"
            };

            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            int newVersionId = api.versionInfo.Max(s => Convert.ToInt32(s.VersionID));
            var newVersionMapping = api.epsMapping.Where(s => s.versionID == newVersionId).
                Select(s => new { s.pdlFlag, s.worldPayFieldName, s.worldPayTableName, s.worldPayJoinFields, s.worldPayCondition, s.worldPayOrderBy, s.worldPayFieldDescription, s.paramID, s.effectiveBeginDate, s.effectiveEndDate, s.viperCondition, s.viperFieldName, s.viperTableName, s.charStartIndex, s.charLength }).ToList();
            var oldVersionMapping = api.epsMapping.Where(s => s.versionID == baseversionId).
                Select(s => new { s.pdlFlag, s.worldPayFieldName, s.worldPayTableName, s.worldPayJoinFields, s.worldPayCondition, s.worldPayOrderBy, s.worldPayFieldDescription, s.paramID, s.effectiveBeginDate, s.effectiveEndDate, s.viperCondition, s.viperFieldName, s.viperTableName, s.charStartIndex, s.charLength }).ToList();

            var newTable = api.petroTable.Where(s => s.VersionID == newVersionId)
                .Select(s => new { s.SchemaDef, s.DefaultXML, s.Active, s.CreatedDate, s.DefinitionOnly, s.EffectiveDate, s.LastUpdatedBy, s.LastUpdatedDate, s.TableID, s.TableName }).ToList();
            var oldTable = api.petroTable.Where(s => s.VersionID == baseversionId)
                .Select(s => new { s.SchemaDef, s.DefaultXML, s.Active, s.CreatedDate, s.DefinitionOnly, s.EffectiveDate, s.LastUpdatedBy, s.LastUpdatedDate, s.TableID, s.TableName }).ToList();

            var currentVersion = api.versionInfo.Where(s => Convert.ToInt32(s.VersionID) == newVersionId).FirstOrDefault();
            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(currentVersion, commanderVersion);
            Assert.Equal(newVersionMapping, oldVersionMapping);
            Assert.Equal(newTable, oldTable);
        }

        [Fact]
        public async Task CommanderVersionControllerTestCreateVersion_BaseVersionFail()
        {
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();

            var localizer = new MockStringLocalizer<CommanderVersionController>();
            localizer[0] = new LocalizedString("CommanderCreateversionsErrorMsg", "Base version not exists");
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi, localizer);


            Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion = new Wp.CIS.LynkSystems.Model.CommanderVersion()
            {
                BaseVersionID = 1000,
                VersionDescription = "WPYPAK 6.02.08",
                CreatedByUser = "test"
            };
            //.. Act

            var dinfo = await controller.CreateVersion(commanderVersion);
            var actualRecord = (Microsoft.AspNetCore.Mvc.ObjectResult)dinfo;

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.InternalServerError);
            Assert.Equal((actualRecord.Value), "Base version not exists");
        }

        [Fact]
        public async Task CommanderVersionControllerTestDeleteVersion_Success()
        {
            int versionID = 1;
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi);

            //.. Act

            var dinfo = await controller.DeleteVersion(versionID, "");
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            bool versionExists = api.versionInfo.Any(s => Convert.ToInt32(s.VersionID) == versionID);
            bool mappingExists = api.epsMapping.Any(s => Convert.ToInt32(s.versionID) == versionID);
            bool tableExits = api.petroTable.Any(s => Convert.ToInt32(s.VersionID) == versionID);

            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(versionExists, false);
            Assert.Equal(mappingExists, false);
            Assert.Equal(tableExits, false);
        }

        [Fact]
        public async Task CommanderVersionControllerTestDeleteVersion_Fail()
        {
            var localizer = new MockStringLocalizer<CommanderVersionController>();
            localizer[0] = new LocalizedString("CommanderVersionDeleteErrorMsg", "Error occured while deleting version");
            int versionID = 100;
            MockCommanderVersionRepository api = new MockCommanderVersionRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ICommanderVersionApi commanderVersionApi = new CommanderVersionApi(appSettings, api);
            CommanderVersionController controller = FakeController(_cache, commanderVersionApi, localizer);
            //.. Act

            var dinfo = await controller.DeleteVersion(versionID, "");
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured while deleting version");
        }
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private static CommanderVersionController FakeController(IDistributedCache cache, ICommanderVersionApi commandApi, MockStringLocalizer<CommanderVersionController> localizer = null)
        {
            if (localizer == null)
                localizer = new MockStringLocalizer<CommanderVersionController>();
            IOperation fakeOperation = FakeOperation(cache);
            ILoggingFacade fakeLogger = FakeLogger();

            var controller = new CommanderVersionController(cache, commandApi, localizer, fakeOperation, fakeLogger)
            {
            };
            return controller;
        }

        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private static IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            return fakeOperation;
        }
        private static IConfigurationRoot getConfigurationBuilder()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
            return config;
        }
    }
}

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

namespace CIS.WebApi.UnitTests.EpsMapping
{
    public class TestEPSMappingApiController
    {
        [Fact]
        public async Task RetrieveEPSMappingAsync_Success()
        {
            int id = 3;
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            EPSMappingController controller = FakeController(_cache, epsMappingApi);
            
            //.. Act
            var dinfo = await controller.Get(id);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            //..Assert
            Assert.Equal((List<EPSMapping>)(actualRecord), bApi.epsMapping.Where(s => s.versionID == 3).ToList());
        }

        [Fact]
        public async Task RetrieveEPSMappingAsync_Fail()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingRetrieveErrorMsg", "Version id is not provided");
            EPSMappingController controller = FakeController(_cache, epsMappingApi,localizer);

            //.. Act
            var dinfo = await controller.Get(0);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal((actualRecord).StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Version id is not provided");
        }

        [Fact]
        public async Task InsertEPSMappingTest_Success()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            EPSMappingController controller = FakeController(_cache, epsMappingApi);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 1;
            epsMapping.pdlFlag = false;
            epsMapping.paramID = 200;
            epsMapping.worldPayFieldName = "PrimaryPhoneNbr";
            epsMapping.worldPayTableName = "tbTranSurcharge";
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount1";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.effectiveEndDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ Fee / FeeRow / FeeAmount";
            epsMapping.viperCondition = "Fee";
            epsMapping.charStartIndex = 102;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Post(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            //..Assert
            Assert.Equal(((bool)actualRecord), true);
        }

        [Fact]
        public async Task InsertEPSMappingTest_FailOnPdfFlagTrue()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingParamNameErrorMsg", "ParamName is mandatory");

            EPSMappingController controller = FakeController(_cache, epsMappingApi, localizer);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 1;
            epsMapping.pdlFlag = true;
            epsMapping.paramID = 200;
            epsMapping.worldPayFieldName = null;
            epsMapping.worldPayTableName = null;
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount1";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.effectiveEndDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ Fee / FeeRow / FeeAmount";
            epsMapping.viperCondition = null;
            epsMapping.charStartIndex = 102;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Post(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "ParamName is mandatory");
        }

        [Fact]
        public async Task InsertEPSMappingTest_FailOnPdfFlagFalse()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingTable_FieldNameErrorMsg", "Worldpay Table/Field name are mandatory");
            EPSMappingController controller = FakeController(_cache, epsMappingApi,localizer);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 1;
            epsMapping.pdlFlag = false;
            epsMapping.paramID = 200;
            epsMapping.worldPayFieldName = null;
            epsMapping.worldPayTableName = null;
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount1";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.effectiveEndDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ Fee / FeeRow / FeeAmount";
            epsMapping.viperCondition = null;
            epsMapping.charStartIndex = 102;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Post(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "Worldpay Table/Field name are mandatory");
        }

        [Fact]
        public async Task InsertEPSMappingTest_FailOnDate()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingDatesErrorMsg", "Effective Begin and end dates are mandatory");
            EPSMappingController controller = FakeController(_cache, epsMappingApi,localizer);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 1;
            epsMapping.pdlFlag = false;
            epsMapping.paramID = 200;
            epsMapping.worldPayFieldName = "PrimaryPhoneNbr";
            epsMapping.worldPayTableName = "tbTranSurcharge";
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount1";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ Fee / FeeRow / FeeAmount";
            epsMapping.viperCondition = "Fee";
            epsMapping.charStartIndex = 102;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Post(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);
            
            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "Effective Begin and end dates are mandatory");
        }

        [Fact]
        public async Task InsertEPSMappingTest_Fail()
        {
            MockEPSMappingApiRepository bApi = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingTable_FieldNameErrorMsg", "Worldpay Table/Field name are mandatory");

            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, bApi);

            EPSMappingController controller = FakeController(_cache, epsMappingApi, localizer);

            //.. Act
            var dinfo = await controller.Post(new EPSMapping());
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "Worldpay Table/Field name are mandatory");
        }

        //[Fact]
        //public async Task BulkInsertEPSMappingTest_Success()
        //{
        //    IDistributedCache _cache = FakeCache();
        //    Settings appSettings = new Settings() { CISConnectionString = getConfigurationBuilder()["ConnectionStrings:CISConnectionString"] };
        //    IOptions<Settings> options = Options.Create(appSettings);
        //    IEPSMappingApi epsMappingApi = new EPSMappingApi(options);
        //    EPSMappingController controller = FakeController(_cache, epsMappingApi);

        //    string formatFile = @"\\lynk\development\ApplicationSystemsStage\CIS\EPS\MappingFileFormat.xml";
        //    string dataFile = @"\\lynk\development\ApplicationSystemsStage\CIS\EPS\MappingDataFile_PN.dat";
        //    int versionId = 5;

        //    //.. Act
        //    var resultObj = await controller.Post(versionId, formatFile, dataFile);
        //    var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)resultObj).Value;

        //    //..Assert
        //    Assert.Equal(((bool)actualRecord), true);
        //}

        //[Fact]
        //public async Task CopyMappingTest_Success()
        //{
        //    IDistributedCache _cache = FakeCache();
        //    Settings appSettings = new Settings()
        //    {
        //        CISConnectionString = getConfigurationBuilder()["ConnectionStrings:CISConnectionString"]
        //    };
        //    IOptions<Settings> options = Options.Create(appSettings);
        //    IEPSMappingApi epsMappingApi = new EPSMappingApi(options);
        //    EPSMappingController controller = FakeController(_cache, epsMappingApi);

        //    int fromVersionId = 1;
        //    int toVersionId = 9;

        //    //.. Act
        //    var resultObj = await controller.Post(fromVersionId, toVersionId);
        //    var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult) resultObj).Value;

        //    //..Assert
        //    Assert.Equal(((bool) actualRecord), true);
        //}

        [Fact]
        public async Task UpdateEPSMappingTest_Success()
        {
            MockEPSMappingApiRepository api = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, api);
            EPSMappingController controller = FakeController(_cache, epsMappingApi);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 3;
            epsMapping.mappingID = 399;
            epsMapping.pdlFlag = true;
            epsMapping.paramID = 200;
            epsMapping.paramName = "Test";
            epsMapping.worldPayFieldName = null;
            epsMapping.worldPayTableName = null;
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount2";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.effectiveEndDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ FeeRow / FeeAmount";
            epsMapping.viperCondition = null;
            epsMapping.charStartIndex = 103;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Update(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            var updatedMapping = api.epsMapping.Where(s => s.versionID == 3).FirstOrDefault();
            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(updatedMapping, epsMapping);
        }

        [Fact]
        public async Task UpdateEPSMappingVersionDetailsTest_Success()
        {
            MockEPSMappingApiRepository api = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, api);
            EPSMappingController controller = FakeController(_cache, epsMappingApi);
            EPSMapping epsMapping = new EPSMapping();
            epsMapping.versionID = 3;
            epsMapping.mappingID = 399;
            epsMapping.pdlFlag = true;
            epsMapping.paramName = "Test";
            epsMapping.paramID = 200;
            epsMapping.worldPayFieldName = null;
            epsMapping.worldPayTableName = null;
            epsMapping.worldPayJoinFields = null;
            epsMapping.worldPayCondition = null;
            epsMapping.worldPayOrderBy = null;
            epsMapping.worldPayFieldDescription = "Master Cutoff Amount2";
            epsMapping.effectiveBeginDate = DateTime.Now;
            epsMapping.effectiveEndDate = DateTime.Now;
            epsMapping.viperTableName = "Fee";
            epsMapping.viperFieldName = "/ FeeRow / FeeAmount";
            epsMapping.viperCondition = null;
            epsMapping.charStartIndex = 103;
            epsMapping.charLength = 3;
            epsMapping.createdByUser = "test";
            //.. Act
            var dinfo = await controller.Update(epsMapping);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            var updatedMapping = api.epsMapping.Where(s => s.versionID == 3).FirstOrDefault();
            //..Assert
            Assert.Equal(epsMapping.pdlFlag, true);
            Assert.Equal(updatedMapping, epsMapping);
        }


        [Fact]
        public async Task UpdateEPSMappingTest_Fail()
        {
            MockEPSMappingApiRepository api = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, api);
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingTable_FieldNameErrorMsg", "Worldpay Table/Field name are mandatory");
            EPSMappingController controller = FakeController(_cache, epsMappingApi, localizer);

            //.. Act
            var dinfo = await controller.Update(new EPSMapping());
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal(actualRecord.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
            Assert.Equal(actualRecord.Value, "Worldpay Table/Field name are mandatory");
        }


        [Fact]
        public async Task CopyEpsMappingAsync_Success()
        {
            MockEPSMappingApiRepository api = new MockEPSMappingApiRepository();
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, api);
            EPSMappingController controller = FakeController(_cache, epsMappingApi);

            //.. Act
            var dinfo = await controller.Copy(new EPSCopyMapping() { FromVersionID = 1, ToVersionID = 2 });
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            var fromMapping = api.epsMapping.Where(s => s.versionID == 1).
                Select(s => new { s.pdlFlag, s.worldPayFieldName, s.worldPayTableName, s.worldPayJoinFields, s.worldPayCondition, s.worldPayOrderBy, s.worldPayFieldDescription, s.paramID, s.effectiveBeginDate, s.effectiveEndDate, s.viperCondition, s.viperFieldName, s.viperTableName, s.charStartIndex, s.charLength }).ToList();
            var toMapping = api.epsMapping.Where(s => s.versionID == 2).
                Select(s => new { s.pdlFlag, s.worldPayFieldName, s.worldPayTableName, s.worldPayJoinFields, s.worldPayCondition, s.worldPayOrderBy, s.worldPayFieldDescription, s.paramID, s.effectiveBeginDate, s.effectiveEndDate, s.viperCondition, s.viperFieldName, s.viperTableName, s.charStartIndex, s.charLength }).ToList();

            var newTable = api.petroTable.Where(s => s.VersionID == 1)
                .Select(s => new { s.SchemaDef, s.DefaultXML, s.Active, s.CreatedDate, s.DefinitionOnly, s.EffectiveDate, s.LastUpdatedBy, s.LastUpdatedDate, s.TableID, s.TableName }).ToList();
            var oldTable = api.petroTable.Where(s => s.VersionID == 2)
                .Select(s => new { s.SchemaDef, s.DefaultXML, s.Active, s.CreatedDate, s.DefinitionOnly, s.EffectiveDate, s.LastUpdatedBy, s.LastUpdatedDate, s.TableID, s.TableName }).ToList();

            //..Assert
            Assert.Equal(((bool)actualRecord), true);
            Assert.Equal(fromMapping, toMapping);
            Assert.Equal(newTable, oldTable);
        }

        [Fact]
        public async Task CopyEpsMappingAsync_FromVersionNotExistsFail()
        {
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, new MockEPSMappingApiRepository());
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingCopyErrorMsg", "Error occured while copying Mappings");

            EPSMappingController controller = FakeController(_cache, epsMappingApi, localizer);

            //.. Act
            var dinfo = await controller.Copy(new EPSCopyMapping() { FromVersionID = 5, ToVersionID = 2 });
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal((actualRecord.StatusCode), (int)System.Net.HttpStatusCode.InternalServerError);
            Assert.Equal((actualRecord.Value), "Error occured while copying Mappings");
        }

        [Fact]
        public async Task CopyEpsMappingAsync_ToVersionNotExistsFail()
        {
            IDistributedCache _cache = FakeCache();
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            IEPSMappingApi epsMappingApi = new EPSMappingApi(appSettings, new MockEPSMappingApiRepository());
            var localizer = new MockStringLocalizer<EPSMappingController>();
            localizer[0] = new LocalizedString("EPSMappingCopyErrorMsg", "Error occured while copying Mappings");

            EPSMappingController controller = FakeController(_cache, epsMappingApi, localizer);

            //.. Act
            var dinfo = await controller.Copy(new EPSCopyMapping() { FromVersionID = 1, ToVersionID = 6 });
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            //..Assert
            Assert.Equal((actualRecord.StatusCode), (int)System.Net.HttpStatusCode.InternalServerError);
            Assert.Equal((actualRecord.Value), "Error occured while copying Mappings");
        }

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private static EPSMappingController FakeController(IDistributedCache cache, IEPSMappingApi epsmapping, MockStringLocalizer<EPSMappingController> localizer = null)
        {

            if( localizer == null)
             localizer = new MockStringLocalizer<EPSMappingController>();
            IOperation fakeOperation = FakeOperation(cache);
            ILoggingFacade fakeLogger = FakeLogger();

            var controller = new EPSMappingController(cache, epsmapping, localizer, fakeOperation, fakeLogger)
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

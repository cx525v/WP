using CIS.WebApi.UnitTests.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.DashboardInfo;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.DashboardInfo
{
    public class TestDashboardInfoController
    {
        [Fact]
        public void DashboardInfoControllerTest_ModelState_Invalid()
        {
            //Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_TerminalSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);

	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).CustProfile.customerID, 393727);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).TermInfo.customerID, 393727);
        }

        [Fact]
        public async Task DashboardInfoControllerTest_MerchantSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockMerchantData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);

	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.customerID, 393727);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).CustProfile.customerID, 393727);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).TermInfo, null);
        }

        [Fact]
        public async Task DashboardInfoControllerTest_CustomerSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockCustomerData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);

	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo, null);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).CustProfile.customerID, 393727);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).TermInfo, null);
        }

 
        [Fact]
        public async Task DashboardInfoControllerTerminalTest_GetAnException()
        {
           // Arrange
           MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();
            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
          IStringLocalizer<DashboardInfoController> localizer
                      = Substitute.For<IStringLocalizer<DashboardInfoController>>();
            string key = "GetDashboardInfoErrorMessage";
            string value = "Test Localized String";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);    
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = new DashboardInfoApi(appSettings, mockRepo);
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, 0).ThrowsForAnyArgs(new System.Exception());
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);


           // Act
           var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, 0);
           var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

          // Assert
           Assert.Equal(actualRecord.StatusCode, 500);
           Assert.Equal(actualRecord.Value, "Test Localized String");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_TerminalAccountSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoCust, repository.custDemographicsList);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoMerch, repository.merchDemographicsList);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoTerm, repository.termDemographics);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.merchFedTaxID, "561005071");
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.acquiringBank, "Citizens Trust Tier 1 Tier 2");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_MerchantAccountSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockMerchantData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.MerchantNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoCust, repository.custDemographicsList);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoMerch, repository.merchDemographicsList);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoTerm, null);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.merchFedTaxID, "561005071");
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).MerchInfo.acquiringBank, "Citizens Trust Tier 1 Tier 2");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_CustomerAccountSuccess()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockCustomerData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid).Returns(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);
            // Act
            var dinfo = await controller.Get(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoCust, repository.custDemographicsList);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoMerch, null);
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoTerm, null);
        }

        [Fact]
        public async Task DashboardInfoControllerTerminalDetailsTest_GetAnException()
        {
            // Arrange
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();
            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IStringLocalizer<DashboardInfoController> localizer
                        = Substitute.For<IStringLocalizer<DashboardInfoController>>();
            string key = "GenericError";
            string value = "Error occured";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
	        ILoggingFacade fakeLogger = FakeLogger();
			IDashboardInfoApi dAPI = new DashboardInfoApi(appSettings, mockRepo);
            dAPI.GetTerminalDetails(0).ThrowsForAnyArgs(new System.Exception());
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);


            // Act
            var dinfo = await controller.GetTerminalDetails(0);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Error occured");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_TerminalDetailsAccountSuccess()
        {
            // Arrange
            int lid = 588799;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalDetails();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
	        ILoggingFacade fakeLogger = FakeLogger();
            IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetTerminalDetails(lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            // Act
            var dinfo = await controller.GetTerminalDetails(lid);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.TerminalDetails)actualRecord), expectedResult.Result);
        }
        #region Unit Test GetDashBoardInfoSearch

        [Fact]
        public void DashboardInfoControllerTest_GetDashBoardInfoSearch_ModelState_Invalid()
        {
            //Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();

            IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.DashboardInfo>())).DoNotCallBase();
            dAPI.GetDashboardSearchResults(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);

            //Act
            controller.ModelState.AddModelError("key", "error message");
            var dinfo = controller.GetDashBoardInfoSearch(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, Convert.ToString(lid)).Result;

            //Assert
            Assert.Equal(((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).StatusCode.ToString(), "400");
        }

        [Fact]
        public async Task DashboardInfoControllerTest_GetDashBoardInfoSearch_GetAnException()
        {
            // Arrange
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockTerminalData();
            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();

            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            IStringLocalizer<DashboardInfoController> localizer
                        = Substitute.For<IStringLocalizer<DashboardInfoController>>();
            string key = "GetDashboardInfoErrorMessage";
            string value = "Test Localized String";
            var localizedString = new LocalizedString(key, value);
            localizer[Arg.Any<string>()].ReturnsForAnyArgs(localizedString);
            IOperation fakeOperation = FakeOperation(_cache);
            IOptions<Settings> appSettings = Substitute.For<IOptions<Settings>>();
            ILoggingFacade fakeLogger = FakeLogger();
            IDashboardInfoApi dAPI = new DashboardInfoApi(appSettings, mockRepo);

            dAPI.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, 0).Throws(new Exception());
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);


            // Act
            var dinfo = await controller.GetDashBoardInfoSearch(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.TerminalNbr, "0");
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo);

            // Assert
            Assert.Equal(actualRecord.StatusCode, 500);
            Assert.Equal(actualRecord.Value, "Test Localized String");
        }

        [Fact]
        ///Unit Test for the RetrieveCache()
        public async Task DashboardInfoControllerTest_GetDashBoardInfoSearch_GetDataFromCache()
        {
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockCustomerData();
            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();
            IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.DashboardInfo>())).DoNotCallBase();

            fakeOperation.RetrieveCache("_FakeStringID", new Wp.CIS.LynkSystems.Model.DashboardInfo()).ReturnsForAnyArgs(expectedResult.Result);

            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);
            // Act
            var dinfo = await controller.GetDashBoardInfoSearch(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, Convert.ToString(lid));
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoCust, repository.custDemographicsList);
        }
        [Fact]
        public async Task DashboardInfoControllerTest_GetDashBoardInfoSearch_Success()
        {
            // Arrange
            int lid = 589547;
            MockDashboardInfoRepository repository = new MockDashboardInfoRepository();
            var expectedResult = repository.GetMockCustomerData();

            IDashboardInfoRepository mockRepo = FakeRepository();
            IDistributedCache _cache = FakeCache();
            IOperation fakeOperation = FakeOperation(_cache);
            ILoggingFacade fakeLogger = FakeLogger();
            IDashboardInfoApi dAPI = Substitute.For<IDashboardInfoApi>();
            dAPI.GetDashboardSearchResultsPagination(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, lid).ReturnsForAnyArgs(expectedResult);
            DashboardInfoController controller = FakeController(dAPI, mockRepo, _cache, fakeOperation, fakeLogger);
            // Act
            var dinfo = await controller.GetDashBoardInfoSearch(Wp.CIS.LynkSystems.Model.Helper.LIDTypes.CustomerNbr, Convert.ToString(lid));
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            // Assert
            Assert.Equal(((Wp.CIS.LynkSystems.Model.DashboardInfo)actualRecord).DemographicsInfoCust, repository.custDemographicsList);
        }

        #endregion

        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }

        private IDashboardInfoRepository FakeRepository()
        {
            return Substitute.For<IDashboardInfoRepository>();
        }
	    private ILoggingFacade FakeLogger()
	    {
		    return Substitute.For<ILoggingFacade>();
	    }

		private IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            fakeOperation.WhenForAnyArgs(x => x.RetrieveCache(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.DashboardInfo>())).DoNotCallBase();
            fakeOperation.WhenForAnyArgs(x => x.AddCacheAsync(Arg.Any<string>(), Arg.Any<Wp.CIS.LynkSystems.Model.DashboardInfo>())).DoNotCallBase();
            return fakeOperation;
        }

        private DashboardInfoController FakeController(IDashboardInfoApi dashboardInfo, IDashboardInfoRepository mockRepo, IDistributedCache mockCache, IOperation fakeOperation, ILoggingFacade loggingFacade)
        {
            var localizer = new MockStringLocalizer<DashboardInfoController>();
            localizer[0] = new LocalizedString("GenericError", "Error occured");
            return new DashboardInfoController(mockCache, dashboardInfo, localizer, fakeOperation, loggingFacade);
        }
    }
}

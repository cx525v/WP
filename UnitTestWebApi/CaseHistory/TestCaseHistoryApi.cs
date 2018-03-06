using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Wp.CIS.LynkSystems.Interfaces;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;
using Worldpay.CIS.DataAccess.ContactList;
using Microsoft.Extensions.Localization;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using System;
using NSubstitute.ExceptionExtensions;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;
using Worldpay.CIS.DataAccess.CaseHistory;
using CIS.WebApi.UnitTests.CaseHistory;

namespace Worldpay.CIS.WebApi.UnitTests.CaseHistory
{
    public class TestCaseHistoryApi
    {
        [Fact]
        public void CaseHistoryApiTest_Success()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";
            string ExtraId = null;
            

            PaginationCaseHistory page = new PaginationCaseHistory();
            
            int CaseID = 8715123;

            int lid = 648988;

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);
            
            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();

            ICaseHistoryRepository mockRepo = Substitute.For<ICaseHistoryRepository>();

            ICaseHistoryApi casehistoryApi = Substitute.For<ICaseHistoryApi>();

            mockRepo.GetCaseHistoryInfo(LIDType, LID, ExtraId, page).ReturnsForAnyArgs(expectedResult.Result);

            casehistoryApi = new CaseHistoryApi(optionsAccessor, mockRepo);

            // Act
            var casehistList = casehistoryApi.GetCaseHistory(LIDType, LID,  ExtraId, page).Result;

            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.CaseHistory>)casehistList.Result.ReturnedRecords; 

            string caseInfo = actualRecord.Where(x => x.caseId == CaseID).FirstOrDefault().caseLevel;

            //// Assert
            Assert.Equal(((IList<Wp.CIS.LynkSystems.Model.CaseHistory>)actualRecord).Count, 6);

            Assert.Equal(caseInfo, "Customer");
        }

        [Fact]
        public async Task CaseHistoryApiTest_Exception()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";

            PaginationCaseHistory page = new PaginationCaseHistory();

            int lid = 648988;

            string ExtraId = null;
            

            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);

            IOptions<Settings> optionsAccessor = Substitute.For<IOptions<Settings>>();

            ICaseHistoryRepository mockRepo = Substitute.For<ICaseHistoryRepository>();
            
            ICaseHistoryApi casehistoryApi = Substitute.For<ICaseHistoryApi>();
            
            IDistributedCache mockCache = Substitute.For<IDistributedCache>();
            
            mockRepo.GetCaseHistoryInfo(LIDType, LID, ExtraId, page).Throws(new Exception());

            casehistoryApi = new CaseHistoryApi(optionsAccessor, mockRepo);

            //Assert   
            await Assert.ThrowsAsync<Exception>(() => casehistoryApi.GetCaseHistory(LIDType, LID, ExtraId, page));
            
        }


        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }


        private ICaseHistoryRepository FakeRepository()
        {
            return Substitute.For<ICaseHistoryRepository>();

        }
    }
}

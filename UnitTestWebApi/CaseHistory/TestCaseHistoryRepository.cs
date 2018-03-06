
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Linq;
using Xunit;
using Worldpay.CIS.DataAccess.ContactList;
using Worldpay.CIS.DataAccess.CaseHistory;

using Worldpay.CIS.DataAccess.Connection;
using Wp.CIS.LynkSystems.Model;
using System.Collections.Generic;
using Wp.CIS.LynkSystems.Model.Pagination;
using Wp.CIS.LynkSystems.Model.Enums;
using CIS.WebApi.UnitTests.CaseHistory;

namespace Worldpay.CIS.WebApi.UnitTests.CaseHistory
{
    public class TestCaseHistoryRepository
    {
        [Fact]

        //Would be revisiting to modify the actual way of call method.
        public void CaseHistoryControllerTest_Success()
        {
            // Arrange
            LidTypeEnum LIDType = LidTypeEnum.CustomerNbr;
            string LID = "";
            PaginationCaseHistory page = new PaginationCaseHistory();
            
            string ExtraId = null;
            int lid = 648988;
            int CaseID = 8715123;
            
            MockCaseHistoryRepository mockCaseHistoryRepository = new MockCaseHistoryRepository();
            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expectedResult = mockCaseHistoryRepository.GetMockData(lid);
            
            IOptions<DataContext> optionsAccessor = Substitute.For<IOptions<DataContext>>();
            IDatabaseConnectionFactory connectionFactory = Substitute.For<IDatabaseConnectionFactory>();

            ICaseHistoryRepository mockRepo = Substitute.For<ICaseHistoryRepository>();
            
            mockRepo.GetCaseHistoryInfo(LIDType, LID, ExtraId, page).ReturnsForAnyArgs(expectedResult.Result);
            
            // Act
            var caseHistory = mockRepo.GetCaseHistoryInfo(LIDType, "648988", ExtraId, page).Result;
            
            var actualRecord = (IList<Wp.CIS.LynkSystems.Model.CaseHistory>)caseHistory.ReturnedRecords;

            string caseInfo = actualRecord.Where(x => x.caseId == CaseID).FirstOrDefault().caseLevel;
            
            //// Assert

            Assert.Equal(((IList<Wp.CIS.LynkSystems.Model.CaseHistory>)actualRecord).Count, 6);

            Assert.Equal(caseInfo, "Customer");
        }
    }
}

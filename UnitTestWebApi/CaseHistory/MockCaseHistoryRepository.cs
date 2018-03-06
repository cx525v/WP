using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.TransactionsInqDetailsInfo;
using Worldpay.CIS.DataAccess.CaseHistory;
using Wp.CIS.LynkSystems.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace CIS.WebApi.UnitTests.CaseHistory
{
    //public class MockCaseHistoryRepository : ICaseHistoryRepository
     public class MockCaseHistoryRepository 
    {
        public ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> GetMockData(int merchantid)
        {
            ICollection<Wp.CIS.LynkSystems.Model.CaseHistory> contactResults = new Collection<Wp.CIS.LynkSystems.Model.CaseHistory>()
            {
                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },

                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },

                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },

                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },

                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },
                new Wp.CIS.LynkSystems.Model.CaseHistory(){
                        caseId      = 8715123, createDate =  new DateTime(2008,06,13), caseDesc = " ", caseDescId  =   null,
                    orgDeptName     = "Customer Care", terminalId  =   "  ", merchantId = 648988, merchantNbr = "  ",  customerNbr =  "1000446304",
                    merchantName    = null,  currDept = null, referredFrom = null, priorityId = 3, closedDate = new DateTime(2008,06,13),
                    rtnToOriginator = false, hasAttachment = true, hasCustomForm = false, hasReminder = false, parentCaseId =  0,
                    caseStatusId    = 1, hasEscalated = false, isCaseOpen = false, caseLevel = "Customer"
                    },
            };

            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>> expected = new ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>>()
            {
                Result = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.CaseHistory>() {
                    PageSize=500,
                    SkipRecords=0,
                    TotalNumberOfRecords=500,
                    ReturnedRecords = contactResults.Where(x => x.merchantId == merchantid).ToList()

                }
            };
            return expected;
        }
    }
}

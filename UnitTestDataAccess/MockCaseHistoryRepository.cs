using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model;

namespace UnitTestDataAccess
{
    public class MockCaseHistoryRepository
    {
        private List<Wp.CIS.LynkSystems.Model.CaseHistory> caseHistList;
        public MockCaseHistoryRepository()
        {
            caseHistList = new List<CaseHistory>
            {
                new CaseHistory
                {
                    caseId=8854517,
                    createDate = DateTime.Parse("2008-07-23 16:59:02.977"),
                    customerNbr="1000098113",
                    merchantNbr="542929008018764",
                    terminalId = "LYK12345",
                    merchantId=195225,
                    caseDescId = null,
                    caseDesc = null,
                    merchantName="Rita B's Salon                                   ",
                    orgDeptName="Sales Support",
                    isCaseOpen = false,
                    currDept="",
                    referredFrom="",priorityId = 2,
                    closedDate= DateTime.Parse("2008-07-23 16:59:27.000"),
                    rtnToOriginator = true,
                    hasAttachment=true,
                    hasCustomForm = false,
                    hasReminder = false,
                    parentCaseId=0,
                    caseStatusId=1,
                    hasEscalated=false
                    
                },
                new CaseHistory
                {
                    caseId=8839240,
                    createDate = DateTime.Parse("2008-07-18 15:56:42.560"),
                    customerNbr="1000098113",
                    merchantNbr="542929008018764",
                    merchantId=195225,
                    terminalId = "LYK12345",
                    caseDescId = null,
                    caseDesc = null,
                    merchantName="Rita B's Salon                                   ",
                    orgDeptName="Sales Support",
                    isCaseOpen = false,
                    currDept="",
                    referredFrom="", priorityId = 2,
                    closedDate= DateTime.Parse("2008-07-18 15:56:52.000"),
                    rtnToOriginator = true,
                    hasAttachment=true,
                    hasCustomForm = false,
                    hasReminder = false,
                    parentCaseId=0,
                    caseStatusId=1,
                    hasEscalated=false
                }
            };

        }

        public List<CaseHistory> GetCaseHistory()
        {
            return caseHistList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace CIS.WebApi.UnitTests.TerminalList
{
    public class MockTerminalListRepository
    {

        public ApiResult<GenericPaginationResponse<Terminal>> GetMockData(int terminalNbr)
        {
            IList<Terminal> merchResults = new List<Terminal>()
            {
                new Terminal(){
                    TerminalNbr = 589587, TerminalID = "LK429486", Software = "LSPR3271",
                    DeactivateActivateDate = DateTime.Now, Status = "Active", Equipment = "PDBI"
                    },
                new Terminal(){
                TerminalNbr = 589588, TerminalID = "LK429489", Software = "LSPR3277",
                DeactivateActivateDate = DateTime.Now, Status = "Active", Equipment = "PDBI1"
                }               

            };

            ApiResult<GenericPaginationResponse<Terminal>> expected = new ApiResult<GenericPaginationResponse<Terminal>>()
            {
                Result = new GenericPaginationResponse<Terminal>()
                {
                    PageSize = 500,
                    SkipRecords = 0,
                    TotalNumberOfRecords = 8,
                    ReturnedRecords = merchResults.Where(x => x.TerminalNbr == terminalNbr).ToList()
                }                
            };
            return expected;
        }

        #region Old MockData
        public ApiResult<ICollection<Terminal>> GetMockData(string terminalNbr)
        {
            int terminalNo = Convert.ToInt32(terminalNbr);
            IList<Terminal> merchResults = new List<Terminal>()
            {
                new Terminal(){
                    TerminalNbr = 589587, TerminalID = "LK429486", Software = "LSPR3271",
                    DeactivateActivateDate = DateTime.Now, Status = "Active", Equipment = "PDBI"
                    },
                new Terminal(){
                TerminalNbr = 589588, TerminalID = "LK429489", Software = "LSPR3277",
                DeactivateActivateDate = DateTime.Now, Status = "Active", Equipment = "PDBI1"
                }

            };

            ApiResult<ICollection<Terminal>> expected = new ApiResult<ICollection<Terminal>>()
            {
                Result = merchResults.Where(x => x.TerminalNbr == terminalNo).ToList()
            };
            return expected;
        }

        #endregion
        public PaginationTerminal GetPagination()
        {
            PaginationTerminal page = new PaginationTerminal();
            page.FilterTID = "MerchantID";
            page.FilterSoftware = "";

            return page;
        }
    }
}

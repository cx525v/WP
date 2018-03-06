using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wp.CIS.LynkSystems.Model;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace CIS.WebApi.UnitTests.TransactionHistoryList
{
    public class MockTransactionHistoryRepository
    {

        public ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> GetMockData(string transactionType)
        {
            IList<Wp.CIS.LynkSystems.Model.TransactionHistory> merchResults = new List<Wp.CIS.LynkSystems.Model.TransactionHistory>()
            {
                new Wp.CIS.LynkSystems.Model.TransactionHistory(){
                    REQ_AMT = "589587", REQ_TRAN_TYPE = "Credit", REQ_PAN_4 = "1234",
                    REQ_BUS_DATE = DateTime.Now, RESP_NETWRK_AUTH_CD = "Active"
                    },
                new Wp.CIS.LynkSystems.Model.TransactionHistory(){
                REQ_AMT = "589588", REQ_TRAN_TYPE = "Debit", REQ_PAN_4 = "1434",
                REQ_BUS_DATE = DateTime.Now, RESP_NETWRK_AUTH_CD = "Active"
                }               

            };

            ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>> expected = new ApiResult<GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>>()
            {
                Result = new GenericPaginationResponse<Wp.CIS.LynkSystems.Model.TransactionHistory>()
                {
                    PageSize = 500,
                    SkipRecords = 0,
                    TotalNumberOfRecords = 8,
                    ReturnedRecords = merchResults.Where(x => x.REQ_TRAN_TYPE == transactionType).ToList()
                }                
            };
            return expected;
        }
        
        public PaginationTransactionHistory GetPagination()
        {
            PaginationTransactionHistory page = new PaginationTransactionHistory();
            page.FilterByNetworkCD = "MerchantID";
            page.FilterByLast4 = "1234";

            return page;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Wp.CIS.LynkSystems.Model;
using System.Collections.ObjectModel;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace CIS.WebApi.UnitTests.MerchantList
{
    public class MockMerchantListRepository
    {
        public ApiResult<GenericPaginationResponse<Merchant>> GetMockData(int CustId)
        {
            ICollection<Merchant> merchResults = new Collection<Merchant>()
            {
                new Merchant(){
                    MID = "191805", CustomerID = 191809, Name = "ABC Corp12",
                    State = "GA", ZipCode = "30648", StatusIndicator = "Active"
                    },
                new Merchant(){
                    MID = "191807", CustomerID = 191809, Name = "ABC Corp",
                    State = "GA", ZipCode = "30648", StatusIndicator = "Active"
                    },
                new Merchant(){
                    MID = "191807", CustomerID = 89765, Name = "ABC Corp123",
                    State = "GA", ZipCode = "30648", StatusIndicator = "Active"
                    },
                new Merchant(){
                    MID = "191807", CustomerID = 89765, Name = "ABC Corp145",
                    State = "GA", ZipCode = "30648", StatusIndicator = "Active"
                    }
            };

            ApiResult<GenericPaginationResponse<Merchant>> expected = new ApiResult<GenericPaginationResponse<Merchant>>()
            {
                Result = new GenericPaginationResponse<Merchant>()
                {
                    PageSize = 500,
                    SkipRecords = 0,
                    TotalNumberOfRecords = 8,
                    ReturnedRecords = merchResults.Where(x => x.CustomerID == CustId).ToList()

                }
                //Result = merchResults.Where(x => x.CustomerID == CustId ).ToList()
            };
            return expected;
        }

        public PaginationMerchant GetPagination()
        {
            PaginationMerchant page = new PaginationMerchant();
            page.FilterMID = "MerchantID";
            page.FilterName = "";

            return page;
        }



    }
}

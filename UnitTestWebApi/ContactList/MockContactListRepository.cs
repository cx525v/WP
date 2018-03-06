using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Wp.CIS.LynkSystems.Model;
using System.Collections.ObjectModel;

namespace CIS.WebApi.UnitTests.ContactList
{
    public class MockContactListRepository
    {
        public ApiResult<GenericPaginationResponse<Demographics>> GetMockData(string ssn)
        {
            ICollection<Demographics> contactResults = new Collection<Demographics>()
            {
                new Demographics(){
                    Level = "Customer", NameAddressID = 3301636, Name = "Golden Corral Corporation",
                    Address = "5151 Glenwood Ave.", State = "NC", City ="Raleigh", ZipCode = "27612", Contact="Terri Warren", SSN="3425"
                    },
                new Demographics(){
                    Level = "Customer", NameAddressID = 6258471, Name = "Golden Corral Corporation",
                    State = "NC", ZipCode = "27612", Title = "Terri Warren", Contact="ABC", SSN="3425"
                    },
                new Demographics(){
                    Level = "Merchant", NameAddressID = 3301638, Address = "5151 Glenwood Ave", Name = "ABC Corp12",
                    State = "NC", ZipCode = "27614", Title = "Terri Warren", Contact="Terri Warren", SSN="3425"
                    },
                new Demographics(){
                    Level = "Customer", NameAddressID = 3301639, Address = "5151 Glenwood Ave1", Name = "ABC Corp12",
                    State = "NC", ZipCode = "27612", Title = "Terri Warren", Contact="5156 Glenwood Ave", SSN="5467"
                    },
                new Demographics(){
                    Level = "Merchant", NameAddressID = 3301836, Address = "5151 Glenwood Ave", Name = "ABC Corp12",
                    State = "NC", ZipCode = "27612", Title = "Active", Contact="5151 Glenwood Ave", SSN="5467"
                    },
                new Demographics(){
                    Level = "Terminal", NameAddressID = 3304636, Address = "5151 Glenwood Ave", Name = "ABC Corp12",
                    State = "NC", ZipCode = "27612", Title = "Active", Contact="5151 Glenwood Ave", SSN="5467"
                    },
                new Demographics(){
                    Level = "Terminal", NameAddressID = 3321636, Address = "5151 Glenwood Ave", Name = "ABC Corp12",
                    State = "NC", ZipCode = "27612", Title = "Active", Contact="5151 Glenwood Ave", SSN="5467"
                    },

            };

            ApiResult<GenericPaginationResponse<Demographics>> expected = new ApiResult<GenericPaginationResponse<Demographics>>()
            {
                Result = new GenericPaginationResponse<Demographics>()
                {
                    PageSize = 500,                    
                    SkipRecords = 0,
                    TotalNumberOfRecords = 8,
                    ReturnedRecords = contactResults.Where(x => x.SSN == ssn).ToList()

                 }

                //Result = new GenericPaginationResponse {
                //    d = contactResults.Where(x => x.SSN == ssn).ToList();
            };
         
                return expected;
        }


               
    }
}

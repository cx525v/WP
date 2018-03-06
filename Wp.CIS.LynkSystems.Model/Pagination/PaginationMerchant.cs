using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class PaginationMerchant : Pagination
    {
        public string FilterMID { get; set; }
        public string FilterName { get; set; }
        public string FilterState { get; set; }
        public string FilterZipCode { get; set; }
        public string FilterStatusIndicator { get; set; }

    }
}

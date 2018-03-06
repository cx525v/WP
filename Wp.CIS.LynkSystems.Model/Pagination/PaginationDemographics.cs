using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class PaginationDemographics :Pagination
    {
        public string FilterContact { get; set; }
        public string FilterRole { get; set; }
        public string FilterLast4 { get; set; }
    }
}

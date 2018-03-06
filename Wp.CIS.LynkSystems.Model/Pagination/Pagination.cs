using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class Pagination
    {
        public int PageSize { get; set; }

        public int SkipRecordNumber { get; set; }

        public string SortField { get; set; }
        public Boolean SortFieldByAsc  { get; set; }

        
    }
}

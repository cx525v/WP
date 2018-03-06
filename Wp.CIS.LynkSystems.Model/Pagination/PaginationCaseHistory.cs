using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class PaginationCaseHistory : Pagination
    {
        public string FilterCaseId { get; set; }
        public string FilterCaseDesc { get; set; }
        public string FilterOrgDeptName { get; set; }
        public string FilterCaseLevel { get; set; }
        public DateTime? FilterCreateDate { get; set; }
    }
}

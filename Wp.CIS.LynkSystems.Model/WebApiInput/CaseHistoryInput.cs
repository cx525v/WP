using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Model.WebApiInput
{
    public class CaseHistoryInput : ClientInputBase
    {
        public PaginationCaseHistory Page { get; set; }
        public string ExtraID { get; set; }
    }
}

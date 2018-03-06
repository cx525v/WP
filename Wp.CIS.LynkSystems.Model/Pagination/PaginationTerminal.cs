using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class PaginationTerminal : Pagination
    {
        public string FilterTID { get; set; }
        public string FilterDate { get; set; }
        public string FilterSoftware { get; set; }
        public string FilterStatus { get; set; }
        public string FilterStatusEquipment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Model.Dashboard
{
    public class DashboardPrimaryKeysModel
    {
        public int? TerminalNbr { get; set; }

        public int? MerchantID { get; set; }

        public int? CustomerID { get; set; }

        public int? ConvertedLidPk { get; set; }

        public bool RecordFound { get; set; }

        public LidTypeEnum LidType { get; set; }
    }
}

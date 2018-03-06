using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Model.Administrative
{
    public class AuditHistoryModel
    {
        public LidTypeEnum LidType { get; set; }

        public int Lid { get; set; }

        public ActionTypeEnum ActionType { get; set; }

        public DateTime ActionDate { get; set; }

        public string UserName { get; set; }

        public string Notes { get; set; }

        public int AuditId { get; set; }
    }
}

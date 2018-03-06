using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class EPSPetroAudit
    {
        public int auditId { get; set; }
        public int versionId { get; set; }
        public string actionType { get; set; }
        public string entityName { get; set; }
        public string previousValue { get; set; }
        public string newValue { get; set; }
        public string userName { get; set; }
        public DateTime auditDate { get; set; }
        public string tableID { get; set; }
        public string tableName { get; set; }

    }
}

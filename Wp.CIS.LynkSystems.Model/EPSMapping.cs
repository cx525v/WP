using System;
using System.ComponentModel;

namespace Wp.CIS.LynkSystems.Model
{
    public class EPSMapping
    {
        public int versionID { get; set; }
        public int? mappingID { get; set; }
        public bool pdlFlag { get; set; }

        public int? paramID { get; set; }
        public string paramName { get; set; }

        public string worldPayFieldName { get; set; }
        public string worldPayTableName { get; set; }
        public string worldPayJoinFields { get; set; }
        public string worldPayCondition { get; set; }
        public string worldPayOrderBy { get; set; }
        public string worldPayFieldDescription { get; set; }
        public DateTime? effectiveBeginDate { get; set; }
        public DateTime? effectiveEndDate { get; set; }
        public string viperTableName { get; set; }
        public string viperFieldName { get; set; }
        public string viperCondition { get; set; }
        public int? charStartIndex { get; set; }
        public int? charLength { get; set; }
        [DefaultValue("admin")]
        public string createdByUser { get; set; }
        public string lastUpdatedBy { get; set; }
    }
}

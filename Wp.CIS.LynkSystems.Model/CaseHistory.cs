using System;

namespace Wp.CIS.LynkSystems.Model
{
    public class CaseHistory
    {
        public int caseId { get; set; }
        public DateTime? createDate { get; set; }
        public string caseDesc { get; set; }
        public int? caseDescId { get; set; }
        public string caseLevel { get; set; }
        public string orgDeptName { get; set; }
        public string terminalId { get; set; }
        public int? merchantId { get; set; }
        public int? customerId { get; set; }
        public string merchantNbr { get; set; }
        public string customerNbr { get; set; }
        public string merchantName { get; set; }
        public string currDept { get; set; }
        public string referredFrom { get; set; }
        public int? priorityId { get; set; }
        public DateTime? closedDate { get; set; }
        public bool? rtnToOriginator { get; set; }
        public bool? hasAttachment { get; set; }
        public bool? hasCustomForm { get; set; }
        public bool? hasReminder { get; set; }
        public int? parentCaseId { get; set; }
        public int? caseStatusId { get; set; }
        public bool? hasEscalated { get; set; }
        public bool? isCaseOpen { get; set; }
    }
}

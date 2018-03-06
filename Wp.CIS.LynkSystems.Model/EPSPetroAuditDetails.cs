namespace Wp.CIS.LynkSystems.Model
{
    public class EPSPetroAuditDetails
    {
        public string affectedField { get; set; }
        public string prevValue { get; set; }
        public string newValue { get; set; }
        public int auditId { get; set; }
    }
}

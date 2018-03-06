namespace Wp.CIS.LynkSystems.Model
{
    public class PatientAddress
    {
        public long PatientAddressId { get; set; }
        public byte[] RecordVersion { get; set; }
        public long PatientId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? AddressTypeId { get; set; }
    }
}
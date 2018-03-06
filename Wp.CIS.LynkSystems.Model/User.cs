namespace Wp.CIS.LynkSystems.Model
{
    public class User
    {
        public int UserId { get; set; }
        public byte[] RecordVersion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber1 { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? TitleId { get; set; }
        public string DeaNumber { get; set; }
        public string NpiNumber { get; set; }
    }
}
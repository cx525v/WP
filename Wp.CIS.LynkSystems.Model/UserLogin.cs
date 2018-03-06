using System;

namespace Wp.CIS.LynkSystems.Model
{
    public class UserLogin : User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Activated { get; set; }
        public Guid? ActivationKey { get; set; }
        public byte? PasswordAttempts { get; set; }
        public DateTime? LastPasswordSet { get; set; }
        public bool IsActiveUser { get; set; }
        public int? SecurityQuestion1Id { get; set; }
        public string SecurityAnswer1 { get; set; }
        public int? SecurityQuestion2Id { get; set; }
        public string SecurityAnswer2 { get; set; }
    }
}
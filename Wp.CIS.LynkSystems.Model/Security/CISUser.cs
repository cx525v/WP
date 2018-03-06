using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Authentication
{
    public class CISUser
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name ="IsAuthenticated")]
        public bool IsAuthenticated { get; set; }
        public UserRole UserRole { get; set; }

        [Display(Name = "DomainName")]
        public string DomainName { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Wp.CIS.LynkSystems.Model.Authentication
{
    public class UserRole : IdentityRole
    {
        [Required]
        [Display(Name = "UserInRole")]
        [DefaultValue(Roles.CIS_STAGE_Reader)]
        public Roles Role { get; set; }

        public string Contract { get; set; }
    }
   
    public enum Roles
    {         
        CIS_Writer,
        CIS_Reader,
        CIS_Admin,
        CIS_STAGE_Writer,
        CIS_STAGE_Reader,
        CIS_STAGE_Admin
    }
}

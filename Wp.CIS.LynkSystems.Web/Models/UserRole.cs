using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Web.Models
{
    public class UserRole 
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


using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace Wp.CIS.LynkSystems.Model.Authentication
{
    public class Users : IdentityUser
    {
        public string Name { get; set; }
    }
}

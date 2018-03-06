using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wp.CIS.LynkSystems.Web.Models;
using Wp.CIS.LynkSystems.Model.Authentication;

namespace Wp.CIS.LynkSystems.Web.Models
{
    public class IndexPageModel
    {
        public CISUser User { get; set; }

        public AppConfigSettings AppConfigSettings { get; set; }
    }
}

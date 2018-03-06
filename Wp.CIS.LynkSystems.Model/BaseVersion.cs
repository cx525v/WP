using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Wp.CIS.LynkSystems.Model
{
   public class BaseVersion
    {
        public string VersionID { get; set; }
        public string VersionDescription { get; set; }

        [DefaultValue("admin")]
        public string CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; }
    }
}

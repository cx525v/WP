using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Model.WebApiInput
{
    public class ClientInputBase
    {
        public LidTypeEnum lidTypeEnum { get; set; }
        public string LIDValue { get; set; }
    }
}

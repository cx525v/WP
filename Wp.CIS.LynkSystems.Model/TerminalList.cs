using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class Terminal
    {
        [Display(Name = "MerchantID")]
        public string MID { get; set; }
        public int TerminalNbr { get; set; }
        public string TerminalID { get; set; }

        public string Equipment { get; set; }

        public string Software { get; set; }

        public DateTime DeactivateActivateDate { get; set; }

        public string Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class Merchant
    {
        public int CustomerID { get; set; }

        [Display(Name = "MerchantNbr")]
        public string MID { get; set; }

        public int MerchantID { get; set; }

        [Display(Name = "Merchant Name")]
        public string Name { get; set; }

        [Display(Name = "Merchant State")]
        public string State { get; set; }

        public string ZipCode { get; set; }

        public string StatusIndicator { get; set; }
    }
}
 
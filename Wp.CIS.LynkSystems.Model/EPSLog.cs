using System.ComponentModel.DataAnnotations;

namespace Wp.CIS.LynkSystems.Model
{
    public class EPSLog
    {
        public string merchantNbr { get; set; }
        public string terminalID { get; set; }

        [Display(Name = "Download Date")]
        public string downloadDate { get; set; }

        [Display(Name = "Action Type")]
        public string actionType { get; set; }
        public string download { get; set; }
        public string success { get; set; }

        public string responseMessage { get; set; }
    }
}
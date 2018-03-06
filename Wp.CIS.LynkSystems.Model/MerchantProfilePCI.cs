using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Model
{
    public class MerchantProfilePCI
    {
        public int MerchantId { get; set; }
        public int action { get; set; }
        public int enrollOptionChanged{ get; set; }
        public int enrollOption{ get; set; }
        public int enrollOptionNotes{ get; set; }
        public int communicationOptionChanged{ get; set; }
        public int communicationOption{ get; set; }
        public int communicationOptionNotes{ get; set; }
        public int fineOptionChanged{ get; set; }
        public int fineOption{ get; set; }
        public int fineOptionNotes{ get; set; }
    }
}


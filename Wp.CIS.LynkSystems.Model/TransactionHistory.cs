using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class TransactionHistory
    {
        public DateTime REQ_BUS_DATE { get; set; }

        public string REQ_AMT { get; set; }

        public string REQ_PAN_4 { get; set; }
        public string REQ_TRAN_TYPE { get; set; }
        public string DESCRIPT { get; set; }
        public string RESP_NETWRK_AUTH_CD { get; set; }
    }
}

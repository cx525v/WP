using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Enums;

namespace Wp.CIS.LynkSystems.Model.Pagination
{
    public class PaginationTransactionHistory : Pagination
    {        
        public string FilterByDate { get; set; }
        public string FilterByAmount { get; set; }
        public string FilterByLast4 { get; set; }
        public string FilterByTranType { get; set; }
        public string FilterByNetworkCD { get; set; }
        public string FilterByDesc { get; set; }

        public TransactionTypeEnum TransactionType;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Model.WebApiInput
{
    public class TransactionHistoryInput : ClientInputBase
    {
        public PaginationTransactionHistory Page { get; set; }        
    }
}

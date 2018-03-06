using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Model.WebApiInput
{
    public class TerminalListInput : ClientInputBase
    {
        public PaginationTerminal Page { get; set; }
    }
}

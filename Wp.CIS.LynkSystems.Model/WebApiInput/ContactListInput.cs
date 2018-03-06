using System;
using System.Collections.Generic;
using System.Text;
using Wp.CIS.LynkSystems.Model.Enums;
using Wp.CIS.LynkSystems.Model.Pagination;

namespace Wp.CIS.LynkSystems.Model.WebApiInput
{
    public class ContactListInput : ClientInputBase
    {
        public PaginationDemographics Page { get; set; }
    }
}

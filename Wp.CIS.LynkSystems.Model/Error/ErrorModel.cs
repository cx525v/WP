using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Error
{
    public class ErrorModel
    {
        public string ErrorId { get; set; }

        public string Description { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}

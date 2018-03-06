using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Error
{
    public class GenericErrorInfoResponse<T>
    {
        public bool DidSucceed { get; set; }

        public T ErrorCode { get; set; }
    }
}

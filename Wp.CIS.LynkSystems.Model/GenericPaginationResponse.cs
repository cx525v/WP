using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class GenericPaginationResponse<T>
    {
        public int SkipRecords { get; set; }

        public int PageSize { get; set; }

        public int TotalNumberOfRecords { get; set; }

        public IEnumerable<T> ReturnedRecords { get; set; }

        public string ModelMessage { get; set; }
    }
}

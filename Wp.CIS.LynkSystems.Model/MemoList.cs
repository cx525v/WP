using System.Collections.Generic;

namespace Wp.CIS.LynkSystems.Model
{
    public class MemoList
    {
        public List<MemoInfo> customerMemo { get; set; }
        public List<MemoInfo> merchMemo { get; set; }
        public List<MemoInfo> termMemo { get; set; }
        public List<MemoInfo> groupMemo { get; set; }
    }
}

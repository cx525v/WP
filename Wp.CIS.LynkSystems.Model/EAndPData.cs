using System.Collections.Generic;

namespace Wp.CIS.LynkSystems.Model
{
    public class EAndPData
    {
        public TerminalDetails terminalDetails { get; set; }
        public ActiveServices activeServices { get; set; }
        public TerminalInfo terminalInfo { get; set; }     
        public SensitivityInfo sensitivityInfo { get; set; }
    }
}

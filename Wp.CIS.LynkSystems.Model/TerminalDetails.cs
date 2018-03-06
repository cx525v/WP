using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model
{
    public class TerminalDetails
    {
        public int debit { get; set; }
        public int credit { get; set; }
        public int checkSvc { get; set; }
        public int pob { get; set; }
        public int visaMC { get; set; }
        public int discover { get; set; }
        public int discCanx { get; set; }
        public int diners { get; set; }
        public int amex { get; set; }
        public int amexCanx { get; set; }
        public int revPip { get; set; }
        public int ebt { get; set; }
        public int rewardsLynk { get; set; }
        public int giftLynk { get; set; }
        public string tid { get; set; }
        public string autoSettleOverride { get; set; }
        public string cutOffTime { get; set; }
        public string timeZone { get; set; }
        public string autoSettleIndicator { get; set; }
        public string autoSettleTime { get; set; }
        public string terminalDescription { get; set; }
        public string lynkAdvantageDesc { get; set; }
        public string terminalType { get; set; }
    }
}

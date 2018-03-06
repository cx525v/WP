using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Model
{
   public class CustomerProfile
    {
       public int customerID { get; set; }
        public string description { get; set; }
        public DateTime activationDt { get; set; }
        public int statusIndicator { get; set; }
        public int legalType { get; set; }
        public string businessEstablishedDate { get; set; }
        public DateTime lynkAdvantageDate { get; set; }
        public string customerNbr { get; set; }
        public int classCode { get; set; }
        public int sensitivityLevel { get; set; }
        public string stmtTollFreeNumber { get; set; }
        public DateTime deactivationDt { get; set; }
        public string legalDesc { get; set; }
        public string senseLvlDesc { get; set; }
        public string statDesc { get; set; }
        public int lynkAdvantage { get; set; }
        public int pinPadPlus { get; set; }
        public int giftLynk { get; set; }
        public int rewardsLynk { get; set; }
        public int demoID { get; set; }
        public string  custName { get; set; }
        public string custContact { get; set; }
        public int prinID { get; set; }
        public string prinName { get; set; }
        public string prinAddress { get; set; }
        public string prinCity { get; set; }
        public string prinState { get; set; }
        public string prinZipcode { get; set; }
        public string prinSSN { get; set; }
        public string custFederalTaxID { get; set; }
        public int irsVerificationStatus { get; set; }
        public int propHasEmployees{ get; set; }

    }
}

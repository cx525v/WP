using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.CIS.LynkSystems.Model
{
    public class ActiveServices
    {
        public int LIDType { get; set; }
        public int LID { get; set; }
        public int BillingMethodType { get; set; }
        public string BillMtdDesc { get; set; }
        public string ChkName { get; set; }
        public bool GiftLynk_ON { get; set; }
        public bool RewardsLynk_ON { get; set; }
        public DateTime LastProcessingDt { get; set; }
        public bool Amex_ON { get; set; }
        /// <summary>
        /// DiscoverNS
        /// </summary>
        public bool Discover_ON { get; set; }
        public bool Discover_CT21_ON { get; set; }
        public bool Diner_ON { get; set; }
        public bool JCB_ON { get; set; }
        public int OpenCase { get; set; }
        public bool TerminalRental_ON { get; set; }
        public bool PrinterRental_ON { get; set; }
        public bool PINPadRental_ON { get; set; }
        public string SoftDesc { get; set; }
        public bool CreditST_ON { get; set; }
        public bool DebitST_ON { get; set; }
        public bool CheckST_ON { get; set; }
        public bool ACHST_ON { get; set; }
        public bool LynkAdvantage_ON { get; set; }
        public string ExternalTermID { get; set; }
        public string AuthProcessorDesc { get; set; }
        public string SICDesc { get; set; }
        public string LADesc { get; set; }

        public string ActiveServiesDesc {
            get {

                #region ActiveServiesDesc
                StringBuilder activeServicesDesc = new StringBuilder();
                if (CreditST_ON)
                {
                    activeServicesDesc.Append("Credit");
                    if (Amex_ON || Discover_CT21_ON || Discover_ON || JCB_ON || Diner_ON)
                        activeServicesDesc.Append(" (");
                    if (Amex_ON)
                        activeServicesDesc.Append("Amex");
                    if (Discover_ON)
                        activeServicesDesc.Append(", Discover NS");
                    if (Discover_CT21_ON)
                        activeServicesDesc.Append(", Discover");
                    if (Diner_ON)
                        activeServicesDesc.Append(", Diner");
                    if (JCB_ON)
                        activeServicesDesc.Append(", JCB");

                    if(activeServicesDesc.ToString().IndexOf("(,") > -1) activeServicesDesc.Remove(activeServicesDesc.ToString().IndexOf("(,")+1,1);

                    if (activeServicesDesc.ToString().IndexOf("(") > -1) activeServicesDesc.Append(")");

                }

                if (DebitST_ON)
                    if (activeServicesDesc.ToString() == string.Empty)
                        activeServicesDesc.Append("Debit");
                    else
                        activeServicesDesc.Append(", Debit");

                if (CheckST_ON)
                {
                    if (activeServicesDesc.ToString() == string.Empty)
                        activeServicesDesc.Append("Checks");
                    else
                        activeServicesDesc.Append(", Checks");
                    if (!string.IsNullOrWhiteSpace(ChkName))
                        activeServicesDesc.Append("(" + ChkName + ")");
                }

                if (GiftLynk_ON || RewardsLynk_ON)
                {
                    if (activeServicesDesc.ToString() == string.Empty)
                        activeServicesDesc.Append("Gift/Loyalty");
                    else
                        activeServicesDesc.Append(", Gift/Loyalty");

                    if (GiftLynk_ON)
                        activeServicesDesc.Append("(Gift Card");
                    if (RewardsLynk_ON)
                    {
                        if (GiftLynk_ON)
                            activeServicesDesc.Append(", Loyalty Card)");
                        else
                            activeServicesDesc.Append("Loyalty Card)");
                    }
                    else
                        activeServicesDesc.Append(")");
                }

                if(LynkAdvantage_ON)
                {
                    if (activeServicesDesc.ToString() == string.Empty)
                        activeServicesDesc.Append("Equipment Replacement and Supplies");
                    else
                        activeServicesDesc.Append(", Equipment Replacement and Supplies");
                    if (!string.IsNullOrWhiteSpace(LADesc))
                        activeServicesDesc.Append("(" + LADesc + ")");                    
                }

                if(ACHST_ON)
                {
                    if (activeServicesDesc.ToString() == string.Empty)
                        activeServicesDesc.Append("ACH");
                    else
                        activeServicesDesc.Append(", ACH");
                }
                #endregion
                return activeServicesDesc.ToString();
            }
        }

    }
}

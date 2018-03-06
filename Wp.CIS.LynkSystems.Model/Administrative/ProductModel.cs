using System;
using System.Collections.Generic;
using System.Text;

namespace Wp.CIS.LynkSystems.Model.Administrative
{
    [Serializable]
    public class ProductModel
    {
        public int ProductCode { get; set; }

        public int ProductTypeID { get; set; }

        public string MfgCode { get; set; }

        public string Description { get; set; }

        public bool ObsoleteIndicator { get; set; }

        public bool IntegratedPrinter { get; set; }

        public int IntegratedPinPad { get; set; }

        public string SupportLevel { get; set; }

        public bool UsedEquipment { get; set; }

        public bool Wireless { get; set; }

        public bool VARTerminal { get; set; }

        public bool GLOnlyTerminal { get; set; }

        public bool SupportsDeposits { get; set; }

        public bool? UserVarTemplate { get; set; }

        public string TemplateFileName { get; set; }

        public bool IsTSYSEquip { get; set; }

        public int? TSYSAutoUpdate { get; set; }

        public bool Integrated { get; set; }

        public int DLTypeID { get; set; }

        public bool PCIReport { get; set; }

        public int HIDEnabled { get; set; }

        public bool SupportsDirectDebit { get; set; }

        public bool DirectDebitOnly { get; set; }

        public int IsInVAMS { get; set; }

        public bool IsPinlessDebit { get; set; }

        public bool SupportE2EE { get; set; }

        public bool E2EEEnabled { get; set; }

        public bool SupporteWIC { get; set; }

        public bool IsSmallMerchant { get; set; }

        public string ThirdPartyProductName { get; set; }

        public int? VTMajorVersion { get; set; }

        public int? IsMobile { get; set; }

        public bool isSupports24MID { get; set; }

        public int BrandId { get; set; }

        public int TerminalSort { get; set; }

        public int? BusinessType { get; set; }

        public int? InstallType { get; set; }

        public string TerminalType { get; set; }

        public string TerminalManufCode { get; set; }

        public string TerminalDescription { get; set; }

        public string TerminalCategory { get; set; }

        public string PinPadType { get; set; }

        public int? PrinterID { get; set; }

        public string PrinterName { get; set; }

        public string PrinterDescription { get; set; }

        public int? CheckReaderID { get; set; }

        public string CheckReaderName { get; set; }

        public string CheckReaderDescription { get; set; }

        public string ProductType { get; set; }

        public string Manufacturer { get; set; }

        public string DownloadTime { get; set; }

        public string BalanceMethodDesc { get; set; }

        public int? ATMBalanceMethodID { get; set; }

        public bool? EPSEnabled { get; set; }

        public int OverallCount { get; set; }
    }
}

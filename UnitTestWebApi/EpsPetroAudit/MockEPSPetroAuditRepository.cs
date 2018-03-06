using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worldpay.CIS.DataAccess.EpsPetroAudit;
using Wp.CIS.LynkSystems.Model;
using System.Linq;

namespace CIS.WebApi.UnitTests.EpsPetroAudit
{
    public class MockEPSPetroAuditRepository : IEPSPetroAuditRepository
    {
        public ICollection<EPSPetroAudit> epsPetroAudits;

        public ICollection<EPSPetroAuditDetails> epsPetroAuditDetails;
        public bool FailGet { get; set; }


        public MockEPSPetroAuditRepository()
        {
            epsPetroAudits = new List<EPSPetroAudit> {
            new EPSPetroAudit{
             auditId = 3003,
             versionId = 1259,
             actionType = "INSERT",
             entityName = "Tables/Schemas",
             userName = "dveerepalli",
             auditDate =Convert.ToDateTime( "2017-10-17 14:51:06.937")
             ,newValue = @"<_x0023_tbEPSPetroTableAudit Active='1' DefinitionOnly='0' EffectiveDate='2017-09-29T00:00:00'>
  <SchemaDef>
    <!-- edited with XMLSpy v2008 (http://www.altova.com) by Bradford Loewy (VERIFONE) -->
    <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Clerley Silveira (VeriFone, Inc.) -->
    <xs:schema xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns='http://www.verifone.com/eps/viper/namespace/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
      <xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd' />
      <xs:element name = 'EPSGeneralParameters' >
        < xs:annotation>
          <xs:documentation>EPS General Parameters Table</xs:documentation>
        </xs:annotation>
        <xs:complexType>
          <xs:sequence>
            <xs:element name = 'SiteName' type= 'xs:string' >
              < xs:annotation>
                <xs:documentation>Name of the store location (i.e.Bradford’s Convenience Store)</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteID' type='xs:string'>
              <xs:annotation>
                <xs:documentation>Site ID as known from the system providing the EPS tables. (may be used on receipts, reports, asset page)</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteTimeSyncControl' >
              < xs:annotation>
                <xs:documentation>Determines is POS or FEP controls EPS time.  “POS”: EPS Time is controlled by the POS.  If the time is to be controlled by a FEP, this should be the FEPName corresponding to that field in the FEPTable.</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:string' />
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'SitePrimaryCurrencyCode' type= 'xs:string' >
              < xs:annotation>
                <xs:documentation>Site Primary Currency Code (used by the FEP)</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteAlternativeCurrencyCode' type='xs:string' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>Site Secondary Currency Code(used by the FEP)</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SitePrimaryISOCurrencyCode' type='xs:string'>
              <xs:annotation>
                <xs:documentation>Site Primary Currency Code - ISO 4217 format</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteAlternativeISOCurrencyCode' type='xs:string' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>Site Secondary Currency Code - ISO 4217 format</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SitePrimaryLanguageCode' type='xs:string'>
              <xs:annotation>
                <xs:documentation>Site Primary Language Code - ISO 639 format</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteAlternative1LanguageCode' type='xs:string' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>Site Alternative 1 Language Code - ISO 639  format</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteAlternative2LanguageCode' type='xs:string' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>Site Alternative 2 Language Code - ISO 639  format</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteSendPOSAdminMessages' type='xs:boolean'>
              <xs:annotation>
                <xs:documentation>Flag to control the forwarding to the host of POS Admin Messages.Setting this flag to false will prevent the forwarding to he FEP of any POS admin messages the EPS receives.</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForPOSAdmin1Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP POSAdmin1 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForPOSAdmin2Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP POSAdmin2 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForPOSAdmin3Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP POSAdmin3 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForPOSAdmin4Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP POSAdmin4 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SiteSendEPSAdminMessages' type= 'xs:boolean' >
              < xs:annotation>
                <xs:documentation>Flag to control the forwarding to the host of EPSAdmin Messages.Setting this flag to false will prevent the forwarding to he FEP of all EPS admin messages.</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForEPSAdmin1Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP EPSAdmin1 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForEPSAdmin2Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP EPSAdmin2 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForEPSAdmin3Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP EPSAdmin3 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'FEPIndicatorForEPSAdmin4Messages' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference into the FEPIndicatorTable to override to which FEP EPSAdmin4 Messages are sent</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'ConfigMessage' type= 'xs:string' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>Reference store defined declined message when the feature is enabled</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'NetworkLastRequired' type= 'xs:boolean' >
              < xs:annotation>
                <xs:documentation>True - The site does not support split tender or requires that the network transaction is last ; False - The site supports split tender of network transactions</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'CashBackPrompt' type= 'xs:boolean' >
              < xs:annotation>
                <xs:documentation>True - The site may prompt for cash back on cards that permit cash back ; False - The site is not allowed to prompt for cash back, regardless of the flag on the individual card</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'DualCardProcessingCode' >
              < xs:annotation>
                <xs:documentation>Controls how cards that can be processed as either credit or debit are handled</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:string'>
                  <xs:enumeration value = 'ForceDebit' >
                    < xs:annotation>
                      <xs:documentation>MOP Type will be forced to Debit</xs:documentation>
                    </xs:annotation>
                  </xs:enumeration>
                  <xs:enumeration value = 'ForceCredit' >
                    < xs:annotation>
                      <xs:documentation>MOP Type will be forced to Credit</xs:documentation>
                    </xs:annotation>
                  </xs:enumeration>
                  <xs:enumeration value = 'CustomerSelect' >
                    < xs:annotation>
                      <xs:documentation>Customer will be prompted for MOP Type</xs:documentation>
                    </xs:annotation>
                  </xs:enumeration>
                  <xs:enumeration value = 'POSSelect' >
                    < xs:annotation>
                      <xs:documentation>Cashier will be prompted for MOP Type</xs:documentation>
                    </xs:annotation>
                  </xs:enumeration>
                </xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'OverallStoreAndForward' >
              < xs:annotation>
                <xs:documentation>Maximum number of transactions allowed in the EPS Store and Forward</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:integer'>
                  <xs:minInclusive value = '0' />
                  < xs:maxInclusive value = '9999' />
                </ xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'NumberDaysDataStored' >
              < xs:annotation>
                <xs:documentation>Number of days to worth of data the EPS will store</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:integer'>
                  <xs:minInclusive value = '1' />
                  < xs:maxInclusive value = '30' />
                </ xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'PayPointBatchDayCloseTime' >
              < xs:annotation>
                <xs:documentation>Time of day for PayPoint Batch Close.HH:MM in 24hr time.  Seconds are defautled to 0.</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:string'>
                  <xs:pattern value = '([0-1][0-9]|2[0-3]):[0-5][0-9]' />
                </ xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'SecurityDayCount' >
              < xs:annotation>
                <xs:documentation>Amount of days after which certain steps will be followed to maximize security.</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:integer'>
                  <xs:minInclusive value = '1' />
                </ xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'ReportMaskingFlag' type= 'xs:boolean' >
              < xs:annotation>
                <xs:documentation>True - The site to mask account number on acquirer batch report; False - The site to unmask account number on acquirer batch report</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'LoyaltyReportMaskingFlag' type= 'xs:boolean' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>True - The site to mask account number on Loyalty report; False - The site to unmask account number on Loyalty report</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'ClearVelocityHoursCount' type='xs:integer' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>The number of hours velocity will wait before clearing any entry</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'OnLineVelocityCheck' type='xs:boolean' minOccurs='0'>
              <xs:annotation>
                <xs:documentation>If enabled velocity will be checked for online transaction as well as offline transactions</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'SignatureCaptureEnabled' type= 'xs:boolean' minOccurs= '0' />
            < xs:element name = 'DisplaySignatureToCashier' type= 'xs:boolean' minOccurs= '0' />
            < xs:element name = 'PrintSignatureOnReceipt' type= 'xs:boolean' minOccurs= '0' />
            < xs:element name = 'OutsideCashierMessageSupported' type= 'xs:boolean' minOccurs= '0' >
              < xs:annotation>
                <xs:documentation>True - Allow prompts with TargetUserType = POS to be sent during outside transactions for display to the cashier (e.g.help icon message); False - Prevent prompt with TargetUserType = POS from being sent during outdoor transaction.POS/DCR subsystem to rely on POS generated default message instead</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'CashbackFeeIndicator' type= 'tt:FeeIndicatorType' minOccurs= '0' >
   
                 < xs:annotation>
                <xs:documentation>Points to the cashback fee indicator in the FeeTable or 0 if none</xs:documentation>
                 </xs:annotation>
            </xs:element>
            <xs:element name = 'EBTCashbackFeeIndicator' type= 'tt:FeeIndicatorType' minOccurs= '0' >
   
                 < xs:annotation>
                <xs:documentation>Points to the cashback fee indicator in the FeeTable or 0 if none</xs:documentation>
                 </xs:annotation>
            </xs:element>
            <xs:element name = 'DisallowKioskEPSSplitTender' type= 'xs:boolean' default='false' minOccurs= '0' />
   
               < xs:element name = 'TerminalBatchLimit' minOccurs= '0' >
   
                 < xs:annotation>
                <xs:documentation>Maximum terminal batches allowed</xs:documentation>
              </xs:annotation>
              <xs:simpleType>
                <xs:restriction base='xs:integer'>
                  <xs:minInclusive value = '1' />
   
                     < xs:maxInclusive value = '99999' />
   
                   </ xs:restriction>
              </xs:simpleType>
            </xs:element>
            <xs:element name = 'DisplayPINpadPromptsToCashier' type= 'xs:boolean' default='false' minOccurs= '0' >
   
                 < xs:annotation>
                <xs:documentation>Gives visibility to cashier on input prompts displayed on PINpad</xs:documentation>
              </xs:annotation>
            </xs:element>
            <xs:element name = 'UseStoreError' type= 'xs:boolean' default='false' minOccurs= '0' >
   
                 < xs:annotation>
                <xs:documentation>Enables the Cashier to define a message to be displayed on the DCR</xs:documentation>
                 </xs:annotation>
            </xs:element>
          </xs:sequence>
          <xs:attribute name = 'version' type= 'tt:VersionType' use= 'required' />
   
           </ xs:complexType>
      </xs:element>
    </xs:schema>
  </SchemaDef>
  <DefaultXML>
    <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->
    <EPSGeneralParameters xmlns = 'http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt= 'http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xsi= 'http://www.w3.org/2001/XMLSchema-instance' version= '0' xsi:schemaLocation= 'http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/EPSGeneralParametersTable.xsd' >
   
         < SiteName > VeriFone Gold Disk</SiteName>
      <SiteID>9999999</SiteID>
      <SiteTimeSyncControl>worldpay</SiteTimeSyncControl>
         <SitePrimaryCurrencyCode>840</SitePrimaryCurrencyCode>
      <SiteAlternativeCurrencyCode />
      <SitePrimaryISOCurrencyCode>USD</SitePrimaryISOCurrencyCode>
         <SiteAlternativeISOCurrencyCode>USD</SiteAlternativeISOCurrencyCode>
         <SitePrimaryLanguageCode>en</SitePrimaryLanguageCode>
         <SiteAlternative1LanguageCode />
      <SiteAlternative2LanguageCode />
      <SiteSendPOSAdminMessages>true</SiteSendPOSAdminMessages>
      <FEPIndicatorForPOSAdmin1Messages>01</FEPIndicatorForPOSAdmin1Messages>
      <FEPIndicatorForPOSAdmin2Messages>01</FEPIndicatorForPOSAdmin2Messages>
      <FEPIndicatorForPOSAdmin3Messages>01</FEPIndicatorForPOSAdmin3Messages>
      <FEPIndicatorForPOSAdmin4Messages />
      <SiteSendEPSAdminMessages>true</SiteSendEPSAdminMessages>
      <FEPIndicatorForEPSAdmin1Messages>01</FEPIndicatorForEPSAdmin1Messages>
      <FEPIndicatorForEPSAdmin2Messages>01</FEPIndicatorForEPSAdmin2Messages>
      <FEPIndicatorForEPSAdmin3Messages>01</FEPIndicatorForEPSAdmin3Messages>
      <FEPIndicatorForEPSAdmin4Messages>01</FEPIndicatorForEPSAdmin4Messages>
      <NetworkLastRequired>false</NetworkLastRequired>
      <CashBackPrompt>true</CashBackPrompt>
      <DualCardProcessingCode>CustomerSelect</DualCardProcessingCode>
         <OverallStoreAndForward>500</OverallStoreAndForward>
      <NumberDaysDataStored>15</NumberDaysDataStored>
      <PayPointBatchDayCloseTime>00:00</PayPointBatchDayCloseTime>
      <SecurityDayCount>2</SecurityDayCount>
      <ReportMaskingFlag>false</ReportMaskingFlag>
      <ClearVelocityHoursCount>01</ClearVelocityHoursCount>
      <OnLineVelocityCheck>true</OnLineVelocityCheck>
      <SignatureCaptureEnabled>true</SignatureCaptureEnabled>
      <DisplaySignatureToCashier>true</DisplaySignatureToCashier>
      <PrintSignatureOnReceipt>true</PrintSignatureOnReceipt>
      <OutsideCashierMessageSupported>true</OutsideCashierMessageSupported>
      <CashbackFeeIndicator>1</CashbackFeeIndicator>
    </EPSGeneralParameters>
  </DefaultXML>
</_x0023_tbEPSPetroTableAudit>"
             , previousValue=@"<_x0023_tbEPSPetroTableAudit />"
             ,tableID = "1903"
             ,tableName = "CAPKTable"
            }
            , new EPSPetroAudit{
             auditId =2943,
             versionId = 1247,
             actionType = "INSERT",
             entityName = "Tables/Schemas",
             userName = "WCampbell",
             auditDate =Convert.ToDateTime( "2017-10-17 08:46:07.277")
             ,newValue = @"<_x0023_tbEPSPetroTableAudit TableID='1853' TableName='FIDTable' Active='1' DefinitionOnly='0' EffectiveDate='2017-10-17T00:00:00'>
  <SchemaDef>
    <!-- edited with XMLSpy v2008 (http://www.altova.com) by Bradford Loewy (VERIFONE) -->
    <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->
    <xs:schema xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns='http://www.verifone.com/eps/viper/namespace/v1' xmlns:this='http://www.verifone.com/eps/viper/namespace/v' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
      <xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd' />
      <xs:element name = 'FieldUsageIdentifierTable' >
        < xs:complexType>
          <xs:sequence maxOccurs = 'unbounded' >
            < xs:element name = 'FieldUsageIdentifierRow' type='FieldUsageIdentifierRowType' />
          </xs:sequence>
          <xs:attribute name = 'version' type='tt:VersionType' use='required' />
        </xs:complexType>
        <xs:key name = 'FieldUsageIdPK' >
          < xs:selector xpath = 'this:FieldUsageIdentifierRow' />
 
           < xs:field xpath = 'this:FieldUsageIdentifier' />
  
          </ xs:key>
      </xs:element>
      <xs:complexType name = 'FieldUsageIdentifierRowType' >
        < xs:sequence>
          <xs:element name = 'FieldUsageIdentifier' >
            < xs:simpleType>
              <xs:restriction base='tt:FieldUsageIdType>
                <xs:minLength value = '1' />
              </ xs:restriction>
            </xs:simpleType>
          </xs:element>
          <xs:element name = 'Usage' >
            < xs:simpleType>
              <xs:restriction base='xs:string' />
            </xs:simpleType>
          </xs:element>
          <xs:element name = 'DefaultValue' minOccurs='0'>
            <xs:annotation>
              <xs:documentation>Default value for this FID represented as a string.</xs:documentation>
            </xs:annotation>
            <xs:simpleType>
              <xs:restriction base='xs:string' />
            </xs:simpleType>
          </xs:element>
        </xs:sequence>
      </xs:complexType>
    </xs:schema>
  </SchemaDef>
  <DefaultXML>
    <FieldUsageIdentifierTable xmlns = 'http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt= 'http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xsi= 'http://www.w3.org/2001/XMLSchema-instance' version= '45' xsi:schemaLocation= 'http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/FIDTable.xsd' >
      < FieldUsageIdentifierRow >
        < FieldUsageIdentifier > 2PLY</FieldUsageIdentifier>
        <Usage>2Ply Receipt Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ACQUIRERBATCH</FieldUsageIdentifier>
        <Usage>Acquirer Batch Report</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ADDRESS</FieldUsageIdentifier>
        <Usage>Address</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ALPHADATA</FieldUsageIdentifier>
        <Usage>Entered data (alphanumeric)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AMEX</FieldUsageIdentifier>
        <Usage>Amex (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AMPM</FieldUsageIdentifier>
        <Usage>AMPM Card Falg (boolean)</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>APPLICATIONSENDER</FieldUsageIdentifier>
        <Usage>Application Sender</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AUTHNUM</FieldUsageIdentifier>
        <Usage>Authorization Number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AVS</FieldUsageIdentifier>
        <Usage>AVS Flag (boolean)</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CARDNAME</FieldUsageIdentifier>
        <Usage>Card Name</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CASHBACK</FieldUsageIdentifier>
        <Usage>Cashback Amount</Usage>
        <DefaultValue>100.00</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CASHBACKALLOWED</FieldUsageIdentifier>
        <Usage>Cash Back Allowed</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CHECKID</FieldUsageIdentifier>
        <Usage>Velocity ID Check</Usage>
        <DefaultValue>1</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CIRCLEK</FieldUsageIdentifier>
        <Usage>CircleK Card Flag (boolean)</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CLIENT</FieldUsageIdentifier>
        <Usage>Client number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>CVV</FieldUsageIdentifier>
        <Usage>CVV Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DATESTART</FieldUsageIdentifier>
        <Usage>Date Start for Day Total Rpt</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEBITCREDITPROMPT</FieldUsageIdentifier>
        <Usage>Outside Deb/Cred Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEBITYNPROMPT</FieldUsageIdentifier>
        <Usage>Outside Deb Y/N Falg (boolean)</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEPT</FieldUsageIdentifier>
        <Usage>Department number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DIESELOUT</FieldUsageIdentifier>
        <Usage>Diesel outside Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DISC</FieldUsageIdentifier>
        <Usage>Discover Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DISCOUNTACCEPTED</FieldUsageIdentifier>
        <Usage>Customer accepts disc prompt (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DRIVERID</FieldUsageIdentifier>
        <Usage>Driver ID</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DRIVLIC</FieldUsageIdentifier>
        <Usage>Driver License number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>EMPNUM</FieldUsageIdentifier>
        <Usage>Employee number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ENABLEALTERNATIVEID</FieldUsageIdentifier>
        <Usage>Enable AltID Progam (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ENABLENATIONALLOYALTY</FieldUsageIdentifier>
        <Usage>Enable Natl Loy Prog (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ENTRYMODE</FieldUsageIdentifier>
        <Usage>Entry Mode of Card 0 through 9</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>EPSREFNUMBER</FieldUsageIdentifier>
        <Usage>Terminal Batch /STAN (ttttnnnnnnnnnn)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>EXPIRATIONDATE</FieldUsageIdentifier>
        <Usage>Expiration Date</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FAILEDCTIVR</FieldUsageIdentifier>
        <Usage>Index of Failed CTI Val Routine</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FAILEDNACSVR</FieldUsageIdentifier>
        <Usage>Index of Failed NACS Val Routine</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEE</FieldUsageIdentifier>
        <Usage>Fee</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEEAMOUNT</FieldUsageIdentifier>
        <Usage>Fee from Fee Table</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPLOYALTYONLINE</FieldUsageIdentifier>
        <Usage>Loyalty FEP online (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPMERCHANTID</FieldUsageIdentifier>
        <Usage>Site SVB from FEP Table</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPONLINE</FieldUsageIdentifier>
        <Usage>Payment FEP Online (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPSTATUSEXCENTUS</FieldUsageIdentifier>
        <Usage>Loyalty FEP status (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPSTATUSPAYPOINT</FieldUsageIdentifier>
        <Usage>Payment FEP status (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FEPTERMINALID</FieldUsageIdentifier>
        <Usage>Terminal ID from FEP Table</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>FLOORLIMIT</FieldUsageIdentifier>
        <Usage>Floor Limit from Limit Table</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>GALLONLIMIT</FieldUsageIdentifier>
        <Usage>Tran gal limit 0 if no limit (Number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>HUB</FieldUsageIdentifier>
        <Usage>Hub reading</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>HYBRIDLOYALTY</FieldUsageIdentifier>
        <Usage>Enable Hybrid Program (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>IDEVENTTYPE</FieldUsageIdentifier>
        <Usage>Event Type</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>IDUPDATETIME</FieldUsageIdentifier>
        <Usage>Update Date Time</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>INVOICE</FieldUsageIdentifier>
        <Usage>Invoice number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ISOUTDOORS</FieldUsageIdentifier>
        <Usage>Transaction outdoors</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LALTERNATEIDYN</FieldUsageIdentifier>
        <Usage>Cust select AltID (String)</Usage>
        <DefaultValue>NO</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LCARDNAME</FieldUsageIdentifier>
        <Usage>Loyalty Card Name</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LCARDPRESENT</FieldUsageIdentifier>
        <Usage>Loyalty token presented (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LFEPMESSAGETEXTCASHIER</FieldUsageIdentifier>
        <Usage>Text provided by loyalty host (String)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LFEPMESSAGETEXTINSIDE</FieldUsageIdentifier>
        <Usage>Loyalty host inside text (String)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LFEPMESSAGETEXTOUTSIDE</FieldUsageIdentifier>
        <Usage>Loyalty host outside text (String)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LFEPRECEIPT</FieldUsageIdentifier>
        <Usage>Loyalty receipt (String)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LFEPSEQUENCEID</FieldUsageIdentifier>
        <Usage>Loyalty Stan</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LOYALTYCAPABLE</FieldUsageIdentifier>
        <Usage>POS device loyalty capable (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LOYALTYCUSTOMER</FieldUsageIdentifier>
        <Usage>Loy cust (Enumerated String(unknwn, t, f)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>LOYALTYPROMPTING</FieldUsageIdentifier>
        <Usage>Loyalty Prompt (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MANUALMSR</FieldUsageIdentifier>
        <Usage>Manual card entry</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MSR</FieldUsageIdentifier>
        <Usage>Accepts card swipe</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWCHARGEBACK</FieldUsageIdentifier>
        <Usage>New Chargeback</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWDNLDNOTICE</FieldUsageIdentifier>
        <Usage>New Download Notice</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWINFONOTICE</FieldUsageIdentifier>
        <Usage>New Info Message</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWMAIL</FieldUsageIdentifier>
        <Usage>New mail message</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWMAILTITLE</FieldUsageIdentifier>
        <Usage>Title of First Unprinted Mail Available</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWPRICENOTE</FieldUsageIdentifier>
        <Usage>New price notification</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWREWARDSTL</FieldUsageIdentifier>
        <Usage>New Reward Settlement Notification</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWRFCO</FieldUsageIdentifier>
        <Usage>New RFCO Notification</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NEWSETTLEMENT</FieldUsageIdentifier>
        <Usage>New Settlement Notification</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NUMDATA</FieldUsageIdentifier>
        <Usage>Entered data (numeric)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ODOMETER</FieldUsageIdentifier>
        <Usage>Odometer reading</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>OUTPAY</FieldUsageIdentifier>
        <Usage>Payment Outside Flag (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PCARDPRESENT</FieldUsageIdentifier>
        <Usage>Payment token presented (boolean)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PHONENUM</FieldUsageIdentifier>
        <Usage>Phone number</Usage>
        <DefaultValue>(972) 367-3602</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PIN</FieldUsageIdentifier>
        <Usage>Pin number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PO</FieldUsageIdentifier>
        <Usage>PO number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>POPID</FieldUsageIdentifier>
        <Usage>POP ID of transaction</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PPGDISCOUNT</FieldUsageIdentifier>
        <Usage>PPG disc from loyalty host (Number)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PREAUTHRCPT</FieldUsageIdentifier>
        <Usage>PreAuth Receipt Flag (boolean)</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PREPAIDMSR</FieldUsageIdentifier>
        <Usage>PrePaid Card</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REFERENCENUMBER</FieldUsageIdentifier>
        <Usage>Reference Number (aaebbsss)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REFERRALPHONENUMBER</FieldUsageIdentifier>
        <Usage>Referral Phone Number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REFUNDSTAN</FieldUsageIdentifier>
        <Usage>STAN of Original Transaction for Refund</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REGION</FieldUsageIdentifier>
        <Usage>Region for welcome (string)</Usage>
        <DefaultValue>OUR STORE</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REMBAL</FieldUsageIdentifier>
        <Usage>CashCard Balance</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REQTOTALSALE</FieldUsageIdentifier>
        <Usage>SVS total amount</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>REQUESTTYPE</FieldUsageIdentifier>
        <Usage>Request Type</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>RESPONSECODE</FieldUsageIdentifier>
        <Usage>Response Code</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>SALEITEMAMOUNT</FieldUsageIdentifier>
        <Usage>Sale Amount in NACS Table Validation</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>SALEITEMQUANTITY</FieldUsageIdentifier>
        <Usage>Sale Quantity in NACS Table Validation</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>SECURITYCODE</FieldUsageIdentifier>
        <Usage>Security Code (CVV)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>SOCSEC</FieldUsageIdentifier>
        <Usage>Social Security number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>SPLIT</FieldUsageIdentifier>
        <Usage>Split Flag from IFSF Message</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>TERMINALBATCH</FieldUsageIdentifier>
        <Usage>Terminal Batch Report</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>TOTALACTIVE</FieldUsageIdentifier>
        <Usage>CashCard Activated Amount</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>TOTALSALE</FieldUsageIdentifier>
        <Usage>Total Sale</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>TRACE</FieldUsageIdentifier>
        <Usage>Trace Number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>TRAILERNUM</FieldUsageIdentifier>
        <Usage>Trailer number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>UNENCRYPTEDID</FieldUsageIdentifier>
        <Usage>Unencrypted ID number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>USAGEFLAG</FieldUsageIdentifier>
        <Usage>Usage Flag (MOP)</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VEHICLE</FieldUsageIdentifier>
        <Usage>Vehicle number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VIMC</FieldUsageIdentifier>
        <Usage>Visa Mastercard Flag (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VOYFLEET</FieldUsageIdentifier>
        <Usage>Voyager Fleet Code</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>WO</FieldUsageIdentifier>
        <Usage>Work Order number</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ZIP</FieldUsageIdentifier>
        <Usage>Zip/Postal code</Usage>
        <DefaultValue />
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEALERNAME</FieldUsageIdentifier>
        <Usage>Dealer Name</Usage>
        <DefaultValue>ALON EMV Station 1</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEALERADDRESS</FieldUsageIdentifier>
        <Usage>Dealer Address</Usage>
        <DefaultValue>12700 Park Central Dr</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEALERCITY</FieldUsageIdentifier>
        <Usage>Dealer City</Usage>
        <DefaultValue>Dallas</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEALERSTATE</FieldUsageIdentifier>
        <Usage>Dealer State</Usage>
        <DefaultValue>TX</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DEALERZIP</FieldUsageIdentifier>
        <Usage>Dealer Zip Code</Usage>
        <DefaultValue>75251</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>EODHOSTPOSTING</FieldUsageIdentifier>
        <Usage>Host Posting Code (1-3)</Usage>
        <DefaultValue>3</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MASTERCUTOFFAMOUNT</FieldUsageIdentifier>
        <Usage>Max Amount For Network Sales</Usage>
        <DefaultValue>9999</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VIAVS</FieldUsageIdentifier>
        <Usage>AVS Prompting Visa</Usage>
        <DefaultValue>8</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MCAVS</FieldUsageIdentifier>
        <Usage>AVS Prompting MasterCard</Usage>
        <DefaultValue>8</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AXAVS</FieldUsageIdentifier>
        <Usage>AVS Prompting AMEX</Usage>
        <DefaultValue>8</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DSAVS</FieldUsageIdentifier>
        <Usage>AVS Prompting Discover</Usage>
        <DefaultValue>8</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VICVV</FieldUsageIdentifier>
        <Usage>CVV Prompting Visa</Usage>
        <DefaultValue>2</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MCCVV</FieldUsageIdentifier>
        <Usage>CVV Prompting MasterCard</Usage>
        <DefaultValue>2</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>AXCVV</FieldUsageIdentifier>
        <Usage>CVV Prompting AMEX</Usage>
        <DefaultValue>2</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>DSCVV</FieldUsageIdentifier>
        <Usage>CVV Prompting Discover</Usage>
        <DefaultValue>2</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>MCALLOWED</FieldUsageIdentifier>
        <Usage>DEBIT FIRST ALLOWED FOR MASTER CARD</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>VIALLOWED</FieldUsageIdentifier>
        <Usage>DEBIT FIRST ALLOWED FOR VISA CARD</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PROMPTCODE</FieldUsageIdentifier>
        <Usage>WEX Prompt Code</Usage>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ACAVS</FieldUsageIdentifier>
        <Usage>AVS Prompting Alon Credit</Usage>
        <DefaultValue>8</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ACCVV</FieldUsageIdentifier>
        <Usage>CVV Prompting Alon Credit</Usage>
        <DefaultValue>2</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>PRINTCUSTCOPY</FieldUsageIdentifier>
        <Usage>Customer Copy Receipt</Usage>
        <DefaultValue>true</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>ICCCASHBACKFORCEONLINE</FieldUsageIdentifier>
        <Usage>Flag for forcing ICC Cashback Transactions Online (boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
      <FieldUsageIdentifierRow>
        <FieldUsageIdentifier>NOCVMENABLED</FieldUsageIdentifier>
        <Usage>NoCVM flag(boolean)</Usage>
        <DefaultValue>false</DefaultValue>
      </FieldUsageIdentifierRow>
    </FieldUsageIdentifierTable>
  </DefaultXML>
</_x0023_tbEPSPetroTableAudit>"
             ,previousValue=@"<_x0023_tbEPSPetroTableAudit TableID='1853' TableName='FIDTable' />"
             ,tableID = "1853"
             ,tableName = "FIDTable"
            }
            , new EPSPetroAudit{
             auditId = 2912,
             versionId = 1245,
             actionType = "INSERT",
             entityName = "Tables/Schemas",
             userName = "WCampbell",
             auditDate =Convert.ToDateTime( "2017-10-17 08:12:09.620")
             ,newValue = @"<_x0023_tbEPSPetroTableAudit TableID='1826' TableName='ICCConfigTable' Active='1' DefinitionOnly='0' EffectiveDate='2017-10-17T00:00:00'>
  <SchemaDef>
    <!-- edited with XMLSpy v2014 (http://www.altova.com) by Farhan (Verifone Inc.) -->
    <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:this='http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns='http://www.verifone.com/eps/viper/namespace/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
      <xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd' />
      <xs:element name = 'ICCConfigTable' >
        < xs:complexType>
          <xs:sequence>
            <xs:element name = 'ICCConfigRow' type='this:ICCConfigRowType' minOccurs='0' maxOccurs='2' />
          </xs:sequence>
          <xs:attribute name = 'version' type='tt:VersionType' use='required' />
        </xs:complexType>
        <xs:key name = 'ICCConfigPK' >
          < xs:selector xpath = 'this:ICCConfigRow' />
           < xs:field xpath = 'this:ICCMode' />
          </ xs:key>
      </xs:element>
      <xs:complexType name = 'ICCConfigRowType' >
        < xs:sequence>
          <xs:element name = 'ICCMode' type='tt:ModeTypes'>
            <xs:annotation>
              <xs:documentation>It indicates whether the terminal parameters are for Contact or Contactless</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'EnableICC' type='xs:boolean'>
            <xs:annotation>
              <xs:documentation>It indicates whether EMV should be enabled on the terminal or not</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TerminalType' >
            < xs:annotation>
              <xs:documentation>Indicates the environment of the terminal, its communications capability and its operational control</xs:documentation>
            </xs:annotation>
            <xs:simpleType>
              <xs:restriction base='xs:string'>
                <xs:length value = '2' />
                < xs:enumeration value = '11' />
                < xs:enumeration value = '12' />
                < xs:enumeration value = '13' />
                < xs:enumeration value = '14' />
                < xs:enumeration value = '15' />
                < xs:enumeration value = '16' />
                < xs:enumeration value = '21' />
                < xs:enumeration value = '22' />
                < xs:enumeration value = '23' />
                < xs:enumeration value = '24' />
                < xs:enumeration value = '25' />
                < xs:enumeration value = '26' />
                < xs:enumeration value = '34' />
                < xs:enumeration value = '35' />
                < xs:enumeration value = '36' />
              </ xs:restriction>
            </xs:simpleType>
          </xs:element>
          <xs:element name = 'AdditionalTerminalCapabilities' >
            < xs:annotation>
              <xs:documentation>Indicates the data input and output capabilities of the terminal</xs:documentation>
            </xs:annotation>
            <xs:simpleType>
              <xs:restriction base='xs:string'>
                <xs:length value = '10' />
              </ xs:restriction>
            </xs:simpleType>
          </xs:element>
          <xs:element name = 'TransactionCurrencyCode' type= 'tt:StringIndexThreeType' >
            < xs:annotation>
              <xs:documentation>Indicates the currency code of the transaction</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TransactionCurrencyExponent' type= 'tt:DigitIndexOneType' default='2'>
            <xs:annotation>
              <xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TransactionReferenceCurrencyCode' type= 'tt:StringIndexThreeType' minOccurs= '0' >
            < xs:annotation>
              <xs:documentation>Code defining the common currency used by the terminal in case the Transaction Currency Code is different from the Application Currency Code</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TransactionReferenceCurrencyExponent' type= 'tt:DigitIndexOneType' minOccurs= '0' >
            < xs:annotation>
              <xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount, with the Transaction Reference Currency Code</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TerminalCountryCode' type= 'tt:StringIndexThreeType' >
            < xs:annotation>
              <xs:documentation>Indicates the country code of the terminal</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'MerchantCategoryCode' type= 'tt:StringIndexFourType' default='5541'>
            <xs:annotation>
              <xs:documentation>Classifies the type of business being done by the merchant. Default would be 5541 (Gasoline Service Station)</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'TransactionCategoryCode' default='R'>
            <xs:annotation>
              <xs:documentation>Used to assist with Card Risk Management.Default is R which means Retail.Used only for Mastercard.</xs:documentation>
            </xs:annotation>
            <xs:simpleType>
              <xs:restriction base='xs:string'>
                <xs:length value = '1' />
               < xs:enumeration value = 'A' />
               < xs:enumeration value = 'C' />
               < xs:enumeration value = 'F' />
               < xs:enumeration value = 'H' />
               < xs:enumeration value = 'O' />
               < xs:enumeration value = 'R' />
               < xs:enumeration value = 'T' />
               < xs:enumeration value = 'U' />
               < xs:enumeration value = 'X' />
               < xs:enumeration value = 'Z' />
             </ xs:restriction>
            </xs:simpleType>
          </xs:element>
          <xs:element name = 'PinEntryTimeout' type= 'tt:StringIndexThreeType' minOccurs= '0' >
           < xs:annotation>
              <xs:documentation>EMV PIN entry timeout value in seconds</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'BaseTagListInclusion' type= 'TagListType' minOccurs= '0' maxOccurs= '1' >
           < xs:annotation>
              <xs:documentation>Defines list of required tags to be used for all transaction</xs:documentation>
            </xs:annotation>
          </xs:element>
          <xs:element name = 'BaseTagListExclusion' type= 'TagListType' minOccurs= '0' maxOccurs= '1' >
           < xs:annotation>
              <xs:documentation>Defines list of required that should not be used in any transaction</xs:documentation>
            </xs:annotation>
          </xs:element>
        </xs:sequence>
      </xs:complexType>
      <xs:simpleType name = 'TagListType' >
       < xs:annotation>
          <xs:documentation>
               Represents a list of EMV tag types.Each list is a hexadecimal representation of TLV type. An tag entry is either of 2 or 4 characters of length and no more than 255 entries are allowed
           </xs:documentation>
        </xs:annotation>
        <xs:restriction base='xs:string'>
          <xs:maxLength value = '1020' />
         < xs:pattern value = '([a-fA-F0-9])*' />
       </ xs:restriction>
      </xs:simpleType>
    </xs:schema>
  </SchemaDef>
  <DefaultXML>
    <ICCConfigTable xmlns = 'http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt= 'http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xsi= 'http://www.w3.org/2001/XMLSchema-instance' version= '1' xsi:schemaLocation= 'http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd' >
     < ICCConfigRow >
       < ICCMode > Contact </ ICCMode >
       < EnableICC > true </ EnableICC >
       < TerminalType > 22 </ TerminalType >
       < AdditionalTerminalCapabilities > 7100D0B001</AdditionalTerminalCapabilities>
        <TransactionCurrencyCode>840</TransactionCurrencyCode>
        <TransactionCurrencyExponent>2</TransactionCurrencyExponent>
        <TerminalCountryCode>840</TerminalCountryCode>
        <MerchantCategoryCode>5541</MerchantCategoryCode>
        <TransactionCategoryCode>R</TransactionCategoryCode>
      </ICCConfigRow>
      <ICCConfigRow>
        <ICCMode>Contactless</ICCMode>
        <EnableICC>false</EnableICC>
        <TerminalType>22</TerminalType>
        <AdditionalTerminalCapabilities>6000F0F001</AdditionalTerminalCapabilities>
        <TransactionCurrencyCode>840</TransactionCurrencyCode>
        <TransactionCurrencyExponent>2</TransactionCurrencyExponent>
        <TerminalCountryCode>840</TerminalCountryCode>
        <MerchantCategoryCode>5541</MerchantCategoryCode>
        <TransactionCategoryCode>R</TransactionCategoryCode>
      </ICCConfigRow>
    </ICCConfigTable>
  </DefaultXML>
</_x0023_tbEPSPetroTableAudit>"
             , previousValue= @"<_x0023_tbEPSPetroTableAudit TableID='1826' TableName='ICCConfigTable' />"
             , tableID = "1826"
             , tableName = "ICCConfigTable"
            }

            };

            epsPetroAuditDetails = new List<EPSPetroAuditDetails>() {
                new EPSPetroAuditDetails(){affectedField="Version",prevValue="BaseVersionID:1126",auditId=1695 },
                new EPSPetroAuditDetails(){affectedField="Active",newValue="1",auditId=2471 },
                new EPSPetroAuditDetails(){affectedField="DefinitionOnly",newValue="0",auditId=2471 },
                new EPSPetroAuditDetails(){affectedField="EffectiveDate",newValue="2017-10-11T00:00:00",auditId=2471 }
            };
        }

        public Task<ICollection<EPSPetroAudit>> GetEPSPetroAuditsAsync(int versionID, string startDate, string endDate)
        {
            if (versionID <= 0)
            {
                throw new Exception("VersionID is invalid");
            }
            ICollection<EPSPetroAudit> audits = epsPetroAudits.Where(s => s.versionId == versionID && Convert.ToDateTime(startDate)<=s.auditDate && Convert.ToDateTime(endDate) >= s.auditDate).ToList();
            return Task.Run(() => audits);
        }

        private string GetNewXMLForV90A1()
        {
            var retXml = "xml vlue";
            return retXml;
        }

        public Task<ICollection<EPSPetroAuditDetails>> GetEPSPetroAuditDetailsAsync(int auditID)
        {
            if (auditID <= 0)
            {
                throw new Exception("auditID is invalid");
            }
            ICollection<EPSPetroAuditDetails> audits = epsPetroAuditDetails.Where(s => s.auditId == auditID).ToList();
            return Task.Run(() => audits);

        }
    }
}

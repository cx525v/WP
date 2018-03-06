using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestDataAccess;
using Worldpay.CIS.DataAccess.CommanderVersion;
using Wp.CIS.LynkSystems.Model;
using models = Wp.CIS.LynkSystems.Model;

namespace CIS.WebApi.UnitTests.CommanderVersion
{
    public class MockCommanderVersionRepository: ICommanderVersionRepository
    {
        public List<models.CommanderVersion> versionInfo;
        public ICollection<BaseVersion> baseVersionInfo;
        public ICollection<EPSMapping> epsMapping;
        public ICollection<PetroTable> petroTable;

        public MockCommanderVersionRepository()
        {

            versionInfo = new List<models.CommanderVersion>();
            versionInfo.Add(new models.CommanderVersion()
            {
                VersionID = "1",
                VersionDescription = "WPYPAK 06.02.01"
            });
            versionInfo.Add(new models.CommanderVersion()
            {
                VersionID = "2",
                VersionDescription = "WPYPAK 06.02.02"
            });


            baseVersionInfo = new List<BaseVersion>();
            baseVersionInfo.Add(new BaseVersion()
            {
                VersionID = "3",
                VersionDescription = "WPYPAK 06.02.01"
            });
            baseVersionInfo.Add(new BaseVersion()
            {
                VersionID = "4",
                VersionDescription = "WPYPAK 06.02.02"
            });

            epsMapping = new List<EPSMapping>();
            epsMapping.Add(new EPSMapping()
            {
                versionID = 1,
                pdlFlag = true,
                worldPayFieldName = "PrimaryPhoneNbr",
                worldPayTableName = "tbDemographics,tbNameAddress ",
                worldPayJoinFields = null,
                worldPayCondition = null,
                worldPayOrderBy = "EffectiveBeginDt DESC",
                worldPayFieldDescription = "Payment Type",
                paramID = 148,
                effectiveBeginDate = Convert.ToDateTime("2013-05-02"),
                effectiveEndDate = Convert.ToDateTime("2013-08-02"),
                viperTableName = "FIDTable",
                viperFieldName = "FieldUsageIdentifierTable/FieldUsageIdentifierRow/DefaultValue",
                viperCondition = @"/FieldUsageIdentifierTable/FieldUsageIdentifierRow[FieldUsageIdentifier=DEALERNAME]",
                charStartIndex = 101,
                charLength = 100
            });
            epsMapping.Add(new EPSMapping()
            {
                versionID = 2,
                pdlFlag = true,
                worldPayFieldName = "SecondaryPhoneNbr",
                worldPayTableName = "tbTranSurcharge ",
                worldPayJoinFields = null,
                worldPayCondition = null,
                worldPayOrderBy = "EffectiveBeginDt DESC",
                worldPayFieldDescription = "Account Type",
                paramID = 156,
                effectiveBeginDate = Convert.ToDateTime("2013-05-04"),
                effectiveEndDate = Convert.ToDateTime("2013-08-01"),
                viperTableName = "FIDTable",
                viperFieldName = "FieldUsageIdentifierTable/FieldUsageIdentifierRow/DefaultValue",
                viperCondition = @"/FieldUsageIdentifierTable/FieldUsageIdentifierRow[FieldUsageIdentifier=AVS]",
                charStartIndex = 201,
                charLength = 200
            });

            petroTable = new List<PetroTable>();
            PetroTable table = new PetroTable();
            table.TableID = 1;
            table.TableName = "AIDTable";
            table.VersionID = 1;
            table.Active = true;
            table.DefinitionOnly = false;
            table.SchemaDef = "<!-- edited with XMLSpy v2014 (http://www.altova.com) by Farhan (Verifone Inc.) --><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:this=\"http://www.verifone.com/eps/viper/namespace/v1\" xmlns:tt=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" xmlns=\"http://www.verifone.com/eps/viper/namespace/v1\" targetNamespace=\"http://www.verifone.com/eps/viper/namespace/v1\" elementFormDefault=\"qualified\" attributeFormDefault=\"unqualified\"><xs:import namespace=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" schemaLocation=\"Dictionary.xsd\" /><xs:element name=\"AIDTable\"><xs:complexType><xs:sequence><xs:element name=\"AIDRow\" type=\"this:AIDRowType\" minOccurs=\"0\" maxOccurs=\"unbounded\" /></xs:sequence><xs:attribute name=\"version\" type=\"tt:VersionType\" use=\"required\" /></xs:complexType><xs:key name=\"AIDPK\"><xs:selector xpath=\"this:AIDRow\" /><xs:field xpath=\"this:AID\" /></xs:key></xs:element><xs:complexType name=\"AIDRowType\"><xs:sequence><xs:element name=\"AID\" type=\"tt:AIDType\"><xs:annotation><xs:documentation>Uniquely identifies the application (RID + PIX)</xs:documentation></xs:annotation></xs:element><xs:element name=\"AIDName\"><xs:annotation><xs:documentation>AID name used for cardholder confirmation if not present in card </xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:minLength value=\"1\" /><xs:maxLength value=\"16\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"RIDName\" minOccurs=\"0\"><xs:annotation><xs:documentation>Card scheme label. E.g. VISA, Mastercard, etc.</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:minLength value=\"1\" /><xs:maxLength value=\"35\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"AIDEnabled\" type=\"xs:boolean\"><xs:annotation><xs:documentation>Indicates whether the AID is allowed or not</xs:documentation></xs:annotation></xs:element><xs:element name=\"AIDMode\" type=\"tt:ModeTypes\"><xs:annotation><xs:documentation>Defines whether it is a Contact or Contactless AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"AIDTagList\" type=\"TagListType\" minOccurs=\"0\"><xs:annotation><xs:documentation>Additional list of transaction tags required to be send to the host for this AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"AIDTagListExclude\" type=\"TagListType\" minOccurs=\"0\" maxOccurs=\"1\"><xs:annotation><xs:documentation>Defines list of required that should not be used for the corresponding AID in the transaction</xs:documentation></xs:annotation></xs:element><xs:element name=\"PinBypassFlag\" type=\"tt:PinBypassType\" minOccurs=\"0\"><xs:annotation><xs:documentation>Indicates whether PIN bypass should be allowed for this AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"TransactionType\" type=\"tt:UsageFlag\" minOccurs=\"0\" maxOccurs=\"unbounded\"><xs:annotation><xs:documentation>Indicates the transaction types associated with the AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"TerminalApplicationVersionNumber1\"><xs:annotation><xs:documentation>Version number assigned by the payment system for the application</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"4\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TerminalApplicationVersionNumber2\" minOccurs=\"0\"><xs:annotation><xs:documentation>Version number assigned by the payment system for the application</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"4\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TerminalRiskManagementData\" minOccurs=\"0\"><xs:annotation><xs:documentation>Application specific value used by the card for risk management purposes</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:minLength value=\"2\" /><xs:maxLength value=\"16\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TerminalCapabilities\"><xs:annotation><xs:documentation>Indicates the card data input, CVM, and security capabilities of the terminal</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"6\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"ApplicationCountryCode\" type=\"tt:StringIndexThreeType\" minOccurs=\"0\"><xs:annotation><xs:documentation>Indicates the Country Code associated with the AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"TerminalFloorLimit\" type=\"tt:DigitIndexEightType\"><xs:annotation><xs:documentation>Indicates the floor limit in the terminal in conjunction with the AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"RandomSelectionThreshold\" type=\"tt:DigitIndexEightType\" minOccurs=\"0\"><xs:annotation><xs:documentation>Value used in terminal risk management for random transaction selection. Must be zero or a positive number less than the floor limit</xs:documentation></xs:annotation></xs:element><xs:element name=\"TargetRandomSelectionPercent\" type=\"tt:DigitIndexTwoType\" minOccurs=\"0\"><xs:annotation><xs:documentation>The desired percentage of transactions just below the threshold value that will be selected for Random Selection</xs:documentation></xs:annotation></xs:element><xs:element name=\"MaxRandomSelectionPercent\" type=\"tt:DigitIndexTwoType\" minOccurs=\"0\"><xs:annotation><xs:documentation>The desired percentage of transactions just below the floor limit that will be selected for Random Selection</xs:documentation></xs:annotation></xs:element><xs:element name=\"TACDefault\"><xs:annotation><xs:documentation>Specifies the acquirer‘s conditions that cause a transaction to be rejected if it might have been approved online, but the terminal is unable to process the transaction online</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"10\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TACDenial\"><xs:annotation><xs:documentation>Specifies the acquirer‘s conditions that cause the denial of a transaction without attempt to go online</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"10\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TACOnline\"><xs:annotation><xs:documentation>Specifies the acquirer‘s conditions that cause a transaction to be transmitted online</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"10\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"TDOLDefault\" type=\"xs:string\"><xs:annotation><xs:documentation>TDOL to be used for generating the TC Hash Value if the TDOL in the card is not present</xs:documentation></xs:annotation></xs:element><xs:element name=\"DDOLDefault\" type=\"xs:string\" minOccurs=\"0\"><xs:annotation><xs:documentation>DDOL to be used for constructing the INTERNAL AUTHENTICATE command if the DDOL in the card is not present</xs:documentation></xs:annotation></xs:element><xs:element name=\"StandinTVRMask\" minOccurs=\"0\"><xs:annotation><xs:documentation>TVR Mask used for EMV Stand-In processing</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"10\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"StandinTSIMask\" minOccurs=\"0\"><xs:annotation><xs:documentation>TSI Mask used for EMV Stand-In processing</xs:documentation></xs:annotation><xs:simpleType><xs:restriction base=\"xs:string\"><xs:length value=\"4\" /></xs:restriction></xs:simpleType></xs:element><xs:element name=\"PinBypassAmount\" type=\"tt:StringIndexFourType\" default=\"0000\" minOccurs=\"0\"><xs:annotation><xs:documentation>If the transaction amount is less than or equal to this amount, PIN bypass should be allowed for this AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"AllowEMVStandInOnOfflineDecline\" type=\"xs:boolean\" default=\"false\" minOccurs=\"0\"><xs:annotation><xs:documentation>Indicates whether EMV Stand-In processing is allowed for this AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"CheckCountryCodeForEMVStandIn\" type=\"xs:boolean\" default=\"false\" minOccurs=\"0\"><xs:annotation><xs:documentation>Indicates whether the Country Code should be checked during EMV Stand-In processing for this AID</xs:documentation></xs:annotation></xs:element><xs:element name=\"EMVStandInFloorLimit\" type=\"tt:StringIndexFourType\" default=\"0000\" minOccurs=\"0\"><xs:annotation><xs:documentation>If the transaction amount is less than this value, then the transaction would be applicable for EMV Stand-In processing</xs:documentation></xs:annotation></xs:element></xs:sequence></xs:complexType><xs:simpleType name=\"TagListType\"><xs:annotation><xs:documentation>\r\n\t\t\t\tRepresents a list of EMV tag types. Each list is a hexadecimal representation of TLV type. An tag entry is either of 2 or 4 characters of length and no more than 255 entries are allowed\r\n\t\t\t</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"1020\" /><xs:pattern value=\"([a-fA-F0-9])*\" /></xs:restriction></xs:simpleType></xs:schema>";
            table.DefaultXML = "<AIDTable xmlns=\"http://www.verifone.com/eps/viper/namespace/v1\" xmlns:tt=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" version=\"1\" xsi:schemaLocation=\"http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/AIDTable.xsd\"><AIDRow><AID>A0000000031010</AID><AIDName>VISA CREDIT</AIDName><RIDName>VISA</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>008C</TerminalApplicationVersionNumber1><TerminalRiskManagementData>A444A</TerminalRiskManagementData><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000120</RandomSelectionThreshold><TargetRandomSelectionPercent>30</TargetRandomSelectionPercent><MaxRandomSelectionPercent>40</MaxRandomSelectionPercent><TACDefault>DC4000A800</TACDefault><TACDenial>0010000000</TACDenial><TACOnline>DC4004F800</TACOnline><TDOLDefault>9F020695055F2A029A039C019F3704</TDOLDefault><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000032010</AID><AIDName>Visa Electron</AIDName><RIDName>Visa</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0096</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>DC4000A800</TACDefault><TACDenial>0010000000</TACDenial><TACOnline>DC4004F800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000033010</AID><AIDName>Visa Interlink</AIDName><RIDName>Visa</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0096</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>DC4000A800</TACDefault><TACDenial>0010000000</TACDenial><TACOnline>DC4004F800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000041010</AID><AIDName>MC CREDIT</AIDName><RIDName>MASTERCARD</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>1</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0002</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000120</RandomSelectionThreshold><TargetRandomSelectionPercent>30</TargetRandomSelectionPercent><MaxRandomSelectionPercent>40</MaxRandomSelectionPercent><TACDefault>FC50BC2000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>FC50BCF800</TACOnline><TDOLDefault>9F020695055F2A029A039C019F3704</TDOLDefault><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000043060</AID><AIDName>MC Credit</AIDName><RIDName>MasterCard</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0002</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>FC50BC2000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>FC50BCF800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000001523010</AID><AIDName>DISCOVER CREDIT</AIDName><RIDName>DISCOVER</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>1</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0001</TerminalApplicationVersionNumber1><TerminalApplicationVersionNumber2>0001</TerminalApplicationVersionNumber2><TerminalCapabilities>E0B8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000120</RandomSelectionThreshold><TargetRandomSelectionPercent>30</TargetRandomSelectionPercent><MaxRandomSelectionPercent>40</MaxRandomSelectionPercent><TACDefault>C800000000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>C800000000</TACOnline><TDOLDefault>9F020695055F2A029A039C019F3704</TDOLDefault><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A00000002501</AID><AIDName>AMEX CREDIT</AIDName><RIDName>AMEX</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>1</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0001</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>DC00002000</TACDefault><TACDenial>0010000000</TACDenial><TACOnline>FCE09CF800</TACOnline><TDOLDefault>9F020695055F2A029A039C019F3704</TDOLDefault><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000980840</AID><AIDName>Common Debit</AIDName><RIDName>Visa</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0001</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>DC4000A800</TACDefault><TACDenial>0010000000</TACDenial><TACOnline>DC4004F800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000001524010</AID><AIDName>DISCOVER DEBIT</AIDName><RIDName>DISCOVER</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0001</TerminalApplicationVersionNumber1><TerminalApplicationVersionNumber2>0001</TerminalApplicationVersionNumber2><TerminalCapabilities>E0B8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000120</RandomSelectionThreshold><TargetRandomSelectionPercent>30</TargetRandomSelectionPercent><MaxRandomSelectionPercent>40</MaxRandomSelectionPercent><TACDefault>C800000000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>C800000000</TACOnline><TDOLDefault>9F020695055F2A029A039C019F3704</TDOLDefault><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000000042203</AID><AIDName>MASTERCARD Debit</AIDName><RIDName>MASTERCARD</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0002</TerminalApplicationVersionNumber1><TerminalCapabilities>E0F8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>FC50BC2000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>FC50BCF800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow><AIDRow><AID>A0000006200620</AID><AIDName>Common Debit</AIDName><RIDName>DNA</RIDName><AIDEnabled>true</AIDEnabled><AIDMode>Contact</AIDMode><PinBypassFlag>0</PinBypassFlag><TransactionType>0</TransactionType><TransactionType>1</TransactionType><TerminalApplicationVersionNumber1>0096</TerminalApplicationVersionNumber1><TerminalCapabilities>E0B8C8</TerminalCapabilities><TerminalFloorLimit>00000000</TerminalFloorLimit><RandomSelectionThreshold>00000000</RandomSelectionThreshold><TargetRandomSelectionPercent>00</TargetRandomSelectionPercent><MaxRandomSelectionPercent>00</MaxRandomSelectionPercent><TACDefault>FC50ACA000</TACDefault><TACDenial>0000000000</TACDenial><TACOnline>FC50BCF800</TACOnline><TDOLDefault /><DDOLDefault>9F3704</DDOLDefault><StandinTVRMask>FD7FFB270F</StandinTVRMask><StandinTSIMask>E800</StandinTSIMask><AllowEMVStandInOnOfflineDecline>false</AllowEMVStandInOnOfflineDecline><CheckCountryCodeForEMVStandIn>false</CheckCountryCodeForEMVStandIn><EMVStandInFloorLimit>0000</EMVStandInFloorLimit></AIDRow></AIDTable>";
            table.CreatedDate = DateTime.Parse("2017-07-25 13:16:18.25");
            table.EffectiveDate = DateTime.Parse("2017-07-25 00:00:00");
            table.LastUpdatedBy = "PNandyala";
            petroTable.Add(table);
        }

        private bool ValidateVersionFormat(string version)
        {
            string regularExpression = @"^[A-Z\s.0-9#$*()?!+_-]{1,20}$";
            Regex regex = new Regex(regularExpression, RegexOptions.Singleline);
             Match m = regex.Match(version);
            return m.Success;
        }

        public Task<bool> CreateVersionAsync(Wp.CIS.LynkSystems.Model.CommanderVersion commanderVersion)
        {
            if (versionInfo.Any(s => s.VersionDescription == commanderVersion.VersionDescription))
                throw new Exception("Same version already exists");
            if (!ValidateVersionFormat(commanderVersion.VersionDescription))
                throw new Exception("Invalid version format");
            if (commanderVersion.BaseVersionID.HasValue)
            {
                if (!versionInfo.Any(s => Convert.ToInt32(s.VersionID) == commanderVersion.BaseVersionID))
                    throw new Exception("Base version not exists");
            }
            int newVersionId = versionInfo.Max(s => Convert.ToInt32(s.VersionID)) + 1;
            commanderVersion.VersionID = Convert.ToString(newVersionId);
            versionInfo.Add(commanderVersion);
            if (commanderVersion.BaseVersionID.HasValue)
            {
                var mapping = (EPSMapping)MockUtility.CloneObject(epsMapping.Where(s => s.versionID == commanderVersion.BaseVersionID.Value).FirstOrDefault());
                mapping.versionID = newVersionId;
                epsMapping.Add(mapping);

                var table = (PetroTable)MockUtility.CloneObject(petroTable.Where(s => s.VersionID == commanderVersion.BaseVersionID.Value).FirstOrDefault());
                table.VersionID = newVersionId;
                petroTable.Add(table);
            }
            return Task.Run(() => true);
        }

        public Task<bool> DeleteVersionAsync(int versionID, string userName)
        {
            if (this.versionInfo.Any(p => p.VersionID == versionID.ToString()))
            {
                this.versionInfo.RemoveAll(p => p.VersionID == versionID.ToString());
                var removeList = epsMapping.ToList().Where(s => s.versionID == versionID).ToList();
                foreach (var item in removeList)
                {
                    epsMapping.Remove(item);
                }
                var removeTableList = petroTable.ToList().Where(s => s.VersionID == versionID).ToList();
                foreach (var item in removeTableList)
                {
                    petroTable.Remove(item);
                }
                return Task.Run(() =>true);
            }
            else
            {
                throw new Exception("Versionid Invalid");
            }
        }

        public Task<ICollection<BaseVersion>> GetBaseVersionsAsync()
        {
            return Task.Run(() => baseVersionInfo);
        }

        public Task<ICollection<Wp.CIS.LynkSystems.Model.CommanderVersion>> GetVersionsAsync()
        {
            ICollection<Wp.CIS.LynkSystems.Model.CommanderVersion> obj = versionInfo;
            return Task.Run(() => obj);
        }

    }
}

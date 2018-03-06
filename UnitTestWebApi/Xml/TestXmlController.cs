using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Worldpay.Logging.Providers.Log4Net.Facade;
using Wp.CIS.LynkSystems.Model.Tree;
using Wp.CIS.LynkSystems.Services;
using Wp.CIS.LynkSystems.WebApi.Common;
using Wp.CIS.LynkSystems.WebApi.Controllers;
using Xunit;

namespace CIS.WebApi.UnitTests.Xml
{
    public class TestXmlController
    {
        [Fact]
        public async Task EPSUpdatePetroTableDefaultXml_Success()
        {
            IDistributedCache _cache = FakeCache();        
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            XmlController controller = new XmlController(_cache,  null,  fakeLogger, xmlApi);
            UpdateXmlModel data = new UpdateXmlModel();
            LeafData ld = new LeafData
            {
                colName = "EnableICC",
                rowNum = 0,
                newValue = "false"
            };
            LeafData[] updates = new LeafData[1];
            updates[0] = ld;
            data.Updates = updates;
            string xmlAppender = "<?xml version='1.0' encoding='UTF-8' standalone='yes'?>";
            data.xml = xmlAppender + @"
    <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
    <ICCConfigRow>
        <ICCMode>Contact</ICCMode>
        <EnableICC>true</EnableICC>
        <TerminalType>22</TerminalType>
        <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
    </ICCConfigTable>";
            
            string newDefaultXml = xmlAppender + @"
    <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
    <ICCConfigRow>
        <ICCMode>Contact</ICCMode>
        <EnableICC>false</EnableICC>
        <TerminalType>22</TerminalType>
        <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
    </ICCConfigTable>";

            
            //.. Act
            var dinfo = await controller.EPSUpdatePetroTableDefaultXml(data);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            XDocument oldXml = XDocument.Parse((xmlAppender + (string)actualRecord).Replace("'", "\"").Replace("\n", "").Trim());
            XDocument newXml = XDocument.Parse(newDefaultXml.Replace("'", "\"").Replace("\n", "").Trim());
            Assert.Equal(oldXml.ToString(), newXml.ToString());






        }

        [Fact]
        public async Task EPSUpdatePetroTableDefaultXml_Fail()
        {
            IDistributedCache _cache = FakeCache();
           ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            XmlController controller = new XmlController(_cache,  null, fakeLogger,xmlApi);
            UpdateXmlModel data = new UpdateXmlModel();
            LeafData ld = new LeafData
            {
                colName = "EnableICC",
                rowNum = 0,
                newValue = "astring"
            };
            LeafData[] updates = new LeafData[1];
            updates[0] = ld;
            data.Updates = updates;
            string xmlAppender = "<?xml version='1.0' encoding='UTF-8' standalone='yes'?>";
            data.xml = xmlAppender + @"
    <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
    <ICCConfigRow>
        <ICCMode>Contact</ICCMode>
        <EnableICC>true</EnableICC>
        <TerminalType>22</TerminalType>
        <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
    </ICCConfigTable>";

            string newDefaultXml = xmlAppender + @"
    <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
    <ICCConfigRow>
        <ICCMode>Contact</ICCMode>
        <EnableICC>false</EnableICC>
        <TerminalType>astring</TerminalType>
        <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
    </ICCConfigTable>";


            //.. Act
            var dinfo = await controller.EPSUpdatePetroTableDefaultXml(data);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            XDocument oldXml = XDocument.Parse((xmlAppender + (string)actualRecord).Replace("'", "\"").Replace("\n", "").Trim());
            XDocument newXml = XDocument.Parse(newDefaultXml.Replace("'", "\"").Replace("\n", "").Trim());
            Assert.NotEqual(oldXml.ToString(), newXml.ToString());
        }

        [Fact]
        public async Task GetTableSchema_Success()
        {
            IDistributedCache _cache = FakeCache();         
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            XmlController controller = new XmlController(_cache, localizer, fakeLogger, xmlApi);
            string schemaDef = @"<!-- edited with XMLSpy v2008 sp1 (http://www.altova.com) by Himanshu Yadav (VERIFONE) -->
        <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->
        <xs:schema xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns='http://www.verifone.com/eps/viper/namespace/v1' xmlns:this='http://www.verifone.com/eps/viper/namespace/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
          <xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd' />
          <xs:element name='LimitTable'>
            <xs:complexType>
              <xs:sequence minOccurs='0' maxOccurs='unbounded'>
                <xs:element name='LimitRow' type='LimitRowType' />
              </xs:sequence>
              <xs:attribute name='version' type='tt:VersionType' use='required' />
            </xs:complexType>
            <xs:key name='LimitPK'>
              <xs:selector xpath='this:LimitRow' />
              <xs:field xpath='this:LimitTableIndex' />
            </xs:key>
          </xs:element>
          <xs:simpleType name='StringFourType'>
            <xs:restriction base='xs:string'>
              <xs:pattern value='\d\d\d\d' />
            </xs:restriction>
          </xs:simpleType>
          <xs:complexType name='LimitRowType'>
            <xs:sequence>
              <xs:element name='LimitTableIndex' type='tt:LimitIndexType' />
              <xs:choice>
                <xs:element name='OfflineFloorLimit' type='StringFourType' />
                <xs:sequence>
                  <xs:element name='InsideOfflineFloorLimit' type='StringFourType' />
                  <xs:element name='OutsideOfflineFloorLimit' type='StringFourType' />
                </xs:sequence>
              </xs:choice>
              <xs:choice>
                <xs:element name='DispenserPreAuth' type='StringFourType' />
                <xs:sequence>
                  <xs:element name='InsideDispenserPreAuth' type='StringFourType' />
                  <xs:element name='OutsideDispenserPreAuth' type='StringFourType' />
                </xs:sequence>
              </xs:choice>
              <xs:choice>
                <xs:element name='DispenserFuelingLimit' type='StringFourType' />
                <xs:sequence>
                  <xs:element name='InsideDispenserFuelingLimit' type='StringFourType' />
                  <xs:element name='OutsideDispenserFuelingLimit' type='StringFourType' />
                </xs:sequence>
              </xs:choice>
              <xs:element name='VelocityTrigger' type='tt:VelocityGrpIndType' />
              <xs:element name='VelocityTriggerActionCode' type='tt:ActionCodeType' />
              <xs:element name='PaymentSelectionThreshold' type='StringFourType' default='0000' minOccurs='0' />
              <xs:element name='MaxFuelProductCount' type='StringFourType' default='0000' minOccurs='0'>
                <xs:annotation>
                  <xs:documentation>Number of fuel products allowed in a transaction using the card. The default value is 0 which means no restriction for fuel product.</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name='MaxProductCount' type='StringFourType' default='0000' minOccurs='0'>
                <xs:annotation>
                  <xs:documentation>Number of products allowed in a transaction using the card. The default value is 0 which means no restriction for product.</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name='SmallTicketLimit' type='StringFourType' default='0000' minOccurs='0'>
                <xs:annotation>
                  <xs:documentation>If the transaction amount is less than or equal to this limit, no signature line will be printed on the receipt.</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name='OutsideOfflineFloorCount' type='StringFourType' maxOccurs='1' minOccurs='0' />
              <xs:element name='InsideOfflineFloorCount' type='StringFourType' maxOccurs='1' minOccurs='0' />
              <xs:element name='CashBackLimit' type='StringFourType' maxOccurs='1' minOccurs='0' />
              <xs:element name='MerchandiseLimit' type='StringFourType' maxOccurs='1' minOccurs='0' />
              <xs:element name='ZipCodeOverLimit' type='StringFourType' maxOccurs='1' minOccurs='0' />
            </xs:sequence>
          </xs:complexType>
        </xs:schema>";

            var dinfo = await controller.GetTableSchema(schemaDef);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            Assert.Equal(((TableProperty)actualRecord).tableName, "LimitTable");
        }

        [Fact]
        public async Task GetTableSchema_Fail()
        {
            IDistributedCache _cache = FakeCache();       
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            XmlController controller = new XmlController(_cache, localizer,  fakeLogger, xmlApi);
            string schemaDef = "";

            var dinfo = await controller.GetTableSchema(schemaDef);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value as XmlException;
            
            Assert.Equal(actualRecord.Message, "Root element is missing.");
        }

        [Fact]
        public async Task GetTreeNode_Success()
        {
            IDistributedCache _cache = FakeCache();          
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            XmlController controller = new XmlController(_cache, localizer, fakeLogger, xmlApi);
            DefaultXmlModel xml = new DefaultXmlModel() { defaultXml = @"
        <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
            <ICCConfigRow>
                <ICCMode>Contact</ICCMode>
                <EnableICC>true</EnableICC>
                <TerminalType>22</TerminalType>
                <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
        </ICCConfigTable>" };

            var dinfo = await controller.GetTreeNode(xml);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            Assert.Equal(((Tree)actualRecord).data.FirstOrDefault().label, "ICCConfigTable");
            Assert.Equal(((Tree)actualRecord).data.FirstOrDefault().children.Count, 2);
        }

        [Fact]
        public async Task GetTreeNode_Fail()
        {
            IDistributedCache _cache = FakeCache();           
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = Substitute.For<IXmlApi>();
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            XmlController controller = new XmlController(_cache,  localizer, fakeLogger,xmlApi);

            DefaultXmlModel xml = new DefaultXmlModel() { defaultXml = @"
        <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
            <ICCConfigRow1>
                <ICCMode>Contact</ICCMode>
                <EnableICC>true</EnableICC>
                <TerminalType>22</TerminalType>
                <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
        </ICCConfigTable>" };

            var dinfo = await controller.GetTreeNode(xml);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value as Tree;

            Assert.Equal(actualRecord,null);
        }

        [Fact]
        public async Task Validate_FailInvalid()
        {
            IDistributedCache _cache = FakeCache();        
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();

            XmlController controller = new XmlController(_cache, localizer, fakeLogger,xmlApi);
            ValidateXmlModel data = new ValidateXmlModel();
            data.xsd = @"<?xml version='1.0' standalone='yes'?>
        <!-- edited with XMLSpy v2014 (http://www.altova.com) by Farhan (Verifone Inc.) -->
        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:this='http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns='http://www.verifone.com/eps/viper/namespace/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
        	<xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd'/>
        	<xs:element name='ICCConfigTable'>
        		<xs:complexType>
        			<xs:sequence>
        				<xs:element name='ICCConfigRow' type='this:ICCConfigRowType' minOccurs='0' maxOccurs='2'/>
        			</xs:sequence>
        			<xs:attribute name='version' type='tt:VersionType' use='required'/>
        		</xs:complexType>
        		<xs:key name='ICCConfigPK'>
        			<xs:selector xpath='this:ICCConfigRow'/>
        			<xs:field xpath='this:ICCMode'/>
        		</xs:key>
        	</xs:element>
        	<xs:complexType name='ICCConfigRowType'>
        		<xs:sequence>
        			<xs:element name='ICCMode' type='tt:ModeTypes'>
        				<xs:annotation>
        					<xs:documentation>It indicates whether the terminal parameters are for Contact or Contactless </xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='EnableICC' type='xs:boolean'>
        				<xs:annotation>
        					<xs:documentation>It indicates whether EMV should be enabled on the terminal or not</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TerminalType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the environment of the terminal, its communications capability and its operational control</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='2'/>
        						<xs:enumeration value='11'/>
        						<xs:enumeration value='12'/>
        						<xs:enumeration value='13'/>
        						<xs:enumeration value='14'/>
        						<xs:enumeration value='15'/>
        						<xs:enumeration value='16'/>
        						<xs:enumeration value='21'/>
        						<xs:enumeration value='22'/>
        						<xs:enumeration value='23'/>
        						<xs:enumeration value='24'/>
        						<xs:enumeration value='25'/>
        						<xs:enumeration value='26'/>
        						<xs:enumeration value='34'/>
        						<xs:enumeration value='35'/>
        						<xs:enumeration value='36'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='AdditionalTerminalCapabilities'>
        				<xs:annotation>
        					<xs:documentation>Indicates the data input and output capabilities of the terminal</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='10'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='TransactionCurrencyCode' type='tt:StringIndexThreeType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the currency code of the transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionCurrencyExponent' type='tt:DigitIndexOneType' default='2'>
        				<xs:annotation>
        					<xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionReferenceCurrencyCode' type='tt:StringIndexThreeType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>Code defining the common currency used by the terminal in case the Transaction Currency Code is different from the Application Currency Code</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionReferenceCurrencyExponent' type='tt:DigitIndexOneType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount, with the Transaction Reference Currency Code</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TerminalCountryCode' type='tt:StringIndexThreeType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the country code of the terminal</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='MerchantCategoryCode' type='tt:StringIndexFourType' default='5541'>
        				<xs:annotation>
        					<xs:documentation>Classifies the type of business being done by the merchant. Default would be 5541 (Gasoline Service Station)</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionCategoryCode' default='R'>
        				<xs:annotation>
        					<xs:documentation>Used to assist with Card Risk Management. Default is R which means Retail. Used only for Mastercard.</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='1'/>
        						<xs:enumeration value='A'/>
        						<xs:enumeration value='C'/>
        						<xs:enumeration value='F'/>
        						<xs:enumeration value='H'/>
        						<xs:enumeration value='O'/>
        						<xs:enumeration value='R'/>
        						<xs:enumeration value='T'/>
        						<xs:enumeration value='U'/>
        						<xs:enumeration value='X'/>
        						<xs:enumeration value='Z'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='PinEntryTimeout' type='tt:StringIndexThreeType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>EMV PIN entry timeout value in seconds</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='BaseTagListInclusion' type='TagListType' minOccurs='0' maxOccurs='1'>
        				<xs:annotation>
        					<xs:documentation>Defines list of required tags to be used for all transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='BaseTagListExclusion' type='TagListType' minOccurs='0' maxOccurs='1'>
        				<xs:annotation>
        					<xs:documentation>Defines list of required that should not be used in any transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        		</xs:sequence>
        	</xs:complexType>
        	<xs:simpleType name='TagListType'>
        		<xs:annotation>
        			<xs:documentation>
        				Represents a list of EMV tag types. Each list is a hexadecimal representation of TLV type. An tag entry is either of 2 or 4 characters of length and no more than 255 entries are allowed
        			</xs:documentation>
        		</xs:annotation>
        			<xs:restriction base='xs:string'>
        				<xs:maxLength value='1020'/>
        				<xs:pattern value='([a-fA-F0-9])*'/>
        			</xs:restriction>
        	</xs:simpleType>
        </xs:schema>";
            data.xml = @"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
        <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
            <ICCConfigRow>
                <ICCMode>Contact</ICCMode>
                <EnableICC>true</EnableICC>
                <TerminalType>22</TerminalType>
                <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
        ";
            var dinfo = await controller.Validate(data);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;

            Assert.Equal(((XmlValidationMessage)actualRecord).IsValid, false);
        }

        [Fact]
        public async Task Validate_Fail()
        {
            IDistributedCache _cache = FakeCache();          
            ILoggingFacade fakeLogger = FakeLogger();
            IXmlApi xmlApi = Substitute.For<IXmlApi>();
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            XmlController controller = new XmlController(_cache,  localizer,  fakeLogger,xmlApi);
            ValidateXmlModel data = new ValidateXmlModel();
            data.xsd = @"<?xml version='1.0' standalone='yes'?>
        <!-- edited with XMLSpy v2014 (http://www.altova.com) by Farhan (Verifone Inc.) -->
        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns:this='http://www.verifone.com/eps/viper/namespace/v1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' xmlns='http://www.verifone.com/eps/viper/namespace/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
        	<xs:import namespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' schemaLocation='Dictionary.xsd'/>
        	<xs:element name='ICCConfigTable'>
        		<xs:complexType>
        			<xs:sequence>
        				<xs:element name='ICCConfigRow' type='this:ICCConfigRowType' minOccurs='0' maxOccurs='2'/>
        			</xs:sequence>
        			<xs:attribute name='version' type='tt:VersionType' use='required'/>
        		</xs:complexType>
        		<xs:key name='ICCConfigPK'>
        			<xs:selector xpath='this:ICCConfigRow'/>
        			<xs:field xpath='this:ICCMode'/>
        		</xs:key>
        	</xs:element>
        	<xs:complexType name='ICCConfigRowType'>
        		<xs:sequence>
        			<xs:element name='ICCMode' type='tt:ModeTypes'>
        				<xs:annotation>
        					<xs:documentation>It indicates whether the terminal parameters are for Contact or Contactless </xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='EnableICC' type='xs:boolean'>
        				<xs:annotation>
        					<xs:documentation>It indicates whether EMV should be enabled on the terminal or not</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TerminalType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the environment of the terminal, its communications capability and its operational control</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='2'/>
        						<xs:enumeration value='11'/>
        						<xs:enumeration value='12'/>
        						<xs:enumeration value='13'/>
        						<xs:enumeration value='14'/>
        						<xs:enumeration value='15'/>
        						<xs:enumeration value='16'/>
        						<xs:enumeration value='21'/>
        						<xs:enumeration value='22'/>
        						<xs:enumeration value='23'/>
        						<xs:enumeration value='24'/>
        						<xs:enumeration value='25'/>
        						<xs:enumeration value='26'/>
        						<xs:enumeration value='34'/>
        						<xs:enumeration value='35'/>
        						<xs:enumeration value='36'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='AdditionalTerminalCapabilities'>
        				<xs:annotation>
        					<xs:documentation>Indicates the data input and output capabilities of the terminal</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='10'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='TransactionCurrencyCode' type='tt:StringIndexThreeType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the currency code of the transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionCurrencyExponent' type='tt:DigitIndexOneType' default='2'>
        				<xs:annotation>
        					<xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionReferenceCurrencyCode' type='tt:StringIndexThreeType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>Code defining the common currency used by the terminal in case the Transaction Currency Code is different from the Application Currency Code</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionReferenceCurrencyExponent' type='tt:DigitIndexOneType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>Indicates the implied position of the decimal point from the right of the transaction amount, with the Transaction Reference Currency Code</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TerminalCountryCode' type='tt:StringIndexThreeType'>
        				<xs:annotation>
        					<xs:documentation>Indicates the country code of the terminal</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='MerchantCategoryCode' type='tt:StringIndexFourType' default='5541'>
        				<xs:annotation>
        					<xs:documentation>Classifies the type of business being done by the merchant. Default would be 5541 (Gasoline Service Station)</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='TransactionCategoryCode' default='R'>
        				<xs:annotation>
        					<xs:documentation>Used to assist with Card Risk Management. Default is R which means Retail. Used only for Mastercard.</xs:documentation>
        				</xs:annotation>
        				<xs:simpleType>
        					<xs:restriction base='xs:string'>
        						<xs:length value='1'/>
        						<xs:enumeration value='A'/>
        						<xs:enumeration value='C'/>
        						<xs:enumeration value='F'/>
        						<xs:enumeration value='H'/>
        						<xs:enumeration value='O'/>
        						<xs:enumeration value='R'/>
        						<xs:enumeration value='T'/>
        						<xs:enumeration value='U'/>
        						<xs:enumeration value='X'/>
        						<xs:enumeration value='Z'/>
        					</xs:restriction>
        				</xs:simpleType>
        			</xs:element>
        			<xs:element name='PinEntryTimeout' type='tt:StringIndexThreeType' minOccurs='0'>
        				<xs:annotation>
        					<xs:documentation>EMV PIN entry timeout value in seconds</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='BaseTagListInclusion' type='TagListType' minOccurs='0' maxOccurs='1'>
        				<xs:annotation>
        					<xs:documentation>Defines list of required tags to be used for all transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        			<xs:element name='BaseTagListExclusion' type='TagListType' minOccurs='0' maxOccurs='1'>
        				<xs:annotation>
        					<xs:documentation>Defines list of required that should not be used in any transaction</xs:documentation>
        				</xs:annotation>
        			</xs:element>
        		</xs:sequence>
        	</xs:complexType>
        	<xs:simpleType name='TagListType'>
        		<xs:annotation>
        			<xs:documentation>
        				Represents a list of EMV tag types. Each list is a hexadecimal representation of TLV type. An tag entry is either of 2 or 4 characters of length and no more than 255 entries are allowed
        			</xs:documentation>
        		</xs:annotation>
        			<xs:restriction base='xs:string'>
        				<xs:maxLength value='1020'/>
        				<xs:pattern value='([a-fA-F0-9])*'/>
        			</xs:restriction>
        	</xs:simpleType>
        </xs:schema>";
            data.xml = @"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
        <ICCConfigTable xmlns='http://www.verifone.com/eps/viper/namespace/v1' version='1' xmlns:tt='http://www.verifone.com/eps/viper/namespace/Dictionary/v1'  xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/ICCConfigTable.xsd'>
            <ICCConfigRow>
                <ICCMode>Contact</ICCMode>
                <EnableICC>true</EnableICC>
                <TerminalType>22</TerminalType>
                <AdditionalTerminalCapabilities>7100D0B001</AdditionalTerminalCapabilities>
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
        </ICCConfigTable>";

            var dinfo = await controller.Validate(data);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value as Tree;

            Assert.Equal(actualRecord, null);
        }

        [Fact]
        public async Task Validate_Success()
        {
            IDistributedCache _cache = FakeCache();         
            ILoggingFacade fakeLogger = FakeLogger();
            var localizer = Substitute.For<IStringLocalizer<XmlController>>();
            IXmlApi xmlApi = new XmlApi(fakeLogger);
            XmlController controller = new XmlController(_cache,localizer,  fakeLogger,xmlApi);
            ValidateXmlModel data = new ValidateXmlModel();
            data.xsd = @"<?xml version='1.0' encoding='UTF-8' standalone='yes'?>
        <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->
        <!-- edited with XMLSpy v2005 rel. 3 U (http://www.altova.com) by Michael Ryan (Web Business Solutions, Inc.) -->
        <!--W3C Schema generated by XMLSpy v2005 rel. 3 U (http://www.altova.com)-->
        <xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' elementFormDefault='qualified'>
        	<xs:element name='createDate'>
        		<xs:simpleType>
        			<xs:restriction base='xs:dateTime'/>
        		</xs:simpleType>
        	</xs:element>
        	<xs:element name='customerNum'>
        		<xs:simpleType>
        			<xs:restriction base='xs:string'/>
        		</xs:simpleType>
        	</xs:element>
        	<xs:element name='messageLine'>
        		<xs:simpleType>
        			<xs:restriction base='xs:string'/>
        		</xs:simpleType>
        	</xs:element>
        	<xs:element name='priceNote'>
        		<xs:complexType>
        			<xs:sequence>
        				<xs:element ref='priceNoteItem'/>
        			</xs:sequence>
        		</xs:complexType>
        	</xs:element>
        	<xs:element name='priceNoteItem'>
        		<xs:complexType>
        			<xs:sequence>
        				<xs:element ref='svb'/>
        				<xs:element ref='rbuNum'/>
        				<xs:element ref='customerNum'/>
        				<xs:element ref='createDate'/>
        				<xs:element ref='priceNoteMessage'/>
        			</xs:sequence>
        		</xs:complexType>
        	</xs:element>
        	<xs:element name='priceNoteMessage'>
        		<xs:complexType>
        			<xs:sequence>
        				<xs:element ref='messageLine' maxOccurs='unbounded'/>
        			</xs:sequence>
        		</xs:complexType>
        	</xs:element>
        	<xs:element name='rbuNum'>
        		<xs:simpleType>
        			<xs:restriction base='xs:string'/>
        		</xs:simpleType>
        	</xs:element>
        	<xs:element name='svb'>
        		<xs:simpleType>
        			<xs:restriction base='xs:string'/>
        		</xs:simpleType>
        	</xs:element>
        </xs:schema>";
            data.xml = @"<?xml version='1.0' encoding='utf-8'?>
        <priceNote>
          <priceNoteItem>
            <svb>str1234</svb>
            <rbuNum>str1234</rbuNum>
            <customerNum>str1234</customerNum>
            <createDate>2012-12-13T12:12:12</createDate>
            <priceNoteMessage>
              <messageLine>str1234</messageLine>
            </priceNoteMessage>
          </priceNoteItem>
        </priceNote>";
            data.xsd = data.xsd.Replace("'", "\"");
            data.xml = data.xsd.Replace("'", "\"");
            
            data.dictionaries = new string []{ dict.Replace("'","\"")};
            var dinfo = await controller.Validate(data);
            var actualRecord = ((Microsoft.AspNetCore.Mvc.ObjectResult)dinfo).Value;
            bool isTrue =( (XmlValidationMessage)actualRecord).IsValid;
            Assert.Equal(isTrue, true);
        }
        private IDistributedCache FakeCache()
        {
            return Substitute.For<IDistributedCache>();
        }
        private static ILoggingFacade FakeLogger()
        {
            return Substitute.For<ILoggingFacade>();
        }

        private static IOperation FakeOperation(IDistributedCache cache)
        {
            IOperation fakeOperation = Substitute.For<Operation>(cache);
            return fakeOperation;
        }

        string dict = @"<?xml version='1.0' encoding='UTF-8'?>
<!-- edited with XMLSpy v2008 (http://www.altova.com) by Bradford Loewy (VERIFONE) -->
<!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->
<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' xmlns='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' targetNamespace='http://www.verifone.com/eps/viper/namespace/Dictionary/v1' elementFormDefault='qualified' attributeFormDefault='unqualified'>
	<xs:simpleType name='RegExType'>
		<xs:annotation>
			<xs:documentation>Regular Expression</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'/>
	</xs:simpleType>
	<xs:simpleType name='VersionType'>
		<xs:annotation>
			<xs:documentation>Table Version</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:decimal'/>
	</xs:simpleType>
	<xs:simpleType name='DigitIndexOneType'>
		<xs:annotation>
			<xs:documentation>Index of length 1 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='DigitIndexTwoType'>
		<xs:annotation>
			<xs:documentation>Index of length 2 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d{2}'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='StringIndexThreeType'>
		<xs:annotation>
			<xs:documentation>Index of length 3 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='StringIndexFourType'>
		<xs:annotation>
			<xs:documentation>Index of length 4 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='DigitIndexSixType'>
		<xs:annotation>
			<xs:documentation>Index of length 6 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d{6}'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='DigitIndexEightType'>
		<xs:annotation>
			<xs:documentation>Index of length 8 and type string</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d{8}'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ActionCodeType'>
		<xs:annotation>
			<xs:documentation>Action Code</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='CardHandlingRoutineIndType'>
		<xs:annotation>
			<xs:documentation>Card Handling Routine Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ActionCodehandlerIndType'>
		<xs:annotation>
			<xs:documentation>Card Handling Routine Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='FeeIndicatorType'>
		<xs:annotation>
			<xs:documentation>Fee Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:integer'/>
	</xs:simpleType>
	<xs:simpleType name='FeeIndexType'>
		<xs:annotation>
			<xs:documentation>Fee Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='FEPIndicatorType'>
		<xs:annotation>
			<xs:documentation>FEP Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='FEPNameType'>
		<xs:annotation>
			<xs:documentation>FEP Name</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='FEPTypeType'>
		<xs:annotation>
			<xs:documentation>FEP Type</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='Payment'/>
			<xs:enumeration value='Prepaid'/>
			<xs:enumeration value='Loyalty'/>
			<xs:enumeration value='EBT'/>
			<xs:enumeration value='Other'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='FieldUsageIdType'>
		<xs:annotation>
			<xs:documentation>Field Usage Identifier</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:minLength value='0'/>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='LCIType'>
		<xs:annotation>
			<xs:documentation>Logic Control Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='LimitIndexType'>
		<xs:annotation>
			<xs:documentation>Limit Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='LogicIndicatorType'>
		<xs:annotation>
			<xs:documentation>Logic Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='MaskingTableIndexType'>
		<xs:annotation>
			<xs:documentation>Masking Table Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='PriceTierType'>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='cash'/>
			<xs:enumeration value='credit'/>
			<xs:enumeration value='debit'/>
			<xs:enumeration value='storedValue'/>
			<xs:enumeration value='fleet'/>
			<xs:enumeration value='proprietary'/>
			<xs:enumeration value='loyalty'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ProductIndType'>
		<xs:annotation>
			<xs:documentation>Product Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='PromptIdType'>
		<xs:annotation>
			<xs:documentation>Prompt ID</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='PromptIndicatorType'>
		<xs:annotation>
			<xs:documentation>Prompt Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ActionCodeHandlerIdType'>
		<xs:restriction base='xs:string'>
			<xs:minLength value='1'/>
			<xs:maxLength value='20'/>
			<xs:pattern value='\d*'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ActionCodeHandlerIndicatorType'>
		<xs:annotation>
			<xs:documentation>Action Code Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ReceiptIndicatorType'>
		<xs:annotation>
			<xs:documentation>Receipt Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ValidationIndicatorType'>
		<xs:annotation>
			<xs:documentation>Validation Indicator</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ValidationIndexType'>
		<xs:annotation>
			<xs:documentation>Validation ID</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ReceiptIndexType'>
		<xs:annotation>
			<xs:documentation>Receipt Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='20'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='VelocityGrpIndType'>
		<xs:annotation>
			<xs:documentation>Velocity Group Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='NACSProductCodeType'>
		<xs:annotation>
			<xs:documentation>NACS Product Code</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='IPAddressType'>
		<xs:annotation>
			<xs:documentation>IP Address formatted xxx.xxx.xxx.xxx</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d{1,3}.\d{1,3}.\d{1,3}.\d{1,3}'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='SerialAddressType'>
		<xs:annotation>
			<xs:documentation>Serial Address</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'/>
	</xs:simpleType>
	<xs:simpleType name='TCPPort'>
		<xs:annotation>
			<xs:documentation>IP Port</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:integer'>
			<xs:minInclusive value='1'/>
			<xs:maxInclusive value='65535'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='SerialPort'>
		<xs:annotation>
			<xs:documentation>Serial communication port that the hardware will use.</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='COM0'/>
			<xs:enumeration value='COM1'/>
			<xs:enumeration value='COM2'/>
			<xs:enumeration value='COM3'/>
			<xs:enumeration value='COM4'/>
			<xs:enumeration value='COM5'/>
			<xs:enumeration value='COM6'/>
			<xs:enumeration value='TTYS1'/>
			<xs:enumeration value='PortA1-1'/>
			<xs:enumeration value='PortA1-2'/>
			<xs:enumeration value='PortA1-3'/>
			<xs:enumeration value='PortA1-4'/>
			<xs:enumeration value='PortA1-5'/>
			<xs:enumeration value='PortA1-6'/>
			<xs:enumeration value='PortA1-7'/>
			<xs:enumeration value='PortA1-8'/>
			<xs:enumeration value='PortA2-1'/>
			<xs:enumeration value='PortA2-2'/>
			<xs:enumeration value='PortA2-3'/>
			<xs:enumeration value='PortA2-4'/>
			<xs:enumeration value='PortA2-5'/>
			<xs:enumeration value='PortA2-6'/>
			<xs:enumeration value='PortA2-7'/>
			<xs:enumeration value='PortA2-8'/>
			<xs:enumeration value='PortA3-1'/>
			<xs:enumeration value='PortA3-2'/>
			<xs:enumeration value='PortA3-3'/>
			<xs:enumeration value='PortA3-4'/>
			<xs:enumeration value='PortA3-5'/>
			<xs:enumeration value='PortA3-6'/>
			<xs:enumeration value='PortA3-7'/>
			<xs:enumeration value='PortA3-8'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:attributeGroup name='ViperPort'>
		<xs:annotation>
			<xs:documentation>Optional port Channel and Codec packages</xs:documentation>
		</xs:annotation>
		<xs:attribute name='ChannelClassName' use='optional' default='com.verifone.isd.viper.eps.pres.channels.ip.IpChannel'>
			<xs:simpleType>
				<xs:restriction base='xs:string'>
					<xs:enumeration value='com.verifone.isd.viper.eps.pres.channels.ip.IpChannel'/>
					<xs:enumeration value='com.verifone.isd.viper.eps.pres.channels.serial.SerialChannel'/>
				</xs:restriction>
			</xs:simpleType>
		</xs:attribute>
		<xs:attribute name='CodecClassName' use='optional' default='com.verifone.isd.viper.eps.pres.codecs.IFSF.IFSFCodec'>
			<xs:simpleType>
				<xs:restriction base='xs:string'>
					<xs:enumeration value='com.verifone.isd.viper.eps.pres.codecs.IFSF.IFSFCodec'/>
					<xs:enumeration value='com.verifone.isd.viper.eps.pres.codecs.serial.SerialCodec'/>
				</xs:restriction>
			</xs:simpleType>
		</xs:attribute>
	</xs:attributeGroup>
	<xs:attributeGroup name='ViperPOPPort'>
		<xs:annotation>
			<xs:documentation>Optional port Channel and Codec packages</xs:documentation>
		</xs:annotation>
		<xs:attribute name='ChannelClassName' type='xs:string' use='optional' default='com.verifone.isd.viper.eps.pres.channels.ip.IpChannel'/>
		<xs:attribute name='CodecClassName' type='xs:string' use='optional' default='com.verifone.isd.viper.eps.pres.codecs.pop.PopCodec'/>
	</xs:attributeGroup>
	<xs:complexType name='ViperTCPPort'>
		<xs:annotation>
			<xs:documentation>Port plus optional Channel and Codec packages</xs:documentation>
		</xs:annotation>
		<xs:simpleContent>
			<xs:extension base='TCPPort'>
				<xs:attributeGroup ref='ViperPort'/>
			</xs:extension>
		</xs:simpleContent>
	</xs:complexType>
	<xs:complexType name='ViperPOPTCPPort'>
		<xs:annotation>
			<xs:documentation>Port plus optional Channel and Codec packages</xs:documentation>
		</xs:annotation>
		<xs:simpleContent>
			<xs:extension base='TCPPort'>
				<xs:attributeGroup ref='ViperPOPPort'/>
			</xs:extension>
		</xs:simpleContent>
	</xs:complexType>
	<xs:complexType name='ViperSerialPort'>
		<xs:annotation>
			<xs:documentation>Port plus optional Channel and Codec packages (not functional for Serial)</xs:documentation>
		</xs:annotation>
		<xs:simpleContent>
			<xs:extension base='SerialPort'>
				<xs:attributeGroup ref='ViperPort'/>
			</xs:extension>
		</xs:simpleContent>
	</xs:complexType>
	<xs:complexType name='ViperPOPSerialPort'>
		<xs:annotation>
			<xs:documentation>Port plus optional Channel and Codec packages (not functional for Serial)</xs:documentation>
		</xs:annotation>
		<xs:simpleContent>
			<xs:extension base='SerialPort'>
				<xs:attributeGroup ref='ViperPOPPort'/>
			</xs:extension>
		</xs:simpleContent>
	</xs:complexType>
	<xs:simpleType name='TrackMaskTableIndexType'>
		<xs:annotation>
			<xs:documentation>Track Masking Table Index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:pattern value='\d\d\d\d'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ICCParamsIdType'>
		<xs:annotation>
			<xs:documentation>ICCParams ID</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ICCParamType'>
		<xs:annotation>
			<xs:documentation>Parameters used to exchange information with an ICC capable device. Parameters in this list apply for ICC card processing but are not defined by a standard EMV tag	</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='AIDName'/>
			<xs:enumeration value='CAPKChecksum'/>
			<xs:enumeration value='CAPKExpDate'/>
			<xs:enumeration value='CAPKExponent'/>
			<xs:enumeration value='CAPKModulus'/>
			<xs:enumeration value='CtlsCVMLimit'/>
			<xs:enumeration value='CtlsTransactionLimit'/>
			<xs:enumeration value='CtlsTransactionScheme'/>
			<xs:enumeration value='CtlsVisaTTQ'/>
			<xs:enumeration value='DDOLDefault'/>
			<xs:enumeration value='EncipheredPINBlock'/>
			<xs:enumeration value='EnableICC'/>
			<xs:enumeration value='EnableRemoteAIDSelection'/>
			<xs:enumeration value='HashAlgorithmIndicator'/>
			<xs:enumeration value='HostDecision'/>
			<xs:enumeration value='LanguageCodeDefault'/>
			<xs:enumeration value='MaxRandomSelectionPercent'/>
			<xs:enumeration value='MerchantDecision'/>
			<xs:enumeration value='PinBypassFlag'/>
			<xs:enumeration value='PinEntryTimeOut'/>
			<xs:enumeration value='RandomSelectionThreshold'/>
			<xs:enumeration value='RID'/>
			<xs:enumeration value='RIDName'/>
			<xs:enumeration value='ScriptResult'/>
			<xs:enumeration value='SignAlgorithmIndicator'/>
			<xs:enumeration value='TACDefault'/>
			<xs:enumeration value='TACDenial'/>
			<xs:enumeration value='TACOnline'/>
			<xs:enumeration value='TargetRandomSelectionPercent'/>
			<xs:enumeration value='TDOLDefault'/>
			<xs:enumeration value='TerminalApplicationVersionNumber2'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='PromptRoutineType'>
		<xs:annotation>
			<xs:documentation>Prompt Validation Routine index</xs:documentation>
		</xs:annotation>
		<xs:restriction base='xs:integer'>
			<xs:minInclusive value='0'/>
			<xs:maxInclusive value='999'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='ModeTypes'>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='Contact'/>
			<xs:enumeration value='Contactless'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='AIDType'>
		<xs:restriction base='xs:string'>
			<xs:minLength value='10'/>
			<xs:maxLength value='32'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='UsageFlag'>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='0'/>
			<xs:enumeration value='1'/>
			<xs:enumeration value='2'/>
			<xs:enumeration value='3'/>
			<xs:enumeration value='4'/>
			<xs:enumeration value='5'/>
			<xs:enumeration value='6'/>
			<xs:enumeration value='7'/>
			<xs:enumeration value='8'/>
			<xs:enumeration value='9'/>
			<xs:enumeration value='10'/>
			<xs:enumeration value='99'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='CAPKIdType'>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='PinBypassType'>
		<xs:restriction base='xs:string'>
			<xs:enumeration value='0'/>
			<xs:enumeration value='1'/>
		</xs:restriction>
	</xs:simpleType>
	<xs:simpleType name='RuleIdType'>
		<xs:restriction base='xs:string'>
			<xs:maxLength value='25'/>
		</xs:restriction>
	</xs:simpleType>
</xs:schema>";
    }
}

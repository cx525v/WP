import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DebugElement } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule, GrowlModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar, Growl } from 'primeng/primeng';

import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { LoadingComponent } from '../loading.component';
import { DateServcie } from '../../../services/petro/date.service';
import { AuditServcie } from '../../../services/petro/audit.service';
import '../../../app.module.client';
import { commanderversion, commanderbaseversion } from '../../../models/petro/commanderversion.model';
import { Audit } from '../../../models/petro/audit.model';
import { PetroTable } from '../../../models/petro/PetroTable.model';
import { TableMapping, Mapping } from '../../../models/petro/petroTablemapping.model';
import { ActionItem } from '../../../models/petro/auditActionItem.enum';
import { AuditComponent } from './audit.component';
import { CommanderVersionComponent } from './commanderversion/version.component';
import { PetroMappingComponent} from './petroMapping/petroMapping.component';
import { PetroTableComponent} from './petroTable/petroTable.component';
import { SortPipe } from '../../../pipes/sort.pipe';
import { XmlPipe } from '../../../pipes/XmlPipe';

const mockdata = [{ "auditId": 601, "versionId": 982, "actionType": "Insert", "entityName": "Tables/Schemas", "previousValue": "<_x0023_tbEPSPetroTables TableID=\"526\" TableName=\"Dictionary\" VersionID=\"982\" Active=\"1\" DefinitionOnly=\"1\" CreatedDate=\"2017-09-27T11:48:33.720\" EffectiveDate=\"2017-09-27T00:00:00\" LastUpdatedBy=\"WCampbell\" LastUpdatedDate=\"2017-09-27T13:12:21.207\"><SchemaDef><!-- edited with XMLSpy v2008 (http://www.altova.com) by Bradford Loewy (VERIFONE) --><!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) --><xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" targetNamespace=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" elementFormDefault=\"qualified\" attributeFormDefault=\"unqualified\"><xs:simpleType name=\"RegExType\"><xs:annotation><xs:documentation>Regular Expression</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\" /></xs:simpleType><xs:simpleType name=\"VersionType\"><xs:annotation><xs:documentation>Table Version</xs:documentation></xs:annotation><xs:restriction base=\"xs:decimal\" /></xs:simpleType><xs:simpleType name=\"DigitIndexOneType\"><xs:annotation><xs:documentation>Index of length 1 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"DigitIndexTwoType\"><xs:annotation><xs:documentation>Index of length 2 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d{2}\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"StringIndexThreeType\"><xs:annotation><xs:documentation>Index of length 3 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"StringIndexFourType\"><xs:annotation><xs:documentation>Index of length 4 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"DigitIndexSixType\"><xs:annotation><xs:documentation>Index of length 6 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d{6}\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"DigitIndexEightType\"><xs:annotation><xs:documentation>Index of length 8 and type string</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d{8}\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ActionCodeType\"><xs:annotation><xs:documentation>Action Code</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"CardHandlingRoutineIndType\"><xs:annotation><xs:documentation>Card Handling Routine Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ActionCodehandlerIndType\"><xs:annotation><xs:documentation>Card Handling Routine Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"FeeIndicatorType\"><xs:annotation><xs:documentation>Fee Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:integer\" /></xs:simpleType><xs:simpleType name=\"FeeIndexType\"><xs:annotation><xs:documentation>Fee Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"FEPIndicatorType\"><xs:annotation><xs:documentation>FEP Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"FEPNameType\"><xs:annotation><xs:documentation>FEP Name</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"FEPTypeType\"><xs:annotation><xs:documentation>FEP Type</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:enumeration value=\"Payment\" /><xs:enumeration value=\"Prepaid\" /><xs:enumeration value=\"Loyalty\" /><xs:enumeration value=\"EBT\" /><xs:enumeration value=\"Other\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"FieldUsageIdType\"><xs:annotation><xs:documentation>Field Usage Identifier</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:minLength value=\"0\" /><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"LCIType\"><xs:annotation><xs:documentation>Logic Control Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"LimitIndexType\"><xs:annotation><xs:documentation>Limit Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"LogicIndicatorType\"><xs:annotation><xs:documentation>Logic Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"MaskingTableIndexType\"><xs:annotation><xs:documentation>Masking Table Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"PriceTierType\"><xs:restriction base=\"xs:string\"><xs:enumeration value=\"cash\" /><xs:enumeration value=\"credit\" /><xs:enumeration value=\"debit\" /><xs:enumeration value=\"storedValue\" /><xs:enumeration value=\"fleet\" /><xs:enumeration value=\"proprietary\" /><xs:enumeration value=\"loyalty\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ProductIndType\"><xs:annotation><xs:documentation>Product Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"PromptIdType\"><xs:annotation><xs:documentation>Prompt ID</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"PromptIndicatorType\"><xs:annotation><xs:documentation>Prompt Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ActionCodeHandlerIdType\"><xs:restriction base=\"xs:string\"><xs:minLength value=\"1\" /><xs:maxLength value=\"20\" /><xs:pattern value=\"\\d*\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ActionCodeHandlerIndicatorType\"><xs:annotation><xs:documentation>Action Code Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ReceiptIndicatorType\"><xs:annotation><xs:documentation>Receipt Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ValidationIndicatorType\"><xs:annotation><xs:documentation>Validation Indicator</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ValidationIndexType\"><xs:annotation><xs:documentation>Validation ID</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ReceiptIndexType\"><xs:annotation><xs:documentation>Receipt Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"20\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"VelocityGrpIndType\"><xs:annotation><xs:documentation>Velocity Group Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"NACSProductCodeType\"><xs:annotation><xs:documentation>NACS Product Code</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"IPAddressType\"><xs:annotation><xs:documentation>IP Address formatted xxx.xxx.xxx.xxx</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d{1,3}.\\d{1,3}.\\d{1,3}.\\d{1,3}\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"SerialAddressType\"><xs:annotation><xs:documentation>Serial Address</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\" /></xs:simpleType><xs:simpleType name=\"TCPPort\"><xs:annotation><xs:documentation>IP Port</xs:documentation></xs:annotation><xs:restriction base=\"xs:integer\"><xs:minInclusive value=\"1\" /><xs:maxInclusive value=\"65535\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"SerialPort\"><xs:annotation><xs:documentation>Serial communication port that the hardware will use.</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:enumeration value=\"COM0\" /><xs:enumeration value=\"COM1\" /><xs:enumeration value=\"COM2\" /><xs:enumeration value=\"COM3\" /><xs:enumeration value=\"COM4\" /><xs:enumeration value=\"COM5\" /><xs:enumeration value=\"COM6\" /><xs:enumeration value=\"TTYS1\" /><xs:enumeration value=\"PortA1-1\" /><xs:enumeration value=\"PortA1-2\" /><xs:enumeration value=\"PortA1-3\" /><xs:enumeration value=\"PortA1-4\" /><xs:enumeration value=\"PortA1-5\" /><xs:enumeration value=\"PortA1-6\" /><xs:enumeration value=\"PortA1-7\" /><xs:enumeration value=\"PortA1-8\" /><xs:enumeration value=\"PortA2-1\" /><xs:enumeration value=\"PortA2-2\" /><xs:enumeration value=\"PortA2-3\" /><xs:enumeration value=\"PortA2-4\" /><xs:enumeration value=\"PortA2-5\" /><xs:enumeration value=\"PortA2-6\" /><xs:enumeration value=\"PortA2-7\" /><xs:enumeration value=\"PortA2-8\" /><xs:enumeration value=\"PortA3-1\" /><xs:enumeration value=\"PortA3-2\" /><xs:enumeration value=\"PortA3-3\" /><xs:enumeration value=\"PortA3-4\" /><xs:enumeration value=\"PortA3-5\" /><xs:enumeration value=\"PortA3-6\" /><xs:enumeration value=\"PortA3-7\" /><xs:enumeration value=\"PortA3-8\" /></xs:restriction></xs:simpleType><xs:attributeGroup name=\"ViperPort\"><xs:annotation><xs:documentation>Optional port Channel and Codec packages</xs:documentation></xs:annotation><xs:attribute name=\"ChannelClassName\" use=\"optional\" default=\"com.verifone.isd.viper.eps.pres.channels.ip.IpChannel\"><xs:simpleType><xs:restriction base=\"xs:string\"><xs:enumeration value=\"com.verifone.isd.viper.eps.pres.channels.ip.IpChannel\" /><xs:enumeration value=\"com.verifone.isd.viper.eps.pres.channels.serial.SerialChannel\" /></xs:restriction></xs:simpleType></xs:attribute><xs:attribute name=\"CodecClassName\" use=\"optional\" default=\"com.verifone.isd.viper.eps.pres.codecs.IFSF.IFSFCodec\"><xs:simpleType><xs:restriction base=\"xs:string\"><xs:enumeration value=\"com.verifone.isd.viper.eps.pres.codecs.IFSF.IFSFCodec\" /><xs:enumeration value=\"com.verifone.isd.viper.eps.pres.codecs.serial.SerialCodec\" /></xs:restriction></xs:simpleType></xs:attribute></xs:attributeGroup><xs:attributeGroup name=\"ViperPOPPort\"><xs:annotation><xs:documentation>Optional port Channel and Codec packages</xs:documentation></xs:annotation><xs:attribute name=\"ChannelClassName\" type=\"xs:string\" use=\"optional\" default=\"com.verifone.isd.viper.eps.pres.channels.ip.IpChannel\" /><xs:attribute name=\"CodecClassName\" type=\"xs:string\" use=\"optional\" default=\"com.verifone.isd.viper.eps.pres.codecs.pop.PopCodec\" /></xs:attributeGroup><xs:complexType name=\"ViperTCPPort\"><xs:annotation><xs:documentation>Port plus optional Channel and Codec packages</xs:documentation></xs:annotation><xs:simpleContent><xs:extension base=\"TCPPort\"><xs:attributeGroup ref=\"ViperPort\" /></xs:extension></xs:simpleContent></xs:complexType><xs:complexType name=\"ViperPOPTCPPort\"><xs:annotation><xs:documentation>Port plus optional Channel and Codec packages</xs:documentation></xs:annotation><xs:simpleContent><xs:extension base=\"TCPPort\"><xs:attributeGroup ref=\"ViperPOPPort\" /></xs:extension></xs:simpleContent></xs:complexType><xs:complexType name=\"ViperSerialPort\"><xs:annotation><xs:documentation>Port plus optional Channel and Codec packages (not functional for Serial)</xs:documentation></xs:annotation><xs:simpleContent><xs:extension base=\"SerialPort\"><xs:attributeGroup ref=\"ViperPort\" /></xs:extension></xs:simpleContent></xs:complexType><xs:complexType name=\"ViperPOPSerialPort\"><xs:annotation><xs:documentation>Port plus optional Channel and Codec packages (not functional for Serial)</xs:documentation></xs:annotation><xs:simpleContent><xs:extension base=\"SerialPort\"><xs:attributeGroup ref=\"ViperPOPPort\" /></xs:extension></xs:simpleContent></xs:complexType><xs:simpleType name=\"TrackMaskTableIndexType\"><xs:annotation><xs:documentation>Track Masking Table Index</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:pattern value=\"\\d\\d\\d\\d\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ICCParamsIdType\"><xs:annotation><xs:documentation>ICCParams ID</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ICCParamType\"><xs:annotation><xs:documentation>Parameters used to exchange information with an ICC capable device. Parameters in this list apply for ICC card processing but are not defined by a standard EMV tag\t</xs:documentation></xs:annotation><xs:restriction base=\"xs:string\"><xs:enumeration value=\"AIDName\" /><xs:enumeration value=\"CAPKChecksum\" /><xs:enumeration value=\"CAPKExpDate\" /><xs:enumeration value=\"CAPKExponent\" /><xs:enumeration value=\"CAPKModulus\" /><xs:enumeration value=\"CtlsCVMLimit\" /><xs:enumeration value=\"CtlsTransactionLimit\" /><xs:enumeration value=\"CtlsTransactionScheme\" /><xs:enumeration value=\"CtlsVisaTTQ\" /><xs:enumeration value=\"DDOLDefault\" /><xs:enumeration value=\"EncipheredPINBlock\" /><xs:enumeration value=\"EnableICC\" /><xs:enumeration value=\"EnableRemoteAIDSelection\" /><xs:enumeration value=\"HashAlgorithmIndicator\" /><xs:enumeration value=\"HostDecision\" /><xs:enumeration value=\"LanguageCodeDefault\" /><xs:enumeration value=\"MaxRandomSelectionPercent\" /><xs:enumeration value=\"MerchantDecision\" /><xs:enumeration value=\"PinBypassFlag\" /><xs:enumeration value=\"PinEntryTimeOut\" /><xs:enumeration value=\"RandomSelectionThreshold\" /><xs:enumeration value=\"RID\" /><xs:enumeration value=\"RIDName\" /><xs:enumeration value=\"ScriptResult\" /><xs:enumeration value=\"SignAlgorithmIndicator\" /><xs:enumeration value=\"TACDefault\" /><xs:enumeration value=\"TACDenial\" /><xs:enumeration value=\"TACOnline\" /><xs:enumeration value=\"TargetRandomSelectionPercent\" /><xs:enumeration value=\"TDOLDefault\" /><xs:enumeration value=\"TerminalApplicationVersionNumber2\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"PromptRoutineType\"><xs:annotation><xs:documentation>Prompt Validation Routine index</xs:documentation></xs:annotation><xs:restriction base=\"xs:integer\"><xs:minInclusive value=\"0\" /><xs:maxInclusive value=\"999\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"ModeTypes\"><xs:restriction base=\"xs:string\"><xs:enumeration value=\"Contact\" /><xs:enumeration value=\"Contactless\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"AIDType\"><xs:restriction base=\"xs:string\"><xs:minLength value=\"10\" /><xs:maxLength value=\"32\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"UsageFlag\"><xs:restriction base=\"xs:string\"><xs:enumeration value=\"0\" /><xs:enumeration value=\"1\" /><xs:enumeration value=\"2\" /><xs:enumeration value=\"3\" /><xs:enumeration value=\"4\" /><xs:enumeration value=\"5\" /><xs:enumeration value=\"6\" /><xs:enumeration value=\"7\" /><xs:enumeration value=\"8\" /><xs:enumeration value=\"9\" /><xs:enumeration value=\"10\" /><xs:enumeration value=\"99\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"CAPKIdType\"><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"PinBypassType\"><xs:restriction base=\"xs:string\"><xs:enumeration value=\"0\" /><xs:enumeration value=\"1\" /></xs:restriction></xs:simpleType><xs:simpleType name=\"RuleIdType\"><xs:restriction base=\"xs:string\"><xs:maxLength value=\"25\" /></xs:restriction></xs:simpleType></xs:schema></SchemaDef><DefaultXML /></_x0023_tbEPSPetroTables>", "newValue": null, "userName": "WCampbell", "auditDate": "2017-09-27T13:12:21.21", "tableID": null, "tableName": null }, { "auditId": 602, "versionId": 982, "actionType": "Insert", "entityName": "Mapping", "previousValue": null, "newValue": null, "userName": "WCampbell", "auditDate": "2017-09-27T13:12:21.21", "tableID": null, "tableName": null }, { "auditId": 600, "versionId": 982, "actionType": "Insert", "entityName": "Software Version", "previousValue": "BaseVersionID:950", "newValue": null, "userName": "WCampbell", "auditDate": "2017-09-27T13:12:21.203", "tableID": null, "tableName": null }];
const version: commanderbaseversion = {
    versionDescription: 'testVersion',
    versionID: 982,  
    createdByUser: 'testUser',
    createdDate:'2017-10-10'
};
export class MockService extends AuditServcie {

    getAudit(versionID: number,startDate: string, endDate: string) {
        return Observable.of(mockdata);
    }
}

describe('EPSLogComponent Component', () => {
    let fixture: ComponentFixture<AuditComponent>,
        comp: AuditComponent,
        debugElement: DebugElement,
        element: HTMLElement;

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpModule, BrowserAnimationsModule, ReactiveFormsModule, NoopAnimationsModule,
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, CalendarModule, GrowlModule],
            declarations: [AuditComponent, LoadingComponent, CommanderVersionComponent,PetroTableComponent, PetroMappingComponent, XmlPipe],
            providers: [AuditComponent, DateServcie]
        });

        TestBed.overrideComponent(
            AuditComponent,
            { set: { providers: [{ provide: AuditServcie, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AuditComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('submit button should be disabled if date range is greater than 30 days', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.EndDate = new Date('10/06/2017');
        comp.StartDate = new Date('09/06/2017');
        fixture.detectChanges();

        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBeTruthy();

    }));

    it('submit button should be enabled if date range is less than 30 days', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.EndDate = new Date('10/06/2017');
        comp.StartDate = new Date('09/07/2017');
        fixture.detectChanges();

        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBeFalsy();

    }));

    it('should display last 30 days audit when clicking submit button', async(() => {
        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        comp.ngOnInit();
        fixture.detectChanges();
        comp.version = version;
        comp.StartDate = new Date();
        comp.EndDate = new Date();
        fixture.detectChanges();
        bSubmit.click();
        fixture.detectChanges();    

        expect(comp.dataSource.length).toBe(3);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length).toBe(3);
        expect(rows[0].nativeElement.textContent.indexOf('Tables/Schemas') > 0).toBe(true);

      
    }));
});



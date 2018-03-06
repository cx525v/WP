import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { DebugElement, SimpleChanges, SimpleChange, Input } from '@angular/core';

import { TEST_BASESERVER } from '../../../../shared/spec.global.component';
import '../../../../app.module.client';
import { PetroTableComponent } from './petroTable.component';
import { Audit } from '../../../../models/petro/audit.model';
import { PetroTable } from '../../../../models/petro/petroTable.model';
import { AuditServcie } from '../../../../services/petro/audit.service';
import { DateServcie } from '../../../../services/petro/date.service';
import { XmlPipe } from '../../../../pipes/XmlPipe';
const auditUpdate: Audit = {
    auditId: 3084,
    previousValue: 'oldValue',
    newValue: 'newValue',
    actionType: 'Update',
    entityName: '',
    versionId: 982,
    userName: 'testUser',
    auditDate: '2017-10-10',
    tableID: 200,
    tableName: 'testTable'
};

const auditInsert: Audit = {
    auditId: 3084,
    previousValue: '',
    newValue: 'mappingNewValue',
    actionType: 'Insert',
    entityName: '',
    versionId: 982,
    userName: 'testUser',
    auditDate: '2017-10-10',
    tableID: 200,
    tableName: 'testTable'
};

const oldTable = { "tableID": 1370, "tableName": "LimitTable", "versionID": 0, "active": false, "definitionOnly": false, "schemaDef": "Number of fuel products allowed in a transaction using the card. The default value is 0 which means no restriction for fuel product.Number of products allowed in a transaction using the card. The default value is 0 which means no restriction for product.If the transaction amount is less than or equal to this limit, no signature line will be printed on the receipt.", "defaultXML": "0100000001007599001000002000000010100990010000030000009000909900100000400000075007599001000005000000010150990010000060000025002509900100000700000050005099001000008000000000000990010000090000005000509900100001000000001015099001000011000000010150990010000120000007500759900100001300000100010099001000014000000500050990010000150000000103009900100001600000075007500001000017000001000500000011800000100050000001190000000103009900100002000000001015099001000021000000010100990010000", "createdDate": null, "effectiveDate": "2017-10-09T00:00:00", "lastUpdatedBy": null, "lastUpdatedDate": null };
const newTable = { "tableID": 1370, "tableName": "LimitTable", "versionID": 0, "active": true, "definitionOnly": false, "schemaDef": "Number of fuel products allowed in a transaction using the card. The default value is 0 which means no restriction for fuel product.Number of products allowed in a transaction using the card. The default value is 0 which means no restriction for product.If the transaction amount is less than or equal to this limit, no signature line will be printed on the receipt.", "defaultXML": "0100000001007599001000002000000010100990010000030000009000909900100000400000075007599001000005000000010150990010000060000025002509900100000700000050005099001000008000000000000990010000090000005000509900100001000000001015099001000011000000010150990010000120000007500759900100001300000100010099001000014000000500050990010000150000000103009900100001600000075007500001000017000001000500000011800000100050000001190000000103009900100002000000001015099001000021000000010100990010000", "createdDate": null, "effectiveDate": "2017-10-09T00:00:00", "lastUpdatedBy": null, "lastUpdatedDate": null };
const tables = [{ "tableID": 1549, "tableName": "LimitTable", "versionID": 0, "active": true, "definitionOnly": false, "schemaDef": "\r\n  <!-- edited with XMLSpy v2008 sp1 (http://www.altova.com) by Himanshu Yadav (VERIFONE) -->\r\n  <!-- edited with XMLSPY v2004 rel. 3 U (http://www.xmlspy.com) by Bradford Loewy (VeriFone, Inc.) -->\r\n  <xs:schema xmlns:tt=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns=\"http://www.verifone.com/eps/viper/namespace/v1\" xmlns:this=\"http://www.verifone.com/eps/viper/namespace/v1\" targetNamespace=\"http://www.verifone.com/eps/viper/namespace/v1\" elementFormDefault=\"qualified\" attributeFormDefault=\"unqualified\">\r\n    <xs:import namespace=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" schemaLocation=\"Dictionary.xsd\" />\r\n    <xs:element name=\"LimitTable\">\r\n      <xs:complexType>\r\n        <xs:sequence minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n          <xs:element name=\"LimitRow\" type=\"LimitRowType\" />\r\n        </xs:sequence>\r\n        <xs:attribute name=\"version\" type=\"tt:VersionType\" use=\"required\" />\r\n      </xs:complexType>\r\n      <xs:key name=\"LimitPK\">\r\n        <xs:selector xpath=\"this:LimitRow\" />\r\n        <xs:field xpath=\"this:LimitTableIndex\" />\r\n      </xs:key>\r\n    </xs:element>\r\n    <xs:simpleType name=\"StringFourType\">\r\n      <xs:restriction base=\"xs:string\">\r\n        <xs:pattern value=\"\\d\\d\\d\\d\" />\r\n      </xs:restriction>\r\n    </xs:simpleType>\r\n    <xs:complexType name=\"LimitRowType\">\r\n      <xs:sequence>\r\n        <xs:element name=\"LimitTableIndex\" type=\"tt:LimitIndexType\" />\r\n        <xs:choice>\r\n          <xs:element name=\"OfflineFloorLimit\" type=\"StringFourType\" />\r\n          <xs:sequence>\r\n            <xs:element name=\"InsideOfflineFloorLimit\" type=\"StringFourType\" />\r\n            <xs:element name=\"OutsideOfflineFloorLimit\" type=\"StringFourType\" />\r\n          </xs:sequence>\r\n        </xs:choice>\r\n        <xs:choice>\r\n          <xs:element name=\"DispenserPreAuth\" type=\"StringFourType\" />\r\n          <xs:sequence>\r\n            <xs:element name=\"InsideDispenserPreAuth\" type=\"StringFourType\" />\r\n            <xs:element name=\"OutsideDispenserPreAuth\" type=\"StringFourType\" />\r\n          </xs:sequence>\r\n        </xs:choice>\r\n        <xs:choice>\r\n          <xs:element name=\"DispenserFuelingLimit\" type=\"StringFourType\" />\r\n          <xs:sequence>\r\n            <xs:element name=\"InsideDispenserFuelingLimit\" type=\"StringFourType\" />\r\n            <xs:element name=\"OutsideDispenserFuelingLimit\" type=\"StringFourType\" />\r\n          </xs:sequence>\r\n        </xs:choice>\r\n        <xs:element name=\"VelocityTrigger\" type=\"tt:VelocityGrpIndType\" />\r\n        <xs:element name=\"VelocityTriggerActionCode\" type=\"tt:ActionCodeType\" />\r\n        <xs:element name=\"PaymentSelectionThreshold\" type=\"StringFourType\" default=\"0000\" minOccurs=\"0\" />\r\n        <xs:element name=\"MaxFuelProductCount\" type=\"StringFourType\" default=\"0000\" minOccurs=\"0\">\r\n          <xs:annotation>\r\n            <xs:documentation>Number of fuel products allowed in a transaction using the card. The default value is 0 which means no restriction for fuel product.</xs:documentation>\r\n          </xs:annotation>\r\n        </xs:element>\r\n        <xs:element name=\"MaxProductCount\" type=\"StringFourType\" default=\"0000\" minOccurs=\"0\">\r\n          <xs:annotation>\r\n            <xs:documentation>Number of products allowed in a transaction using the card. The default value is 0 which means no restriction for product.</xs:documentation>\r\n          </xs:annotation>\r\n        </xs:element>\r\n        <xs:element name=\"SmallTicketLimit\" type=\"StringFourType\" default=\"0000\" minOccurs=\"0\">\r\n          <xs:annotation>\r\n            <xs:documentation>If the transaction amount is less than or equal to this limit, no signature line will be printed on the receipt.</xs:documentation>\r\n          </xs:annotation>\r\n        </xs:element>\r\n        <xs:element name=\"OutsideOfflineFloorCount\" type=\"StringFourType\" maxOccurs=\"1\" minOccurs=\"0\" />\r\n        <xs:element name=\"InsideOfflineFloorCount\" type=\"StringFourType\" maxOccurs=\"1\" minOccurs=\"0\" />\r\n        <xs:element name=\"CashBackLimit\" type=\"StringFourType\" maxOccurs=\"1\" minOccurs=\"0\" />\r\n        <xs:element name=\"MerchandiseLimit\" type=\"StringFourType\" maxOccurs=\"1\" minOccurs=\"0\" />\r\n        <xs:element name=\"ZipCodeOverLimit\" type=\"StringFourType\" maxOccurs=\"1\" minOccurs=\"0\" />\r\n      </xs:sequence>\r\n    </xs:complexType>\r\n  </xs:schema>\r\n", "defaultXML": "\r\n  <!-- Created by the Viper Toolkit Application -->\r\n  <LimitTable xmlns=\"http://www.verifone.com/eps/viper/namespace/v1\" xmlns:tt=\"http://www.verifone.com/eps/viper/namespace/Dictionary/v1\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" version=\"0\" xsi:schemaLocation=\"http://www.verifone.com/eps/viper/namespace/v1 ../schemas/tables/LimitTable.xsd\">\r\n    <LimitRow>\r\n      <!-- 01 - VISA -->\r\n      <LimitTableIndex>01</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0075</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 02 - MASTERCARD -->\r\n      <LimitTableIndex>02</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0100</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 03 - AMEX -->\r\n      <LimitTableIndex>03</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0090</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0090</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 04 - DISCOVER -->\r\n      <LimitTableIndex>04</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0075</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0075</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 05 - WEX -->\r\n      <LimitTableIndex>05</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0150</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 06 - FLEET ONE -->\r\n      <LimitTableIndex>06</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0250</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0250</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 07 - DEBIT -->\r\n      <LimitTableIndex>07</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0050</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0050</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 08 - FOOD STAMP -->\r\n      <LimitTableIndex>08</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0000</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0000</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 09 - EBT -->\r\n      <LimitTableIndex>09</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0050</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0050</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 10 - VISA FLEET -->\r\n      <LimitTableIndex>10</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0150</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 11 - MASTERCARD FLEET -->\r\n      <LimitTableIndex>11</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0150</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 12 - VOYAGER -->\r\n      <LimitTableIndex>12</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0075</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0075</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 13 - STORED VALUE CARD -->\r\n      <LimitTableIndex>13</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0100</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0100</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 14 - FLEET COR -->\r\n      <LimitTableIndex>14</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0050</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0050</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 15 - MFA PREFERRED-->\r\n      <LimitTableIndex>15</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0300</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 16 - GIFT CARD-->\r\n      <LimitTableIndex>16</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0075</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0075</DispenserFuelingLimit>\r\n      <VelocityTrigger>00</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 17 - RESERVED -->\r\n      <LimitTableIndex>17</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0100</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0500</DispenserFuelingLimit>\r\n      <VelocityTrigger>00</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 18 - RESERVED -->\r\n      <LimitTableIndex>18</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0100</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0500</DispenserFuelingLimit>\r\n      <VelocityTrigger>00</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 19 - FUEL LYNK -->\r\n      <LimitTableIndex>19</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0300</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 20 - ALON FLEET-->\r\n      <LimitTableIndex>20</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0150</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n    <LimitRow>\r\n      <!-- 21 - ALON -->\r\n      <LimitTableIndex>21</LimitTableIndex>\r\n      <OfflineFloorLimit>0000</OfflineFloorLimit>\r\n      <DispenserPreAuth>0001</DispenserPreAuth>\r\n      <DispenserFuelingLimit>0100</DispenserFuelingLimit>\r\n      <VelocityTrigger>99</VelocityTrigger>\r\n      <VelocityTriggerActionCode>001</VelocityTriggerActionCode>\r\n      <SmallTicketLimit>0000</SmallTicketLimit>\r\n    </LimitRow>\r\n  </LimitTable>\r\n", "createdDate": null, "effectiveDate": "2017-10-11T00:00:00", "lastUpdatedBy": null, "lastUpdatedDate": null }];
export class MockService extends AuditServcie {

    getPetroTable(value: string) {      
        if (value == 'oldValue') {
            return Observable.of(oldTable);
        }
        else {
            return Observable.of(newTable);
        }
    }

    getPetroTables(value: string) {
        return Observable.of(tables);
    }
}

describe('PetroTableComponent Component', () => {
    let fixture: ComponentFixture<PetroTableComponent>,
        comp: PetroTableComponent,
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
            declarations: [PetroTableComponent, XmlPipe],
            providers: [PetroTableComponent, AuditServcie, DateServcie]
        });

        TestBed.overrideComponent(
            PetroTableComponent,
            { set: { providers: [{ provide: AuditServcie, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PetroTableComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));


    it('should show update template', async(() => {
        comp.audit = auditUpdate;
        comp.ngOnChanges({
            audit: new SimpleChange(null, comp.audit, false)
        });
        fixture.detectChanges();

        expect(comp.IsUpdate).toBeTruthy();

        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));
        expect(divTableCell[4].nativeElement.textContent).toEqual('false');
        expect(divTableCell[5].nativeElement.textContent).toEqual('true');
      
    }));

    it('should show insert template', async(() => {
        comp.audit = auditInsert;
        comp.ngOnChanges({
            audit: new SimpleChange(null, comp.audit, false)
        });
        fixture.detectChanges();
        expect(comp.IsUpdate).toBeFalsy();
        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));

        expect(divTableCell[6].nativeElement.textContent).toEqual('LimitTable');
        expect(divTableCell[7].nativeElement.textContent).toEqual('true');
        expect(divTableCell[8].nativeElement.textContent).toEqual('false');
        expect(divTableCell[11].nativeElement.textContent).toEqual('2017-10-11');
    }));

});



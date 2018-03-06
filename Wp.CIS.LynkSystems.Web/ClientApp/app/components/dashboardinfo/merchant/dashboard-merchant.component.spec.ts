import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DebugElement, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { TerminalEquipmentComponent } from './terminal-equipment.component';
import { DashboardMerchantComponent } from './dashboard-merchant.component';
import { MemoComponent } from '../common/memo.component';
import { MemoListComponent } from '../common/memo-list.component';
import { MemoListItemComponent } from '../common/memo-list-item.component';
import { AccountInfoComponent } from '../common/account-info.component';
import { BusinessInfoComponent } from '../common/business-info.component';
import { ContactsComponent } from '../common/contacts.component';
import { CaseHistoryComponent } from '../common/case-history.component';
import { DashboardTitleComponent } from '../common/dashboard-title.component';
import { CustomerMerchantComponent } from '../common/customer-merchant.component';
import { TwoColumnPipe } from '../../../pipes/twocolumn.pipe';
import { LoadingComponent } from '../../epstableload/loading.component';
import { RecentStatementsComponent } from '../common/recent-statements.component';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { TerminalDetailsDialogComponent} from '../terminal/terminal-details-dialog.component';
import { TerminalDetailsComponent } from '../terminal/terminal-details.component';
import { TransactionsComponent } from '../terminal/transactions.component';
import { TransactionComponent } from '../terminal/transaction.component';
import { DateTimeFormatPipe} from '../../../pipes/date.pipe';
import { TerminalInfoService } from '../../../services/dashboardinfo/terminal.service';
import { TerminalEquipment } from './../../../models/dashboardInfo/terminalequipment.model';
import { YearMonthDatePipe } from '../../../pipes/dateymd.pipe';
import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';
import { BankingInfoComponent } from '../terminal/banking-info.component';

import {
    TabViewModule,
    DataTableModule,
    FileUploadModule,
    ButtonModule,
    TreeModule,
    DropdownModule,
    DialogModule,
    CheckboxModule,
    DataGridModule,
    ConfirmDialogModule,
    MessagesModule,
    AccordionModule,
  
} from 'primeng/primeng';
import {
    DataTable,
    ConfirmationService,
    ConfirmDialog,
    Message,
    Messages,
    Calendar,
    AccordionTab,
    Accordion,
    TabView,
    TabPanel,
    CalendarModule
} from 'primeng/primeng';
import { DashboardInfoService } from '../../../services/dashboardinfo.service';
import { RecentStatement } from './../../../models/dashboardInfo/recentstatement.model';

const mockdata = { "custProfile": { "customerID": 1, "description": "Premier Camera", "activationDt": "2001-11-29T16:35:08", "statusIndicator": 7, "legalType": 1, "businessEstablishedDate": "101998", "lynkAdvantageDate": "2001-02-22T00:00:00", "customerNbr": "1000000001", "classCode": 0, "sensitivityLevel": 0, "stmtTollFreeNumber": null, "deactivationDt": "2016-11-21T07:23:20", "legalDesc": "Proprietor", "senseLvlDesc": "Default", "statDesc": "Deactivated", "lynkAdvantage": 1, "pinPadPlus": 0, "giftLynk": 1, "rewardsLynk": 0, "demoID": 1, "custName": "Premier Camera                                    ", "custContact": "Test", "prinID": 1, "prinName": "Premier Camera                                    ", "prinAddress": "1625 E. County Line Rd. Ste. 2                    ", "prinCity": "JACKSON", "prinState": "MS", "prinZipcode": "39211", "prinSSN": null, "custFederalTaxID": "000000001", "irsVerificationStatus": 0, "propHasEmployees": 0 }, "actvServices": { "lidType": 2, "lid": 570343, "billingMethodType": 0, "billMtdDesc": null, "chkName": null, "giftLynk_ON": true, "rewardsLynk_ON": true, "lastProcessingDt": "2007-08-26T00:00:00", "amex_ON": true, "discover_ON": true, "discover_CT21_ON": false, "diner_ON": false, "jcB_ON": false, "openCase": 0, "terminalRental_ON": false, "printerRental_ON": false, "pinPadRental_ON": false, "softDesc": null, "creditST_ON": true, "debitST_ON": false, "checkST_ON": false, "achsT_ON": false, "lynkAdvantage_ON": false, "externalTermID": null, "authProcessorDesc": null, "sicDesc": "5812 RESTAURANTS", "laDesc": "", "activeServiesDesc": "Credit (Amex, Discover NS, Gift/Loyalty(Gift Card, Loyalty Card)" }, "groupInfo": { "groupID": 114972, "groupName": "Retail - Tier 1 / Tier 2 ", "groupType": "National Sales Vertical Market", "statusIndicator": "Group Activated" }, "termProfile": null, "termInfo": null, "merchInfo": { "merchantId": 570343, "customerID": 393727, "activationDt": "2005-08-17T17:33:59", "sicCode": 5812, "industryType": 2, "merchantNbr": "542929802109199", "acquiringBankId": 13, "programType": 40, "statusIndicator": 6, "fnsNbr": "0", "benefitType": 0, "riskLevelID": 1, "merchantType": 1, "internetURL": null, "deactivationDt": "0001-01-01T00:00:00", "incrementalDt": "2005-09-19T00:00:00", "thresholdDt": "0001-01-01T00:00:00", "brandID": 1, "sicDesc": "RESTAURANTS", "merchantClass": "A", "riskLevel": "1", "statDesc": "New Account", "indTypeDesc": "Restaurant", "mchName": "Golden Corral 919                                 ", "mchAddress": "2701 Coors Blvd NW                                ", "mchCity": "Albuquerque", "mchState": "NM", "mchZipCode": "87120", "mchPhone": "5058314607", "mchContact": "Store Manager", "acquiringBank": "Citizens Trust Tier 1 Tier 2", "benefitTypeDesc": "None", "merchFedTaxID": "561005071" }, "demographicsInfo": [{ "level": "Customer", "addressTypeID": 25, "nameAddressID": 3302205, "addressType": "National: Systems", "name": "Ann Perez                                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198814612", "contact": "Ann Perez                                         ", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 23, "nameAddressID": 3302203, "addressType": "National: Finance", "name": "Dale Whitworth                                    ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198815228", "contact": "Dale Whitworth                                    ", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 11, "nameAddressID": 3301636, "addressType": "Customer", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 68, "nameAddressID": 6736478, "addressType": "IRS 1099", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 24, "nameAddressID": 3302204, "addressType": "National: Operations", "name": "Terri Warren                                      ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198815185", "contact": "Terri Warren                                      ", "email": null, "title": "Operations Manager", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 17, "nameAddressID": 3302202, "addressType": "Principal", "name": "Theodore Fowler", "address": "5151 Glenwood Ave", "address2": null, "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": null, "county": "Wake", "phone": "9197819310", "fax": null, "contact": "Theodore Fowler", "email": null, "title": null, "dob": "1/1/1941 12:00:00 AM", "ssn": "561005071", "lastFour": "0507" }, { "level": "Merchant", "addressTypeID": 12, "nameAddressID": 3327447, "addressType": "Merchant Location", "name": "Golden Corral 919                                 ", "address": "2701 Coors Blvd NW                                ", "address2": "                                                  ", "city": "Albuquerque", "state": "NM", "zipCode": "87120", "zipCode4": "", "county": "Bernalillo          ", "phone": "5058314607", "fax": "", "contact": "Store Manager", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 4, "nameAddressID": 3327448, "addressType": "Shipping", "name": "Golden Corral 919                                 ", "address": "2701 Coors Blvd NW                                ", "address2": "                                                  ", "city": "Albuquerque", "state": "NM", "zipCode": "87120", "zipCode4": "", "county": "Bernalillo          ", "phone": "5058314607", "fax": "", "contact": "Store Manager", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 7, "nameAddressID": 3327450, "addressType": "ChargeBack - Credit Acquiring", "name": "Golden Corral Corporation", "address": "5151 Glenwood Ave", "address2": "", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "WAKE", "phone": "9198815169", "fax": "", "contact": "Loren Morgan", "email": null, "title": "", "dob": "1/1/1900 12:00:00 AM", "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 15, "nameAddressID": 3327451, "addressType": "Retrieval Requests - Credit A.", "name": "Golden Corral Corporation", "address": "5151 Glenwood Ave", "address2": "", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "WAKE", "phone": "9198815169", "fax": "", "contact": "Loren Morgan", "email": null, "title": "", "dob": "1/1/1900 12:00:00 AM", "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 68, "nameAddressID": 6258419, "addressType": "IRS 1099", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake                ", "phone": "9198815169", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 13, "nameAddressID": 3327449, "addressType": "Statement", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake                ", "phone": "9198815169", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }], "demographicsInfoCust": [{ "level": "Customer", "addressTypeID": 25, "nameAddressID": 3302205, "addressType": "National: Systems", "name": "Ann Perez                                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198814612", "contact": "Ann Perez                                         ", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 23, "nameAddressID": 3302203, "addressType": "National: Finance", "name": "Dale Whitworth                                    ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198815228", "contact": "Dale Whitworth                                    ", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 11, "nameAddressID": 3301636, "addressType": "Customer", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 68, "nameAddressID": 6736478, "addressType": "IRS 1099", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 24, "nameAddressID": 3302204, "addressType": "National: Operations", "name": "Terri Warren                                      ", "address": "5151 Glenwood Ave.                                ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake", "phone": "9197819310", "fax": "9198815185", "contact": "Terri Warren                                      ", "email": null, "title": "Operations Manager", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Customer", "addressTypeID": 17, "nameAddressID": 3302202, "addressType": "Principal", "name": "Theodore Fowler", "address": "5151 Glenwood Ave", "address2": null, "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": null, "county": "Wake", "phone": "9197819310", "fax": null, "contact": "Theodore Fowler", "email": null, "title": null, "dob": "1/1/1941 12:00:00 AM", "ssn": "561005071", "lastFour": "0507" }], "demographicsInfoMerch": [{ "level": "Merchant", "addressTypeID": 12, "nameAddressID": 3327447, "addressType": "Merchant Location", "name": "Golden Corral 919                                 ", "address": "2701 Coors Blvd NW                                ", "address2": "                                                  ", "city": "Albuquerque", "state": "NM", "zipCode": "87120", "zipCode4": "", "county": "Bernalillo          ", "phone": "5058314607", "fax": "", "contact": "Store Manager", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 4, "nameAddressID": 3327448, "addressType": "Shipping", "name": "Golden Corral 919                                 ", "address": "2701 Coors Blvd NW                                ", "address2": "                                                  ", "city": "Albuquerque", "state": "NM", "zipCode": "87120", "zipCode4": "", "county": "Bernalillo          ", "phone": "5058314607", "fax": "", "contact": "Store Manager", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 7, "nameAddressID": 3327450, "addressType": "ChargeBack - Credit Acquiring", "name": "Golden Corral Corporation", "address": "5151 Glenwood Ave", "address2": "", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "WAKE", "phone": "9198815169", "fax": "", "contact": "Loren Morgan", "email": null, "title": "", "dob": "1/1/1900 12:00:00 AM", "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 15, "nameAddressID": 3327451, "addressType": "Retrieval Requests - Credit A.", "name": "Golden Corral Corporation", "address": "5151 Glenwood Ave", "address2": "", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "WAKE", "phone": "9198815169", "fax": "", "contact": "Loren Morgan", "email": null, "title": "", "dob": "1/1/1900 12:00:00 AM", "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 68, "nameAddressID": 6258419, "addressType": "IRS 1099", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake                ", "phone": "9198815169", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }, { "level": "Merchant", "addressTypeID": 13, "nameAddressID": 3327449, "addressType": "Statement", "name": "Golden Corral Corporation                         ", "address": "5151 Glenwood Ave                                 ", "address2": "                                                  ", "city": "Raleigh", "state": "NC", "zipCode": "27612", "zipCode4": "", "county": "Wake                ", "phone": "9198815169", "fax": "", "contact": "Terri Warren", "email": null, "title": "", "dob": null, "ssn": "", "lastFour": "----" }], "demographicsInfoTerm": [], "merchantsList": [], "caseHistorysList": [{ "caseId": 7649983, "createDate": "2007-09-18T12:08:15.247", "caseDesc": null, "caseDescId": null, "caseLevel": "Merchant", "orgDeptName": "Customer Care", "terminalId": "        ", "merchantId": 570343, "merchantNbr": "542929802109199", "customerNbr": "1000393727", "merchantName": "Golden Corral Corporation                         ", "currDept": "", "referredFrom": "", "priorityId": 3, "closedDate": "2007-09-18T12:08:15", "rtnToOriginator": false, "hasAttachment": true, "hasCustomForm": false, "hasReminder": false, "parentCaseId": 0, "caseStatusId": 1, "hasEscalated": false, "isCaseOpen": false }, { "caseId": 7471681, "createDate": "2007-08-30T17:09:12.813", "caseDesc": null, "caseDescId": null, "caseLevel": "Merchant", "orgDeptName": "Customer Care", "terminalId": "        ", "merchantId": 570343, "merchantNbr": "542929802109199", "customerNbr": "1000393727", "merchantName": "Golden Corral Corporation                         ", "currDept": "", "referredFrom": null, "priorityId": 3, "closedDate": "2007-08-30T17:09:12", "rtnToOriginator": false, "hasAttachment": true, "hasCustomForm": false, "hasReminder": false, "parentCaseId": 0, "caseStatusId": 1, "hasEscalated": false, "isCaseOpen": false }] };
const recentStatements = [];
const terminalEquipments = [
    { "mid": '', "terminalNbr": 589532, "terminalID": "LK429221", "equipment": "XPNT", "software": "VAR Software", "deactivateActivateDate": "2005-08-17T17:34:06", "status": "Active" },
{ "mid": '', "terminalNbr": 589533, "terminalID": "LK429222", "equipment": "TEST", "software": "TEST Software", "deactivateActivateDate": "2005-09-17T17:34:06", "status": "Deactive" }
];
const mockDetail = { "debit": 0, "credit": 1, "checkSvc": 0, "pob": 0, "visaMC": 1, "discover": 1, "discCanx": 0, "diners": 1, "amex": 1, "amexCanx": 0, "revPip": 0, "ebt": 0, "tid": "LK351654", "autoSettleOverride": "0", "cutOffTime": "1600", "timeZone": "CST", "autoSettleIndicator": "True", "autoSettleTime": "2300", "terminalDescription": "Terminal - Omni 3750 " };
export class MockTerminalService extends TerminalInfoService {
    getTerminalInfoDetail(merchantNbr): any {
        return Observable.of(mockDetail);
    }

    public getTerminalEquipments(merchantId: number): Observable<any[]> {

        return Observable.of([]);
    }
}
export class MockService extends DashboardInfoService {

    getDashboardInfo(lidType: LidTypesEnum, lidIdPk: string): any {
        return Observable.of(mockdata);
    }

    getRecentStatements(merchantNbr: string): Observable<any[]> {
        return Observable.of(recentStatements);
    }

    getTerminalEquipments(merchantId: number): Observable<TerminalEquipment[]> {    
        return Observable.of(terminalEquipments);
    }
}
describe('Dashboard-merchant', () => {
    let fixture: ComponentFixture<DashboardMerchantComponent>,
        comp: DashboardMerchantComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, AccordionModule, TabViewModule,
                CalendarModule, RouterTestingModule],
            declarations: [
                DashboardMerchantComponent,
                TerminalEquipmentComponent,
                RecentStatementsComponent,
                MemoComponent,
                MemoListComponent,
                MemoListItemComponent,
                CaseHistoryComponent,
                CustomerMerchantComponent,
                ContactsComponent,
                BusinessInfoComponent,
                AccountInfoComponent,
                TwoColumnPipe,
                YearMonthDatePipe,
                DashboardTitleComponent,
                LoadingComponent,
                TerminalDetailsDialogComponent,
                TerminalDetailsComponent,
                DateTimeFormatPipe,
                BankingInfoComponent,
                TransactionsComponent,
                TransactionComponent
            ],
            providers: [
                {
                    provide: DashboardInfoService,
                    useClass: MockService
                },
                {
                    provide: TerminalInfoService,
                    useClass: MockTerminalService
                },
                DashboardEventsService
            ]
        });
        TestBed.overrideComponent(
            DashboardMerchantComponent,
            {
                set: {
                    providers:
                    [
                        { provide: DashboardInfoService, useClass: MockService },
                        { provide: TerminalInfoService, useClass: MockTerminalService }
                    ]
                }
            },

        );

        TestBed.overrideComponent(
            TerminalEquipmentComponent,
            {
                set: {
                    providers:
                    [
                        { provide: TerminalInfoService, useClass: MockTerminalService }
                    ]
                }
            });

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(DashboardMerchantComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(comp).toBeTruthy();
    });

    it('MemoComponent should create the app', async(() => {
        let fixture1 = TestBed.createComponent(MemoComponent);
        let app = fixture1.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('MemoListComponent should create the app', async(() => {
        let fixture2 = TestBed.createComponent(MemoListComponent);
        let app = fixture2.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('MemoListItemComponent should create the app', async(() => {
        let fixture3 = TestBed.createComponent(MemoListItemComponent);
        let app = fixture3.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('AccountInfoComponent should create the app', async(() => {
        let fixture4 = TestBed.createComponent(AccountInfoComponent);
        let app = fixture4.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('BusinessInfoComponent should create the app', async(() => {
        let fixture5 = TestBed.createComponent(BusinessInfoComponent);
        let app = fixture5.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

  
    it('ContactsComponent should create the app', async(() => {
        let fixture7 = TestBed.createComponent(ContactsComponent);
        let app = fixture7.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('CaseHistoryComponent should create the app', async(() => {
        let fixture8 = TestBed.createComponent(CaseHistoryComponent);
        let app = fixture8.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('CustomerMerchantComponent should create the app', async(() => {
        let fixture9 = TestBed.createComponent(CustomerMerchantComponent);
        let app = fixture9.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
   
    it('DashboardTitleComponent should create the app', async(() => {
        let fixture11 = TestBed.createComponent(DashboardTitleComponent);
        let app = fixture11.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('create an tow colomn Pipe instance', () => {
        let pipe = new TwoColumnPipe();
        expect(pipe).toBeTruthy();
    });

    
    it('should display accountInfo data', () => {
        comp.SearchParamsPk = { lidIdPk: 393727, lidType: 3, customerId: 4, merchantId: 5, terminalNbr: 6 };
        comp.ngOnInit();
      
        fixture.detectChanges();

        expect(comp.accountInfo.name).toEqual(mockdata.custProfile.senseLvlDesc + ' Account');
        expect(comp.accountInfo.prinAddress).toEqual(mockdata.merchInfo.mchAddress);
        expect(comp.accountInfo.prinCity).toEqual(mockdata.merchInfo.mchCity);
        expect(comp.accountInfo.prinState).toEqual(mockdata.merchInfo.mchState);
        expect(comp.accountInfo.prinZipcode).toEqual(mockdata.merchInfo.mchZipCode);

    });

   it('should display businessInfo data', () => {
        comp.SearchParamsPk = { lidIdPk: 393727, lidType: 3, customerId: 4, merchantId: 5, terminalNbr: 6 };
        comp.ngOnInit();       
        fixture.detectChanges();

        expect(comp.businessInfo.custFederalTaxID).toEqual(mockdata.merchInfo.merchFedTaxID);
        expect(comp.businessInfo.status).toEqual(mockdata.merchInfo.statDesc);

    });

  
    it('should display customer contacts data', () => {
        comp.SearchParamsPk = { lidIdPk: 393727, lidType: 3, customerId: 4, merchantId: 5, terminalNbr: 6 };
        comp.ngOnInit();
       
        fixture.detectChanges();
        expect(comp.customerContacts.length).toEqual(6);
    });

    it('should display merchant contacts data', () => {
        comp.SearchParamsPk = { lidIdPk: 393727, lidType: 3, customerId: 4, merchantId: 5, terminalNbr: 6 };
        comp.ngOnInit();

        fixture.detectChanges();
        expect(comp.merchantContacts.length).toEqual(6);
    });


    it('should display casehistory data', () => {
        comp.SearchParamsPk = { lidIdPk: 393727, lidType: 3, customerId: 4, merchantId: 5, terminalNbr: 6 };
        comp.ngOnInit();
       
        fixture.detectChanges();
        expect(comp.casehistory.length).toEqual(2);
    });

});



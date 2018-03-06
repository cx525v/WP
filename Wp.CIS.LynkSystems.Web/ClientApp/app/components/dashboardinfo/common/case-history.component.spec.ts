import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { Location } from '@angular/common';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DebugElement, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar } from 'primeng/primeng';
import { CaseHistoryComponent } from './case-history.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { CaseHistory } from './../../../models/dashboard.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'

import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';
import { CaseHistoryService } from '../../../services/dashboardinfo/case-history.service';
import { TerminalEquipment } from '../../../models/dashboardInfo/terminalequipment.model'
import { GenericPaginationResponse } from '../../../models/transactions/genericPaginationResponse.model';
import { CaseHistoryInputModel } from '../../../models/caseHistory/case-history-input.model';

const mockData: CaseHistory[] = [
    {
        caseDesc: 'caseDesc1',
        caseDescId:1,
        caseId:8865652,
        caseLevel:"Terminal",
        caseStatusId:1,
        closedDate:new Date("2008-07-28T10:54:51"),
        createDate:new Date("2008-07-28T10:54:26.837"),
        currDept:"",
        customerNbr:"1000393727",
        hasAttachment:false,
        hasCustomForm:false,
        hasEscalated:false,
        hasReminder:false,
        isCaseOpen:false,
        merchantId:570314,
        merchantName:"GOLDEN CORRAL 671",
        merchantNbr:"542929802106716",
        orgDeptName:"NACS",
        parentCaseId:0,
        priorityId:2,
        referredFrom:"Risk",
        rtnToOriginator:true,
        terminalId: "LK429472",
        deptName: 'dept1',
        description:'description1'
    },

    {
        caseDesc: 'caseDesc2',
        caseDescId: 2,
        caseId: 8865653,
        caseLevel: "Terminal",
        caseStatusId: 1,
        closedDate: new Date("2008-07-28T10:54:51"),
        createDate: new Date("2008-07-28T10:54:26.837"),
        currDept: "",
        customerNbr: "1000393727",
        hasAttachment: false,
        hasCustomForm: false,
        hasEscalated: false,
        hasReminder: false,
        isCaseOpen: false,
        merchantId: 570313,
        merchantName: "GOLDEN CORRAL 673",
        merchantNbr: "542929802106736",
        orgDeptName: "NACS",
        parentCaseId: 0,
        priorityId: 2,
        referredFrom: "Risk",
        rtnToOriginator: true,
        terminalId: "LK429472",
        deptName: 'dept2',
        description: 'description2'
    },

    {
        caseDesc: 'caseDesc3',
        caseDescId: 3,
        caseId: 8865653,
        caseLevel: "Terminal",
        caseStatusId: 1,
        closedDate: new Date("2008-07-28T10:54:51"),
        createDate: new Date("2008-07-28T10:54:26.837"),
        currDept: "",
        customerNbr: "1000393727",
        hasAttachment: true,
        hasCustomForm: false,
        hasEscalated: false,
        hasReminder: false,
        isCaseOpen: false,
        merchantId: 570313,
        merchantName: "GOLDEN CORRAL 677",
        merchantNbr: "542929802106736",
        orgDeptName: "NACS",
        parentCaseId: 0,
        priorityId: 2,
        referredFrom: "Risk",
        rtnToOriginator: true,
        terminalId: "LK429472",
        deptName: 'dept3',
        description: 'description3'
    }
];


class LocationMock {

}

class CaseHistoryServiceMock {

    public getPageOfCaseHistoryRecords(input: CaseHistoryInputModel): Observable<GenericPaginationResponse<CaseHistory>> {

        let caseHistoryRecs: Array<CaseHistory> = new Array<CaseHistory>();
        let response: GenericPaginationResponse<CaseHistory> = new GenericPaginationResponse<CaseHistory>(0, caseHistoryRecs);

        return Observable.of(response);
    }
}

describe('CaseHistoryComponent', () => {
    let fixture: ComponentFixture<CaseHistoryComponent>,
        comp: CaseHistoryComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, CalendarModule],
            declarations: [CaseHistoryComponent],
            providers: [
                DashboardEventsService
            ]
        });

        TestBed.overrideComponent(
            CaseHistoryComponent,
            { set: { providers: [{ provide: CaseHistoryService, useClass: CaseHistoryServiceMock }] } }
        );


    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CaseHistoryComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('should display contacts table', async(() => {

        comp.caseHistory = mockData;
        comp.TotalNumberOfCaseHistoryRecords = mockData.length;
        comp.LidValue = 393911;
        comp.LidType = 3;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length == 3).toBe(true);

    }));

    it('should display merchant location table row 1', async(() => {
        comp.ngOnInit();
        comp.caseHistory = mockData;
        comp.LidValue = 393911;
        comp.LidType = 3;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));

        const ico = cells[0].query(By.css('div'));
        var hasAttachment: boolean;
        if (!ico) {
            hasAttachment = false;
        } else {
            var text: string = ico.nativeElement.innerHTML;
            hasAttachment = text.indexOf('fa fa-paperclip') > 0;
        }

        let formattedDate = new Date(Date.parse(cells[3].nativeElement.textContent.trim()));

        expect(hasAttachment).toEqual(mockData[0].hasAttachment);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[0].caseLevel);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[0].caseId.toString());
        expect(formattedDate.getDate()).toBe(mockData[0].createDate.getDate());
        expect(formattedDate.getFullYear()).toBe(mockData[0].createDate.getFullYear());
        expect(formattedDate.getHours()).toBe(mockData[0].createDate.getHours());
        expect(formattedDate.getMinutes()).toBe(mockData[0].createDate.getMinutes());
        expect(cells[4].nativeElement.textContent).toEqual(mockData[0].caseDesc);
        expect(cells[5].nativeElement.textContent).toEqual(mockData[0].orgDeptName);

    }));

    it('should display merchant location table row 2', async(() => {
        comp.ngOnInit();
        comp.caseHistory = mockData;
        comp.TotalNumberOfCaseHistoryRecords = mockData.length;
        comp.LidValue = 393911;
        comp.LidType = 3;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[1].queryAll(By.css('td span.ui-cell-data'));

        const ico = cells[0].query(By.css('div'));
        var hasAttachment: boolean;
        if (!ico) {
            hasAttachment = false;
        } else {
            var text: string = ico.nativeElement.innerHTML;
            hasAttachment = text.indexOf('fa fa-paperclip') > 0;
        }

        let formattedDate = new Date(Date.parse(cells[3].nativeElement.textContent.trim()));

        expect(hasAttachment).toEqual(mockData[1].hasAttachment);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[1].caseLevel);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[1].caseId.toString());
        expect(formattedDate.getFullYear()).toBe(mockData[1].createDate.getFullYear());
        expect(formattedDate.getHours()).toBe(mockData[1].createDate.getHours());
        expect(formattedDate.getMinutes()).toBe(mockData[1].createDate.getMinutes());
        expect(cells[4].nativeElement.textContent).toEqual(mockData[1].caseDesc);
        expect(cells[5].nativeElement.textContent).toEqual(mockData[1].orgDeptName);


    }));

    it('should display merchant location table row 3', async(() => {
        comp.ngOnInit();
        comp.caseHistory = mockData;
        comp.LidValue = 393911;
        comp.LidType = 3;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[2].queryAll(By.css('td span.ui-cell-data'));
         
        const ico = cells[0].query(By.css('div'));
        var hasAttachment: boolean;
        if (!ico) {
            hasAttachment = false;
        } else {
            var text: string = ico.nativeElement.innerHTML;
            hasAttachment = text.indexOf('fa fa-paperclip') > 0;
        }

        let formattedDate = new Date(Date.parse(cells[3].nativeElement.textContent.trim()));

        expect(hasAttachment).toEqual(mockData[2].hasAttachment);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[2].caseLevel);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[2].caseId.toString());
        //expect(cells[3].nativeElement.textContent).toEqual(mockData[2].createDate.toString());
        expect(formattedDate.getFullYear()).toBe(mockData[2].createDate.getFullYear());
        expect(formattedDate.getHours()).toBe(mockData[2].createDate.getHours());
        expect(formattedDate.getMinutes()).toBe(mockData[2].createDate.getMinutes());
        expect(cells[4].nativeElement.textContent).toEqual(mockData[2].caseDesc);
        expect(cells[5].nativeElement.textContent).toEqual(mockData[2].orgDeptName);
    }));

    it('should enable lazy loading when not all records are returned', () => {

        let service: DashboardEventsService = TestBed.get(DashboardEventsService);

        comp.caseHistory = mockData;
        comp.LidValue = 393911;
        comp.LidType = 3;
        let termEquip: TerminalEquipment = new TerminalEquipment();
        termEquip.terminalID = "LK429472";
        termEquip.terminalNbr = 1;

        let cacheService = fixture.debugElement.injector.get(CaseHistoryService);
        let caseHistoryRecords: Array<CaseHistory> = new Array<CaseHistory>();
        for (let count: number = 0; count < 100; count++) {

            caseHistoryRecords.push({
                caseDesc: 'caseDesc1',
                caseDescId: 1,
                caseId: 8865652,
                caseLevel: "Terminal",
                caseStatusId: 1,
                closedDate: new Date("2008-07-28T10:54:51"),
                createDate: new Date("2008-07-28T10:54:26.837"),
                currDept: "",
                customerNbr: "1000393727",
                hasAttachment: false,
                hasCustomForm: false,
                hasEscalated: false,
                hasReminder: false,
                isCaseOpen: false,
                merchantId: 570314,
                merchantName: "GOLDEN CORRAL 671",
                merchantNbr: "542929802106716",
                orgDeptName: "NACS",
                parentCaseId: 0,
                priorityId: 2,
                referredFrom: "Risk",
                rtnToOriginator: true,
                terminalId: "LK429472",
                deptName: 'dept1',
                description: 'description1'
            }
            );
        }

        let customerInfo = new GenericPaginationResponse<CaseHistory>(499, caseHistoryRecords);
        let spy: jasmine.Spy = spyOn(cacheService, 'getPageOfCaseHistoryRecords')
            .and.returnValue(Observable.of(customerInfo));

        comp.caseHistory = caseHistoryRecords;
        comp.TotalNumberOfCaseHistoryRecords = 499;
        comp.LidType = 1;
        comp.LidValue = 333333;

        fixture.detectChanges();

        let enableLazyLoading: boolean = comp.shouldEnableLazyLoading();

        expect(enableLazyLoading).toBe(true);

    });

});
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
import { TerminalEquipmentComponent } from './terminal-equipment.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { TerminalEquipment } from './../../../models/dashboardInfo/terminalequipment.model';
import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';
import { TerminalInfoService } from '../../../services/dashboardinfo/terminal.service';

const mockData = [
    { "mid": '', "terminalNbr": 589532, "terminalID": "LK429221", "equipment": "XPNT", "software": "VAR Software", "deactivateActivateDate": "2005-08-17T17:34:06", "status": "Active" },
    { "mid": '', "terminalNbr": 589533, "terminalID": "LK429222", "equipment": "TEST", "software": "TEST Software", "deactivateActivateDate": "2005-09-17T17:34:06", "status": "Deactive" }
];

class LocationMock {

    replaceState(path: string, query?: string): void {

    }
}

export class MockService extends TerminalInfoService {

    getTerminalEquipments(merchantId: number): Observable<any[]> {
        return Observable.of(mockData);
    }
}

describe('TerminalEquipmentComponent', () => {
    let fixture: ComponentFixture<TerminalEquipmentComponent>,
        comp: TerminalEquipmentComponent,
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
            declarations: [TerminalEquipmentComponent],
            providers: [
                DashboardEventsService,
                {
                    provide: Location,
                    useClass: LocationMock
                },
                TerminalInfoService,

            ]
        });

        TestBed.overrideComponent(
            TerminalEquipmentComponent,
            { set: { providers: [{ provide: TerminalInfoService, useClass: MockService }] } }
        );


    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TerminalEquipmentComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(comp).toBeTruthy();
    });

    it('should bind title', () => {
        const lTerminalEquipment = fixture.debugElement.query(By.css('#lTerminalEquipment')).nativeElement;
        expect(lTerminalEquipment.textContent).toEqual('Terminal Equipment');
       
       
    });

    it('should display terminal list table', async(() => {

        comp.terminalEquipments = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length == 2).toBe(true);

    }));


    it('should display terminal list table row 1', async(() => {
        comp.ngOnInit();
        comp.terminalEquipments = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));
      
        expect(cells[0].nativeElement.textContent).toEqual(mockData[0].terminalID);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[0].equipment);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[0].software);       
        expect(cells[3].nativeElement.textContent).toEqual(mockData[0].status);
        expect(cells[4].nativeElement.textContent.trim()).toEqual(mockData[0].deactivateActivateDate);

    }));


    it('should display terminal list table row 2', async(() => {
        comp.ngOnInit();
        comp.terminalEquipments = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[1].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[1].terminalID);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[1].equipment);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[1].software);      
        expect(cells[3].nativeElement.textContent).toEqual(mockData[1].status);
        expect(cells[4].nativeElement.textContent.trim()).toEqual(mockData[1].deactivateActivateDate);

    }));

    it('it should call the events service when the selected row changes', () => {

        let myVar: boolean = true;

        let router = TestBed.get(DashboardEventsService);
        let spy = spyOn(router, 'announceTerminalEquipmentChange');

        comp._selectedTerminalEquipment = new TerminalEquipment();
        comp._selectedTerminalEquipment.terminalID = "1";
        comp._selectedTerminalEquipment.terminalNbr = 2;
        comp._selectedTerminalEquipment.mid = "3";

        comp.handleRowSelect(new Event("event"));

        expect(spy).toHaveBeenCalled();
    });

});



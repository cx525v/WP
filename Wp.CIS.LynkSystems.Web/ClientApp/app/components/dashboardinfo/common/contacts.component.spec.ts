import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
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
import { ContactsComponent } from './contacts.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { ContactService } from '../../../services/dashboardinfo/contact.service';
import { ContactFilter, ContactPage, Contacts } from '../../../models/dashboardInfo/contacts.model';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
const mockData = [
    {
        id: "1000393727",
        lastFour: "3333",
        contact: "Ann Perez",
        addressType: "National: Operations"
    },
    {
        id: "1000393727",
        lastFour: "2222",
        contact: "Dale Whitworth",
        addressType: "National: Finance"
    },
    {
        id:"1000393727",
        lastFour:"1111",
        contact:"Terri Warren",
        addressType:"Customer"
    },  
];

const mockResponse: apiResponse<Contacts>= {
    totalNumberOfRecords: 100,
    returnedRecords: mockData
};

export class MockService extends ContactService {
    getContacts(data: any): Observable<any> {
        return Observable.of(mockResponse);
    }
}

describe('ContactsComponent', () => {
    let fixture: ComponentFixture<ContactsComponent>,
        comp: ContactsComponent,
        debugElement: DebugElement,
        element: HTMLElement;

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpModule, DataTableModule, DialogModule, BrowserAnimationsModule, ReactiveFormsModule,
                NoopAnimationsModule, ConfirmDialogModule],
            declarations: [ContactsComponent],
            providers: [ContactService]
        });

        TestBed.overrideComponent(
            ContactsComponent,
            { set: { providers: [{ provide: ContactService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ContactsComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });


    it('should display contacts table', async(() => {
        comp.ngOnInit();
        comp.contacts = mockData;
        fixture.detectChanges();
      
        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length == 3).toBe(true);     

    }));

    it('should display contact table row 1', async(() => {
        comp.ngOnInit();
        comp.contacts = mockData;
        fixture.detectChanges();
        expect(comp.contacts.length).toBe(3);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[0].id);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[0].contact);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[0].addressType);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[0].lastFour);

    }));

    it('should display contact table row 2', async(() => {
        comp.ngOnInit();
        comp.contacts = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[1].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[1].id);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[1].contact);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[1].addressType);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[1].lastFour);

    }));

    it('should display contact table row 3', async(() => {
        comp.ngOnInit();
        comp.contacts = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[2].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[2].id);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[2].contact);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[2].addressType);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[2].lastFour);

    }));


});



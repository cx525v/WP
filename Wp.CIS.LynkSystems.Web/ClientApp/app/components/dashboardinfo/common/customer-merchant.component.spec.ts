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
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar, TabPanel, TabView } from 'primeng/primeng';
import { CustomerMerchantComponent } from './customer-merchant.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Contacts } from '../../../models/dashboardInfo/contacts.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { ContactsComponent} from '../common/contacts.component';

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
        id: "1000393727",
        lastFour: "1111",
        contact: "Terri Warren",
        addressType: "Customer"
    },  
];

describe('CustomerMerchantComponent Component', () => {
    let fixture: ComponentFixture<CustomerMerchantComponent>,
        comp: CustomerMerchantComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, CalendarModule, TabViewModule],
            declarations: [CustomerMerchantComponent, ContactsComponent],
            providers: []
        });    
    }));
  
    beforeEach(() => {
        fixture = TestBed.createComponent(CustomerMerchantComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('should dispay contacts', async(() => {
        const contactLabel = fixture.debugElement.query(By.css('#contactLabel')).nativeElement;        
       expect(contactLabel.textContent).toEqual('Contacts');
    }));

    it('should dispay Customer contact TabView', async(() => {
        const pTableView = fixture.debugElement.query(By.css('#pTableView'));//.nativeElement;
        const customerTab = pTableView.queryAll(By.css('p-tabpanel'))[0];
        const header = customerTab.nativeElement.attributes[1];
        expect(header.textContent).toEqual('CUSTOMER');
     }));


    it('should display contact table row 1', async(() => {
        comp.ngOnInit();    
        comp.customerContacts = mockData;
        fixture.detectChanges();
        expect(comp.customerContacts.length).toBe(3);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[0].id);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[0].contact);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[0].addressType);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[0].lastFour);       
       
    }));

    it('should display contact table row 2', async(() => {
        comp.ngOnInit();
        comp.customerContacts = mockData;
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
        comp.customerContacts = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[2].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[2].id);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[2].contact);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[2].addressType);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[2].lastFour);

    }));
    
});




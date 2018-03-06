import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { RouterTestingModule } from '@angular/router/testing';
import { Router } from '@angular/router';
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
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages } from 'primeng/primeng';
import { MerchantLocationsComponent } from './merchant-locations.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { MerchantLocation } from '../../../models/dashboard.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'

import { LidTypesEnum } from '../../../models/common/lid-types.enum';

const mockData = [
    {
        customerID: 0,
        mid:"542929801719261",
        name:"Golden Corral DEACT 546 ",
        state: "NC",
        statusIndicator :"Deactivated",
        zipCode: "27704"
    },
    {
        customerID: 0,
        mid: "542929801719262",
        name: "Golden Corral DEACT 2 ",
        state: "NC",
        statusIndicator: "Deactivated",
        zipCode: "27703"
    }
    ,
    {
        customerID: 0,
        mid: "542929801719263",
        name: "Golden Corral DEACT3 ",
        state: "SC",
        statusIndicator: "Activated",
        zipCode: "27704"
    }
];
describe('MerchantLocationsComponent', () => {
    let fixture: ComponentFixture<MerchantLocationsComponent>,
        comp: MerchantLocationsComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule,
                RouterTestingModule],
            declarations: [MerchantLocationsComponent],

        });


    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(MerchantLocationsComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });


    it('should display contacts table', async(() => {
      
        comp.merchantLocations = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length == 3).toBe(true);

    }));

    it('should display merchant location table row 1', async(() => {
        comp.ngOnInit();
        comp.merchantLocations = mockData;
        fixture.detectChanges();
       
        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[0].mid);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[0].name);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[0].state);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[0].zipCode);
        expect(cells[4].nativeElement.textContent).toEqual(mockData[0].statusIndicator);

    }));

    it('should display merchant location table row 2', async(() => {
        comp.ngOnInit();
        comp.merchantLocations = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[1].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[1].mid);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[1].name);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[1].state);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[1].zipCode);
        expect(cells[4].nativeElement.textContent).toEqual(mockData[1].statusIndicator);


    }));

    it('should display merchant location table row 3', async(() => {
        comp.ngOnInit();
        comp.merchantLocations = mockData;
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[2].queryAll(By.css('td span.ui-cell-data'));

        expect(cells[0].nativeElement.textContent).toEqual(mockData[2].mid);
        expect(cells[1].nativeElement.textContent).toEqual(mockData[2].name);
        expect(cells[2].nativeElement.textContent).toEqual(mockData[2].state);
        expect(cells[3].nativeElement.textContent).toEqual(mockData[2].zipCode);
        expect(cells[4].nativeElement.textContent).toEqual(mockData[2].statusIndicator);

    }));


    it('should navigate to the merchant page', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        fixture.detectChanges();
        comp._selectedMerchantLocation = new MerchantLocation();
        comp._selectedMerchantLocation.mid = 'Test Mid';

        let event: Event = new Event("event type");

        comp.handleRowSelect(event);

        expect(spy).toHaveBeenCalledWith(['/dashboardinfo/', <number>LidTypesEnum.MerchantNbr, comp._selectedMerchantLocation.mid]);

    });


});
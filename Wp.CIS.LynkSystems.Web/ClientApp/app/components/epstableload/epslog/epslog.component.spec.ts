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
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule
} from 'primeng/primeng';
import { DataTable,ConfirmationService, ConfirmDialog, Message, Messages,Calendar } from 'primeng/primeng';
import { EPSLogComponent } from './epslog.component';
import { EPSLogServcie } from "../../../services/petro/epslog.service";
import {TEST_BASESERVER } from '../../../shared/spec.global.component';
import { LoadingComponent} from '../loading.component';

let mockdata = [{ "merchantNbr": "542929803226091", "terminalID": "LK102248", "downloadDate": "2017-07-05 12:29:39", "actionType": "GetUpdateAvailable", "download": "No", "success": "Yes", "responseMessage": null },
{ "merchantNbr": "542929803226091", "terminalID": "LK102248", "downloadDate": "2017-07-05 12:29:39", "actionType": "GetTableUpdate", "download": "No", "success": "Yes", "responseMessage": null },
{ "merchantNbr": "542929803226091", "terminalID": "LK102248", "downloadDate": "2017-07-05 12:30:28", "actionType": "SendAck", "download": "Yes", "success": "Yes", "responseMessage": null },
{ "merchantNbr": "542929803226091", "terminalID": "LK102248", "downloadDate": "2017-07-06 12:29:39", "actionType": "GetUpdateAvailable", "download": "No", "success": "Yes", "responseMessage": null }
];

export class MockService extends EPSLogServcie {
  
    getEPSLog(startDate: string, endDate: string) {
       return Observable.of(mockdata);
    }
}

describe('EPSLogComponent Component', () => {
        let fixture: ComponentFixture<EPSLogComponent>,
        comp: EPSLogComponent,
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
            declarations: [EPSLogComponent, LoadingComponent],
            providers: [EPSLogServcie]
        });

        TestBed.overrideComponent(
            EPSLogComponent,
            { set: { providers: [{ provide: EPSLogServcie, useClass: MockService }] } }
        );
           
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EPSLogComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });
  

    it('should display log table', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();  
        expect(comp.dataSource.length).toBe(4);   

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length == 4).toBe(true); 
        expect(rows[0].nativeElement.textContent.indexOf('GetUpdateAvailable') > 0).toBe(true);
        expect(rows[1].nativeElement.textContent.indexOf('GetTableUpdate') > 0).toBe(true);

    }));

    it('should display today log by when clicking today link button', async(() => {
        const bToday = fixture.debugElement.query(By.css('#bToday')).nativeElement;
        comp.ngOnInit();
        fixture.detectChanges();  
        bToday.click();
        fixture.detectChanges();
        expect(comp.dataSource.length).toBe(4);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length).toBe(4); 
        expect(rows[0].nativeElement.textContent.indexOf('GetUpdateAvailable') > 0).toBe(true);
    }));

    it('should display today log by when clicking week link button', async(() => {
        const bWeek = fixture.debugElement.query(By.css('#bWeek')).nativeElement;
        comp.ngOnInit();
        fixture.detectChanges();
        bWeek.click();
        fixture.detectChanges();
        expect(comp.dataSource.length).toBe(4);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length).toBe(4);
        expect(rows[0].nativeElement.textContent.indexOf('GetUpdateAvailable') > 0).toBe(true);
        expect(rows[1].nativeElement.textContent.indexOf('GetTableUpdate') > 0).toBe(true);
    }));

    it('should display today log by when clicking month link button', async(() => {
        const bMonth = fixture.debugElement.query(By.css('#bMonth')).nativeElement;
        comp.ngOnInit();
        fixture.detectChanges();
        bMonth.click();
        fixture.detectChanges();
        expect(comp.dataSource.length).toBe(4);

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        expect(rows.length).toBe(4);
        expect(rows[0].nativeElement.textContent.indexOf('GetUpdateAvailable') > 0).toBe(true);
        expect(rows[1].nativeElement.textContent.indexOf('GetTableUpdate') > 0).toBe(true);
        expect(rows[2].nativeElement.textContent.indexOf('SendAck') > 0).toBe(true);
    }));

    it('should display data log when submitting a date range', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.StartDate = new Date('2017-09-12');
        comp.EndDate = new Date('06/06/2017');

        fixture.detectChanges();

        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBeTruthy();
        
    }));

    it('submit button should be disabled if date is empty', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.EndDate = new Date('06/06/2017');

        fixture.detectChanges();
       
        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBeTruthy();

    }));

    it('submit button should not be disabled if date is correct', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.StartDate = new Date('2017-06-02');
        comp.EndDate = new Date('2017-06-06');

        fixture.detectChanges();

        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBeFalsy();

    }));

    it('submit button should be disabled if date range is greater than 62 days', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.StartDate = new Date('2017-02-02');
        comp.EndDate = new Date('2017-06-06');

        fixture.detectChanges();

        const bSubmit = fixture.debugElement.query(By.css('#bSubmit')).nativeElement;
        fixture.detectChanges();
        expect(bSubmit.disabled).toBe(true);

    }));
});



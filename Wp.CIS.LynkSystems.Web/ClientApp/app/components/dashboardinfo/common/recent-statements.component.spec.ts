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
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule,AccordionModule
} from 'primeng/primeng';
import { DataTable, ConfirmDialog, Message, Messages, Calendar, TabPanel, TabView, AccordionTab } from 'primeng/primeng';
import { RecentStatementsComponent } from './recent-statements.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { RecentStatement } from '../../../models/dashboardInfo/recentstatement.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { TwoColumnPipe } from '../../../pipes/twocolumn.pipe';
import { DashboardInfoService } from './../../../services/dashboardinfo.service';

const mockData: RecentStatement[] = [
    {
        html_String:'Statement11',      
        yearId: 2017,
        monthId:11
    },
    {
        html_String: 'Statement10',
        yearId: 2017,
        monthId: 10
    },
    {
        html_String: 'Statement9',
        yearId: 2017,
        monthId: 9
    },
    {
        html_String: 'Statement8',
        yearId: 2017,
        monthId: 8
    },
    {
        html_String: 'Statement7',
        yearId: 2017,
        monthId: 7
    },
    {
        html_String: 'Statement6',
        yearId: 2017,
        monthId: 6
    },
    {
        html_String: 'Statement5',
        yearId: 2017,
        monthId: 5
    },
    {
        html_String: 'Statement4',
        yearId: 2017,
        monthId: 4
    },
    {
        html_String: 'Statement3',
        yearId: 2017,
        monthId: 3
    },
    {
        html_String: 'Statement2',
        yearId: 2017,
        monthId: 2
    },
    {
        html_String: 'Statement1',
        yearId: 2017,
        monthId: 1
    },
    {
        html_String: 'Statement0',
        yearId: 2016,
        monthId: 12
    }
];

export class MockService extends DashboardInfoService {

    getRecentStatements(merchantNbr: string): Observable<any[]> {
        return Observable.of(mockData);
    }
}

describe('RecentStatementsComponent Component', () => {
    let fixture: ComponentFixture<RecentStatementsComponent>,
        comp: RecentStatementsComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, CalendarModule,
                TabViewModule, AccordionModule],
            declarations: [RecentStatementsComponent, TwoColumnPipe],
            providers: [DashboardInfoService]
        });
        TestBed.overrideComponent(
            RecentStatementsComponent,
            { set: { providers: [{ provide: DashboardInfoService, useClass: MockService }] } }
        );
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(RecentStatementsComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('should dispay recentStatement Text header', async(() => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();   
        const lrecentStatement = fixture.debugElement.query(By.css('#lrecentStatement')).nativeElement;
        expect(lrecentStatement.textContent).toEqual('Recent Statements');
    }));

    it('should not display recent statements section if no statements', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();
        const lrecentStatement = fixture.debugElement.query(By.css('#lrecentStatement'));      
        expect(lrecentStatement).toBeNull();
    }));  

    
    it('should display recent statements', async(() => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();   
        var statementbtn = fixture.nativeElement.querySelectorAll('button');
        expect(statementbtn.length).toEqual(mockData.length);
       
    }));  

    it('should display more recent statements', async(() => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();

        var morestatementTab = fixture.debugElement.query(By.css('#moreStatementTab')).nativeElement;
        var morestatementbtn = morestatementTab.querySelectorAll('button');
        var moreStmts = mockData.length - 6; 
        if (moreStmts < 0) {
            moreStmts = 0;
        }
        expect(morestatementbtn.length).toEqual(moreStmts);
         
    }));  

    it('should display recent statement details', async(() => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();
        var statementbtn = fixture.nativeElement.querySelector('button');
        statementbtn.click();
        fixture.detectChanges();
        var statementDetaiContent = fixture.debugElement.query(By.css('#statementDetaiContent')).nativeElement;
        expect(statementDetaiContent.textContent).toEqual(mockData[0].html_String);
     
    }));  

    it('should display print button', async(() => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();
        var statementbtn = fixture.nativeElement.querySelector('button');
        statementbtn.click();
        fixture.detectChanges();

        var bPrint = fixture.debugElement.query(By.css('#bPrint')).nativeElement;
        expect(bPrint.textContent).toEqual('Print');      
    }));  

    it('should do print when Print button is clicked', () => {
        comp.ngOnInit();
        comp.MerchantNbr = '1';
        fixture.detectChanges();
        var statementbtn = fixture.nativeElement.querySelector('button');
        statementbtn.click();
        fixture.detectChanges();

        spyOn(comp, 'print');
        var bPrint = fixture.debugElement.query(By.css('#bPrint')).nativeElement;
        bPrint.click();
        fixture.detectChanges();
        expect(comp.print).toHaveBeenCalled();
    });

});




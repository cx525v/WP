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
import { TransactionsComponent } from './transactions.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { TransactionComponent } from './transaction.component';


import { TransactionService } from '../../../services/dashboardinfo/transaction.service';
import { Transaction, TransactionFilter, TransactionPage } from '../../../models/dashboardInfo/transaction.model';
import { LazyLoadEvent } from 'primeng/primeng';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DecimalPipe } from '@angular/common';
const transactions: Transaction[] = [
    {
        reQ_BUS_DATE: '2017-10-12',
        reQ_AMT: 22.00,
        reQ_PAN_4: '1233',
        reQ_TRAN_TYPE: 'type4',
        resP_NETWRK_AUTH_CD: 'ncd4',
        descript: '23432'
    },

    {
        reQ_BUS_DATE: '2017-10-10',
        reQ_AMT: 20.00,
        reQ_PAN_4: '1234',
        reQ_TRAN_TYPE: 'type1',
        resP_NETWRK_AUTH_CD: 'ncd1',
        descript: '2222'
    },
   
];

const mockData: apiResponse<Transaction> = {
    returnedRecords: transactions,
    totalNumberOfRecords: transactions.length
}


export class MockService extends TransactionService {
    getTransactionHistory(filter: TransactionFilter): any {
        return Observable.of(mockData);
    }
}

describe('TransactionsComponent Component', () => {
    let fixture: ComponentFixture<TransactionsComponent>,
        component: TransactionsComponent,
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
                DataTableModule, TabViewModule, CalendarModule],
            declarations: [
                TransactionsComponent, TransactionComponent
            ],
            providers: [TransactionService]
        });

        TestBed.overrideComponent(
            TransactionComponent,
            { set: { providers: [{ provide: TransactionService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TransactionsComponent);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should dispay transactions label', async(() => {
        component.terminalID = '1';
        component.ngOnInit();
        fixture.detectChanges();
        const transactionLabel = fixture.debugElement.query(By.css('#transactionLabel')).nativeElement;
        expect(transactionLabel.textContent).toEqual('Transactions');
    }));


    it('should dispay Settled transaction TabView', async(() => {
        component.terminalID = '1';
        component.ngOnInit();
        fixture.detectChanges();
     
        const pTableView = fixture.debugElement.query(By.css('#pTableView')); 
        const settledTab = pTableView.queryAll(By.css('p-tabpanel'))[0];
        const header = settledTab.nativeElement.attributes[1];
        expect(header.textContent).toEqual('SETTLED');
    }));

    it('should dispay ACQUIRED transaction TabView', async(() => {
        component.terminalID = '1';
        component.ngOnInit();
        fixture.detectChanges();

        const pTableView = fixture.debugElement.query(By.css('#pTableView')); 
        const settledTab = pTableView.queryAll(By.css('p-tabpanel'))[1];
        const header = settledTab.nativeElement.attributes[1];
        expect(header.textContent).toEqual('ACQUIRED');
    }));
});

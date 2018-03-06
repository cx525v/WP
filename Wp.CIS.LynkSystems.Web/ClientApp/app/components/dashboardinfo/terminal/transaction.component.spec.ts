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

import { TransactionComponent } from './transaction.component';

import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { TransactionService } from '../../../services/dashboardinfo/transaction.service';
import { Transaction, TransactionFilter, TransactionPage } from '../../../models/dashboardInfo/transaction.model';
import { LazyLoadEvent } from 'primeng/primeng';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DecimalPipe } from '@angular/common';

const transactions: Transaction[] = [
    {
        reQ_BUS_DATE: '2017-10-12T11:10:01.012',
        reQ_AMT: 22.00,
        reQ_PAN_4: '1233',
        reQ_TRAN_TYPE: 'type4',
        resP_NETWRK_AUTH_CD: 'ncd4',
        descript: '23432'
    },

    {
        reQ_BUS_DATE: '2017-10-10T10:12:03.123',
        reQ_AMT: 20.00,
        reQ_PAN_4: '1234',
        reQ_TRAN_TYPE: 'type1',
        resP_NETWRK_AUTH_CD: 'ncd1',
        descript:'2222'
    },
   
];

const mockData: apiResponse<Transaction> ={
    returnedRecords: transactions,
    totalNumberOfRecords: transactions.length
}


export class MockService extends TransactionService {
    getTransactionHistory(filter: TransactionFilter): any {
        return Observable.of(mockData);
    }
}

describe('TransactionComponent Component', () => {
    let fixture: ComponentFixture<TransactionComponent>,
        component: TransactionComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, TabViewModule, CalendarModule],
            declarations: [
                TransactionComponent,  
            ],
            providers: [TransactionService]
        });

        TestBed.overrideComponent(
            TransactionComponent,
            { set: { providers: [{ provide: TransactionService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(TransactionComponent);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should display transaction history table row 1', () => {
        component.terminalID = '1';      
        fixture.detectChanges();
     
        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[0].queryAll(By.css('td span.ui-cell-data'));
        const transaction = transactions[0];

        let npipe = new DecimalPipe('en');
        expect(cells[0].nativeElement.textContent.trim()).toEqual(component.convertJMS(transaction.reQ_BUS_DATE));
        expect(cells[1].nativeElement.textContent.trim()).toEqual(npipe.transform(transaction.reQ_AMT.toString(), '1.2-2'));
        expect(cells[2].nativeElement.textContent).toEqual(transaction.reQ_PAN_4);
        expect(cells[3].nativeElement.textContent).toEqual(transaction.reQ_TRAN_TYPE);
        expect(cells[4].nativeElement.textContent).toEqual(transaction.resP_NETWRK_AUTH_CD);
        expect(cells[5].nativeElement.textContent).toEqual(transaction.descript);
    });

    it('should display transaction history table row 2', () => {
        component.terminalID = '1';      
        fixture.detectChanges();

        const rows = debugElement.queryAll(By.css('tr.ui-widget-content'));
        const cells = rows[1].queryAll(By.css('td span.ui-cell-data'));
        const transaction = transactions[1];
       
        let npipe = new DecimalPipe('en');     
        expect(cells[0].nativeElement.textContent.trim()).toEqual(component.convertJMS(transaction.reQ_BUS_DATE));
        expect(cells[1].nativeElement.textContent.trim()).toEqual(npipe.transform(transaction.reQ_AMT.toString(),'1.2-2'));
        expect(cells[2].nativeElement.textContent).toEqual(transaction.reQ_PAN_4);
        expect(cells[3].nativeElement.textContent).toEqual(transaction.reQ_TRAN_TYPE);
        expect(cells[4].nativeElement.textContent).toEqual(transaction.resP_NETWRK_AUTH_CD);
        expect(cells[5].nativeElement.textContent).toEqual(transaction.descript);
    });
});


/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

import { PrimeNgSelectItemValue } from '../../../models//transactions/primeNgSelecteItemValue.model';
import { PrimeNgSelectOption } from '../../../models//transactions/primeNgSelectOption.model';


import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { DebugElement } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, ActivatedRoute } from '@angular/router';

import { GenericPaginationResponse } from '../../../models/transactions/genericPaginationResponse.model';
import { TransactionsInquiry } from '../../../models/transactions/transactionsInquiry.model';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
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
    PanelModule,
    CalendarModule,
    InputTextModule,
    RadioButtonModule,
    ConfirmDialogModule
} from 'primeng/primeng';

import { DataTable, ConfirmationService } from 'primeng/primeng';
import { TransactionInquiryComponent } from '../../transaction/transactionInquiry/transactionInquiry.component';
import { TransactionService } from "../../../services/transaction.service";
import { TransactionInquiryTypesService } from "../../../services/transactionInquiryTypes.service";
import { TransactionInquiryTypes } from "../../../models/transactions/transactionInquiryTypes.model"
import { TransactionsInquiryGeneralInfo } from "../../../models/transactions/transactionsInquiryGeneralInfo.model";
import { TEST_BASESERVER } from '../../../shared/spec.global.component';


let fixture: ComponentFixture<TransactionInquiryComponent>;


describe('Transaction Inquiry component', () => {

    let fixture: ComponentFixture<TransactionInquiryComponent>,
        comp: TransactionInquiryComponent,
        debugElement: DebugElement,
        element: HTMLElement;

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule,
                ReactiveFormsModule,
                HttpModule,
                DataTableModule,
                DialogModule,
                BrowserAnimationsModule,
                NoopAnimationsModule,
                TabViewModule,
                DataTableModule,
                FileUploadModule,
                ButtonModule,
                TreeModule,
                DropdownModule,
                DialogModule,
                CheckboxModule,
                DataGridModule,
                PanelModule,
                CalendarModule,
                InputTextModule,
                RadioButtonModule,
                ConfirmDialogModule,
                RouterTestingModule
            ],

            declarations: [TransactionInquiryComponent],
            providers: [
                TransactionService,
                TransactionInquiryTypesService,
                FormBuilder,
                MockBackend,
                {
                    provide: TransactionService, useValue: {

                        getTerminalInfo(terminalNumber: string): Observable<TransactionsInquiryGeneralInfo> {

                            //debugger;
                            //alert("getTerminalInfo");
                            //console.log("getTerminalInfo");
                            return Observable.of(new TransactionsInquiryGeneralInfo("customer number", "merchannt number", "address", "city", "state",
                                "zipcode", 111, "sic Description", "customer test name", "services",
                                "status description", "business description", new Date(), "consolidation", 5, true,
                                111, 222));
                        },
                        getTransactionRecordsForSearch(terminalId: string,
                            beginDate: Date,
                            endDate: Date,
                            searchCriteriaId: number,
                            firstSix: string,
                            lastFour: string,
                            cardType: string,
                            batchNumber: string,
                            skip: number,
                            take: number): Observable<GenericPaginationResponse<TransactionsInquiry>> {

                            let retVal: any = new GenericPaginationResponse<TransactionsInquiry>(0, []);
                            return Observable.of(retVal);
                        }
                    }
                },
                {
                    provide: TransactionInquiryTypesService, useValue: {
                        getAllActiveTransactionInquiryTypes() {

                            return Observable.of([
                                new TransactionInquiryTypes(1, "description 1", "transaction Inquiry 1", 1),
                                new TransactionInquiryTypes(2, "description 2", "transaction Inquiry 2", 1),
                                new TransactionInquiryTypes(3, "description 3", "transaction Inquiry 3", 1),
                                new TransactionInquiryTypes(4, "description 4", "transaction Inquiry 4", 1),
                                new TransactionInquiryTypes(5, "description 5", "transaction Inquiry 5", 1),
                                new TransactionInquiryTypes(6, "description 6", "transaction Inquiry 6", 1),
                                new TransactionInquiryTypes(7, "description 7", "transaction Inquiry 7", 1),
                                new TransactionInquiryTypes(8, "description 8", "transaction Inquiry 8", 1, )
                            ]);
                        }
                    }
                },
                {
                    provide: ActivatedRoute,
                    useValue: {
                        params: Observable.of({ terminalId: 123 })
                    }
                },

                BaseRequestOptions,
                {
                    provide: Http,
                    useFactory: (backend: ConnectionBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                }
            ]
        });//.compileComponents();
        //TestBed.configureTestingModule({ imports: [ReactiveFormsModule], declarations: [TransactionInquiryComponent] });///Check for forms error

    }));

    beforeEach( () => {
        fixture = TestBed.createComponent(TransactionInquiryComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('Transaction Inquiry component should display datatable', async(() => {
        
        fixture.detectChanges();
        expect(debugElement.queryAll(By.css('.ui-datatable')).length).toEqual(1);
    }));

    it('should display terminal name', async(() => {
   

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "address",
            "city", "state", "zipcode", 111, "sicDesc", "customer test name", "services",
            "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#terminalName');

        expect(label.innerText).toEqual("customer test name");

    }));

    it('should display business description', async(() => {


        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "address", "city", "state", "zipcode", 111, "sicDesc", "customer test name", "services", "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#businessDescription');

        expect(label.innerText).toEqual("test business description");

    }));

    it('should display address', async(() => {

        const testAddress: string = "test Address";

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", testAddress, "city", "state", "zipcode",
            111, "sicDesc", "customer test name", "services", "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#theTerminalAddress');

        expect(label.innerText).toEqual(testAddress);

    }));

    it('should display services', async(() => {

        const expectedResult: string = "test services";

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "test address", "city", "state", "zipcode", 111,
            "sicDesc", "customer test name", "test services", "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#terminalServices');

        expect(label.innerText).toEqual("test services");

    }));

    it('should display city state and zip', async(() => {

        const city: string = "test city";
        const state: string = "test state";
        const zip: string = "test zip";
        const expectedResult: string = `${city}, ${state}, ${zip}`;

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "test address", city, state, zip, 111,
            "sicDesc", "customer test name", "test services", "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#theCityStateAndZip');

        expect(label.innerText).toEqual(expectedResult);

    }));

    it('should display sic code', async(() => {

        const sicCode: number = 222;

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "test address", "test city", "test state", "test zip", sicCode,
            "sicDesc", "customer test name", "test services", "statusDesc", " test business description", new Date(), "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#theSicCode');

        expect(label.innerText).toEqual(sicCode.toString());

    }));


    it('should display the last processed date should ', async(() => {

        const lastProcessedDate: Date = new Date(2017, 3, 1);

        comp._transactionInfoModel = new TransactionsInquiryGeneralInfo("customer number", "merchant number", "test address", "test city", "test state", "test zip", 111,
            "sicDesc", "customer test name", "test services", "statusDesc", " test business description", lastProcessedDate, "consolidation",
            5, true, 222, 333);

        fixture.detectChanges();
        const label: HTMLElement = fixture.nativeElement.querySelector('#theLastProcessedDate');

        expect(label.innerText).toEqual("4/1/2017, 12:00 AM");

    }));

    it('search now button be disabled with initial settings', async(() => {

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        expect(searchNowButton.hasAttribute('disabled'));

    }));

    it('search now button should be diabled with date ranges empty', async(() => {

        fixture.detectChanges();
        let searchCriteriaValues: PrimeNgSelectOption<TransactionInquiryTypes>[] = [
            new PrimeNgSelectOption<TransactionInquiryTypes>("test string", new TransactionInquiryTypes(1, "display name", "description", 1))
        ];

        comp._searchCriteriaOptions = searchCriteriaValues;
        comp._searchCriteriaOptionsControl.setValue(searchCriteriaValues[0]);

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        let hasAttribute: boolean = searchNowButton.hasAttribute('disabled');

        expect(hasAttribute).toEqual(true);

    }));

    it('search now button should be diabled with end range empty', async(() => {

        fixture.detectChanges();
        let searchCriteriaValues: PrimeNgSelectOption<TransactionInquiryTypes>[] = [
            new PrimeNgSelectOption<TransactionInquiryTypes>("test string", new TransactionInquiryTypes(1, "display name", "description", 1))
        ];

        comp._searchCriteriaOptions = searchCriteriaValues;
        comp._searchCriteriaOptionsControl.setValue(searchCriteriaValues[0]);

        comp._beginDateRangeCalendarControl.setValue(new Date(2017, 3, 3));

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        let hasAttribute: boolean = searchNowButton.hasAttribute('disabled');

        expect(hasAttribute).toEqual(true);

    }));

    it('search now button should be diabled with begin ranges empty', async(() => {

        fixture.detectChanges();
        let searchCriteriaValues: PrimeNgSelectOption<TransactionInquiryTypes>[] = [
            new PrimeNgSelectOption<TransactionInquiryTypes>("test string", new TransactionInquiryTypes(1, "display name", "description", 1))
        ];

        comp._searchCriteriaOptions = searchCriteriaValues;
        comp._searchCriteriaOptionsControl.setValue(searchCriteriaValues[0]);

        comp._endDateRangeCalendar.setValue(new Date(2017, 3, 10));

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        let hasAttribute: boolean = searchNowButton.hasAttribute('disabled');

        expect(hasAttribute).toEqual(true);

    }));

    it('search now button should be enabled with required settings', async(() => {

        fixture.detectChanges();
        let searchCriteriaValues: PrimeNgSelectOption<TransactionInquiryTypes>[] = [
            new PrimeNgSelectOption<TransactionInquiryTypes>("test string", new TransactionInquiryTypes(1, "display name", "description", 1))
        ];

        comp._searchCriteriaOptions = searchCriteriaValues;
        comp._searchCriteriaOptionsControl.setValue(searchCriteriaValues[0]);

        comp._beginDateRangeCalendarControl.setValue(new Date(2017, 3, 3));
        comp._endDateRangeCalendar.setValue(new Date(2017, 3, 10));

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        let hasAttribute: boolean = searchNowButton.hasAttribute('disabled');

        expect(hasAttribute).toEqual(false);

    })); 

    it('search now button should be disabled with credit card and batch number filters set', async(() => {

        fixture.detectChanges();
        let searchCriteriaValues: PrimeNgSelectOption<TransactionInquiryTypes>[] = [
            new PrimeNgSelectOption<TransactionInquiryTypes>("test string", new TransactionInquiryTypes(1, "display name", "description", 1))
        ];

        comp._searchCriteriaOptions = searchCriteriaValues;
        comp._searchCriteriaOptionsControl.setValue(searchCriteriaValues[0]);

        comp._beginDateRangeCalendarControl.setValue(new Date(2017, 3, 3));
        comp._endDateRangeCalendar.setValue(new Date(2017, 3, 10));
        comp._batchNumberInputControl.setValue("111");
        comp._cardNumberFirstSixControl.setValue("123456");
        comp._cardNumberLastFourControl.setValue("1234");

        fixture.detectChanges();
        const searchNowButton: HTMLElement = fixture.nativeElement.querySelector('#theSearchNowButton');

        let hasAttribute: boolean = searchNowButton.hasAttribute('disabled');

        expect(hasAttribute).toEqual(true);

    })); 

    it('should navigate to the new transaction query', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');


        let button = fixture.debugElement.query(By.css("#theApplyTerminalIdButton"));

        button.triggerEventHandler("click", null);

        expect(spy).toHaveBeenCalledWith(['/transactionInquiry', undefined]);

    });

    it('should get new page of records', () => {

        let spy = spyOn(comp, 'applySearchCriteria');

        let button = fixture.debugElement.query(By.css("#theSearchNowButton"));

        button.triggerEventHandler("click", null);

        fixture.detectChanges();

        expect(spy).toHaveBeenCalled();

    });
});
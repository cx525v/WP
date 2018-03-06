
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
import { BankingInfoComponent } from './banking-info.component';
import { TerminalDetailsDialogComponent } from './terminal-details-dialog.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { TerminalDetail, Terminal, TerminalDetails, TerminalSettlementInfo } from './../../../models/dashboardInfo/terminal.model';
import { ActiveServices, TermInfo } from './../../../models/dashboard.model';
import { DatePipe } from '@angular/common';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { BankingService } from '../../../services/banking.service';
import { DateTimeFormatPipe } from '../../../pipes/date.pipe';
import { DecimalPipe } from '@angular/common';
import { YearMonthDatePipe } from '../../../pipes/dateymd.pipe';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { BankingInfoModel } from '../../../models/bankingInfo/banking-info.model'

export class MockService extends BankingService {
 
    public getBankingInfo(LidType: LidTypesEnum, Lid: string): Observable<BankingInfoModel[]> {

        let bankingInfoList: Array<BankingInfoModel> = new Array<BankingInfoModel>();
        bankingInfoList.push(new BankingInfoModel("Chargeback", "aaaaaaa", "bbbbbb", "Bank Name"));
        bankingInfoList.push(new BankingInfoModel("Settlement", "aaaaaaa", "bbbbbb", "Bank Name"));
        bankingInfoList.push(new BankingInfoModel("Billing", "aaaaaaa", "bbbbbb", "Bank Name"));

        let fakeResponse: Observable<BankingInfoModel[]> = Observable.of(bankingInfoList);

        return fakeResponse;
    }
}

describe('Banking Info Component', () => {
    let fixture: ComponentFixture<BankingInfoComponent>,
        component: BankingInfoComponent,
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
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, TabViewModule],
            declarations: [
                BankingInfoComponent,
                TerminalDetailsDialogComponent,
                DateTimeFormatPipe,
                YearMonthDatePipe
            ],
            providers: [BankingService]
        });

        TestBed.overrideComponent(
            BankingInfoComponent,
            { set: { providers: [{ provide: BankingService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(BankingInfoComponent);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should show settlement in the first row', () => {

        component.terminalNbr = 1111;
        fixture.detectChanges();

        let settlementCell: any = fixture.debugElement.query(By.css("#bankingInfoRecordCell1_0")).nativeElement;

        expect(settlementCell.innerText).toBe("Settlement");
    });

    it('should show Chargeback in the second row', () => {

        component.terminalNbr = 1111;
        fixture.detectChanges();

        let settlementCell: any = fixture.debugElement.query(By.css("#bankingInfoRecordCell1_1")).nativeElement;

        expect(settlementCell.innerText).toBe("Chargeback");
    });

    it('should show Billing in the third row', () => {

        component.terminalNbr = 1111;
        fixture.detectChanges();

        let settlementCell: any = fixture.debugElement.query(By.css("#bankingInfoRecordCell1_2")).nativeElement;

        expect(settlementCell.innerText).toBe("Billing");
    });

});

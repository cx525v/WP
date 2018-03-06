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
import { TerminalDetailsComponent } from './terminal-details.component';
import { TerminalDetailsDialogComponent } from './terminal-details-dialog.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges, SimpleChange } from '@angular/core';
import { TerminalDetail, Terminal, TerminalDetails, TerminalSettlementInfo } from './../../../models/dashboardInfo/terminal.model';
import { ActiveServices,TermInfo } from './../../../models/dashboard.model';
import { DatePipe } from '@angular/common';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { TerminalInfoService } from '../../../services/dashboardinfo/terminal.service';
import { DateTimeFormatPipe} from '../../../pipes/date.pipe';
import { DecimalPipe } from '@angular/common';
import { YearMonthDatePipe } from '../../../pipes/dateymd.pipe';

const mockData: Terminal = {
    "terminalDetails": { "debit": 1, "credit": 1, "checkSvc": 0, "pob": 0, "visaMC": 1, "discover": 0, "discCanx": 0, "diners": 0, "amex": 1, "amexCanx": 0, "revPip": 0, "ebt": 0, "rewardsLynk": 0, "giftLynk": 1, "tid": "LK102280", "autoSettleOverride": "0", "cutOffTime": "1600", "timeZone": "EST", "autoSettleIndicator": "False", "autoSettleTime": "0000", "terminalDescription": "Terminal - Omni 3750 Dual Com Port", "lynkAdvantageDesc": "NONE","terminalType":"terminalType1" },
    "activeServices": { "lidType": 1, "lid": 10008693, "billingMethodType": 11, "billMtdDesc": "SN 3 Tier", "chkName": null, "giftLynk_ON": true, "rewardsLynk_ON": false, "lastProcessingDt": "0001-01-01T00:00:00", "amex_ON": true, "discover_ON": false, "discover_CT21_ON": true, "diner_ON": false, "jcB_ON": false, "openCase": 0, "terminalRental_ON": false, "printerRental_ON": false, "pinPadRental_ON": false, "softDesc": "LSPG3710", "creditST_ON": true, "debitST_ON": true, "checkST_ON": false, "achsT_ON": false, "lynkAdvantage_ON": false, "externalTermID": null, "authProcessorDesc": null, "sicDesc": "5999 MISC. RETAIL STORES", "laDesc": "", "activeServiesDesc": "Credit (Amex, Discover, Debit, Gift/Loyalty(Gift Card)" },
    "terminalInfo": { "customerID": 699663, "merchantId": 10001623, "terminalId": "LK102280", "businessType": 2, "programType": 0, "activationDt": "2014-12-10T15:23:18.01", "downLoadDate": null, "sentToStratusDate": null, "cspStatusInterval": "02", "commType": 1, "statusIndicator": 1, "cutOffTime": "1600", "captureType": 0, "defaultNetwork": 40, "deactivationDt": null, "originalSO": 2002584, "installDate": null, "forcedBillingDate": null, "incrementalDt": "0001-01-01T00:00:00", "busTypeDesc": "Payment", "cashAdv": 0, "checkSvc": 0, "credit": 1, "debit": 1, "ebt": 0, "fleet": 0, "pob": 0, "suppLA": 1, "merchantName": "Standard Transactions", "statDesc": "Active" },
    "sensitivityInfo": { "sensitivityLevel": 6, "senLevelDesc": "Tier 2" }
   };

const mockterminalSettlementInfo = { "nbrOfTrans": 172, "grossAmt": 8543 };
export class MockService extends TerminalInfoService {
    getTerminalInfoDetail(terminalNbr: number): any {
        return Observable.of(mockData);
    }

    getTerminalSettlementInfo(terminalNbr: number): any {
        return Observable.of(mockterminalSettlementInfo);
    }
}

    describe('TerminalDetailsComponent Component', () => {
        let fixture: ComponentFixture<TerminalDetailsComponent>,
            component: TerminalDetailsComponent,
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
                    DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule,TabViewModule],
                declarations: [
                    TerminalDetailsComponent,  
                    TerminalDetailsDialogComponent,
                    DateTimeFormatPipe,
                    YearMonthDatePipe
                ],
                providers: [TerminalInfoService]
            });

            TestBed.overrideComponent(
                TerminalDetailsComponent,
                { set: { providers: [{ provide: TerminalInfoService, useClass: MockService }] } }
            );

        }));

        beforeEach(() => {
            fixture = TestBed.createComponent(TerminalDetailsComponent);
            component = fixture.componentInstance;
            debugElement = fixture.debugElement;
            element = debugElement.nativeElement;
        });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should display terminalNbr', () => {   
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
      
        const lTerminal = fixture.debugElement.query(By.css('#lTerminal')).nativeElement;
        expect(lTerminal.textContent).toEqual('TERMINAL: ' + mockData.terminalInfo.terminalId);
    });

    it('should display Auto Settlement Time', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const autoSettlementTime = fixture.debugElement.query(By.css('#autoSettlementTime')).nativeElement;
        expect(autoSettlementTime.textContent).toEqual(component.getCutOff(mockData.terminalDetails.autoSettleTime));
    });

    it('should display cutOffTime', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const cutOffTime = fixture.debugElement.query(By.css('#cutOffTime')).nativeElement;
        expect(cutOffTime.textContent).toEqual(component.getCutOff(mockData.terminalDetails.cutOffTime));
    });

    it('should display timeZone', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const timeZone = fixture.debugElement.query(By.css('#timeZone')).nativeElement;
        expect(timeZone.textContent).toEqual(mockData.terminalDetails.timeZone);
    });

    it('should display Last Settlement date', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();        
        const lastProcessingDt = fixture.debugElement.query(By.css('#lastProcessingDt')).nativeElement;
        let pipe = new YearMonthDatePipe('en');       
        expect(lastProcessingDt.textContent).toEqual(pipe.transform(mockData.activeServices.lastProcessingDt) +';');
    });

    it('should display settleAmount', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const settleAmount = fixture.debugElement.query(By.css('#settleAmount')).nativeElement;
        let pipe = new DecimalPipe('en');            
        expect(settleAmount.textContent).toEqual('$'+pipe.transform(component.terminalSettlementInfo.grossAmt, '1.2-2') +';');
    });

    it('should display settleTransactions', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const settleTransactions = fixture.debugElement.query(By.css('#settleTransactions')).nativeElement;
        let pipe = new DecimalPipe('en');    
        expect(settleTransactions.textContent).toEqual(pipe.transform(component.terminalSettlementInfo.nbrOfTrans));
    });

    it('should display activeServiesDesc', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const activeServiesDesc = fixture.debugElement.query(By.css('#activeServiesDesc')).nativeElement;
        expect(activeServiesDesc.textContent).toEqual(mockData.services);
    });

    it('should display equipmentSupplies', () => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const equipmentSupplies = fixture.debugElement.query(By.css('#equipmentSupplies')).nativeElement;
        expect(equipmentSupplies.textContent).toEqual(mockData.terminalDetails.lynkAdvantageDesc);
    });

    it('should display detail page when clicking details button', async(() => {
        component.terminalNbr = 1;
        component.ngOnChanges({
            terminalNbr: new SimpleChange(null, component.terminalNbr, false)
        });
        fixture.detectChanges();
        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        component.ngOnInit();
        fixture.detectChanges();
        bDetail.click();     
        fixture.detectChanges();
        expect(component.displayDetail).toBeTruthy();
    }));

});

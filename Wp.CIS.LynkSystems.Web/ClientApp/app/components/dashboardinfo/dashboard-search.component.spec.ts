/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { DebugElement, NO_ERRORS_SCHEMA } from '@angular/core'
import { TestBed, async, ComponentFixture, fakeAsync, tick } from '@angular/core/testing';
import { Http, ConnectionBackend, RequestOptions, HttpModule, BaseRequestOptions } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { Router, RouterModule, ActivatedRoute} from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { MockBackend } from '@angular/http/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { DashBoardSearch } from './dashboard-search.component';
import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { Observable } from 'rxjs/Observable';
import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';
import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory, MerchantProfileData
} from '../../models/dashboard.model';

import { ErrorService } from '../../services/error.service';
import { NotificationService } from '../../services/notification.service';


import {
    DropdownModule
} from 'primeng/primeng';

import { LidPrimaryKeyCacheEventsService } from '../../services/common/lid-primary-key-cache-events.service';
import { LidPrimeyKeyEventModel } from '../../models/common/lid-primary-key-event.model';


export class LidPrimaryKeyCacheEventsServiceMock {

}

export class DashboardInfoServiceMock {

    public getDashboardInfo(lidtype: LidTypesEnum, lid: string): Observable<DashboardInfo> {

        let customerProfile: CustomerProfile = {
            customerID: 1,
            description: "test description",
            activationDt: new Date(),
            statusIndicator: 3,
            legalType: 4,
            businessEstablishedDate: "businessEstablishedDate",
            lynkAdvantageDate: new Date(),
            customerNbr: "1",
            classCode: 5,
            sensitivityLevel: 0,
            stmtTollFreeNumber: "",
            deactivationDt: new Date(),
            legalDesc: "6",
            senseLvlDesc: "7",
            statDesc: "",
            lynkAdvantage: 8,
            pinPadPlus: 9,
            giftLynk: 10,
            rewardsLynk: 11,
            demoID: 12,
            custName: "",
            custContact: "",
            prinID: 13,
            prinName: "",
            prinAddress: "",
            prinCity: "",
            prinState: "",
            prinZipcode: "",
            prinSSN: "",
            custFederalTaxID: "",
            irsverificationStatus: 14,
            propHasEmployees: 15
        };

        let groupInfo: GroupInfo = {
            groupID: 1,
            groupName: "",
            groupType: "",
            statusIndicator: ""
        };

        let actvServices: ActiveServices = {
            billMtdDesc: "",
            billingMethodType: 1,
            chkName: "",
            externalTermID: "",
            lastProcessingDt: "",
            openCase: 2,
            sicDesc: "",
            laDesc: "",
            authProcessorDesc: "",
            softDesc: "",
            activeServiesDesc: ""
        };

        let merchInfo: IMerchantInfo = {
            merchFedTaxID:'taxid',
            merchantId: 1,
            customerID: 2,
            activationDt: new Date(),
            sicCode: 3,
            industryType: 4,
            merchantNbr: "",
            acquiringBankId: 6,
            programType: 7,
            statusIndicator: 8,
            fnsNbr: "",
            benefitType: 9,
            riskLevelID: 10,
            merchantType: 11,
            internetURL: "",
            deactivationDt: new Date(),
            incrementalDt: new Date(),
            thresholdDt: new Date(),
            brandID: 13,
            sicDesc: "",
            merchantClass: "",
            riskLevel: "",
            statDesc: "",
            indTypeDesc: "",
            mchName: "",
            mchAddress: "",
            mchCity: "",
            mchState: "",
            mchZipCode: "",
            mchPhone: "",
            mchContact: "",
            acquiringBank: ""
        };

        let termProfile: TerminalProfileData = {

            terminalId: "",
            merchantId: 1,
            CardType: 2,
            AccountNbr: 3,
            DiscountRate: 4,
            CustomerID: 5,
            ActivationDt: new Date(),
            SicCode: 6,
            IndustryType: 7,
            MerchantNbr: "",
            AcquiringBankId: 8,
            ProgramType: 9,
            StatusIndicator: 10,
            FNSNbr: 11,
            BenefitType: 12,
            RiskLevelID: 13,
            MerchantType: 14,
            InternetURL: "",
            DeactivationDt: new Date(),
            IncrementalDt: new Date(),
            ThresholdDt: new Date(),
            BrandID: 16,
            SicCodeDesc: "",
            AcqBankNameAddressID: 17,
            AcqBankDesc: "",
            IndustryTypeDesc: "",
            ProgramTypeDesc: "",
            DescriptorCd: 18,
            VisaIndustryType: 19,
            HighRiskInd: 20,
            BeneTypeDesc: "",
            StatDesc: "",
            MerchantClass: "",
            RiskLevel: 21,
            RiskLevelDesc: "",
            ChkAcctNbr: 22,
            FederalTaxID: 23,
            StateTaxCode: 24,
            MerchTypeDesc: "",
            SubIndGrpID: 26,
            SubIndGrpDesc: "",
            MVV1: "",
            MVV2: "",
            StoreNbr: 28,
            IRSVerificationStatus: 29,
            CutOffTime: 30
        };

        let dashboardInfo: DashboardInfo = {

            custProfile: customerProfile,
            groupInfo: groupInfo,
            actvServices: actvServices,
            merchInfo: merchInfo,
            termProfile: termProfile,
            demographicsInfo: [],
            demographicsInfoCust: [],
            demographicsInfoMerch: [],
            demographicsInfoTerm: [],
            merchantsList: [],
            caseHistorysList: [],
            totalNumberOfCaseHistoryRecords: 500
        };

        let response: Observable<DashboardInfo> = Observable.of(dashboardInfo);

        return response;
    }

    public getSearchPkInfo(lidType: LidTypesEnum, lid: string): Observable<DashboardPrimaryKeysModel> {

        let primaryKeysModel: DashboardPrimaryKeysModel = new DashboardPrimaryKeysModel();
        primaryKeysModel.convertedLidPk = 1;
        primaryKeysModel.customerID = 2;
        primaryKeysModel.lidType = 3;
        primaryKeysModel.merchantID = 5;
        primaryKeysModel.recordFound = true;
        primaryKeysModel.terminalNbr = 6

        let response: Observable<DashboardPrimaryKeysModel> = Observable.of(primaryKeysModel);

        return response;
    }
}

describe('Dashboard Search Component test', () => {

    let fixture: ComponentFixture<DashBoardSearch>;
    var component: DashBoardSearch;
    let debugElement: DebugElement;
    let element: HTMLElement;

    beforeEach(() => {

        TestBed.resetTestEnvironment();


        // First, initialize the Angular testing environment.
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );

        TestBed.configureTestingModule({
            declarations: [DashBoardSearch],
            imports: [RouterTestingModule,
                HttpModule,
                FormsModule,
                ReactiveFormsModule,
                BrowserAnimationsModule,
                DropdownModule
            ],
            providers: [
                FormBuilder,
                MockBackend,
                Http,
                {
                    provide: DashboardInfoService,
                    useClass: DashboardInfoServiceMock
                },
                {
                    provide: LidPrimaryKeyCacheEventsService,
                    useClass: LidPrimaryKeyCacheEventsServiceMock
                },
                BaseRequestOptions,
                {
                    provide: Http,
                    useFactory: (backend: ConnectionBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                },
                ErrorService,
                NotificationService
            ],
            schemas: [NO_ERRORS_SCHEMA]
        });

    });

    beforeEach(() => {

        fixture = TestBed.createComponent(DashBoardSearch);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('should have no errors when customer id has 6 numbers', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.CustomerNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("click event");
        event.value = LidTypesEnum.CustomerNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidCustomerNbrMsg: DebugElement = debugElement.query(By.css('#invalidCustomerNbrMsg'));
        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#customerIdMoreThan10Msg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericValuesCustomerMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidCustomerNbrMsg).toBeFalsy();
        expect(customerIdMoreThan10Msg).toBeFalsy();
        expect(onlyNumericValuesCustomerMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

  
    it('should display error when customer id has 5 characters', () => {        

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.CustomerNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("click event");
        event.value = LidTypesEnum.CustomerNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
                                ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidCustomerNbrMsg: DebugElement = debugElement.query(By.css('#invalidCustomerNbrMsg'));
        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#customerIdMoreThan10Msg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericValuesCustomerMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidCustomerNbrMsg).toBeTruthy();
        expect(customerIdMoreThan10Msg).toBeFalsy();
        expect(onlyNumericValuesCustomerMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);
    });

    it('should have no errors when customer id has 10 numbers', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.CustomerNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("click event");
        event.value = LidTypesEnum.CustomerNbr;
        component.searchTypeChangedHandler(event);
 
        component
            ._lidControl
            .setValue("1000123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidCustomerNbrMsg: DebugElement = debugElement.query(By.css('#invalidCustomerNbrMsg'));
        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#customerIdMoreThan10Msg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericValuesCustomerMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidCustomerNbrMsg).toBeFalsy();
        expect(customerIdMoreThan10Msg).toBeFalsy();
        expect(onlyNumericValuesCustomerMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should display error when customer id has non numeric character', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.CustomerNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.CustomerNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12345a");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidCustomerNbrMsg: DebugElement = debugElement.query(By.css('#invalidCustomerNbrMsg'));
        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#customerIdMoreThan10Msg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericValuesCustomerMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidCustomerNbrMsg).toBeFalsy();
        expect(customerIdMoreThan10Msg).toBeFalsy();
        expect(onlyNumericValuesCustomerMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when customer id when longer than 10 characters', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.CustomerNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.CustomerNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("10001234567");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidCustomerNbrMsg: DebugElement = debugElement.query(By.css('#invalidCustomerNbrMsg'));
        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#customerIdMoreThan10Msg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericValuesCustomerMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidCustomerNbrMsg).toBeFalsy();
        expect(customerIdMoreThan10Msg).toBeTruthy();
        expect(onlyNumericValuesCustomerMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);
    });

    it('should display no error when merchant id is 15 numbers', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.MerchantNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.MerchantNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("123456789012345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let customerIdMoreThan10Msg: DebugElement = debugElement.query(By.css('#merchantNbr15CharsMsg'));
        let onlyNumericValuesCustomerMsg: DebugElement = debugElement.query(By.css('#onlyNumericCharsMerchantIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(customerIdMoreThan10Msg).toBeFalsy();
        expect(onlyNumericValuesCustomerMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
    });

    it('should display error when merchant id is less than 15 numbers', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.MerchantNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.MerchantNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12345678901234");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let merchantNbr15CharsMsg: DebugElement = debugElement.query(By.css('#merchantNbr15CharsMsg'));
        let onlyNumericCharsMerchantIdMsg: DebugElement = debugElement.query(By.css('#onlyNumericCharsMerchantIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(merchantNbr15CharsMsg).toBeTruthy();
        expect(onlyNumericCharsMerchantIdMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);
        
    });

    it('should display error when merchant id is greater than 15 numbers', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.MerchantNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.MerchantNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("1234567890123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let merchantNbr15CharsMsg: DebugElement = debugElement.query(By.css('#merchantNbr15CharsMsg'));
        let onlyNumericCharsMerchantIdMsg: DebugElement = debugElement.query(By.css('#onlyNumericCharsMerchantIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(merchantNbr15CharsMsg).toBeTruthy();
        expect(onlyNumericCharsMerchantIdMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);
        
    });

    it('should display error when merchant id has a non numeric character', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.MerchantNbr);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.MerchantNbr;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12345678901234a");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let merchantNbr15CharsMsg: DebugElement = debugElement.query(By.css('#merchantNbr15CharsMsg'));
        let onlyNumericCharsMerchantIdMsg: DebugElement = debugElement.query(By.css('#onlyNumericCharsMerchantIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(merchantNbr15CharsMsg).toBeFalsy();
        expect(onlyNumericCharsMerchantIdMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);
        
    });

    it('should not display error when terminal id has is 8 characters starting with lyk', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lyk12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should not display error when terminal id has is 8 characters starting with LYK', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("LYK12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should not display error when terminal id has is 8 characters starting with LyK', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("LyK12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should not display error when terminal id has is 8 characters starting with LK', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("LK123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should not display error when terminal id has is 8 characters starting with lk', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lk123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);

    });

    it('should not display error when terminal id has is 8 characters starting with lK', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lK123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let searchSubmit: any = debugElement.query(By.css('#searchSubmit')).nativeElement;

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(component._lidControl.value).toEqual("lK123456");

    });

    it('should not display error when terminal id has is 2 numbers', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        submitButton.triggerEventHandler("click", null);

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(spy).toHaveBeenCalledWith(["/dashboardinfo/", LidTypesEnum.TerminalID, "LYK00012"]);
        expect(component._lidControl.value).toEqual("LYK00012");

    });

    it('should not display error when terminal id has is 3 numbers', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("123");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        submitButton.triggerEventHandler("click", null);

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(spy).toHaveBeenCalledWith(["/dashboardinfo/", LidTypesEnum.TerminalID, "LYK00123"]);
        expect(component._lidControl.value).toEqual("LYK00123");

    });

    it('should not display error when terminal id has is 4 numbers', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("1234");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        submitButton.triggerEventHandler("click", null);

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(spy).toHaveBeenCalledWith(["/dashboardinfo/", LidTypesEnum.TerminalID, "LYK01234"]);
        expect(component._lidControl.value).toEqual("LYK01234");

    });


    it('should not display error when terminal id has is 5 numbers', () => {


        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        submitButton.triggerEventHandler("click", null);

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(spy).toHaveBeenCalledWith(["/dashboardinfo/", LidTypesEnum.TerminalID, "LYK12345"]);
        expect(component._lidControl.value).toEqual("LYK12345");

    });

    it('should not display error when terminal id has is 6 numbers', () => {

        let router = TestBed.get(Router);
        let spy = spyOn(router, 'navigate');

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(true);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        submitButton.triggerEventHandler("click", null);

        expect(invalidMerchantNbrMsg).toBeFalsy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(false);
        expect(spy).toHaveBeenCalledWith(["/dashboardinfo/", LidTypesEnum.TerminalID, "LK123456"]);
        expect(component._lidControl.value).toEqual("LK123456");

    });

    it('should display error when terminal id has 1 digit', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("1");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id starts with lt', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lt123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id ends with a letter', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lk12345a");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id has a letter in the middle', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lk12a456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id 7 characters', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lk12345");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id 7 characters and starts with lyk', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lyk1234");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id 9 characters and starts with lyk', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);

        component
            ._lidControl
            .setValue("lyk123456");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

    it('should display error when terminal id 9 characters and starts with lyk', () => {

        component
            ._selectedValueControl
            .setValue(LidTypesEnum.TerminalID);

        component
            ._selectedValueControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._selectedValueControl
            .markAsDirty();

        fixture.detectChanges();

        let event: any = new Event("change");
        event.value = LidTypesEnum.TerminalID;
        component.searchTypeChangedHandler(event);
        let selectedValueControl: DebugElement = debugElement.query(By.css('#selectedValue'));

        component
            ._lidControl
            .setValue("lk1234567");

        component
            ._lidControl
            .updateValueAndValidity({ onlySelf: false, emitEvent: true });

        component
            ._lidControl
            .markAsDirty();

        fixture.detectChanges();

        let isValid: boolean = component
            ._lidControl
            .valid;

        expect(isValid).toEqual(false);

        let invalidMerchantNbrMsg: DebugElement = debugElement.query(By.css('#invalidTerminalIdMsg'));
        let submitButton: DebugElement = debugElement.query(By.css('#searchSubmit'));
        let searchSubmit: any = submitButton.nativeElement;

        expect(invalidMerchantNbrMsg).toBeTruthy();
        expect(searchSubmit.hasAttribute('disabled')).toEqual(true);

    });

});
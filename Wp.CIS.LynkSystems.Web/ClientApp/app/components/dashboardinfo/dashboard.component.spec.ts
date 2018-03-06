
/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { DebugElement, NO_ERRORS_SCHEMA } from '@angular/core'
import { TestBed, async, ComponentFixture, fakeAsync, tick } from '@angular/core/testing';
import { Http, ConnectionBackend, RequestOptions, HttpModule, BaseRequestOptions } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { By } from '@angular/platform-browser';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { MockBackend } from '@angular/http/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard.component';
import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
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

import { ActivatedRouteStub } from '../../../test/router-stub';

import {
    DropdownModule
} from 'primeng/primeng';

import { LidPrimaryKeyCacheEventsService } from '../../services/common/lid-primary-key-cache-events.service';
import { LidPrimeyKeyEventModel } from '../../models/common/lid-primary-key-event.model';

import { CustomerPrimaryKeyInfoModel } from '../../models/common/customer-primary-key-info.model';
import { MerchantPrimaryKeyInfoModel } from '../../models/common/merchant-primary-key-info.model';
import { TerminalPrimaryKeyInfoModel } from '../../models/common/terminal-primary-key-info.model';

//export class ActivatedRouteStubTim {
    
    //get params(): any {
    //    return Observable.of({ lidType: 3, lidTypeValue: 123456 });
    //}

    

//}

//var ActivatedRouteStubTim = { params: Observable.of({ lidType: 3, lidTypeValue: 123456 }) };

export class ActivatedRouteStubTim extends ActivatedRoute {
    constructor() {
        //super(null, null, null, null, null);
        super();
        this.params = Observable.of({ lidType: 3, lidTypeValue: 123456 });
    }
}


export class LidPrimaryKeyCacheEventsServiceMock {

    public addTerminalInfoToCache(terminalInfo: TerminalPrimaryKeyInfoModel): void {

    };

    public addMerchantInfoToCache(merchantInfo: MerchantPrimaryKeyInfoModel): void {

     };

    public addCustomerInfoToCache(customerInfo: CustomerPrimaryKeyInfoModel): void {

    };

    public getTerminalInfoFromCache(terminalId: string): TerminalPrimaryKeyInfoModel {

        return null;
    }

    public getMerchantInfoFromCache(merchantNbr: string): MerchantPrimaryKeyInfoModel {

        return null;
    }

    public getCustomerInfoFromCache(customerNbr: string): CustomerPrimaryKeyInfoModel {

        return null;
    }

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

describe('Dashboard Component test', () => {

    let fixture: ComponentFixture<DashboardComponent>;
    var component: DashboardComponent;
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
            declarations: [DashboardComponent],
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
                LidPrimaryKeyCacheEventsService,
                //{
                //    provide: LidPrimaryKeyCacheEventsService,
                //    useClass: LidPrimaryKeyCacheEventsServiceMock
                //},
                //{
                //    provide: ActivatedRoute,
                //    useValue: {
                //        params: Observable.of({lidType: 3, lidTypeValue: 123456})
                //    }
                //},
                //{
                //    provide: ActivatedRoute,
                //    use: ActivatedRouteStubTim
                //},
                {
                    provide: ActivatedRoute,
                    useClass: ActivatedRouteStubTim
                },
                //{
                //    provide: ActivatedRoute,
                //    useValue: new ActivatedRouteStub({ lidType: 3, lidTypeValue: 123456 })
                //},
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

        fixture = TestBed.createComponent(DashboardComponent);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('should not call the DashboarService.getSearchPkInfo() method when customer information found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 7, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let customerInfo = new CustomerPrimaryKeyInfoModel(1, "test");
        let spy: jasmine.Spy = spyOn(cacheService, 'getCustomerInfoFromCache')
            .and.returnValue(Observable.of(customerInfo));

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.CustomerNbr;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(0);
        expect(component._showMerchantComponent).toBe(false);
        expect(component._showCustomerComponent).toBe(true);
    });

    it('should call the DashboarService.getSearchPkInfo() method when customer information not found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 7, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let customerInfo = new CustomerPrimaryKeyInfoModel(1, "test");
        let spy: jasmine.Spy = spyOn(cacheService, 'getCustomerInfoFromCache')
            .and.returnValue(null);

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.CustomerNbr;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(1);
        expect(component._showMerchantComponent).toBe(false);
        expect(component._showCustomerComponent).toBe(true);
    });

    it('should not call the DashboarService.getSearchPkInfo() method when merchant information found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 6, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let merchantInfo = new MerchantPrimaryKeyInfoModel(1, "test", 2, "customer nbr");
        let spy: jasmine.Spy = spyOn(cacheService, 'getMerchantInfoFromCache')
            .and.returnValue(Observable.of(merchantInfo));

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.MerchantNbr;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(0);
        expect(component._showMerchantComponent).toBe(true);
        expect(component._showCustomerComponent).toBe(false);
    });

    it('should call the DashboarService.getSearchPkInfo() method when merchant information not found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 6, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let merchantInfo = new MerchantPrimaryKeyInfoModel(1, "test", 2, "customer nbr");
        let spy: jasmine.Spy = spyOn(cacheService, 'getMerchantInfoFromCache')
            .and.returnValue(null);

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.MerchantNbr;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(1);
        expect(component._showMerchantComponent).toBe(true);
        expect(component._showCustomerComponent).toBe(false);
    });

    it('should not call the DashboarService.getSearchPkInfo() method when terminal information found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 5, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let merchantInfo = new TerminalPrimaryKeyInfoModel(1, "test", 2, "merchant nbr", 3, "customer nbr");
        let spy: jasmine.Spy = spyOn(cacheService, 'getTerminalInfoFromCache')
            .and.returnValue(Observable.of(merchantInfo));

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.TerminalID;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(0);
        expect(component._showMerchantComponent).toBe(true);
        expect(component._showCustomerComponent).toBe(false);
    });

    it('should call the DashboarService.getSearchPkInfo() method when merchant information not found', () => {

        let actRoute: ActivatedRoute = fixture.debugElement.injector.get(ActivatedRoute);
        actRoute.params = Observable.of({ lidType: 5, lidTypeValue: 123456 });

        // Setup spy on the `getCustomerInfoFromCache` method
        let cacheService = fixture.debugElement.injector.get(LidPrimaryKeyCacheEventsService);
        let merchantInfo = new MerchantPrimaryKeyInfoModel(1, "test", 2, "customer nbr");
        let spy: jasmine.Spy = spyOn(cacheService, 'getTerminalInfoFromCache')
            .and.returnValue(null);

        let dashboardService = fixture.debugElement.injector.get(DashboardInfoService);
        let primaryKeyInfo = new DashboardPrimaryKeysModel();
        primaryKeyInfo.convertedLidPk = 1;
        primaryKeyInfo.customerID = 2;
        primaryKeyInfo.lidType = LidTypesEnum.TerminalID;
        primaryKeyInfo.merchantID = 3;
        primaryKeyInfo.recordFound = true;
        primaryKeyInfo.terminalNbr = 4;
        let spyDashboard: jasmine.Spy = spyOn(dashboardService, "getSearchPkInfo")
            .and
            .returnValue(Observable.of(primaryKeyInfo));

        fixture.detectChanges();

        expect(spyDashboard.calls.count()).toBe(1);
        expect(component._showMerchantComponent).toBe(true);
        expect(component._showCustomerComponent).toBe(false);
    });

});

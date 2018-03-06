
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
//import { DashBoardSearch } from './../dashboard-search.component';
import { DashboardTitleComponent } from './../common/dashboard-title.component';
import { DashboardInfoService } from './../../../services/dashboardinfo.service';
import { Observable } from 'rxjs/Observable';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DashboardPrimaryKeysModel } from '../../../models/dashboardInfo/dashboard-primary-keys.model';
import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory, MerchantProfileData
} from '../../../models/dashboard.model';

import { ErrorService } from '../../../services/error.service';
import { NotificationService } from '../../../services/notification.service';


import {
    DropdownModule
} from 'primeng/primeng';

import { LidPrimaryKeyCacheEventsService } from '../../../services/common/lid-primary-key-cache-events.service';
import { LidPrimeyKeyEventModel } from '../../../models/common/lid-primary-key-event.model';

describe('Dashboard Search Component test', () => {

    let fixture: ComponentFixture<DashboardTitleComponent>;
    var component: DashboardTitleComponent;
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
            declarations: [DashboardTitleComponent],
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
                //{
                //    provide: DashboardInfoService,
                //    useClass: DashboardInfoServiceMock
                //},
                //{
                //    provide: LidPrimaryKeyCacheEventsService,
                //    useClass: LidPrimaryKeyCacheEventsServiceMock
                //},
                //ConnectionBackend
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

        fixture = TestBed.createComponent(DashboardTitleComponent);
        component = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('should display the customer name field', () => {

        expect(true).toEqual(true);

        const testValue: string = "Test Customer Name";

        component.CustomerName = testValue;

        fixture.detectChanges();

        let guiField: any = debugElement.query(By.css('#customerNameField')).nativeElement;

        expect(guiField.innerText).toEqual(testValue);

    });

    it('should display the LID Record Name Field', () => {

        expect(true).toEqual(true);

        const testValue: string = "Test LID Record Name";

        component.LidRecordName = testValue;

        fixture.detectChanges();

        let guiField: any = debugElement.query(By.css('#lidRecordNameField')).nativeElement;

        expect(guiField.innerText).toEqual(testValue);

    });

    it('should display the LID ID', () => {

        expect(true).toEqual(true);

        const testValue: string = "Test LID ID";

        component.LidRecordId = testValue;

        fixture.detectChanges();

        let guiField: any = debugElement.query(By.css('#lidRecordIdField')).nativeElement;

        expect(guiField.innerText).toEqual(testValue);

    });

});

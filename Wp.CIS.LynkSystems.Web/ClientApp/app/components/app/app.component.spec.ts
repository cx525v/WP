/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { DebugElement, NO_ERRORS_SCHEMA } from '@angular/core'
import { AppComponent } from './app.component';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { Router, ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashBoardSearch } from './../dashboardinfo/dashboard-search.component';
import { NotificationService } from '../../services/notification.service';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule,
    MessagesModule, CalendarModule, SplitButtonModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar, GrowlModule, Growl } from 'primeng/primeng';

import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { Http, ConnectionBackend, RequestOptions, HttpModule } from '@angular/http';
import { AuthenticationService, AuthGuard, TokenService, User } from './../../services/Authentication/index';
import { Observable } from "rxjs/Observable";
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import '../../app.module.client';
import { LidPrimaryKeyCacheEventsService } from '../../services/common/lid-primary-key-cache-events.service';

export class LidPrimaryKeyCacheEventsServiceMock {

}
let comp: AppComponent;
let de: DebugElement;
let el: HTMLElement;
let el1: HTMLElement;
let service: TokenService;

describe('AppComponent Component', () => {
    let fixture: ComponentFixture<AppComponent>,
        comp: AppComponent,
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
                HttpModule,
                BrowserAnimationsModule,
                ReactiveFormsModule,
                NoopAnimationsModule,
                RouterTestingModule,
                GrowlModule,
                
                SplitButtonModule,
                DropdownModule],
            declarations: [AppComponent, DashBoardSearch],
            providers: [AuthenticationService,
                TokenService,
                AuthGuard,
                NotificationService,
                FormBuilder,
                ReactiveFormsModule,  
                DashboardInfoService,  
                {
                    provide: LidPrimaryKeyCacheEventsService,
                    useClass: LidPrimaryKeyCacheEventsServiceMock
                }
            ]
        });

        TestBed.overrideComponent(
            AppComponent,
            { set: { providers: [{ provide: AuthenticationService, useClass: MockService }] } }

        );

        let store = {};
        const mockLocalStorage = {
            getItem: (key: string): string => {
                return key in store ? store[key] : null;
            },
            setItem: (key: string, value: string) => {
                store[key] = `${value}`;
            },
            removeItem: (key: string) => {
                delete store[key];
            },
            clear: () => {
                store = {};
            }
        };

        spyOn(localStorage, 'getItem')
            .and.callFake(mockLocalStorage.getItem);
        spyOn(localStorage, 'setItem')
            .and.callFake(mockLocalStorage.setItem);
        spyOn(localStorage, 'removeItem')
            .and.callFake(mockLocalStorage.removeItem);
        spyOn(localStorage, 'clear')
            .and.callFake(mockLocalStorage.clear);
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(AppComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

        fixture = TestBed.createComponent(AppComponent);
        this.service = new TokenService();
        fixture.detectChanges();
        el = fixture.nativeElement.querySelector('div');

        el1 = fixture.debugElement.query(By.css('#dashSearch')).nativeElement;
    });

    it('should create the app', async(() => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
     

    it('should store the token ("WorldPay.cis.currentUser") in localStorage',
    () => {
       this.service.setAccessToken('WorldPay.cis.currentUser','sometoken');
        expect(localStorage.getItem('WorldPay.cis.currentUser')).toEqual('sometoken');
    });


    it('should return stored token from localStorage',
        () => {
            localStorage.setItem('WorldPay.cis.currentUser', 'anothertoken');
            expect(this.service.getAccessToken('WorldPay.cis.currentUser')).toEqual('anothertoken');
        });

    it('should get a token, when ngOnit of appComponent is called', () => {          
           localStorage.setItem('WorldPay.cis.currentUser', 'token');   
           comp.ngOnInit();
           fixture.detectChanges();           
           expect(localStorage.getItem('WorldPay.cis.currentUserToken')).toEqual('token1'); 
       })
});

export class MockService {
    token: string = 'token1';
    login(user: User) {
        return Observable.of(this.getToken());
    }
    getToken() {
        localStorage.setItem('WorldPay.cis.currentUserToken', this.token);
        return this.token;
    }
}


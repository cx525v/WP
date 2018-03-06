import { TestBed, async, ComponentFixture, inject } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DataTableModule } from 'primeng/primeng';
import { GlobalErrorHandler } from './globalerror.handler';
import { FakeServcie } from '../services/fake.service';
import { ErrorHandler } from '@angular/core';
import { NotificationService } from '../services/notification.service';

describe('GlobalerrorHandler Component', () => {

    beforeEach(() => {

        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );

        TestBed.configureTestingModule({
            providers: [
                FakeServcie,
                MockBackend,
                BaseRequestOptions,
                {
                    provide: Http,
                    useFactory: (backend: ConnectionBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                }
            ],
            imports: [
                FormsModule, HttpModule, DataTableModule, BrowserAnimationsModule
            ]
        });
        TestBed.configureTestingModule({
            imports: [ReactiveFormsModule]
            , providers: [NotificationService,{ provide: ErrorHandler, useClass: GlobalErrorHandler }],
        });
    });

    it('should call fake service', async(inject([Http], (http) => {
        let service = TestBed.get(FakeServcie);
        const spy = spyOn(console, 'log');

        service.fakeCall().subscribe(null, error => {
            expect(spy).toHaveBeenCalledWith(error);
        });
    })));

    it('should call service with 401 error', async(inject([Http], (http) => {
        let service = TestBed.get(FakeServcie);
        let mockBackend = TestBed.get(MockBackend);
        let mockError = { status: 401, body: 'error' };
        mockBackend.connections.subscribe((c: any) => {
            c.mockError(new Response(new ResponseOptions(mockError)));
        });

        service.fakeCall().subscribe(null, error => {
            expect(error.status).toEqual(401);
            expect(error._body).toEqual('error');
        });
    })));

    it('should call service with type error', async(inject([Http], (http) => {
        let service = TestBed.get(FakeServcie);
        let mockBackend = TestBed.get(MockBackend);
        mockBackend.connections.subscribe((c: any) => {
            c.mockError(new TypeError("fakeerror"));
        });

        service.fakeCall().subscribe(null, error => {
            expect(error.toString()).toEqual('TypeError: fakeerror');
        });
    })));
});
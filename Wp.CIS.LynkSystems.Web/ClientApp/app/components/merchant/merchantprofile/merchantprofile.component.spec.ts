/// <reference path="../../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { MerchantProfileComponent } from './merchantprofile.component';
import { TestBed, async, ComponentFixture, inject } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MerchantProfileServcie } from "../../../services/merchantprofile.service";
import { Router, RouterModule } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { TabViewModule} from 'primeng/primeng';
let fixture: ComponentFixture<MerchantProfileComponent>;

describe('MerchantProfile Component', () => {
    beforeEach(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );

        TestBed.configureTestingModule({
            providers: [
                MerchantProfileComponent,
                MerchantProfileServcie,
                MockBackend,
                BaseRequestOptions,
                {
                    provide: Http,
                    useFactory: (backend: ConnectionBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                }
            ],
            imports: [
                FormsModule, HttpModule, TabViewModule
            ]
        });
        TestBed.configureTestingModule({ imports: [ReactiveFormsModule], declarations: [MerchantProfileComponent] });///Check for forms error
        fixture = TestBed.createComponent(MerchantProfileComponent);
        fixture.detectChanges();
    });       
    it('should do XHR', async(inject([Http], (http) => {
        let merchService = TestBed.get(MerchantProfileServcie);
        let mockBackend = TestBed.get(MockBackend);

        mockBackend.connections.subscribe((c: any) => {
            c.mockRespond(new Response(new ResponseOptions({ body: '["value11", "value22"]', status: 200 })));
        });
        //back end set, now call any function which calls http.get with mock response
        merchService.getMerchantProfileById(1)
            .subscribe(res => {
                expect(res.json()[0]).toBe("value11");
                //console.log("test");
                expect(res.json()[1]).toBe("value22");
                expect(res.status.toString()).toBe("200");
            });
    })));  
    it('should do XHR', async(inject([Http], (http) => {
        let merchService = TestBed.get(MerchantProfileServcie);
        let mockBackend = TestBed.get(MockBackend);

        mockBackend.connections.subscribe((c: any) => {
            c.mockRespond(new Response(new ResponseOptions({ body: '["value1", "value2"]' })));
        });
        //back end set, now call any ip with mock response
        http.get('http://localhost:65517/api/values').subscribe(res => {
            expect(res.json()[0]).toBe("value1");
            expect(res.json()[1]).toBe("value2");
        });
    })));
});

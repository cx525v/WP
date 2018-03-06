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
import { BusinessInfoComponent } from './business-info.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { BusinessInfo } from './../../../models/dashboardInfo/businessInfo.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'

const mockData = {
    acquiringBank: "",
    custFederalTaxID:"testTaxID",
    ebtbenefitType:"",
    industry:"Corporation",
    sic:"",
    status:"Active"
};
@Component({
    selector: 'test-cmp',
    template: '<business-info [businessInfo]="mockBusinessInfoData"></business-info>',
})
class TestCmpWrapper {
    mockBusinessInfoData: BusinessInfo = mockData;
}


describe('Component: AccountInfoComponent', () => {

    let component: BusinessInfoComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async () => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                BusinessInfoComponent
            ],
            schemas: [CUSTOM_ELEMENTS_SCHEMA]
        })
            .compileComponents();

        fixture = TestBed.createComponent(TestCmpWrapper);
        component = fixture.debugElement.children[0].componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should bind businessInfocustFederalTaxID data', () => {
        const businessInfocustFederalTaxID = fixture.debugElement.query(By.css('#businessInfocustFederalTaxID')).nativeElement;
        expect(businessInfocustFederalTaxID.textContent).toEqual(mockData.custFederalTaxID);
    });

    it('should bind businessInfostatus data', () => {
        const businessInfostatus = fixture.debugElement.query(By.css('#businessInfostatus')).nativeElement;
        expect(businessInfostatus.textContent).toEqual(mockData.status);
    });

   
});



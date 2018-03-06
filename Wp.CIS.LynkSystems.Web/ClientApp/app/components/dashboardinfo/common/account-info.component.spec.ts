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
import { AccountInfoComponent } from './account-info.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { AccountInfo } from './../../../models/dashboardInfo/accountinfo.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'

const mockData = {
    name: "Tier 2 Account",
    prinAddress:"5151 Glenwood Ave",
    prinCity:"Raleigh",
    prinState:"NC",
    prinZipcode:"27612"
};
@Component({
    selector: 'test-cmp',
    template: '<account-info [accountInfo]="mockAccountInfoData"></account-info>',
})
class TestCmpWrapper {
   mockAccountInfoData: AccountInfo = mockData;  
}


describe('Component: AccountInfoComponent', () => {

    let component: AccountInfoComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async () => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                AccountInfoComponent
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

    it('should bind name data', () => {
        const accountInfoname = fixture.debugElement.query(By.css('#accountInfoname')).nativeElement;
        expect(accountInfoname.textContent).toEqual(mockData.name);
    });

    it('should bind Address data', () => {
        const accountInfoprinAddress = fixture.debugElement.query(By.css('#accountInfoprinAddress')).nativeElement;
        expect(accountInfoprinAddress.textContent).toEqual(mockData.prinAddress);
    });

    it('should bind City data', () => {
        const accountInfoprinCity = fixture.debugElement.query(By.css('#accountInfoprinCity')).nativeElement;
        expect(accountInfoprinCity.textContent).toEqual(mockData.prinCity);
    });

    it('should bind State data', () => {
        const accountInfoprinState = fixture.debugElement.query(By.css('#accountInfoprinState')).nativeElement;
        expect(accountInfoprinState.textContent).toEqual(mockData.prinState);
    });

    it('should bind Zipcode data', () => {
        const accountInfoprinZipcode = fixture.debugElement.query(By.css('#accountInfoprinZipcode')).nativeElement;
        expect(accountInfoprinZipcode.textContent).toEqual(mockData.prinZipcode);
    });
  
});



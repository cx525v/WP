import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick} from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting} from "@angular/platform-browser-dynamic/testing";
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
import { SensitivityPartnerComponent } from './sensitivity-partner.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { SensitivityPartner } from './../../../models/dashboardInfo/sensitivitypartner.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'

const mockData= {
    group: 'Retail - Tier 1 / Tier 2 (114972)',
    partnerRelationship: 'National Sales Vertical Market',
    senseLvlDesc: 'Tier 2'
};
@Component({
    selector: 'test-cmp',
    template: '<sensitivity-partner [sensitivityPartner]="mockSensitivityPartnerData"></sensitivity-partner>',
})
class TestCmpWrapper {
    mockSensitivityPartnerData: SensitivityPartner = mockData;   
}


describe('Component: SensitivityPartnerComponent', () => {

    let component: SensitivityPartnerComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async() => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                SensitivityPartnerComponent
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

    it('should bind group data', () => {      
        const group = fixture.debugElement.query(By.css('#spgroup')).nativeElement;
        expect(group.textContent).toEqual(mockData.group);
    });

    it('should bind partnerRelationship data', () => {
        const partnerRelationship = fixture.debugElement.query(By.css('#sppartnerRelationship')).nativeElement;
        expect(partnerRelationship.textContent).toEqual(mockData.partnerRelationship);
    });

    it('should bind senseLvlDesc data', () => {
        const senseLvlDesc = fixture.debugElement.query(By.css('#spsenseLvlDesc')).nativeElement;
        expect(senseLvlDesc.textContent).toEqual(mockData.senseLvlDesc);
    });
});



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

import { TerminalDetailsDialogComponent } from './terminal-details-dialog.component';

import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TerminalDetail, SensitivityInfo } from './../../../models/dashboardInfo/terminal.model';
import { DatePipe } from '@angular/common';
import { DateTimeFormatPipe } from '../../../pipes/date.pipe';

const detail: TerminalDetail = { 
    activationDt: '09/13/2004 12:36:09',
    activeServiesDesc: 'credit',
    billMtdDesc: 'bill method',
    deactivationDt: '',
    softDesc: 'Software 1',
    statDesc: 'Active',
    terminalEquipment: 'terminal equipment',
    tid: 'test',
    terminalType:'terminalType1'
}
const sensitivityInfoDefault: SensitivityInfo = {
    senLevelDesc: "default",
    sensitivityLevel: 0
};

const sensitivityInfo: SensitivityInfo = {
    senLevelDesc: "level 1",
    sensitivityLevel: 1
};

@Component({
    selector: 'test-cmp',
    template: '<terminal-details-dialog [detail]="detail"></terminal-details-dialog>',
})
class TestCmpWrapper {
    detail: TerminalDetail = detail;
}

describe('Component: TerminalDetailsDialogComponent', () => {

    let component: TerminalDetailsDialogComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async () => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                TerminalDetailsDialogComponent,
                DateTimeFormatPipe              
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

    it('should display terminalNbr', () => {
        const lTerminal = fixture.debugElement.query(By.css('#lTerminal')).nativeElement;
        expect(lTerminal.textContent).toEqual('TERMINAL: ' + detail.tid);
    });

    it('should display terminalEquipment', () => {
        const terminalEquipment = fixture.debugElement.query(By.css('#terminalEquipment')).nativeElement;
        expect(terminalEquipment.textContent).toEqual(detail.terminalEquipment);
    });
    it('should display terminalType', () => {
        const terminalType = fixture.debugElement.query(By.css('#terminalType')).nativeElement;
        expect(terminalType.textContent).toEqual(detail.terminalType);
    });
    it('should display softDesc', () => {
        const softDesc = fixture.debugElement.query(By.css('#softDesc')).nativeElement;
        expect(softDesc.textContent).toEqual(detail.softDesc);
    });
    it('should display statDesc', () => {
      
        const statDesc = fixture.debugElement.query(By.css('#statDesc')).nativeElement;
        expect(statDesc.textContent).toEqual(detail.statDesc);
    });

    it('should display activationDt', () => {
        let pipe = new DateTimeFormatPipe('en');
        const activationDt = fixture.debugElement.query(By.css('#activationDt')).nativeElement;
        expect(activationDt.textContent).toEqual(pipe.transform(detail.activationDt));
    });

    it('should display deactivationDt', () => {
        let pipe = new DateTimeFormatPipe('en');
        const deactivationDt = fixture.debugElement.query(By.css('#deactivationDt')).nativeElement;
        expect(deactivationDt.textContent).toEqual(pipe.transform(detail.deactivationDt));
    });

    it('should display activeServiesDesc', () => {
        const activeServiesDesc = fixture.debugElement.query(By.css('#activeServiesDesc')).nativeElement;
        expect(activeServiesDesc.textContent).toContain(detail.activeServiesDesc);
    });

    it('should display billMtdDesc', () => {
        const billMtdDesc = fixture.debugElement.query(By.css('#billMtdDesc')).nativeElement;
        expect(billMtdDesc.textContent).toEqual(detail.billMtdDesc);
    });

    it('should display sensitivity level info if SensitivityLevel not equal 0', () => {
        component.sensitivityInfo = sensitivityInfo;
        fixture.detectChanges();
        const senLevelDesc = fixture.debugElement.query(By.css('#senLevelDesc')).nativeElement;
        expect(senLevelDesc.textContent).toEqual('/'+sensitivityInfo.senLevelDesc);
    });

    it('should not display sensitivity level info if SensitivityLevel equals 0', () => {
        component.sensitivityInfo = sensitivityInfoDefault;
        fixture.detectChanges();
        const senLevelDesc = fixture.debugElement.query(By.css('#senLevelDesc'));
        expect(senLevelDesc).toBeNull();
    });
});

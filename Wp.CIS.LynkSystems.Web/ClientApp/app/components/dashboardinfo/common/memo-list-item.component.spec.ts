
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
import { MemoListItemComponent } from './memo-list-item.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { MemoInfo } from '../../../models/dashboardInfo/memo.model';

const mockData: MemoInfo = {
    categoryDesc: 'IRS',
    memo:'testmemo'

};
@Component({
    selector: 'test-cmp',
    template: '<memo-list-item [item]="data"> </memo-list-item>',
})
class TestCmpWrapper {
   data: MemoInfo = mockData;
}


describe('Component: MemoListItemComponent', () => {

    let component: MemoListItemComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async () => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                MemoListItemComponent
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

    it('should bind memo title', () => {      
        const lTitle = fixture.debugElement.query(By.css('#lTitle')).nativeElement;
        expect(lTitle.textContent).toEqual(mockData.categoryDesc);
    });

    it('should bind memo content', () => {    
        const sMemo = fixture.debugElement.query(By.css('#sMemo')).nativeElement;
        expect(sMemo.textContent).toEqual(mockData.memo);
    });

    it('should show detail button', () => {
        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        expect(bDetail).toBeTruthy();
    });
   
    it('should show detail event is fired when detail button is clicked', () => {
        spyOn(component, 'displayDetails');
        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        bDetail.click();
        fixture.detectChanges();
        expect(component.displayDetails).toHaveBeenCalled();      
    });

    it('should show detail when detail button is clicked', () => {

        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        bDetail.click();
        fixture.detectChanges();
        expect(component.showDetail).toBe(true);
    });


    it('should bind memo title in dialog when detail button is clicked', () => {
        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        bDetail.click();
        fixture.detectChanges();
        const dltitle = fixture.debugElement.query(By.css('#dltitle')).nativeElement;
        expect(dltitle.textContent).toEqual(mockData.categoryDesc);
    });

    it('should bind memo content in dialog when detail button is clicked', () => {
        const bDetail = fixture.debugElement.query(By.css('#bDetail')).nativeElement;
        bDetail.click();
        fixture.detectChanges();
        const dsMemo = fixture.debugElement.query(By.css('#dsMemo')).nativeElement;
        expect(dsMemo.textContent).toEqual(mockData.memo);
    });  
    
});

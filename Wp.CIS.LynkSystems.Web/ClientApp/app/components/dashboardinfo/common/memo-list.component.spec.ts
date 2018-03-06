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
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { MemoInfo } from '../../../models/dashboardInfo/memo.model';
import { TwoColumnPipe } from '../../../pipes/twocolumn.pipe';
import { MemoListComponent } from './memo-list.component';
import { MemoListItemComponent } from './memo-list-item.component';

const mockData: MemoInfo[] =
    [
        {
            categoryDesc: 'IRS1',
            memo: 'testmemo1'
        },
        {
            categoryDesc: 'IRS2',
            memo: 'testmemo2'
        },
        {
            categoryDesc: 'IRS3',
            memo: 'testmemo3'
        },
    ]
@Component({
    selector: 'test-cmp',
    template: '<memo-list [memos]="data"></memo-list>',
})
class TestCmpWrapper {
    data: MemoInfo[] = mockData;
}


describe('Component: MemoListComponent', () => {

    let component: MemoListComponent;
    let fixture: ComponentFixture<TestCmpWrapper>;

    beforeEach(async () => {
        TestBed.configureTestingModule({
            declarations: [
                TestCmpWrapper,
                MemoListComponent,
                TwoColumnPipe,
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

    it('should display memo list', () => {
        const lists = fixture.debugElement.queryAll(By.css('memo-list-item'));
        console.log(lists.length);
        expect(lists.length).toEqual(mockData.length);     
    });

});
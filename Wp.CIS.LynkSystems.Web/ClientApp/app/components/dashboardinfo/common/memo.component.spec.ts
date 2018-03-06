import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { Location } from '@angular/common';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DebugElement, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import { MemoComponent } from './memo.component';
import { MemoListComponent } from './memo-list.component';
import { MemoListItemComponent } from './memo-list-item.component';
import { TEST_BASESERVER } from '../../../shared/spec.global.component';
import { Component, OnInit, OnDestroy, Input } from '@angular/core'
import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';
import { TwoColumnPipe } from '../../../pipes/twocolumn.pipe';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule, AccordionModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar, TabPanel, TabView, AccordionTab } from 'primeng/primeng';

import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { MemoService} from '../../../services/dashboardInfo/memo.service';
import { MemoInfo, MemoList } from '../../../models/dashboardInfo/memo.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { TerminalEquipment } from '../../../models/dashboardInfo/terminalequipment.model'
import { ViewChildren, ViewChild } from '@angular/core';

const termMemos: MemoInfo[] = [
    {
        memo: 'termMemo1',
        categoryDesc: 'termTitle1'
    },
    {
        memo: 'termMemo2',
        categoryDesc: 'termTitle2'
    }
];

const groupMemos: MemoInfo[] = [
    {
        memo: 'groupMemo1',
        categoryDesc: 'groupTitle1'
    }, 
];

const merchMemos: MemoInfo[] = [
    {
        memo: 'merchMemo1',
        categoryDesc:'merchTitle1'
    },
    {
        memo: 'merchMemo2',
        categoryDesc: 'merchTitle2'
    }
];

const custMemos: MemoInfo[] = [
    {
        memo: 'custMemo1',
        categoryDesc: 'custTitle1'
    },
    {
        memo: 'custMemo2',
        categoryDesc: 'custTitle2'
    },
     {
        memo: 'custMemo3',
        categoryDesc: 'custTitle3'
    }
];

const mockData: MemoList ={
    customerMemo: custMemos,
    merchMemo: merchMemos,
    groupMemo: groupMemos,
    termMemo: termMemos
}


export class MockService extends MemoService {
    getMemos(lidType: number, lidIdPk: number) {
        return Observable.of(mockData);
    }
}
describe('MemoComponent', () => {
    let fixture: ComponentFixture<MemoComponent>,
        comp: MemoComponent,
        debugElement: DebugElement,
        element: HTMLElement;

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpModule, BrowserAnimationsModule, ReactiveFormsModule, NoopAnimationsModule, AccordionModule, DialogModule],                
            declarations: [
                MemoComponent,
                MemoListComponent,
                MemoListItemComponent,
                TwoColumnPipe],
            providers: [
                DashboardEventsService,
                MemoService
            ]
        });
        TestBed.overrideComponent(
            MemoComponent,
            { set: { providers: [{ provide: MemoService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(MemoComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create', () => {
        expect(comp).toBeTruthy();
    });

    it('should display memo tab', () => {
        const tab = fixture.debugElement.query(By.css('p-accordionTab'));
        expect(tab.attributes['header']).toEqual('Memos');
    });  
    it('it should get the terminal demo with dashboardeventservice', () => {

        let service: DashboardEventsService = TestBed.get(DashboardEventsService);

        let termEquip: TerminalEquipment = new TerminalEquipment();
        termEquip.terminalID = "LK429472";
        termEquip.terminalNbr = 1;
       
        comp.searchParamsPk = {
            customerId: 1,
            lidIdPk: 1,
            lidType: 1,
            merchantId: 1,
            terminalNbr:1
        };

        fixture.detectChanges()

        service.announceTerminalEquipmentChange(termEquip);

        fixture.detectChanges()

        expect(comp.memoList.customerMemo.length).toEqual(mockData.customerMemo.length);
        expect(comp.memoList.groupMemo.length).toEqual(mockData.groupMemo.length);
        expect(comp.memoList.termMemo.length).toEqual(mockData.termMemo.length);
        expect(comp.memoList.merchMemo.length).toEqual(mockData.merchMemo.length);
    });  

    it('should show all memos', () => {
        comp.searchParamsPk = {
            customerId: 1,
            lidIdPk: 1,
            lidType: 3,
            merchantId: 1,
            terminalNbr: 1
        };
        comp.getAllMemoCount();
        fixture.detectChanges()
        expect(comp.allMemos.length).toEqual(mockData.groupMemo.length + mockData.customerMemo.length + mockData.merchMemo.length + mockData.termMemo.length);
    });


    it('should display customer memos', () => {
        comp.searchParamsPk = {
            customerId: 1,
            lidIdPk: 1,
            lidType: 3,
            merchantId: 1,
            terminalNbr: 1
        };
        comp.getAllMemoCount();
        fixture.detectChanges();
        let el = fixture.debugElement.query(By.css('#customermemos')).nativeElement       
        el.click();       
        fixture.detectChanges();
        expect(comp.memos.length).toEqual(mockData.customerMemo.length);
    });

    it('should display merchant memos', () => {
        comp.searchParamsPk = {
            customerId: 1,
            lidIdPk: 1,
            lidType: 2,
            merchantId: 1,
            terminalNbr: 1
        };
        comp.getAllMemoCount();
        fixture.detectChanges();
        let el = fixture.debugElement.query(By.css('#merchantmemos')).nativeElement
        el.click();
        fixture.detectChanges();

        expect(comp.memos.length).toEqual( mockData.merchMemo.length);
    });

    it('should display terminal memos', () => {
        comp.searchParamsPk = {
            customerId: 1,
            lidIdPk: 1,
            lidType: 1,
            merchantId: 1,
            terminalNbr: 1
        };
        comp.getAllMemoCount();
        fixture.detectChanges()
        let el = fixture.debugElement.query(By.css('#terminalmemos')).nativeElement
        el.click();
        fixture.detectChanges();
        expect(comp.memos.length).toEqual( mockData.termMemo.length);
    });

});
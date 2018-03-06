import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule, MessagesModule, CalendarModule, GrowlModule
} from 'primeng/primeng';
import { DataTable, ConfirmationService, ConfirmDialog, Message, Messages, Calendar, Growl } from 'primeng/primeng';
import { DebugElement, SimpleChanges, SimpleChange, Input } from '@angular/core';

import { TEST_BASESERVER } from '../../../../shared/spec.global.component';
import { AuditServcie } from '../../../../services/petro/audit.service';
import '../../../../app.module.client';
import { commanderversion, commanderbaseversion } from '../../../../models/petro/commanderversion.model';
import { CommanderVersionComponent} from  './version.component';
import { Audit } from '../../../../models/petro/audit.model';

const auditUpdate: Audit = {
    auditId: 3084,
    previousValue: 'oldValue',
    newValue: 'newValue',
    actionType: 'Update',
    entityName: '',
    versionId: 982,
    userName: 'testUser',
    auditDate: '2017-10-10',
    tableID: 200,
    tableName: 'testTable'
};

const auditInsert: Audit = {
    auditId: 3084,
    previousValue: 'base version',
    newValue: 'version description',
    actionType: 'Insert',
    entityName: '',
    versionId: 982,
    userName: 'testUser',
    auditDate: '2017-10-10',
    tableID: 200,
    tableName: 'testTable'
};

const oldVersion= { "obsoleteIndicator": false, "versionID": "1056", "versionDescription": "CCCCCC 1.11.11", "createdByUser": null, "createdDate": null };
const newVersion= { "obsoleteIndicator": true, "versionID": "1056", "versionDescription": "CCCCCC 1.11.11", "createdByUser": null, "createdDate": null };
export class MockService extends AuditServcie {

    getversionaudit(value: string) {
        if (value == 'oldValue') {
            return Observable.of(oldVersion);
        }
        else {
            return Observable.of(newVersion);
        }
    }
}

describe('CommanderVersionComponent Component', () => {
    let fixture: ComponentFixture<CommanderVersionComponent>,
        comp: CommanderVersionComponent,
        debugElement: DebugElement,
        element: HTMLElement;

    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpModule, BrowserAnimationsModule, ReactiveFormsModule, NoopAnimationsModule,
                DataTableModule, DialogModule, ConfirmDialogModule, MessagesModule, CalendarModule, GrowlModule],
            declarations: [CommanderVersionComponent],
            providers: [CommanderVersionComponent, AuditServcie]
        });

        TestBed.overrideComponent(
            CommanderVersionComponent,
            { set: { providers: [{ provide: AuditServcie, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(CommanderVersionComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    
    it('should show update template', async(() => {
        comp.audit = auditUpdate;
        comp.ngOnChanges({
            audit: new SimpleChange(null, comp.audit, false)
        });
        fixture.detectChanges();
        expect(comp.oldVersion.obsoleteIndicator).toBeFalsy();
        expect(comp.newVersion.obsoleteIndicator).toBeTruthy();

        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));

        expect(divTableCell[7].nativeElement.textContent).toEqual('false');
        expect(divTableCell[8].nativeElement.textContent).toEqual('true');


    }));

    it('should show insert template', async(() => {
        comp.audit = auditInsert;
        comp.ngOnChanges({
            audit: new SimpleChange(null, comp.audit, false)
        });
        fixture.detectChanges();  
        expect(comp.IsUpdate).toBeFalsy();
        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));

        expect(divTableCell[2].nativeElement.textContent).toEqual('base version');
        expect(divTableCell[3].nativeElement.textContent).toEqual('version description');
    }));
    
});



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
import '../../../../app.module.client';
import { PetroMappingComponent } from './petroMapping.component';
import { Audit } from '../../../../models/petro/audit.model';
import { TableMapping } from '../../../../models/petro/petroTablemapping.model';
import { AuditServcie } from '../../../../services/petro/audit.service';
import { DateServcie } from '../../../../services/petro/date.service';

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
    previousValue: '',
    newValue: 'mappingNewValue',
    actionType: 'Insert',
    entityName: '',
    versionId: 982,
    userName: 'testUser',
    auditDate: '2017-10-10',
    tableID: 200,
    tableName: 'testTable'
};

const oldMapping = { "versionID": 0, "mappingID": null, "pdlFlag": true, "paramID": 133, "paramName": null, "worldPayFieldName": null, "worldPayTableName": null, "worldPayJoinFields": null, "worldPayCondition": null, "worldPayOrderBy": null, "worldPayFieldDescription": null, "effectiveBeginDate": "2017-10-06T00:00:00", "effectiveEndDate": "2017-10-06T00:00:00", "viperTableName": "1test", "viperFieldName": "1test", "viperCondition": null, "charStartIndex": 2, "charLength": 3, "createdByUser": null, "lastUpdatedBy": null };
const newMapping = { "versionID": 0, "mappingID": null, "pdlFlag": true, "paramID": 133, "paramName": null, "worldPayFieldName": null, "worldPayTableName": null, "worldPayJoinFields": null, "worldPayCondition": null, "worldPayOrderBy": null, "worldPayFieldDescription": null, "effectiveBeginDate": "2017-10-06T00:00:00", "effectiveEndDate": "2017-10-06T00:00:00", "viperTableName": "1test", "viperFieldName": "1test", "viperCondition": null, "charStartIndex": 12, "charLength": 53, "createdByUser": null, "lastUpdatedBy": null };
const mappings = [{ "versionID": 0, "mappingID": null, "pdlFlag": false, "paramID": null, "paramName": null, "worldPayFieldName": null, "worldPayTableName": null, "worldPayJoinFields": null, "worldPayCondition": null, "worldPayOrderBy": null, "worldPayFieldDescription": "Test16", "effectiveBeginDate": "2017-10-17T00:00:00", "effectiveEndDate": "2017-10-18T00:00:00", "viperTableName": "Test16", "viperFieldName": "Test16", "viperCondition": "Test16", "charStartIndex": 16, "charLength": 16, "createdByUser": null, "lastUpdatedBy": null }];
export class MockService extends AuditServcie {

    getMapping(value: string) {
        if (value == 'oldValue') {
            return Observable.of(oldMapping);
        }
        else {
            return Observable.of(newMapping);
        }
    }

    getMappings(value: string) {
        return Observable.of(mappings);
    }
}

describe('PetroMappingComponent Component', () => {
    let fixture: ComponentFixture<PetroMappingComponent>,
        comp: PetroMappingComponent,
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
            declarations: [PetroMappingComponent],
            providers: [PetroMappingComponent, AuditServcie ,DateServcie]
        });

        TestBed.overrideComponent(
            PetroMappingComponent,
            { set: { providers: [{ provide: AuditServcie, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PetroMappingComponent);
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

        expect(comp.IsUpdate).toBeTruthy();

        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));
        expect(divTableCell[43].nativeElement.textContent).toEqual('2');
        expect(divTableCell[44].nativeElement.textContent).toEqual('12');
        expect(divTableCell[46].nativeElement.textContent).toEqual('3');
        expect(divTableCell[47].nativeElement.textContent).toEqual('53');
   
    }));

    it('should show insert template', async(() => {
        comp.audit = auditInsert;
        comp.ngOnChanges({
            audit: new SimpleChange(null, comp.audit, false)
        });
        fixture.detectChanges();
        expect(comp.IsUpdate).toBeFalsy();
        const divTableCell = fixture.debugElement.queryAll(By.css('.divTableCell'));

        expect(divTableCell[15].nativeElement.textContent).toEqual('false');
        expect(divTableCell[17].nativeElement.textContent).toEqual('Test16');
    }));

});



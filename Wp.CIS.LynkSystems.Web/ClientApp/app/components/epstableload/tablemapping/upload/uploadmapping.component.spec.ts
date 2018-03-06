import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DebugElement } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import { TabViewModule, DataTableModule, FileUploadModule, ButtonModule, TreeModule, DropdownModule, DialogModule, CheckboxModule, DataGridModule, ConfirmDialogModule, GrowlModule } from 'primeng/primeng';
import { SelectItem, DataTable, ConfirmationService, ConfirmDialog, Growl, Dropdown, Checkbox, TreeNode } from 'primeng/primeng';
import { UploadMappingComponent } from './uploadmapping.component';
import { TEST_BASESERVER } from '../../../../shared/spec.global.component';
import { CommonModule } from '@angular/common';
import { DomSanitizer } from '@angular/platform-browser';
import { PetroTable, Updates } from '../../../../models/petro/petroTable.model';

describe('UploadMappingComponent', () => {
    let fixture: ComponentFixture<UploadMappingComponent>,
        comp: UploadMappingComponent,
        debugElement: DebugElement,
        element: HTMLElement;
  
    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule, HttpModule, DataTableModule, DialogModule, BrowserAnimationsModule, NoopAnimationsModule, ConfirmDialogModule, GrowlModule, DropdownModule, CheckboxModule, TreeModule],
            declarations: [UploadMappingComponent],
            providers: [
            ]
        });
        TestBed.configureTestingModule({ imports: [ReactiveFormsModule], declarations: [UploadMappingComponent] });///Check for forms error

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UploadMappingComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });

    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

});





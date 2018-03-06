import { assert } from 'chai';
import { TestBed, async, ComponentFixture, inject, fakeAsync, tick } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, NG_VALIDATORS, FormControl, Validator,Validators, AbstractControl, FormsModule, ReactiveFormsModule, NgForm  } from '@angular/forms';
import { DebugElement } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import {
    TabViewModule, DataTableModule, FileUploadModule,
    ButtonModule, TreeModule, DropdownModule, DialogModule,
    CheckboxModule, DataGridModule, ConfirmDialogModule,
    PanelModule, GrowlModule, MessagesModule, AutoCompleteModule, CalendarModule
} from 'primeng/primeng';
import {
    DataTable, ConfirmationService, Dropdown, TabView, SelectItem,
    Dialog, ConfirmDialog, Panel, Growl, Message, Messages,Calendar
} from 'primeng/primeng';
import { EPSTableLoadComponent } from './epstableload.component';
import { CommanderVersionService } from "../../services/petro/commanderversion.service";
import { TEST_BASESERVER } from '../../shared/spec.global.component';
import { SchemaUploadComponent } from './schemaupload/schemaupload.component';
import { TableSchemaComponent } from './schemaupload/tableschema/tableschema.component';
import { TableMappingComponent } from './tablemapping/tablemapping.component';
import { UploadMappingComponent} from './tablemapping/upload/uploadmapping.component';
import { EPSLogComponent } from './epslog/epslog.component';
import { LoadingComponent } from './loading.component';
import { TableTreeComponent } from './schemaupload/treeview/tabletree.component';
import { AuditComponent } from './audit/audit.component';
import { PetroTableComponent} from './audit/petroTable/petroTable.component';
import { PetroMappingComponent} from './audit/petroMapping/petroMapping.component';
import { CommanderVersionComponent} from './audit/commanderversion/version.component';
import { commanderbaseversion } from '../../models/petro/commanderversion.model';
import { SortPipe} from '../../pipes/sort.pipe';
import { XmlPipe } from '../../pipes/XmlPipe';
import { IAppConfigSettings } from '../../models/common/app-config-settings.model';
import { OnlyNumber } from '../../directives/onlyNumber.directive';
declare var gAppConfigSettings: IAppConfigSettings;

let versionmockdata = [
    { "versionID": 821, "versionDescription": "1254b764-eba4-4", "createdByUser": "test", "createdDate": "2017-08-30T13:22:40.56" },
    { "versionID": 820, "versionDescription": "c41de7d2-72fa-4", "createdByUser": "test", "createdDate": "2017-08-30T12:10:01.11" }
];

export class MockService extends CommanderVersionService {
    getCommanderBaseVersion(): Observable<any[]> {
        return Observable.of(versionmockdata);
    }

    deleteCommanderVersion(versionID: number):  Observable<any> {
        return Observable.of(true);
    }
}
describe('EPSTableLoadComponent Component', () => {

    let fixture: ComponentFixture<EPSTableLoadComponent>,
        comp: EPSTableLoadComponent,
        debugElement: DebugElement,
        element: HTMLElement;
   
    beforeEach(async(() => {
        TestBed.resetTestEnvironment();
        TestBed.initTestEnvironment(
            BrowserDynamicTestingModule,
            platformBrowserDynamicTesting()
        );
        TestBed.configureTestingModule({
            imports: [FormsModule,
                HttpModule,
                BrowserAnimationsModule,
                NoopAnimationsModule,
                ReactiveFormsModule,
                DataTableModule,
                TabViewModule,
                DialogModule,
                ButtonModule,
                CheckboxModule,
                DropdownModule,
                FileUploadModule,
                TreeModule,
                ConfirmDialogModule,
                PanelModule,
                GrowlModule,
                MessagesModule,
                CalendarModule,
                AutoCompleteModule],
            declarations: [EPSTableLoadComponent, LoadingComponent, TableMappingComponent,
                SchemaUploadComponent, EPSLogComponent, TableTreeComponent, XmlPipe, SortPipe,
                UploadMappingComponent, TableSchemaComponent, OnlyNumber,
                AuditComponent, PetroTableComponent, PetroMappingComponent, CommanderVersionComponent],
            providers: [
                CommanderVersionService,
                ConfirmationService
            ]
        });    

        TestBed.overrideComponent(
            EPSTableLoadComponent,
            { set: { providers: [{ provide: CommanderVersionService, useClass: MockService }] } }
        );

    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(EPSTableLoadComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;
    });
  
    it('create an mock Xml Pipe instance', () => {
        let pipe = new XmlPipe();
        expect(pipe).toBeTruthy();
    });

    it('create an mock sort Pipe instance', () => {
        let pipe = new SortPipe();
        expect(pipe).toBeTruthy();
    });
    
    it('should create the app', async(() => {
        let app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));
        
    it('SchemaUploadComponent should create the app', async(() => {
        let fixture1 = TestBed.createComponent(SchemaUploadComponent);
        let app = fixture1.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('TableMappingComponent should create the app', async(() => {
        let fixture2 = TestBed.createComponent(TableMappingComponent);
        let app = fixture2.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('EPSLogComponent should create the app', async(() => {
        let fixture3 = TestBed.createComponent(EPSLogComponent);
        let app = fixture3.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('TableSchemaComponent should create the app', async(() => {
        let fixture4 = TestBed.createComponent(TableSchemaComponent);
        let app = fixture4.debugElement.componentInstance;
        expect(app).toBeTruthy();
    }));

    it('EPSTableLoadComponent should display version dropdownlist', async(() => {
        comp.ngOnInit();
        fixture.detectChanges();  
        let ddl = fixture.debugElement.query(By.css('select')).nativeElement;
        expect(ddl.length == 2).toBe(true);
        expect(ddl[0].textContent).toEqual('1254b764-eba4-4');
        expect(ddl[1].textContent).toEqual('c41de7d2-72fa-4');   
    }));

    it('EPSTableLoadComponent when click New Version button it should call', () => {
        comp.ngOnInit();
        fixture.detectChanges();
        const bShowCreateVersionDialog = fixture.debugElement.query(By.css('#bShowCreateVersionDialog')).nativeElement;
        bShowCreateVersionDialog.click();
       
        fixture.detectChanges();
        expect(comp.displayVersionDialog).toBe(true);
    });

     it('EPSTableLoadComponent invalid Version submit button should disabled ', () => {
        comp.ngOnInit();
        fixture.detectChanges();
        const createNewVersion = fixture.debugElement.query(By.css('#bShowCreateVersionDialog')).nativeElement;
        createNewVersion.click();
        fixture.detectChanges();
        comp.newVersion = 'AAAAA 1.11.11a';

        const bSubmitNewVersion = fixture.debugElement.query(By.css('#bCreateVersion')).nativeElement;
        fixture.detectChanges();
        expect(bSubmitNewVersion.disabled).toBeTruthy();
         
    });

     it('EPSTableLoadComponent valid Version submit button should enabled', () => {
         comp.ngOnInit();
         fixture.detectChanges();
         const bShowCreateVersionDialog = fixture.debugElement.query(By.css('#bShowCreateVersionDialog')).nativeElement;
         bShowCreateVersionDialog.click();
         fixture.detectChanges();
         comp.newVersion = 'AAWWAA 11.11.11';

         const bCreateVersion = fixture.debugElement.query(By.css('#bCreateVersion')).nativeElement;
         fixture.detectChanges();
         expect(bCreateVersion.disabled).toBeFalsy();

     });

    
    it('should call CreateVersion when submitted', () => { 
        const spy = spyOn(comp, 'CreateVersion');  
   
        const bCreateVersion = fixture.debugElement.query(By.css('#bCreateVersion')).nativeElement;
        bCreateVersion.click();
        fixture.detectChanges();

        expect(comp.CreateVersion).toHaveBeenCalled();    
      
     });  

    it('should call delete event when clicking on delete version', () => {
        comp.ngOnInit();
        fixture.detectChanges();
        comp.selectedBasisVersion = versionmockdata[0];

        const spy = spyOn(comp, 'delete');  
        comp.delete(comp.commanderBasisVersions[0]);    

        expect(comp.delete).toHaveBeenCalled();

    });  
});

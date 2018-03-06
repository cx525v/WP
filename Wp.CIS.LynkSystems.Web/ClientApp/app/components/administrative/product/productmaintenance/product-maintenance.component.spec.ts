
/// <reference path="../../../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';

import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from "@angular/platform-browser-dynamic/testing";
import { HttpModule, XHRBackend, Response, ResponseOptions, Http, BaseRequestOptions, ConnectionBackend } from '@angular/http';
import { MockBackend } from '@angular/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { DebugElement } from '@angular/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { By } from '@angular/platform-browser';
import {
    TabViewModule,
    DataTableModule,
    TreeModule,
    FileUploadModule,
    ButtonModule,
    DropdownModule,
    PanelModule,
    DialogModule,
    CheckboxModule,
    CalendarModule,
    InputTextModule,
    RadioButtonModule,
    ConfirmDialogModule,
    FieldsetModule,
    GrowlModule,
    MessagesModule,
    SplitButtonModule,
    AutoCompleteModule,
    TieredMenuModule,
    SlideMenuModule,
    DataTable,
    ConfirmationService
} from 'primeng/primeng';

import { AuditHistoryService } from "../../../../services/administrative/products/productmaintenance/audit-history.service";
import { ErrorService } from "../../../../services/error.service";
import { NotificationService } from '../../../../services/notification.service';

import { ProductMaintenanceComponent } from './product-maintenance.component';
import { ProductMaintenanceProductComponent } from './product-maintenance-product.component';
import { ProductEditComponent } from './product-edit.component';
import { LoadingComponent } from '../../../epstableload/loading.component';
import { TEST_BASESERVER } from '../../../../shared/spec.global.component';

import { ErrorModel } from '../../../../models/error/error.model';
import { AuditHistoryModel } from '../../../../models/administration/product/productmaintenance/audit-history.model'
import { LidTypesEnum } from '../../../../models/common/lid-types.enum'
import { ActionTypeEnum } from '../../../../models/common/action-type.enum'
import { ProductModel } from '../../../../models/administration/product/productmaintenance/product.model';

let fixture: ComponentFixture<ProductMaintenanceComponent>;

export class AuditHistoryServiceMock {

    public getLatestAuditHistory(lidType: LidTypesEnum, lid: number, actionType: ActionTypeEnum): Observable<AuditHistoryModel> {

        let auditHistory: AuditHistoryModel = new AuditHistoryModel();
        auditHistory.actionDate = new Date();
        auditHistory.actionType = ActionTypeEnum.ProjectMaintenanceScreen;
        auditHistory.auditId = 1;
        auditHistory.lid = 2;
        auditHistory.lidType = LidTypesEnum.TerminalID;
        auditHistory.notes = 'Notes';
        auditHistory.userName = "user name";

        return Observable.of(auditHistory);
    }

}

export class ErrorServiceMock {

    public logException(message: string, error: Error): void {

    }

    public logError(message: string, description: string, stackTrace: string): void {

    }

    public logErrorModel(errorInfo: ErrorModel): void {

    }
}

export class NotificationServiceMock {

    success(detail: string, summary?: string): void {

    }

    info(detail: string, summary?: string): void {

    }

    warning(detail: string, summary?: string): void {
 
    }

    error(detail: string, summary?: string): void {
 
    }}

describe('Product Maintenanc component', () => {

    let fixture: ComponentFixture<ProductMaintenanceComponent>,
        comp: ProductMaintenanceComponent,
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
                ReactiveFormsModule,
                HttpModule,
                BrowserAnimationsModule,
                TabViewModule,
                DataTableModule,
                TreeModule,
                FileUploadModule,
                ButtonModule,
                DropdownModule,
                PanelModule,
                DialogModule,
                CheckboxModule,
                CalendarModule,
                InputTextModule,
                RadioButtonModule,
                ConfirmDialogModule,
                FieldsetModule,
                GrowlModule,
                MessagesModule,
                SplitButtonModule,
                AutoCompleteModule,
                TieredMenuModule,
                SlideMenuModule,
                RouterTestingModule
            ],
            declarations: [ProductMaintenanceComponent, ProductMaintenanceProductComponent, ProductEditComponent, LoadingComponent],
            providers: [
                FormBuilder,
                MockBackend,
                {
                    provide: AuditHistoryService,
                    useClass: AuditHistoryServiceMock
                },
                {
                    provide: ErrorService,
                    useClass: ErrorServiceMock
                },
                {
                    provide: NotificationService,
                    useClass: NotificationServiceMock
                },
                BaseRequestOptions,
                {
                    provide: Http,
                    useFactory: (backend: ConnectionBackend, options: BaseRequestOptions) => new Http(backend, options),
                    deps: [MockBackend, BaseRequestOptions]
                }
            ]
        })
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ProductMaintenanceComponent);
        comp = fixture.componentInstance;
        debugElement = fixture.debugElement;
        element = debugElement.nativeElement;

    });

    it('Should display the product type', async(() => {

        fixture.detectChanges();

        let testProduct: ProductModel = new ProductModel();
        testProduct.productType = "test product type";

        comp._selectedProduct = testProduct;

        fixture.detectChanges();
        let productType = debugElement.query(By.css('#theProductType'));
        let nativeEl: HTMLElement = productType.nativeElement;

        expect(nativeEl.innerText).toEqual(testProduct.productType);
    }));

    it('Should display the product description', async(() => {

        fixture.detectChanges();

        let testProduct: ProductModel = new ProductModel();
        testProduct.description = "test product description";

        comp._selectedProduct = testProduct;

        fixture.detectChanges();
        let productType = debugElement.query(By.css('#productDescription'));
        let nativeEl: HTMLElement = productType.nativeElement;

        expect(nativeEl.innerText).toEqual(testProduct.description);
    }));

    it('Should display the product last modified by', async(() => {

        fixture.detectChanges();

        let testAudit: AuditHistoryModel = new AuditHistoryModel();
        testAudit.userName = "test userName";

        comp._auditHistoryForSelectedRecord = testAudit;

        fixture.detectChanges();
        let modifiedBy = debugElement.query(By.css('#productLastModifiedBy'));
        let nativeEl: HTMLElement = modifiedBy.nativeElement;

        expect(nativeEl.innerText).toEqual(testAudit.userName);
    }));

});


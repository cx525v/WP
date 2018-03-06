
import { NgModule, ErrorHandler, ChangeDetectorRef} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule, RequestOptions, XHRBackend, Http} from '@angular/http';
import { sharedConfig } from './app.module.shared';
import { MerchantProfileServcie } from './services/merchantprofile.service';
import { NotificationService } from './services/notification.service';
import {
    TabViewModule, DataTableModule, FileUploadModule, ButtonModule, TreeModule,
    DropdownModule, DialogModule, CheckboxModule, ConfirmDialogModule, PanelModule, FieldsetModule,
    CalendarModule, InputTextModule, RadioButtonModule, GrowlModule, MessagesModule, AutoCompleteModule, SplitButtonModule,
    TieredMenuModule, SlideMenuModule, MegaMenuModule, MenuModule, AccordionModule, PaginatorModule
} from 'primeng/primeng';
import { GlobalErrorHandler } from './shared/globalerror.handler';
import { HttpInterceptor } from './shared/httpinterceptor';
import { AuthGuard } from './services/Authentication/index';
import { LidPrimaryKeyCacheEventsService } from './services/common/lid-primary-key-cache-events.service';

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: [sharedConfig.declarations],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpModule,
        TabViewModule,
        DataTableModule,
        TreeModule,
        FileUploadModule,  
        ButtonModule,     
        DropdownModule,
        BrowserAnimationsModule,
        PanelModule,
        DialogModule,
        CheckboxModule,
        PanelModule,
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
        MegaMenuModule,
        MenuModule,
        AccordionModule,
        PaginatorModule,
        ...sharedConfig.imports,
    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        MerchantProfileServcie,
        AuthGuard, 
        NotificationService,
        { provide: ErrorHandler, useClass: GlobalErrorHandler },
        { provide: Http, useClass: HttpInterceptor },
        LidPrimaryKeyCacheEventsService,
        
    ]
})
export class AppModule {
}

import { NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './components/app/app.component'
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { MerchantProfileComponent } from './components/merchant/merchantprofile/merchantprofile.component';
import { DashboardInfoComponent } from './components/dashboardinfo/dashboardinfo.component';
import { DashboardComponent } from './components/dashboardinfo/dashboard.component';
import { DashboardCustomerComponent } from './components/dashboardinfo/customer/dashboard-customer.component';
import { DashBoardSearch } from './components/dashboardinfo/dashboard-search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabViewModule } from 'primeng/primeng';
import { DataTableModule } from 'primeng/primeng';
import { DropdownModule, PanelModule } from 'primeng/primeng';
import { AutoComplete, AutoCompleteModule } from 'primeng/primeng';
import { NoAccessComponent } from './components/AuthModule/no-access/no-access.component';
import { AuthGuard } from './services/Authentication/index';
import { OnlyNumber } from './directives/onlyNumber.directive';

//Petro
import { EPSTableLoadComponent } from './components/epstableload/epstableload.component';
import { EPSLogComponent } from './components/epstableload/epslog/epslog.component';
import { SchemaUploadComponent } from './components/epstableload/schemaupload/schemaupload.component';
import { TableMappingComponent } from './components/epstableload/tablemapping/tablemapping.component';
import { UploadMappingComponent} from './components/epstableload/tablemapping/upload/uploadmapping.component';
import { TableTreeComponent } from './components/epstableload/schemaupload/treeview/tabletree.component';
import { TableSchemaComponent} from './components/epstableload/schemaupload/tableschema/tableschema.component';

//pipes
import { XmlPipe } from './pipes/XmlPipe';
import { SortPipe } from './pipes/sort.pipe';
import { TwoColumnPipe } from './pipes/twocolumn.pipe';
import { DateTimeFormatPipe } from './pipes/date.pipe';
import { CurrencyPipe } from '@angular/common';
import { DecimalPipe } from '@angular/common';
import { YearMonthDatePipe } from './pipes/dateymd.pipe';

import { AuditComponent} from './components/epstableload/audit/audit.component';
import { PetroMappingComponent} from './components/epstableload/audit/petroMapping/petroMapping.component';
import { PetroTableComponent } from './components/epstableload/audit/petroTable/petroTable.component';
import { CommanderVersionComponent} from './components/epstableload/audit/commanderversion/version.component';
import { LoadingComponent} from './components/epstableload/loading.component';
import { XmlComponent } from './components/epstableload/xml/xml.component';

import { TransactionInquiryComponent } from './components/transaction/transactionInquiry/transactionInquiry.component';
import { ProductMaintenanceComponent } from './components/administrative/product/productmaintenance/product-maintenance.component'
import { ProductMaintenanceProductComponent } from './components/administrative/product/productmaintenance/product-maintenance-product.component'
import { ProductEditComponent } from './components/administrative/product/productmaintenance/product-edit.component'

import { DashboardMerchantComponent } from './components/dashboardinfo//merchant/dashboard-merchant.component';
import { MemoComponent } from './components/dashboardinfo/common/memo.component';
import { MemoListComponent } from './components/dashboardinfo/common/memo-list.component';
import { MemoListItemComponent } from './components/dashboardinfo/common/memo-list-item.component';
import { AccountInfoComponent } from './components/dashboardinfo/common/account-info.component';
import { BusinessInfoComponent } from './components/dashboardinfo/common/business-info.component';
import { CustomerMerchantComponent } from './components/dashboardinfo/common/customer-merchant.component';
import { ContactsComponent } from './components/dashboardinfo/common/contacts.component';
import { CaseHistoryComponent } from './components/dashboardinfo/common/case-history.component';
import { RecentStatementsComponent } from './components/dashboardinfo/common/recent-statements.component';
import { DashboardTitleComponent } from './components/dashboardinfo/common/dashboard-title.component';
import { AdvanceSearchComponent } from './components/dashboardinfo/advancesearch-result.component';

import { SensitivityPartnerComponent } from './components/dashboardinfo/customer/sensitivity-partner.component';
import { MerchantLocationsComponent } from './components/dashboardinfo/customer/merchant-locations.component';

import { TerminalEquipmentComponent } from './components/dashboardinfo/merchant/terminal-equipment.component';
import { TerminalDetailsComponent } from './components/dashboardinfo/terminal/terminal-details.component';
import { TerminalDetailsDialogComponent } from './components/dashboardinfo/terminal/terminal-details-dialog.component';
import { TransactionsComponent } from './components/dashboardinfo/terminal/transactions.component';
import { TransactionComponent } from './components/dashboardinfo/terminal/transaction.component';
import { BankingInfoComponent } from './components/dashboardinfo/terminal/banking-info.component';
import { MenuComponent } from './components/menu/menu.component';

export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NoAccessComponent,       
        CounterComponent,
        HomeComponent,
        MerchantProfileComponent,
        EPSTableLoadComponent,
        EPSLogComponent,
        SchemaUploadComponent,
        TableMappingComponent,
        UploadMappingComponent,
        TableTreeComponent,
        DashboardInfoComponent,
        DashboardComponent,
        DashboardCustomerComponent,
        DashBoardSearch,
        AdvanceSearchComponent,
        DashboardMerchantComponent,
        AccountInfoComponent,
        BusinessInfoComponent,
        MemoComponent,
        MemoListComponent,
        MemoListItemComponent,
        SensitivityPartnerComponent,
        CustomerMerchantComponent,
        ContactsComponent,    
        MerchantLocationsComponent,
        CaseHistoryComponent,
        RecentStatementsComponent,
        TerminalEquipmentComponent,
        TerminalDetailsComponent,
        TerminalDetailsDialogComponent,
        TransactionsComponent,  
        TransactionComponent,
        DashboardTitleComponent,
        BankingInfoComponent,
        XmlPipe,
        SortPipe,
        TwoColumnPipe,
        DateTimeFormatPipe,  
        YearMonthDatePipe,
        TransactionInquiryComponent,
        ProductMaintenanceComponent,
        OnlyNumber,
        LoadingComponent,
        ProductMaintenanceProductComponent,
        ProductEditComponent,
        TableSchemaComponent,
        AuditComponent,
        PetroMappingComponent,
        PetroTableComponent,
        XmlComponent,
        CommanderVersionComponent,
        MenuComponent
    ],  
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full', canActivate: [AuthGuard] },
            { path: 'home', component: DashboardComponent, canActivate: [AuthGuard]  },
            { path: 'counter', component: CounterComponent, canActivate: [AuthGuard]  },
            { path: 'merchantprofile', component: MerchantProfileComponent, canActivate: [AuthGuard]  },
            { path: 'epstableload', component: EPSTableLoadComponent, canActivate: [AuthGuard]  },
            { path: 'dashboardinfo/:lidType/:lidTypeValue', component: DashboardComponent, canActivate: [AuthGuard]  },
            { path: 'advancesearch/:lidType/:lidTypeValue', component: DashboardComponent, canActivate: [AuthGuard] },

            { path: 'dashboardinfo', component: DashboardComponent, canActivate: [AuthGuard] },
            { path: 'transactionInquiry/:terminalId', component: TransactionInquiryComponent, canActivate: [AuthGuard]  },
            { path: 'noaccess', component: NoAccessComponent },
            {
                path: "Admin",
                children: [
                    {
                        path: "ProductMaintanence",
                        component: ProductMaintenanceComponent
                    }
                ]
            },
            { path: '**', redirectTo: 'home', canActivate: [AuthGuard]  }
        ]),
        FormsModule, BrowserModule, ReactiveFormsModule, TabViewModule, DataTableModule, DropdownModule, BrowserAnimationsModule, PanelModule, AutoCompleteModule
    ]
};

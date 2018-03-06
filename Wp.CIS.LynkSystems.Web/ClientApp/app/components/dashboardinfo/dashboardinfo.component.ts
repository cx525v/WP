import { Component, Inject, OnInit, Pipe } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Router, RouterModule, ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { BankingService } from '../../services/banking.service';
import { CaseHistoryService } from '../../services/casehistory.service';
import { AccordionModule, MenuItem, AutoCompleteModule, InputTextModule, TabViewModule, DropdownModule, SelectItem, PanelModule } from 'primeng/primeng';  
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import '../../app.module.client';
import 'rxjs/add/operator/toPromise';
import { Subscription } from 'rxjs/Subscription';
import { By } from '@angular/platform-browser';

//import { DashBoardSearchService } from './../../services/dashboard-search.service';

import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory
} from './../../models/dashboard.model';

import { LidTypesEnum } from '../../models/common/lid-types.enum'

@Component({
    selector: 'dashboard-component',
    templateUrl: './dashboardinfo.component.html',
    providers: [DashboardInfoService, BankingService, CaseHistoryService]
})

export class DashboardInfoComponent implements OnInit {
    public formData: FormGroup;
    today: Date;
    dashboardInfo: DashboardInfo;
    merchantInfo: IMerchantInfo;
    customerProfile: CustomerProfile;
    activeServices: ActiveServices;
    termProfile: TerminalProfileData;
    groupInfo: GroupInfo;
    selectId: string;
    selectedValue: number;
    searchList: SelectItem[];
    public bankingInfo: BankingInformation[];
    errorMessage: string;
    public demographicsInfo: DemographicsInfo[];
    public demographicsInfoCust: DemographicsInfo[];
    public demographicsInfoMerch: DemographicsInfo[];
    public demographicsInfoTerm: DemographicsInfo[];
    public caseHistList: CaseHistory[];

    selectedDemo: DemographicsInfo;
    displayDialog: boolean;
    selectedCase: CaseHistory;
    dialogCaseDetails: boolean;
    public loading = false;

    private sub: any;
    lidTypeID: string;
    lidTypeValue: string;
    subscription: Subscription;

    constructor(private dashboardInfoSvc: DashboardInfoService, private bankingSrv: BankingService,
        private caseHistServ: CaseHistoryService,
        private route: ActivatedRoute) {
        this.formData = new FormGroup({
            'selectedValue': new FormControl('', [Validators.required]),
            'selectId': new FormControl('', [Validators.required])
                   
        })
        console.log("In DashboardInfo ..");

        this.searchList = [];
        this.searchList.push({ label: 'Select..', value: null });
        this.searchList.push({ value: 1, label: "TerminalNbr" });
        this.searchList.push({ value: 2, label: "MerchantID" });
        this.searchList.push({ value: 3, label: "CustomerID" });
        this.searchList.push({ value: 4, label: "GroupID" });
        this.searchList.push({ value: 5, label: "TerminalID" });
        this.searchList.push({ value: 6, label: "MerchantNbr" });
        this.searchList.push({ value: 7, label: "CustomerNbr" });

        this.merchantInfo = <IMerchantInfo>{};
        this.customerProfile = <CustomerProfile>{};
        this.activeServices = <ActiveServices>{};
        this.termProfile = <TerminalProfileData>{};
        if (document.querySelector('#search-panel') != null) {
            document.querySelector('#search-panel').removeAttribute("hidden");
        }

    }
    
    ngOnInit() {
        console.log("In DashboardInfo ..");
        this.sub = this.route.params.subscribe(params => {
            this.lidTypeID = LidTypesEnum[+params['LIDType']]; // (+) converts string 'id' to a number
            this.lidTypeValue = params['LIDTypeValue']
            // In a real app: dispatch action to load the details here.
            
            if (LidTypesEnum[this.lidTypeID] != null && this.lidTypeValue != null) {
                this.getDashboardData(LidTypesEnum[this.lidTypeID], this.lidTypeValue);
            }
            
        });              

    }

    private getDashboardData(lidtype: LidTypesEnum, lid: string) {
        this.loading = true;
        //this.dashboardInfoSvc.getDashboardInfo(lidtype, lid).then(r => {
        //    this.dashboardInfo = r;
        //    this.customerProfile = this.dashboardInfo.custProfile;
        //    this.merchantInfo = this.dashboardInfo.merchInfo;
        //    this.activeServices = this.dashboardInfo.actvServices;
        //    this.termProfile = this.dashboardInfo.termProfile;
        //    this.groupInfo = this.dashboardInfo.groupInfo;
        //    this.demographicsInfo = this.dashboardInfo.demographicsInfo;
        //    this.demographicsInfoCust = this.dashboardInfo.demographicsInfoCust;
        //    this.demographicsInfoMerch = this.dashboardInfo.demographicsInfoMerch;
        //    this.demographicsInfoTerm = this.dashboardInfo.demographicsInfoTerm;

        //    //console.log(this.dashboardInfo);
        //    this.loading = false;
        //    if (document.querySelector('#search-panel') != null) {
        //        document.querySelector('#search-panel').setAttribute("hidden", '');
        //    }
        //});

        this.dashboardInfoSvc.getDashboardInfo(lidtype, lid).subscribe(r => {
            this.dashboardInfo = r;
            this.customerProfile = this.dashboardInfo.custProfile;
            this.merchantInfo = this.dashboardInfo.merchInfo;
            this.activeServices = this.dashboardInfo.actvServices;
            this.termProfile = this.dashboardInfo.termProfile;
            this.groupInfo = this.dashboardInfo.groupInfo;
            this.demographicsInfo = this.dashboardInfo.demographicsInfo;
            this.demographicsInfoCust = this.dashboardInfo.demographicsInfoCust;
            this.demographicsInfoMerch = this.dashboardInfo.demographicsInfoMerch;
            this.demographicsInfoTerm = this.dashboardInfo.demographicsInfoTerm;

            //console.log(this.dashboardInfo);
            this.loading = false;
            if (document.querySelector('#search-panel') != null) {
                document.querySelector('#search-panel').setAttribute("hidden", '');
            }
        });

        this.bankingSrv.getBankingInfo(this.formData.value.selectedValue, this.formData.value.selectId).subscribe(
            data => {
                this.bankingInfo = data as BankingInformation[];
                //console.log(this.bankingInfo);
            },
            error => { this.errorMessage = 'Request error!' }
        );

        this.caseHistServ.getCaseHistory(lidtype, lid).subscribe(
            data => {
                this.caseHistList = data as CaseHistory[];
                console.log(this.caseHistList);
            },
            error => { this.errorMessage = 'Request error!' }
        );
    }

    public getDashboardInfo(): void {
        if (this.formData.valid) {
            if (this.formData.value.selectId != null && this.formData.value.selectedValue != null) {
                this.getDashboardData(this.formData.value.selectedValue, this.formData.value.selectId);                
            }
        }
    }

    selectDemo(demographics: DemographicsInfo) {
        this.selectedDemo = demographics;
        this.displayDialog = true;
    }

    onDialogHide() {
        this.selectedDemo = null;
        this.selectedCase = null;
    }

    onRowSelect(event) {
        this.selectDemo(event.data);
    }

    onCaseSelected(event) {
        this.selectCase(event.data);
    }

    selectCase(caseHist: CaseHistory) {
        this.selectedCase = caseHist;
        this.dialogCaseDetails = true;
    }

}


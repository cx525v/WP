
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Response } from '@angular/http';

import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';

import { DashboardInfoService } from '../../../services/dashboardinfo.service';
import { TerminalInfoService } from '../../../services/dashboardinfo/terminal.service';
import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory, MerchantProfileData, TermInfo
} from './../../../models/dashboard.model';

import { AccountInfo } from './../../../models/dashboardInfo/accountinfo.model';
import { BusinessInfo } from './../../../models/dashboardInfo/businessInfo.model';
import { Contacts } from './../../../models/dashboardInfo/contacts.model';
import { TerminalEquipment } from './../../../models/dashboardInfo/terminalequipment.model';
import { RecentStatement } from './../../../models/dashboardInfo/recentstatement.model';

@Component({
    selector: 'dashboard-merchant',
    templateUrl: './dashboard-merchant.component.html',
    styleUrls: ['./dashboard-merchant.component.css'],
    providers: [TerminalInfoService]
})
export class DashboardMerchantComponent implements OnInit, OnDestroy {

    public _customerProfile: CustomerProfile;

    public _merchantProfile: IMerchantInfo;

    private _searchParamsPk: DashboardSearchParamsPk;
    public _errorMessage: string;
    public _displayLoadingDialog: boolean;
    accountInfo: AccountInfo;
    businessInfo: BusinessInfo;
    demographicsInfo: DemographicsInfo[];
    casehistory: CaseHistory[];
    merchantContacts: Contacts[];
    customerContacts: Contacts[];
    ismerchant: boolean;
    terminalEquipments: TerminalEquipment[];
    recentStatements: RecentStatement[];   
    merchantNbr: string;
    terminalID: string;
    terminalNbr: number;
    @Input()   
    set TerminalNbr(value: number) {       
        this.terminalNbr = value;
    }

    public _totalNumberOfCaseHistoryRecords: number;   

    @Input('SearchParamsPk')
    set SearchParamsPk(value: DashboardSearchParamsPk) {
        this.ismerchant = true;
        if (value && value.terminalNbr) {
            this.terminalNbr = value.terminalNbr;
        } else {
            this.terminalNbr = null;
        }

        this._searchParamsPk = value;
       
        this.getRecordInformation();     
    }
    get SearchParamsPk(){
        return this._searchParamsPk;
    }

    get self(): DashboardMerchantComponent {
        return this;
    }


    constructor(private _dashboardService: DashboardInfoService, private _terminalInfoService: TerminalInfoService) {

        this._totalNumberOfCaseHistoryRecords = null;

        this._searchParamsPk = null;

        this._customerProfile = null;

        this._merchantProfile = null;

        this._displayLoadingDialog = false;

        this._errorMessage = null;

        this.terminalNbr = null;
    }

    public ngOnInit(): void {

    };

    public ngOnDestroy(): void {

    };
    public loading(loading: boolean) {
       this._displayLoadingDialog = loading;
    }
    private getRecordInformation(): void {

        this._errorMessage = null;
        this._customerProfile = null;

        this._merchantProfile = null;

        if (this._searchParamsPk
            && this._searchParamsPk.lidIdPk
            && this._searchParamsPk.lidType) {

            this._displayLoadingDialog = true;
            this._dashboardService
                .getDashboardInfo(this._searchParamsPk.lidType, this._searchParamsPk.lidIdPk.toString())
                .subscribe((r: DashboardInfo): void => {                   
                    this._customerProfile = r.custProfile;
                    this._merchantProfile = r.merchInfo
                  
                    if (this._customerProfile && this._merchantProfile) {
                        this.accountInfo = {
                            name: this._customerProfile.senseLvlDesc + ' Account',
                            prinAddress: this._merchantProfile.mchAddress,
                            prinCity: this._merchantProfile.mchCity,
                            prinState: this._merchantProfile.mchState,
                            prinZipcode: this._merchantProfile.mchZipCode
                        };

                        this.businessInfo = {
                            custFederalTaxID: this._merchantProfile.merchFedTaxID,
                            status: this._merchantProfile.statDesc,
                            acquiringBank: this._merchantProfile.acquiringBank,
                            ebtbenefitType: this._merchantProfile.benefitTypeDesc,
                            sic: this._merchantProfile.sicDesc,
                            industry: this._merchantProfile.indTypeDesc
                        };

                        if (r.demographicsInfoMerch) {
                            this.merchantContacts = [];
                            r.demographicsInfoMerch.forEach(di => {
                                this.merchantContacts.push(
                                    {
                                        id: this._merchantProfile.merchantNbr,
                                        contact: di.contact,
                                        addressType: di.addressType,
                                        lastFour: di.lastFour
                                    }
                                );
                            });                          
                        }

                        if (r.demographicsInfoCust) {
                            this.customerContacts = [];
                            r.demographicsInfoCust.forEach(di => {
                                this.customerContacts.push(
                                    {
                                        id: r.custProfile.customerNbr,
                                        contact: di.contact,
                                        addressType: di.addressType,
                                        lastFour: di.lastFour
                                    }
                                );
                            });                          
                        }
                    
                        this.casehistory = r.caseHistorysList;
                        this.merchantNbr = r.merchInfo.merchantNbr  
                        if (r.termInfo) {                           
                            this.terminalID = r.termInfo.terminalId;                          
                        } else {
                            this.terminalID = null;
                        }
                        this._totalNumberOfCaseHistoryRecords = r.totalNumberOfCaseHistoryRecords;
                    }

                    this._displayLoadingDialog = false;
                },
                (error: Response) => {
                    this._displayLoadingDialog = false;
                    this._errorMessage = error.text();
                }
              );
        }
    }
   
}

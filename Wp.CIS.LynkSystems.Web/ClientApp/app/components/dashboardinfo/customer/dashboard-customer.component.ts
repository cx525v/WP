
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Response } from '@angular/http';

import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';

import { DashboardInfoService } from '../../../services/dashboardinfo.service';

import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory, MerchantLocation
} from './../../../models/dashboard.model';

import { AccountInfo } from './../../../models/dashboardInfo/accountinfo.model';
import { BusinessInfo } from './../../../models/dashboardInfo/businessInfo.model';
import { SensitivityPartner } from './../../../models/dashboardInfo/sensitivitypartner.model';
import { Contacts } from './../../../models/dashboardInfo/contacts.model';

@Component({

    selector: "dashboard-customer",
    templateUrl: "./dashboard-customer.component.html",
    styleUrls: ["./dashboard-customer.component.css"],
    providers: [DashboardInfoService]
})
export class DashboardCustomerComponent implements OnInit, OnDestroy {

    public _customerProfile: CustomerProfile;

    public _searchParamsPk: DashboardSearchParamsPk;

    public _displayLoadingDialog: boolean;

    public _totalNumberOfCaseHistoryRecords: number;
    public _totalMerchantRecords: number;   

    public _errorMessage: string;

    accountInfo: AccountInfo;
    businessInfo: BusinessInfo;
    sensitivityPartner: SensitivityPartner;
    merchantLocations: MerchantLocation[];
    demographicsInfo: DemographicsInfo[];
    casehistory: CaseHistory[];
    customerContacts: Contacts[];

    @Input('SearchParamsPk')
    set SearchParamsPk(value: DashboardSearchParamsPk) {

        this._searchParamsPk = value;

        this.getRecordInformation();
    }

    constructor(private _dashboardService: DashboardInfoService) {

        this._customerProfile = null;

        this._searchParamsPk = null;

        this._displayLoadingDialog = false;

        this._errorMessage = null;
    }

    public ngOnInit(): void {
        
        this.getRecordInformation();
            
    }

    public ngOnDestroy(): void {

    }

    private getRecordInformation(): void {    

        this._errorMessage = null;
        this._customerProfile = null;   
        
        if (this._searchParamsPk
            && this._searchParamsPk.lidIdPk
            && this._searchParamsPk.lidType) {

            this._displayLoadingDialog = true;

            this._dashboardService
                .getDashboardInfo(this._searchParamsPk.lidType, this._searchParamsPk.lidIdPk.toString())
                .subscribe((r: DashboardInfo): void => {
                    this._displayLoadingDialog = false;
                   
                    this._customerProfile = r.custProfile;

                    if (this._customerProfile) {
                        this.accountInfo = {
                            name: r.custProfile.senseLvlDesc + ' Account',
                            prinAddress: r.custProfile.prinAddress,
                            prinCity: r.custProfile.prinCity,
                            prinState: r.custProfile.prinState,
                            prinZipcode: r.custProfile.prinZipcode
                        };
                    }


                    this.businessInfo = {
                        custFederalTaxID: r.custProfile.custFederalTaxID,
                        industry: r.custProfile.legalDesc,
                        status: r.custProfile.statDesc,
                        acquiringBank: '',
                        ebtbenefitType: '',
                        sic: ''

                    };

                    this.sensitivityPartner = {
                        group: r.groupInfo?(r.groupInfo.groupName + '(' + r.groupInfo.groupID + ')'):'',
                        senseLvlDesc: r.custProfile.senseLvlDesc,
                        partnerRelationship: r.groupInfo ?r.groupInfo.groupType:''

                    };

                    this.merchantLocations = r.merchantsList;
                    this._totalMerchantRecords = r.totalMerchantRecords;

                    this.demographicsInfo = r.demographicsInfoCust;
               
                    this.casehistory = r.caseHistorysList;
                    this._totalNumberOfCaseHistoryRecords = r.totalNumberOfCaseHistoryRecords;

                    if (this.demographicsInfo) {
                        this.customerContacts = [];
                        this.demographicsInfo.forEach(di => {
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
                },
                (error: Response): void => {

                    this._displayLoadingDialog = false;

                    this._errorMessage = error.text();
                }
            );
        }
    }
}
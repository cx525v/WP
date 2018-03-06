
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule, ActivatedRoute, Params } from '@angular/router';

import { Observable, Subscription } from 'rxjs/Rx'

import { LidTypesEnum } from '../../models/common/lid-types.enum';
import { DashboardComponentParams } from '../../models/dashboardInfo/dashboard-component-params.model';
import { DashboardInfoService } from './../../services/dashboardinfo.service';
import { DashboardEventsService } from '../../services/dashboardinfo/dashboard-events.service';
import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';
import { LidPrimeyKeyEventModel } from '../../models/common/lid-primary-key-event.model';
import { DashboardSearchParamsPk } from '../../models/dashboardInfo/dashboard-search-params-pk.model';
import { LidPrimaryKeyCacheEventsService } from '../../services/common/lid-primary-key-cache-events.service';
import { PrimaryKeyInfoGenericModel } from '../../models/dashboardInfo/primary-key-info-generic.model';

import { SearchCriterialHelper } from './search-custom.validators';

import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices,
    TerminalProfileData, BankingInformation,
    GroupInfo, CaseHistory
} from './../../models/dashboard.model';

import { CustomerPrimaryKeyInfoModel } from '../../models/common/customer-primary-key-info.model';
import { MerchantPrimaryKeyInfoModel } from '../../models/common/merchant-primary-key-info.model';
import { TerminalPrimaryKeyInfoModel } from '../../models/common/terminal-primary-key-info.model';
import { DashboardAdvanceSearch } from './../../models/dashboardInfo/dashboard-advancesearch.model';
import { AdvanceSearchService } from './../../services/dashboardInfo/advancesearch.service';
import { AdvanceSearchComponent } from './advancesearch-result.component';
@Component({
    selector: "dashboard",
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css'],
    providers: [DashboardEventsService, AdvanceSearchService]
})
export class DashboardComponent implements OnInit, OnDestroy {

   /**
    * @field This holds the subscription to the terminal id parameter. When the parameter
    *          changes the subscription will trigger an event.
    */
    public _subscription: Subscription;

    private _lidIdSearch: string;

    private _lidTypeSearch: LidTypesEnum;
    private _searchParamsPk: DashboardSearchParamsPk;

    public _showCustomerComponent: boolean;
    public _showMerchantComponent: boolean;

    public _errorMessages: Array<string>;
    result: DashboardAdvanceSearch[];

    constructor(
        private _activatedRoute: ActivatedRoute,
        private _dashBoardSearchSvc: DashboardInfoService,
        private _primaryKeyCacheService: LidPrimaryKeyCacheEventsService,
        private _advanceSearchService: AdvanceSearchService) {
       
        this._lidIdSearch = null;

        this._lidTypeSearch = null;

        this._showCustomerComponent = false;
        this._showMerchantComponent = false;

        this._errorMessages = new Array<string>();
    }

    public ngOnInit(): void {
        let path = this._activatedRoute.snapshot.url[0].path;
        console.log(path);
        this._subscription = this._activatedRoute
            .params
            .subscribe((params: DashboardComponentParams) => {
                
                this._errorMessages.length = 0;

                this._searchParamsPk = null;

                if (params
                    && params.lidType
                    && params.lidTypeValue) {
                    this.result = null;
                    this._lidTypeSearch = <LidTypesEnum>parseInt(params.lidType);
                    this._lidIdSearch = params.lidTypeValue;

                    if (this._lidTypeSearch) {

                        switch (this._lidTypeSearch) {

                            case LidTypesEnum.CustomerNbr:
                                this._showCustomerComponent = true;
                                this._showMerchantComponent = false;
                                break;

                            case LidTypesEnum.MerchantNbr:
                            case LidTypesEnum.TerminalID:
                                this._showCustomerComponent = false;
                                this._showMerchantComponent = true;
                                break;

                            default:
                                this._showCustomerComponent = false;
                                this._showMerchantComponent = false;
                                break;
                        }
                    } else {

                        this._showCustomerComponent = false;
                        this._showMerchantComponent = false;
                    }

                    this.getSearchRecordInformation(this._lidTypeSearch, this._lidIdSearch);

                } else {

                }

            });
    }

    public ngOnDestroy(): void {

        this._subscription.unsubscribe();

    }

    private getSearchRecordInformation(lidType: LidTypesEnum, lidId: string) {

        let primaryKeyInfo: PrimaryKeyInfoGenericModel = this.getPrimaryKeyFromCache(lidType, lidId);

        if (primaryKeyInfo) {

            this._searchParamsPk = new DashboardSearchParamsPk(primaryKeyInfo.lidType, primaryKeyInfo.lidId, primaryKeyInfo.customerId, primaryKeyInfo.merchantId, primaryKeyInfo.terminalNbr);

        } else {

            this._dashBoardSearchSvc
                .getSearchPkInfo(lidType, lidId)
                .subscribe(
                (data: DashboardPrimaryKeysModel) => {

                    if (true === data.recordFound) {

                        this._searchParamsPk = new DashboardSearchParamsPk(data.lidType, data.convertedLidPk, data.customerID, data.merchantID, data.terminalNbr);

                        let eventModel = new LidPrimeyKeyEventModel(data.lidType, data.convertedLidPk, lidId);
                        switch (data.lidType) {

                            case LidTypesEnum.TerminalNbr: {
                                let terminalInfo: TerminalPrimaryKeyInfoModel = new TerminalPrimaryKeyInfoModel(data.terminalNbr, lidId, data.merchantID, null, data.customerID, null);
                                this._primaryKeyCacheService.addTerminalInfoToCache(terminalInfo);
                            }
                                break;

                            case LidTypesEnum.MerchantID: {
                                let merchantInfo: MerchantPrimaryKeyInfoModel = new MerchantPrimaryKeyInfoModel(data.merchantID, lidId, data.customerID, null);
                                this._primaryKeyCacheService.addMerchantInfoToCache(merchantInfo);
                            }
                                break;

                            case LidTypesEnum.CustomerID: {
                                let merchantInfo: CustomerPrimaryKeyInfoModel = new CustomerPrimaryKeyInfoModel(data.customerID, lidId);
                                this._primaryKeyCacheService.addCustomerInfoToCache(merchantInfo);
                            }
                                break;
                        }

                    } else {
                        let errorMsg = this.getRecordNotFoundErrorMessage(lidType, lidId)
                        this._errorMessages.push(errorMsg);
                    }

                },
                (error: Response) => {
                    this._errorMessages.push("Error occurred");
                });

        }
    }

    public getPrimaryKeyFromCache(lidType: LidTypesEnum, lidId: string): PrimaryKeyInfoGenericModel {

        let primaryKeyInfo: PrimaryKeyInfoGenericModel = null;

        switch (lidType) {

            case LidTypesEnum.TerminalID:
                let terminalInfo: TerminalPrimaryKeyInfoModel = this._primaryKeyCacheService.getTerminalInfoFromCache(lidId);
                if (terminalInfo) {

                    primaryKeyInfo = new PrimaryKeyInfoGenericModel(terminalInfo.terminalNbr, LidTypesEnum.TerminalNbr, terminalInfo.customerId, terminalInfo.merchantId, terminalInfo.terminalNbr);
                }
                break;

            case LidTypesEnum.CustomerNbr:
                let customerInfo: CustomerPrimaryKeyInfoModel = this._primaryKeyCacheService.getCustomerInfoFromCache(lidId);
                if (customerInfo) {

                    primaryKeyInfo = new PrimaryKeyInfoGenericModel(customerInfo.customerId, LidTypesEnum.CustomerID, customerInfo.customerId, null, null);
                }
                break;

            case LidTypesEnum.MerchantNbr:
                let merchantInfo: MerchantPrimaryKeyInfoModel = this._primaryKeyCacheService.getMerchantInfoFromCache(lidId);
                if (merchantInfo) {

                    primaryKeyInfo = new PrimaryKeyInfoGenericModel(merchantInfo.merchantId, LidTypesEnum.MerchantID, merchantInfo.customerId, merchantInfo.merchantId, null);
                }
                break;

            default:
                break;
        }

        return primaryKeyInfo;        
    }

    private getRecordNotFoundErrorMessage(lidType: LidTypesEnum, lidNumber: string): string {

        let errorMessage: string = null;

        switch (lidType) {

            case LidTypesEnum.CustomerNbr:
                errorMessage = `No search results for Customer Nbr ${lidNumber}`;
                break;

            case LidTypesEnum.MerchantNbr:
                errorMessage = `No search results for Merchant Nbr ${lidNumber}`;
                break;

            case LidTypesEnum.TerminalID:
                errorMessage = `No search results for Terminal ID ${lidNumber}`;
                break;

            default:
                errorMessage = `No search results for ${lidNumber}`;
                break;
        }

        return errorMessage;
    }

    private getAdvanceSearchResult(lidType: LidTypesEnum, searchValue: string) {
        this._advanceSearchService.getAdvanceSearchResult(lidType, searchValue).subscribe(res => {
            this.result = res as DashboardAdvanceSearch[];
        });
    }
}
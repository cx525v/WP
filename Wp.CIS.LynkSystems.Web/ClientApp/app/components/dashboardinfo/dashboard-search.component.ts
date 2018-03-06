import { Component, Output } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormsModule, ReactiveFormsModule, ValidatorFn, AbstractControl } from '@angular/forms';
import { DropdownModule, SelectItem, InputTextModule,  } from 'primeng/primeng';
import { MenuItem } from 'primeng/primeng';            //api
import { AutoCompleteModule } from 'primeng/primeng';
import { TabViewModule } from 'primeng/primeng';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { DashboardInfoService } from './../../services/dashboardinfo.service';


import { SearchDataModelComponent } from './../../models/dashboard.search-data.model';
import {
    DashboardInfo, CustomerProfile, IMerchantInfo,
    DemographicsInfo, ActiveServices, TerminalProfileData
} from './../../models/dashboard.model'

import { LidTypesEnum } from '../../models/common/lid-types.enum';

import { DashboardPrimaryKeysModel } from '../../models/dashboardInfo/dashboard-primary-keys.model';

import { LidPrimaryKeyCacheEventsService } from '../../services/common/lid-primary-key-cache-events.service';
import { LidPrimeyKeyEventModel } from '../../models/common/lid-primary-key-event.model';
import {
    customerLidLengthsValidator,
    onlyAllowNumbersValidator,
    merchantLidValidator,
    terminalLidValidator
} from './search-custom.validators';

import { ErrorService } from '../../services/error.service';

import { SearchCriterialHelper } from './search-custom.validators';

@Component({
    selector: 'search-component',
    templateUrl: './dashboard-search.component.html',
    styleUrls: ['../app/app.component.css', './dashboard-search.component.css'],
    providers: [ErrorService]
})
export class DashBoardSearch {

    public formData: FormGroup;

    today: Date;

    dashboardInfo: DashboardInfo;

    merchantInfo: IMerchantInfo;

    customerProfile: CustomerProfile;

    activeServices: ActiveServices;

    termProfile: TerminalProfileData;

    selectId: string;

    selectedValue: number;

    searchList: SelectItem[];

    entityID: string;

    entityValue: string;

    public _errorMessages: Array<string>;

    public _selectedValueControl: FormControl;

    public _lidControl: FormControl;

    @Output() searchQuery: SearchDataModelComponent = new SearchDataModelComponent;

    formValid: boolean = false;
    LOGO = require("./../../../../Assets/logo.png");    

    constructor(fb: FormBuilder, private router: Router,
        private _dashBoardSearchSvc: DashboardInfoService,
        private _primaryKeyCacheService: LidPrimaryKeyCacheEventsService,
        private _errorService: ErrorService
    ) {

        this._selectedValueControl = new FormControl('', [Validators.required]);
        this._lidControl = new FormControl('', [Validators.required]);

        this.formData = fb.group({
            'selectedValue': this._selectedValueControl,
            'selectId': this._lidControl
        });
        
        this.searchList = [];
        this.searchList.push({ value: LidTypesEnum.TerminalID, label: "Terminal ID" });
        this.searchList.push({ value: LidTypesEnum.MerchantNbr, label: "Merchant Nbr" });
        this.searchList.push({ value: LidTypesEnum.CustomerNbr, label: "Customer Nbr" });

        this.merchantInfo = <IMerchantInfo>{};
        this.customerProfile = <CustomerProfile>{};
        this.activeServices = <ActiveServices>{};
        this.termProfile = <TerminalProfileData>{};

        this._errorMessages = new Array<string>();
    }

    public searchTypeChangedHandler(event: any): void {

        let newSearchCriteria: number = parseInt(event.value);

        this.setValidationsForSearchCriteria(newSearchCriteria);
    }


    private setValidationsForSearchCriteria(newSearchCriterial: number): void {

        let lidTypeNum: number = parseInt(this.formData.value.selectedValue);
        let lidType: LidTypesEnum = <LidTypesEnum>lidTypeNum;

        switch (lidType) {

            case LidTypesEnum.CustomerNbr:
                this.setCustomerLidValidators();
                break;

            case LidTypesEnum.MerchantNbr:
                this.setMerchantLidValidators();
                break;

            case LidTypesEnum.TerminalID:
                this.setTerminalLidValidators();
                break;

            default:
                break;
        }

    }

    private setCustomerLidValidators(): void {

        this._lidControl.clearValidators();

        let customerLengthValidator = customerLidLengthsValidator();

        let onlyNumbersValidator = onlyAllowNumbersValidator();

        let validators = [Validators.required, Validators.maxLength(10), onlyNumbersValidator, customerLengthValidator];

        this._lidControl.setValidators(validators);

        this._lidControl.updateValueAndValidity();

    }

    private setMerchantLidValidators(): void {

        this._lidControl.clearValidators();

        let merchantLid = merchantLidValidator();

        let onlyNumbersValidator = onlyAllowNumbersValidator();

        let validators = [Validators.required, Validators.maxLength(15), Validators.minLength(15), onlyNumbersValidator, merchantLid];

        this._lidControl.setValidators(validators);

        this._lidControl.updateValueAndValidity();
    }

    private setTerminalLidValidators(): void {

        this._lidControl.clearValidators();

        let terminalLid = terminalLidValidator();

        let validators = [Validators.required, Validators.maxLength(8), Validators.minLength(2), terminalLid];

        this._lidControl.setValidators(validators);

        this._lidControl.updateValueAndValidity();
    }

    public gotoDashboardPage(): void {

        this._errorMessages.length = 0;

        try {

            if (this.formData.valid) {

                this.searchQuery.searchEnitityType = LidTypesEnum[LidTypesEnum[this.formData.value.selectedValue]];
                this.searchQuery.searchEntityValue = this.formData.value.selectId;

                this.entityID = LidTypesEnum[LidTypesEnum[this.formData.value.selectedValue]];
                this.entityValue = this.searchQuery.searchEntityValue;

                let lidNumber: string = this.searchQuery.searchEntityValue;

                let lidTypeNum: number = parseInt(this.formData.value.selectedValue);
                let lidType: LidTypesEnum = <LidTypesEnum>lidTypeNum;

                let theLidId: string = this.entityValue;

                theLidId = this.formatLidId(lidType, theLidId);

                let lidIdFormatted: string = this.formatLidId(lidType, this.searchQuery.searchEntityValue);

                this._lidControl.setValue(lidIdFormatted);
                this.router.navigate(["/advancesearch/", lidType, lidIdFormatted]);
            }

        } catch (err) {

            this._errorService.logException(`Error in DashboardSearch.getDashboardInfo(). Selected Value: ${this.formData.value.selectedValue}. Selected ID: ${this.formData.value.selectId}`, err);
        }

    }

    private formatLidId(lidType: LidTypesEnum, theLidId: string) {

        let formattedLidId: string = theLidId;

        switch (lidType) {

            case LidTypesEnum.CustomerNbr:
                formattedLidId = this.formatLidIdCustomer(theLidId);
                break;

            case LidTypesEnum.TerminalID:
                formattedLidId = this.formatLidIdTerminal(theLidId);
                break;

            default:
                break;
        }

        return formattedLidId;
    }

    private formatLidIdCustomer(lidId: string): string {

        let formattedLidId: string = lidId;

        let regexp: RegExp = new RegExp(SearchCriterialHelper.SixDigitsRegExp);
        const isSix: boolean = regexp.test(lidId);

        if (true === isSix) {

            formattedLidId = `1000${lidId}`;
        }

        return formattedLidId;
    }

    private formatLidIdTerminal(lidId: string): string {

        let formattedLidId: string = lidId;

        let regexp: RegExp = new RegExp(SearchCriterialHelper.SixDigitsRegExp);
        let isMatch: boolean = regexp.test(lidId);

        if (true === isMatch) {

            formattedLidId = `LK${lidId}`;
            return formattedLidId;
        }

        regexp = new RegExp(SearchCriterialHelper.FiveDigitsRegExp);
        isMatch = regexp.test(lidId);

        if (true === isMatch) {

            formattedLidId = `LYK${lidId}`;
            return formattedLidId;
        }

        regexp = new RegExp(SearchCriterialHelper.TwoToFourDigitsRegExp);
        isMatch = regexp.test(lidId);

        if(true === isMatch) {

            let paddedString = this.pad(lidId, 5);
            formattedLidId = `LYK${paddedString}`;
            return formattedLidId;
        }

        return formattedLidId;
    }

    private pad(num: string, size: number): string {

        let s = num + "";
        while (s.length < size) s = "0" + s;
        return s;
    }

    public shouldDisplayCustomerIdMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.CustomerNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors
                && this._lidControl.value && this._lidControl.value.length > 0) {

                if (!this._lidControl.errors.maxlength && !this._lidControl.errors.onlyAllowNumbers && this._lidControl.errors.customerLidLengths) {
                    display = true;
                } else {
                    display = false;
                }
            }
        }

        return display;
    }

    public shouldDisplayMaxLengthErrorMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.CustomerNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors
                && this._lidControl.value && this._lidControl.value.length > 0) {

                if (this._lidControl.errors.maxlength) {

                    display = true;
                }
            }
        }

        return display;
    }

    public shouldDisplayOnlyNumbersErrorMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.CustomerNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors
                && this._lidControl.value && this._lidControl.value.length > 0) {

                if (this._lidControl.errors.onlyAllowNumbers) {

                    display = true;
                }
            }
        }

        return display;
    }

    public shouldDisplayMerchantGenericMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.MerchantNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors) {

                if (!this._lidControl.errors.maxlength && !this._lidControl.errors.minlength
                    && !this._lidControl.errors.onlyAllowNumbers && this._lidControl.errors.merchantLid
                    && this._lidControl.value && this._lidControl.value.length > 0) {
                    display = true;
                } else {
                    display = false;
                }
            }
        }

        return display;
    }

    public shouldDisplayMerchantOnlyNumbersErrorMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.MerchantNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors
                && this._lidControl.value && this._lidControl.value.length > 0) {

                if (this._lidControl.errors.onlyAllowNumbers) {

                    display = true;
                }
            }
        }

        return display;
    }

    public shouldDisplayMerchantLengthErrorMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.MerchantNbr == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors) {

                if (this._lidControl.errors.minlength || this._lidControl.errors.maxlength
                    && this._lidControl.value && this._lidControl.value.length > 0) {

                    display = true;
                }
            }
        }

        return display;
    }

    public shouldDisplayTerminalNumbersErrorMessage(): boolean {

        let display: boolean = false;

        let searchType: LidTypesEnum = this.getSelectedSearchType();

        if (LidTypesEnum.TerminalID == searchType) {

            if (true === this._lidControl.dirty && this._lidControl.errors
                && this._lidControl.value && this._lidControl.value.length > 0) {

                display = true;
            }
        }

        return display;
    }

    private getSelectedSearchType(): LidTypesEnum {

        let response: LidTypesEnum = <LidTypesEnum>parseInt(this._selectedValueControl.value, 10);

        return response;
    }

    public clearSearchClickHandler(): void {

        this._errorMessages.length = 0;
    }
    menu() {
        this.router.navigate(["/epstableload/"]);
    }
}
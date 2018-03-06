
import { Component, Inject, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { ResponseOptions, Response, ResponseType, ResponseOptionsArgs } from '@angular/http'
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";

import { Observable, Subscription } from 'rxjs/Rx'

import { LazyLoadEvent, DataTable } from 'primeng/primeng';

import { OnlyNumber } from '../../../directives/onlyNumber.directive';


import { TransactionService } from '../../../services/transaction.service';
import { TransactionInquiryTypesService } from '../../../services/transactionInquiryTypes.service';

import { TransactionsInquiryGeneralInfo } from '../../../models//transactions/transactionsInquiryGeneralInfo.model';

import { TransactionsInquiry } from '../../../models//transactions/transactionsInquiry.model';
import { TransactionInquiryTypes } from '../../../models//transactions/transactionInquiryTypes.model';

import { PrimeNgSelectOption } from '../../../models//transactions/primeNgSelectOption.model';
import { PrimeNgSelectItemValue } from '../../../models//transactions/primeNgSelecteItemValue.model';
import { GenericPaginationResponse } from '../../../models//transactions/genericPaginationResponse.model';

let theTransactionComp: TransactionInquiryComponent;

@Component({
    templateUrl: './transactionInquiry.component.html',
    providers: [TransactionService, TransactionInquiryTypesService],
    styleUrls: ['./transactionInquiry.component.css']
})
export class TransactionInquiryComponent implements OnInit, OnDestroy {

    @ViewChild(DataTable) dataTableComponent: DataTable;

    /**
     * @field This holds the general information about the terminal that
     *          is displayed on the screen.
     */
    public _transactionInfoModel: TransactionsInquiryGeneralInfo;

    /**
     * @field This holds the records for the transactions associated with the terminal
     *          These records will be displayed in the grid.
     */
    public _gridDataSource: Array<TransactionsInquiry>;

    /**
     * @field This holds the total number of records for the current search
     *          This is used for by the grid for the paging.
     */
    public _totalRecords: number;

    /**
     * @field This will hold the search criteria options that are being displayed in
     *          the dropdown list box on the screen.
     */
    public _searchCriteriaOptions: Array<PrimeNgSelectOption<TransactionInquiryTypes>>;

    public _batchNumber: string;

    /**
     * @field This holds the error message that will be displayed to the user.
     */
    public _errorMessage: string;

    /**
     * @field This holds the terminal id that is being displayed on the screen.
     */
    private _terminalId: string;

    private _previousTerminalId: string;

    /**
     * @field This holds the new terminal id that the user enters into the screen.
     */
    private _newTerminalId: string;

    private _cardNumberFirstSixErrorMessage: string;

    /**
     * @field This is used to indicate the first page in the grid.
     *          This is used to programmatically set the current
     *          page in the grid.
     */
    public first: number;

    /**
     * @field This holds the form group for the search transaction section
     */
    public _searchForm: FormGroup;

    /**
     * @field This holds the form control for the search criterial dropdown list
     */
    public _searchCriteriaOptionsControl: FormControl;

    /**
     * @field This holds the form control for the begin date range
     */
    public _beginDateRangeCalendarControl: FormControl;

    /**
     * @field This holds the form control for the end date range
     */
    public _endDateRangeCalendar: FormControl;

    /**
     * @field This holds the form control for the first six numbers
     *          of the card number
     */
    public _cardNumberFirstSixControl: FormControl;

    /**
     * @field This holds the form control for the last four numbers of the card.
     */
    public _cardNumberLastFourControl: FormControl

    /**
     * @field This holds the form control for the batch number
     */
    public _batchNumberInputControl: FormControl;

    /**
     * @field This holds the form control for the credit card type
     *          radio buttons.
     */
    private _cardNumberType: FormControl;

    /**
     * @field This is used to display the dialog that 
     */
    private _displayTerminalNotFoundDialog: boolean;

    /**
     * @constructor This  initializes the class.
     * @param _transactionService
     * @param _transactionInquiryTypesService
     * @param _route
     * @param _activatedRoute
     */
    constructor(private _transactionService: TransactionService,
                private _transactionInquiryTypesService: TransactionInquiryTypesService,
                private _route: Router,
                private _activatedRoute: ActivatedRoute,
                private fb: FormBuilder) {

        theTransactionComp = this;

        this.first = 0;

        this._displayTerminalNotFoundDialog = false;

        this._previousTerminalId = null;

        this._cardNumberFirstSixErrorMessage = null;
    }

    /**
     * @method This is used to handle the initialization for the component
     */
    public ngOnInit(): void {

        this.initFormControls();

        this.initSearchCriterialOptions();

        this.initParameters();
    };

    /**
     * @method This performs cleanup for the component
     */
    public ngOnDestroy(): void {

        theTransactionComp = null;
    };

    /**
     * @method This is used to initialize the form controls
     */
    private initFormControls(): void {

        this._searchCriteriaOptionsControl = new FormControl('', null);
        this._beginDateRangeCalendarControl = new FormControl('', Validators.required);
        this._endDateRangeCalendar = new FormControl('', Validators.required);
        this._cardNumberFirstSixControl = new FormControl('',
            [
                Validators.pattern("(.{0}|(4[0-9]{5})|(5[1-5][0-9]{4})|(222[1-9][0-9]{2})"
                    + "|(22[3-9][0-9][0-9]{2})|(2[3-6][0-9]{4})|(27[01][0-9]{3})"
                    + "|(2720[0-9]{2})|(3[47][0-9]{4})|(30[1-5][0-9]{3})"
                    + "|(3[68][0-9]{4})|(5[0-9]{5})|(6011[0-9]{2})|(65[0-9]{4})"
                    + "|(2131[0-9]{2})|(1800[0-9]{2})|(35[0-9]{4}))"
                )
        ]);
        this._cardNumberLastFourControl = new FormControl('', [Validators.pattern("(.{0}|[0-9]{4})")]);
        this._batchNumberInputControl = new FormControl('', null);
        this._cardNumberType = new FormControl('', null);
        this._cardNumberType.setValue("1");

        this._searchForm = this.fb.group({
            "searchCriteriaOptions": this._searchCriteriaOptionsControl,
            "beginDateRangeCalendar": this._beginDateRangeCalendarControl,
            "endDateRangeCalendar": this._endDateRangeCalendar,
            "cardNumberFirstSix": this._cardNumberFirstSixControl,
            "cardNumberLastFour": this._cardNumberLastFourControl,
            "batchNumberInput": this._batchNumberInputControl,
            "cardNumberType": this._cardNumberType
        }
        );

    };

    /**
     * @method This is used to initialize the parameters for the controller
     */
    private initParameters(): void {

        let i: number = 0;

        this._activatedRoute
                        .params
                        .subscribe((params: any) => {
            
                            let theTerminalId = params.terminalId;
                            theTransactionComp._terminalId = theTerminalId
                            theTransactionComp._newTerminalId = theTerminalId;

                            this.getTerminalInfo();
            });

    };

    /**
     * @method This is used to apply the search criteria for the controller.
     */
    public applySearchCriteria() {

        this._errorMessage = "";

        this.dataTableComponent.reset();
    };

    /**
     * @method This is used to check that either the card number or the
     *          batch id have been entered but not both.
     * @returns True is returned if the rule is valid. False is returned
     *          otherwise.
     */
     cardNumberBatchNumberValidationRule(): boolean {

        let isValid: boolean = true;

        if (this._cardNumberFirstSixControl
            && this._cardNumberLastFourControl
            && this._batchNumberInputControl
            && ((this._cardNumberFirstSixControl.value && this._cardNumberFirstSixControl.value.length > 0)
                || (this._cardNumberLastFourControl.value && this._cardNumberLastFourControl.value.length > 0))
            && this._batchNumberInputControl.value.length > 0) { 

            isValid = false;
        }

        return isValid;
    }

    /**
     * @method This is used to ensure that if either the first six or the
     *          last four digits of the card number are filled in that the
     *          the other field is filled in as well.
     * @returns True is returned if the validation passes. False is returned
     *          otherwise.
     */
     cardNumberValidationRule(): boolean {

         let isValid: boolean = true;


        if (!this._cardNumberFirstSixControl.value
            && this._cardNumberLastFourControl.value) {
            isValid = false;
        }

        if (!this._cardNumberLastFourControl.value
            && this._cardNumberFirstSixControl.value) {

            isValid = false;
        }

        if (this._cardNumberLastFourControl.value
            && this._cardNumberFirstSixControl.value
            && this._cardNumberLastFourControl.value.length > 0
            && this._cardNumberFirstSixControl.value.length < 1) {

            isValid = false;
        }

        if (this._cardNumberFirstSixControl.value
            && this._cardNumberLastFourControl.value
            && this._cardNumberFirstSixControl.value.length > 0
            && this._cardNumberLastFourControl.value.length < 1) {

            isValid = false;
        }

         return isValid;
     }

  
    /**
     * @method This is used to determine whether the button for applying the search
     *          criteria should be disabled
     * @returns true is returned if the button should be disabled. False is returned
     *          otherwise.
     */
    public shouldDisableSearchButton(): boolean {

        let isValid: boolean = true;

        let isCreditCardValid = this.cardNumberValidationRule();

        let isCreditCardBatchValid = this.cardNumberBatchNumberValidationRule();

        let isSearchDateRangeValid = this.checkValidDateRange();

        let isEndDateAfterBeginDate = this.checkEndDateAfterBeginDate();

        if (false === this._searchForm.valid
            || false === isCreditCardValid
            || false === isCreditCardBatchValid
            || false === isSearchDateRangeValid
            || false === isEndDateAfterBeginDate) {

            isValid = false;
        }

        return !isValid;
    }

    /**
     * @method This is used to populate the search criteria drop down list box.
     *          The values will come from a table in the database.
     */
    private initSearchCriterialOptions(): void {

        this._searchCriteriaOptions = new Array<PrimeNgSelectOption<TransactionInquiryTypes>>();

         this._transactionInquiryTypesService
            .getAllActiveTransactionInquiryTypes()
            .subscribe(
             (data: Array<TransactionInquiryTypes>) => {
                 
                 for (let index: number = 0; index < data.length; index++) {
                     let currentItem: TransactionInquiryTypes = data[index];
                     let newSelectOption = new PrimeNgSelectOption(currentItem.displayName, currentItem)
                     this._searchCriteriaOptions.push(newSelectOption);
                 }

                 this._searchCriteriaOptionsControl.setValue(this._searchCriteriaOptions[0]);
            },
             (error: Response) => {

                 this._errorMessage = error.text();
            }
            );

    };

    /**
     * @method This is used to retrieve the general data for the terminal
     */
    private getTerminalInfo(): void {

        this._transactionService.getTerminalInfo(this._terminalId)
            .subscribe(
            (data: TransactionsInquiryGeneralInfo) => {

                this._transactionInfoModel = data

                if (null === data) {

                    this._displayTerminalNotFoundDialog = true;
                } else {

                    if (this.dataTableComponent) {
                        this.dataTableComponent.reset();
                    }
                }

            },
            (error: Response) => {

                this._errorMessage = error.text();

            }
            );
    };

    /**
     * @method This is called in order to retrieve a page of data for the
     *          grid.
     * @param event This holds the even information.
     */
    public loadData(event: LazyLoadEvent): void {

        let terminalId = this._terminalId.toString();

        let searchCriteria: PrimeNgSelectOption<TransactionInquiryTypes> = this._searchCriteriaOptionsControl.value as PrimeNgSelectOption<TransactionInquiryTypes>;

        let searchCriteriaId: number = null;

        if (searchCriteria
            && searchCriteria.value) {

            searchCriteriaId = searchCriteria.value.id;
        }

        let beginDateRange: Date = this._beginDateRangeCalendarControl.value as Date;

        let endDateRange: Date = this._endDateRangeCalendar.value as Date;

        let firstSix: string = this._cardNumberFirstSixControl.value as string;

        let lastFour: string = this._cardNumberLastFourControl.value as string;

        let batchNumber: string = this._batchNumberInputControl.value as string;

        let cardType: string = this._cardNumberType.value as string;

        if (searchCriteria
            && beginDateRange
            && endDateRange) {

            this._transactionService.getTransactionRecordsForSearch(
                this._transactionInfoModel,
                terminalId,
                beginDateRange,
                endDateRange,
                searchCriteriaId,
                firstSix,
                lastFour,
                cardType,
                batchNumber,
                event.first,
                event.rows)
                .subscribe(
                (data: GenericPaginationResponse<TransactionsInquiry>) => {

                    if (data) {

                        this._gridDataSource = data.returnedRecords;
                        this._totalRecords = data.totalNumberOfRecords;
                    } else {

                        this._gridDataSource = null;
                        this._totalRecords = 0;
                    }
                    
                },
                (error: Response) => {
                    this._errorMessage = error.text();
                }
                );
        }

 

    };

    /**
     * @method This is called when the user wants to display the information
     *          for a different terminal. This is called when the "Apply Terminal ID"
     *          button is clicked.
     */
    public applyTerminalId(): void {

        this._errorMessage = "";

        this._previousTerminalId = this._terminalId;

        let terminalId: string = this._newTerminalId;

        this._route.navigate(["/transactionInquiry", terminalId]);
    };

    /**
     * @method This is used to determine if the terminal status is active
     * @returns True is returned if the status is active. False is returned
     *          otherwise.
     */
    public isStatusActive(): boolean {

        let response: boolean = false;

        if (this._transactionInfoModel
            && this._transactionInfoModel.statusDesc) {

            let statusLower: string = this._transactionInfoModel.statusDesc.toLowerCase();

            if (statusLower === 'active') {

                response = true;
            }
        }

        return response;
    };

    /**
     * @method This is used to check that the date range is valid.
     * @returns True is returned if the search date range is valid
     *          false is returned otherwise.
     */
    public checkValidDateRange(): boolean {

        let isDateRangeValid: boolean = true;

        if (this._endDateRangeCalendar
            && this._beginDateRangeCalendarControl
            && this._endDateRangeCalendar.dirty
            && this._beginDateRangeCalendarControl.dirty) {

            let daysDifference = this.daysBetweenDates(this._beginDateRangeCalendarControl.value, this._endDateRangeCalendar.value );

            if (daysDifference
                && daysDifference > 32) {

                isDateRangeValid = false;
            }
        }

        return isDateRangeValid;
    }

    /**
     * @method This is used to ensure that the end date is after the beginning date.
     * @returns True is returned if the end date is after the begin date. False is
     *          returned otherwise.
     */
    public checkEndDateAfterBeginDate(): boolean {

        let isDateRangeValid: boolean = true;

        if (this._endDateRangeCalendar
            && this._beginDateRangeCalendarControl
            && this._endDateRangeCalendar.dirty
            && this._beginDateRangeCalendarControl.dirty) {

            let daysDifference = this.daysBetweenDates(this._beginDateRangeCalendarControl.value, this._endDateRangeCalendar.value);

            if (daysDifference
                && daysDifference < 0) {

                isDateRangeValid = false;
            }
        }

        return isDateRangeValid;
    }

    /**
     * @method This is the handler when the begin date range has changed
     *          It will ensure that the begin and end dates are within
     *          32 days of each other.
     */
    public beginDateRangeChangedHandler(): void {

         this.checkValidDateRange();

    };

    /**
     * @method This is called when the end date has changed. This will
     *          alter the min/max values of the begin date in order
     *          to ensure that the dates are within 32 days of one
     *          another.
     */
    public endDateRangeChangedHandler(): void {

        this.checkValidDateRange();
    };

    /**
     * @method This is used to determine the number of days between two dates
     * @param date1 This is the first date that will be used for the comparison
     * @param date2 This is the second date that will be used for the comparison
     * @returns This is the number of days between two dates. If either date is null
     *          then the value will be null.
     */
    private daysBetweenDates(date1: Date, date2: Date): number {   //Get 1 day in milliseconds   

        let daysDiff: number = null;

        if (date1
            && date2) {

            let one_day = 1000 * 60 * 60 * 24;    // Convert both dates to milliseconds
            let date1_ms = date1.getTime();
            let date2_ms = date2.getTime();    // Calculate the difference in milliseconds  
            let difference_ms = date2_ms - date1_ms;        // Convert back to days and return   
            daysDiff = Math.round(difference_ms / one_day);

        }

        return daysDiff;
    } 

    /**
     * @method This is the click handler for the button that 
     */
    public terminalNotFoundDialogOkButtonHandler() {

        this._displayTerminalNotFoundDialog = false;

        if (this._previousTerminalId) {

            let terminalId: string = this._previousTerminalId;

            this._route.navigate(["/transactionInquiry", terminalId]);
        }
 
    };

}

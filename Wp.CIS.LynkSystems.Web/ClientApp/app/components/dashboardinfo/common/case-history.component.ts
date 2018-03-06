
import { Component, OnInit, OnDestroy, Input, ViewChild } from '@angular/core';
import { Response } from '@angular/http';

import { LazyLoadEvent, DataTable, SelectItem } from 'primeng/primeng';

import { CaseHistory } from './../../../models/dashboard.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';

import { Subscription } from 'rxjs/Subscription';

import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';

//import { terminalEquipment } from '../../../models/dashboardInfo/terminalequipment.model'
import { CaseHistoryService } from '../../../services/dashboardinfo/case-history.service';
import { PaginationCaseHistoryModel } from '../../../models/caseHistory/pagination-case-history.model';
import { GenericPaginationResponse } from '../../../models/transactions/genericPaginationResponse.model'
import { CaseHistoryInputModel } from '../../../models/caseHistory/case-history-input.model';
import { TerminalEquipment } from '../../../models/dashboardInfo/terminalequipment.model'
import { ViewEncapsulation } from '@angular/core';
@Component({

    selector: 'case-history',
    templateUrl: './case-history.component.html',
    styleUrls: ['./case-history.component.css'],
    providers: [CaseHistoryService],
    encapsulation: ViewEncapsulation.None,
})
export class CaseHistoryComponent implements OnInit, OnDestroy {

    @ViewChild(DataTable) _dataTableControl: DataTable;

    public _errorMessage: string;

    private _caseHistoryRecords: Array<CaseHistory>;

    private _eventServiceSubscription: Subscription;

    private _totalNumberOfCaseHistoryRecords: number;

    private _lidType: LidTypesEnum;

    private _lidValue: number;

    private _selectedTerminal: TerminalEquipment;

    private _shouldEnableLazyLoading: boolean;

    private _caseHistoryRecordsLoading: boolean;

    private _createDateFilter: Date;

    @Input('caseHistory')
    set caseHistory(value: Array<CaseHistory>) {

        this._caseHistoryRecords = value;
    }

    @Input('TotalNumberOfCaseHistoryRecords')
    set TotalNumberOfCaseHistoryRecords(value: number) {

        this._totalNumberOfCaseHistoryRecords = value;
    }

    get TotalNumberOfCaseHistoryRecords(): number {

        return this._totalNumberOfCaseHistoryRecords;
    }

    @Input("LidType")
    set LidType(value: LidTypesEnum) {

        this._lidType = value;
    }

    @Input("LidValue")
    set LidValue(value: number) {
        this._lidValue = value;
    }

    constructor(private _dashboardEventsService: DashboardEventsService,
        private _caseHistoryService: CaseHistoryService) {

        this._shouldEnableLazyLoading = false;

        this._selectedTerminal = null;

        this._dataTableControl = null;

        this._errorMessage = null;

        this._totalNumberOfCaseHistoryRecords = null;

        this._caseHistoryRecordsLoading = false;

        this._caseHistoryRecords = [];

        this._createDateFilter = null;

        this._eventServiceSubscription = this._dashboardEventsService.terminalEquipmentChangeList$.subscribe(
            (terminal: TerminalEquipment) => {

                this._selectedTerminal = terminal;

                this._lidType = LidTypesEnum.TerminalNbr;
                this._lidValue = terminal.terminalNbr;
                this._dataTableControl.reset();
                this.refreshDataInGrid(terminal);

            });
    }

    public ngOnInit(): void {

        this._shouldEnableLazyLoading = true;
    }

    public ngOnDestroy(): void {

        if (this._eventServiceSubscription) {

            this._eventServiceSubscription.unsubscribe();
            this._eventServiceSubscription = null;
        }
    }

    public shouldEnableLazyLoading(): boolean {


        return this._shouldEnableLazyLoading;
    }

    private refreshDataInGrid(terminal: TerminalEquipment): void {

        this._createDateFilter = null;
        this._dataTableControl.sortField = "createDate";
        this._dataTableControl.sortOrder = -1;

        this._errorMessage = null;
        let inputModel: CaseHistoryInputModel = new CaseHistoryInputModel();
        let pagingModel: PaginationCaseHistoryModel = new PaginationCaseHistoryModel();
        pagingModel.pageSize = this._dataTableControl.rows;
        pagingModel.skipRecordNumber = this._dataTableControl.first;
        pagingModel.sortField = this._dataTableControl.sortField;
        pagingModel.sortFieldByAsc = false;
        inputModel.page = pagingModel;
        inputModel.lidTypeEnum = this._lidType;
        inputModel.lidValue = this._lidValue.toString();

        if (this._dataTableControl.filters) {

            if (this._dataTableControl.filters.caseLevel) {

                inputModel.page.filterCaseLevel = this._dataTableControl.filters.caseLevel.value;
            }

            if (this._dataTableControl.filters.caseDesc) {

                inputModel.page.filterCaseDesc = this._dataTableControl.filters.caseDesc.value;
            }

            if (this._dataTableControl.filters.caseId) {

                inputModel.page.filterCaseId = this._dataTableControl.filters.caseId.value;
            }

            if (this._dataTableControl.filters.ordDeptName) {

                inputModel.page.filterOrgDeptName = this._dataTableControl.filters.ordDeptName.value;
            }
        }

        setTimeout(() => this._caseHistoryRecordsLoading = true, 0);

        this._caseHistoryService
            .getPageOfCaseHistoryRecords(inputModel)
            .subscribe((data: GenericPaginationResponse<CaseHistory>) => {

                this._caseHistoryRecordsLoading = false;

                this._totalNumberOfCaseHistoryRecords = data.totalNumberOfRecords;

                this._caseHistoryRecords = data.returnedRecords;

            }, (data: Response) => {

                this._caseHistoryRecordsLoading = false;
                this._errorMessage = data.text();
            }
        );
    }

    public loadPageOfDataInGrid(event: LazyLoadEvent) {

        this._errorMessage = null;

        let inputModel: CaseHistoryInputModel = new CaseHistoryInputModel();
        let pagingModel: PaginationCaseHistoryModel = new PaginationCaseHistoryModel();
        pagingModel.pageSize = event.rows;
        pagingModel.skipRecordNumber = event.first;
        pagingModel.sortField = event.sortField;
        inputModel.page = pagingModel;
        inputModel.lidTypeEnum = this._lidType;
        inputModel.lidValue = this._lidValue.toString();

        if (event.filters) {

            if (event.filters.caseLevel) {

                inputModel.page.filterCaseLevel = event.filters.caseLevel.value;
            }

            if (event.filters.caseDesc) {

                inputModel.page.filterCaseDesc = event.filters.caseDesc.value;
            }

            if (event.filters.caseId) {

                inputModel.page.filterCaseId = event.filters.caseId.value;
            }

            if (event.filters.orgDeptName) {

                inputModel.page.filterOrgDeptName = event.filters.orgDeptName.value;
            }

            if (event.filters.createDate) {

                inputModel.page.filterCreateDate = event.filters.createDate.value;
            }
        }

        if (event.sortOrder) {

            if (event.sortOrder > 0) {
                pagingModel.sortFieldByAsc = true;
            } else {
                pagingModel.sortFieldByAsc = false;
            }
        } else {
            pagingModel.sortFieldByAsc = true;
        }

        setTimeout(() => this._caseHistoryRecordsLoading = true, 0);

        this._caseHistoryService
            .getPageOfCaseHistoryRecords(inputModel)
            .subscribe((data: GenericPaginationResponse<CaseHistory>) => {

                this._caseHistoryRecordsLoading = false;

                setTimeout(() => {

                    this._totalNumberOfCaseHistoryRecords = data.totalNumberOfRecords;
                    this._caseHistoryRecords = data.returnedRecords;
                });

            }, (data: Response) => {

                this._caseHistoryRecordsLoading = false;
                this._errorMessage = data.text();
            }
        );
    }

    public createdDateFilterSelectedEventHandler(param1: any, param2: any, param3: any): void {

        this._dataTableControl.filter(param1, "createDate", "exact");
    }

    public clearCreateDateFilter(event: Event): void {

        this._createDateFilter = null;

        this._dataTableControl.filter(null, "createDate", "exact");

    }
}
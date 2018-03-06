
import { Component, OnInit, OnDestroy, Input, ViewChild } from '@angular/core';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { TerminalInfoService} from '../../../services/dashboardinfo/terminal.service';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { TerminalEquipment, TerminalFilter, TerminalPage } from '../../../models/dashboardInfo/terminalequipment.model';
import { DashboardMerchantComponent } from '../merchant/dashboard-merchant.component';
import { DashboardEventsService } from '../../../services/dashboardinfo/dashboard-events.service';
import { Location } from '@angular/common';
import { ViewEncapsulation } from '@angular/core';
import { LazyLoadEvent, DataTable, SelectItem } from 'primeng/primeng';
import { DatePipe } from '@angular/common';
@Component({

    selector: 'terminal-equipment',
    templateUrl: './terminal-equipment.component.html',
    styleUrls: ['./terminal-equipment.component.css'],
    providers: [TerminalInfoService],
    encapsulation: ViewEncapsulation.None
})
export class TerminalEquipmentComponent implements OnInit, OnDestroy {
    isTerminal: boolean;
    searchParamsPk: DashboardSearchParamsPk;  
    terminalEquipments: TerminalEquipment[] = [];
    selectedTerminalEquipment: TerminalEquipment;
    totalRecords: number;
    lazy: boolean = false;
    firstTimeload: boolean = true;
    errorMsg: string;
    loading: boolean = true;
    _deActivateDate: Date;
    private _parent: DashboardMerchantComponent;
    public _selectedTerminalEquipment: TerminalEquipment;

    @Input() set parent(value: DashboardMerchantComponent) {
        this.errorMsg = null;
        if (value) {
            this._parent = value;
            this.searchParamsPk = value.SearchParamsPk;
            if (this.searchParamsPk && this.searchParamsPk.terminalNbr) {

                this.isTerminal = true;
            } else {
                this.isTerminal = false;
            }
            this.loadTerminalEquipments(null);
        }
   }
    get parent(): DashboardMerchantComponent {
        return this._parent;
    }
    @ViewChild(DataTable) _dataTableControl: DataTable;
    constructor(
        private _dashboardEventsService: DashboardEventsService,
        private _location: Location,
        private terminalService: TerminalInfoService) {
        this._selectedTerminalEquipment = null;
    }

    public ngOnInit(): void {
       
    }

    public ngOnDestroy(): void {

    }

    loadTerminalEquipments(event) {       
        if (this.errorMsg) {
            this.errorMsg = null;
        }
        this.loading = true;
        var page: TerminalPage;
        if (event) {
            var FilterByDate: string;
            var FilterBySoftware: string;
            var FilterByStatus: string;
            var FilterByEquipment: string;
            var FilterByTID: number;
            if (event.filters) {
                if (event.filters.equipment) {
                    FilterByEquipment = event.filters.equipment.value;
                }
                if (event.filters.software) {
                    FilterBySoftware = event.filters.software.value;
                }

                if (event.filters.deactivateActivateDate) {
                    FilterByDate = event.filters.deactivateActivateDate.value;
                }
                if (event.filters.status) {
                    FilterByStatus = event.filters.status.value;
                }

                if (event.filters.terminalID) {
                    FilterByTID = event.filters.terminalID.value;
                }
            }

            page = {
                FilterDate: FilterByDate,
                FilterSoftware: FilterBySoftware,
                FilterStatus: FilterByStatus,
                FilterStatusEquipment: FilterByEquipment,
                FilterTID: FilterByTID,
                PageSize: event.rows,
                SkipRecordNumber: event.first,
                SortField: event.sortField,
                SortFieldByAsc: event.sortOrder == 1,
            };
        } else {
            page = {
                SkipRecordNumber: 0,
                PageSize: 0,
                SortField: 'terminalID',
                SortFieldByAsc: true
            };
        }

            
        var filter: TerminalFilter = {
            lidTypeEnum: this.searchParamsPk.lidType,
            LIDValue: this.searchParamsPk.merchantId.toString(),
            Page: page
        }

     
        this.terminalService.getTerminalList(filter).subscribe(
            r => {                   
                var resp = r as apiResponse<TerminalEquipment>;
                this.terminalEquipments = resp.returnedRecords as TerminalEquipment[];  
                if (this.terminalEquipments) {
                    this.terminalEquipments.forEach(te => {
                        te.deactivateActivateDate = this.convertShort(te.deactivateActivateDate);

                    });
                }
                    if (this.terminalEquipments && this.isTerminal && this.searchParamsPk.terminalNbr) {
                        this.terminalEquipments = this.terminalEquipments.filter(t => t.terminalNbr == this.searchParamsPk.terminalNbr);
                    }

                    if (this.isTerminal) {
                        if (this.terminalEquipments) {
                            this.totalRecords = this.terminalEquipments.length;
                        } else {
                            this.totalRecords = 0;
                        }
                    } else {
                        this.totalRecords = resp.totalNumberOfRecords;
                    }

                    if (this.firstTimeload) {
                        this.firstTimeload = false;
                        if (this.totalRecords == this.terminalEquipments.length) {      
                            this.lazy = false;
                        }
                    }           
                    this.loading = false;
            },
            error => {
                this.errorMsg = error.text();
                this.loading = false;
            }
        );     
     }

    public handleRowSelect(event: Event): void {

        this._dashboardEventsService
            .announceTerminalEquipmentChange(this._selectedTerminalEquipment);

        this._location.replaceState(`/dashboardinfo/${LidTypesEnum.TerminalID}/${this._selectedTerminalEquipment.terminalID}`);
        if (this.parent) {
            this.parent.TerminalNbr = this._selectedTerminalEquipment.terminalNbr; 
            this.parent.terminalID = this._selectedTerminalEquipment.terminalID;
        }
    }

    public clearCreateDateFilter(event: Event): void {
       this._deActivateDate = null;
       this._dataTableControl.filter(null, "deactivateActivateDate", "exact");
    }


    convertShort(date: any): string {
        if (date) {
            var datePipe = new DatePipe('en-us');
            return datePipe.transform(date, 'short');
        } else {
            return '';
        }
    }

    convertMDY(date: any): string {
        if (date) {
            var datePipe = new DatePipe('en-us');
            return datePipe.transform(date, 'M/d/y');
        } else {
            return '';
        }
    }
}
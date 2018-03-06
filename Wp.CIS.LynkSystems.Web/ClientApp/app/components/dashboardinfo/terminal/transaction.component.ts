
import { Component, OnInit, OnDestroy, SimpleChanges, Input, OnChanges, ViewChild } from '@angular/core';
import { TransactionService } from '../../../services/dashboardinfo/transaction.service';
import { Transaction,TransactionFilter,TransactionPage,TransactionType } from '../../../models/dashboardInfo/transaction.model';
import { LazyLoadEvent,DataTable, SelectItem } from 'primeng/primeng';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { ViewEncapsulation } from '@angular/core';
import { DatePipe } from '@angular/common';
@Component({

    selector: 'transaction',
    templateUrl: './transaction.component.html',
    styleUrls: ['./transaction.component.css'],
    providers: [TransactionService],
    encapsulation: ViewEncapsulation.None
})

export class TransactionComponent implements OnInit, OnDestroy{
   
    errorMsg: string;
    @Input() isSettled: boolean;

    _terminalNbr: string;
    @Input('terminalNbr')
    set terminalNbr(value: string) {
        this._terminalNbr = value;
        this.checkTerminalId();
    }
    get terminalNbr() {
        return this._terminalNbr;
    }
     _terminalID: string;
    @Input('terminalID')
    set terminalID(value: string) { 
        if (this.errorMsg) {
            this.errorMsg = null;
        }        
        if (value) {
            let id: string = value.trim();
            if (id && id !== "*") {
                this._terminalID = id
                this.loadTransaction(null);
            } else { 
                this._terminalID = null;
                this.loading = false;
                this.checkTerminalId();
            } 
        }      
    }
   
    get terminalID() {
        return this._terminalID;
    }
   

    transactions: Transaction[] =[];
    totalRecords: number;
    lazy: boolean = false;
    loading: boolean = true;
    pageSize: number = 5;
    firstTimeLoad: boolean = true;
    @ViewChild(DataTable) _dataTableControl: DataTable;
    _theDate: Date;
    constructor(private transactionSrv: TransactionService) {

    }

    checkTerminalId() {
        if (!this._terminalID) {
            this.errorMsg = 'For terminalNbr ' + this._terminalNbr + ', there is no associated Terminal ID so cannot retrieve Transaction History';
        }
    }

    public ngOnInit(): void {
       
    }

    public ngOnDestroy(): void {
    }

    loadTransaction(event: LazyLoadEvent) {  
        if (this._terminalID) {
            this.loading = true;
            if (this.errorMsg) {
                this.errorMsg = null;
            }

            var transactionType: TransactionType;
            if (this.isSettled) {
                transactionType = TransactionType.Settled;
            } else {
                transactionType = TransactionType.Acquired;
            }

            var page: TransactionPage;
            if (event) {
                var FilterByDate: string;
                var FilterByAmount: string;
                var FilterByLast4: string;
                var FilterByTranType: string;
                var FilterByNetworkCD: string;
                var FilterByDesc: string;
                if (event.filters) {
                    if (event.filters.reQ_BUS_DATE) {
                        FilterByDate = event.filters.reQ_BUS_DATE.value;
                    }
                    if (event.filters.reQ_AMT) {
                        FilterByAmount = event.filters.reQ_AMT.value;
                    }

                    if (event.filters.reQ_PAN_4) {
                        FilterByLast4 = event.filters.reQ_PAN_4.value;
                    }

                    if (event.filters.reQ_TRAN_TYPE) {
                        FilterByTranType = event.filters.reQ_TRAN_TYPE.value;
                    }

                    if (event.filters.resP_NETWRK_AUTH_CD) {
                        FilterByNetworkCD = event.filters.resP_NETWRK_AUTH_CD.value;
                    }
                    if (event.filters.descript) {
                        FilterByDesc = event.filters.descript.value;
                    }
                }

                page = {
                    FilterByDate: FilterByDate,
                    FilterByAmount: FilterByAmount,
                    FilterByLast4: FilterByLast4,
                    FilterByTranType: FilterByTranType,
                    FilterByNetworkCD: FilterByNetworkCD,
                    FilterByDesc: FilterByDesc,
                    PageSize: event.rows,
                    SkipRecordNumber: event.first,
                    SortField: event.sortField,
                    SortFieldByAsc: event.sortOrder == 1,
                    TransactionType: transactionType
                };
            } else {
                page = {
                    PageSize: 0,
                    SkipRecordNumber: 0,
                    TransactionType: transactionType,
                    SortField: 'reQ_BUS_DATE',
                    SortFieldByAsc: false
                };
            }

            var filter: TransactionFilter = {
                lidTypeEnum: LidTypesEnum.TerminalID,
                LIDValue: this.terminalID,
                Page: page
            }


            this.transactionSrv.getTransactionHistory(filter).subscribe(
                r => {
                    var resp = r as apiResponse<Transaction>;

                    this.transactions = resp.returnedRecords as Transaction[];
                    if (this.transactions) {
                        this.transactions.forEach(tr => {
                            tr.reQ_BUS_DATE = this.convertJMS(tr.reQ_BUS_DATE);
                        });
                    }
                    this.totalRecords = resp.totalNumberOfRecords;
                    if (this.firstTimeLoad) {
                        this.firstTimeLoad = false;
                        if (this.totalRecords === resp.returnedRecords.length) {
                            this.lazy = false;
                        } else {
                            this.lazy = true;
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
    }

    public clearDateFilter(event: Event): void {
        this._theDate = null;
        this._dataTableControl.filter(null, "reQ_BUS_DATE", "exact");
    }


    convertJMS(date: any): string {
        if (date) {
            var datePipe = new DatePipe('en-us');
            return this.convertMDY(date) + ' ' + datePipe.transform(date, 'jms');
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
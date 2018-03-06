import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { MerchantLocation } from '../../../models/dashboard.model';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';

import { MerchantFilter, MerchantPage } from '../../../models/dashboardInfo/merchantlocation.model';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { MerchantLocationService } from '../../../services/dashboardinfo/merchantlocation.service';
import { LazyLoadEvent } from 'primeng/primeng';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { ViewEncapsulation } from '@angular/core';


import { Router } from '@angular/router';

@Component({

    selector: 'merchant-locations',
    templateUrl: './merchant-locations.component.html',
    styleUrls: ['./merchant-locations.component.css'],
    providers: [MerchantLocationService],
    encapsulation: ViewEncapsulation.None,
})
export class MerchantLocationsComponent implements OnInit, OnDestroy {

    public _selectedMerchantLocation: MerchantLocation;
    @Input() totalMerchantRecords: number;
    @Input() searchParamsPk: DashboardSearchParamsPk;
    firstTimeload: boolean = true;
    loading = true;
    totalRecords: number;
    lazy: boolean = false;
    errorMsg: string;
    merchantList: MerchantLocation[] = [];
    @Input('merchantLocations')
    set merchantLocations(value: MerchantLocation[]) {
        this.errorMsg = null;
        if (value) {
            if (this.totalMerchantRecords > value.length) {
                this.lazy = true;
            } else {
                this.lazy = false;
                this.loading = false;
                this.merchantList = value;
                this.totalRecords = this.merchantList.length;
            }
        }
    }

    constructor(private _router: Router, private merchantSrv: MerchantLocationService) {


    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }

    public handleRowSelect(event: Event): void {

        this._router.navigate(["/dashboardinfo/", <number>LidTypesEnum.MerchantNbr, this._selectedMerchantLocation.mid]);
    }

    loadMerchantLocations(event: LazyLoadEvent) {
        if (this.errorMsg) {
            this.errorMsg = null;
        }

        this.loading = true;

        var page: MerchantPage;
        if (event) {
            var FilterMID: string;
            var FilterName: string;
            var FilterState: string;
            var FilterZipCode: string;
            var FilterStatusIndicator: string;

            if (event.filters) {
                if (event.filters.mid) {
                    FilterMID = event.filters.mid.value;
                }
                if (event.filters.name) {
                    FilterName = event.filters.name.value;
                }

                if (event.filters.state) {
                    FilterState = event.filters.state.value;
                }
                if (event.filters.zipCode) {
                    FilterZipCode = event.filters.zipCode.value;
                }

                if (event.filters.statusIndicator) {
                    FilterStatusIndicator = event.filters.statusIndicator.value;
                }
            }

            page = {
                FilterMID: FilterMID,
                FilterName: FilterName,
                FilterState: FilterState,
                FilterZipCode: FilterZipCode,
                FilterStatusIndicator: FilterStatusIndicator,
                PageSize: event.rows,
                SkipRecordNumber: event.first,
                SortField: event.sortField,
                SortFieldByAsc: event.sortOrder == 1,
            };

        }
        else {
            page = {
                SkipRecordNumber: 0,
                PageSize: 0,
                SortField: 'mid',
                SortFieldByAsc: true
            };

        }
        var filter: MerchantFilter = {
            lidTypeEnum: this.searchParamsPk.lidType,
            LIDValue: this.searchParamsPk.lidIdPk.toString(),
            Page: page
        }

        this.merchantSrv.getMerchantLocations(filter).subscribe(
            r => {
                var resp = r as apiResponse<MerchantLocation>;
                this.merchantList = resp.returnedRecords as MerchantLocation[];
                this.totalRecords = resp.totalNumberOfRecords;
                this.loading = false;
            },
            error => {
                this.errorMsg = error.text();
                this.loading = false;
            }
        );

    }
}
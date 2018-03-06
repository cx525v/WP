import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { DashboardAdvanceSearch } from '../../models/dashboardInfo/dashboard-advancesearch.model';
import { Router } from '@angular/router';
import { ViewEncapsulation } from '@angular/core';
@Component({

    selector: 'advancesearch-result',
    templateUrl: './advancesearch-result.component.html',
    styleUrls: ['./advancesearch-result.component.css'],    
    encapsulation: ViewEncapsulation.None,
})

export class AdvanceSearchComponent implements OnInit, OnDestroy {
    _result: DashboardAdvanceSearch[];
    selectedResult: DashboardAdvanceSearch;
    @Input('result')
    set result(value: DashboardAdvanceSearch[]) {       
        if (value) {
            this._result = value;
        }
    }
    get result(): DashboardAdvanceSearch[] {
        return this._result;
    }
    constructor(private router: Router) {

    }
    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }

    public handleRowSelect(event: any): void {
        let data = event.data as DashboardAdvanceSearch;
        this.result = null;
        this.router.navigate(["/dashboardinfo/", data.lidType, data.lid]);
    }

}

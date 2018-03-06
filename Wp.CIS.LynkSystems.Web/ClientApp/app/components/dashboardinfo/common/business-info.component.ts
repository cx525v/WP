import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { BusinessInfo } from './../../../models/dashboardInfo/businessInfo.model';
@Component({

    selector: 'business-info',
    templateUrl: './business-info.component.html',
    styleUrls: ['./business-info.component.css']
})
export class BusinessInfoComponent implements OnInit, OnDestroy {
    @Input() businessInfo: BusinessInfo;
    @Input() ismerchant: boolean;
    constructor() {

    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }
}
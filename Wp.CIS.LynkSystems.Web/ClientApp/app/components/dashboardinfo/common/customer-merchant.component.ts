import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import {  Contacts} from './../../../models/dashboardInfo/contacts.model';
@Component({

    selector: 'customer-merchant',
    templateUrl: './customer-merchant.component.html',
    styleUrls: ['./customer-merchant.component.css']
})
export class CustomerMerchantComponent implements OnInit, OnDestroy {
    @Input() merchantContacts: Contacts[];
    @Input() customerContacts: Contacts[];
    @Input() ismerchant: boolean;
    @Input() searchParamsPk: DashboardSearchParamsPk;
    constructor() {

    }

    public ngOnInit(): void {
       
    }

    public ngOnDestroy(): void {

    }
}

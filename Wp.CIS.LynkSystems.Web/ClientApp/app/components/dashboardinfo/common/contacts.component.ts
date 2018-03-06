import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Contacts,ContactFilter,ContactPage } from '../../../models/dashboardInfo/contacts.model';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { ContactService } from '../../../services/dashboardinfo/contact.service';
import { LazyLoadEvent } from 'primeng/primeng';
import { apiResponse } from '../../../models/dashboardInfo/apiResponse.model';
import { ViewEncapsulation } from '@angular/core';
@Component({

    selector: 'contacts',
    templateUrl: './contacts.component.html',
    styleUrls: ['./contacts.component.css'],
    providers: [ContactService],
    encapsulation: ViewEncapsulation.None,
})

export class ContactsComponent implements OnInit, OnDestroy {    
    _contacts: Contacts[] = [];
    @Input() ismerchant: boolean;
    @Input() searchParamsPk: DashboardSearchParamsPk;
    totalRecords: number;
    id: string; 
    errorMsg: string;
    @Input('contacts')
    set contacts(value: Contacts[]) {
        this.errorMsg = null;       
        if (value) {
             if (this._contacts.length > 0) {
                 this.id = this._contacts[0].id;

            }
             this._contacts = value;          
        }
    }
    get contacts(): Contacts[] {
        return this._contacts;
    }
    constructor(private contactService: ContactService) {

    }   
    public ngOnInit(): void {
      
    }

    public ngOnDestroy(): void {

    }
}

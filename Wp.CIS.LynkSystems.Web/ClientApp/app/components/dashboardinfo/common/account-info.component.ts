
import { Component, OnInit, OnDestroy, Input} from '@angular/core';
import { AccountInfo } from './../../../models/dashboardInfo/accountinfo.model';

@Component({

    selector: 'account-info',
    templateUrl: './account-info.component.html',
    styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit, OnDestroy {
    @Input() accountInfo: AccountInfo;

    constructor() {

    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }
    
}

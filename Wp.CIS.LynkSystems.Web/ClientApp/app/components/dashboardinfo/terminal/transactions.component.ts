import { Component, OnInit, OnDestroy, OnChanges, SimpleChanges, Input } from '@angular/core';

@Component({

    selector: 'transactions',
    templateUrl: './transactions.component.html',
    styleUrls: ['./transactions.component.css']
})
export class TransactionsComponent implements OnInit, OnDestroy {
    errorMsg: string;
    @Input() terminalNbr: string;
    @Input() terminalID: string;  
    constructor() {

    }

    public ngOnInit(): void {
       
    }

    public ngOnDestroy(): void {

    }


}

import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { MemoInfo } from '../../../models/dashboardInfo/memo.model';

@Component({

    selector: 'memo-list-item',
    templateUrl: './memo-list-item.component.html',
    styleUrls: ['./memo-list-item.component.css']
})
export class MemoListItemComponent implements OnInit, OnDestroy {
    @Input() item: MemoInfo;
    showDetail: boolean = false;
    constructor() {

    }

    public ngOnInit(): void {
    
    }

    public ngOnDestroy(): void {

    }

    displayDetails() {
        this.showDetail = true;
    }
}
import { Component, OnInit, OnDestroy,Input } from '@angular/core';
import { MemoInfo} from '../../../models/dashboardInfo/memo.model';

@Component({

    selector: 'memo-list',
    templateUrl: './memo-list.component.html',
    styleUrls: ['./memo-list.component.css']
})
export class MemoListComponent implements OnInit, OnDestroy {
    @Input()memos: MemoInfo[];

    constructor() {
    
    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {


    }
}
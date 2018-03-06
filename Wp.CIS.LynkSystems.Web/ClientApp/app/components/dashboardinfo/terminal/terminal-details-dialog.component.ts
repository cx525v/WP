
import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TerminalDetail,SensitivityInfo } from '../../../models/dashboardInfo/terminal.model';

@Component({

    selector: 'terminal-details-dialog',
    templateUrl: './terminal-details-dialog.component.html',
    styleUrls: ['./terminal-details-dialog.component.css']
})
export class TerminalDetailsDialogComponent implements OnInit, OnDestroy, OnChanges {
    @Input() detail: TerminalDetail
    @Input() sensitivityInfo: SensitivityInfo;
    constructor() {

    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }

    ngOnChanges(changes: SimpleChanges) {
       
    }

}
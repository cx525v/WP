
import { Component, Input, OnInit, OnDestroy } from '@angular/core';

@Component({
    selector: "dashboard-title",
    templateUrl: "./dashboard-title.component.html",
    styleUrls: ["./dashboard-title.component.css"]
})
export class DashboardTitleComponent implements OnInit, OnDestroy {

    @Input() LidRecordName: string;

    @Input() LidRecordId: string;

    @Input() CustomerName: string;

    constructor() {

    }

    public ngOnInit(): void {

    }

    public ngOnDestroy(): void {

    }
}
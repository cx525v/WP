
import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { SensitivityPartner } from './../../../models/dashboardInfo/sensitivitypartner.model';

@Component({

    selector: 'sensitivity-partner',
    templateUrl: './sensitivity-partner.component.html',
    styleUrls: ['./sensitivity-partner.component.css']
})
export class SensitivityPartnerComponent implements OnInit, OnDestroy {
    @Input() sensitivityPartner:SensitivityPartner
    constructor() {

    }

    public ngOnInit(): void {
     
    }

    public ngOnDestroy(): void {

    }
}
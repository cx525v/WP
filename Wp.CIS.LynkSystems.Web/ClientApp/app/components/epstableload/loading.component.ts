import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
@Component({
    selector: 'loading',
    templateUrl: './loading.component.html',
    styleUrls: ['./loading.component.css'],
    providers: [],

})

export class LoadingComponent implements OnInit, OnChanges {

    @Input() loading: boolean;
    display: boolean;
    constructor() { }

    ngOnInit() {

    }

    ngOnChanges(changes: SimpleChanges) {

        if (changes['loading']) {
            this.display = this.loading;
        }
    }
}
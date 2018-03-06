import { Component, OnInit, Input, OnChanges, SimpleChanges} from '@angular/core';
import '../../../app.module.client';

@Component({
    selector: 'xml',
    templateUrl: './xml.component.html',
    styleUrls: ['./xml.component.css'],
    providers: [],
})
export class XmlComponent implements OnInit {
    @Input() xmlString: string;
    @Input() display: boolean;
    @Input() title: string;
    ngOnInit() {    }
}
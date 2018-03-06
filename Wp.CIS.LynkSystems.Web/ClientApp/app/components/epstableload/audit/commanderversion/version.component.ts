import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import '../../../../app.module.client';
import { Audit } from '../../../../models/petro/audit.model';
import { AuditServcie } from '../../../../services/petro/audit.service';
import { Message, Growl, Messages } from 'primeng/primeng';
import { commanderbaseversion, commanderversion} from '../../../../models/petro/commanderversion.model';
@Component({
    selector: 'version',
    templateUrl: './version.component.html',
    styleUrls: ['./version.component.css'],
    providers: [AuditServcie],
})
export class CommanderVersionComponent implements OnInit, OnChanges {
    @Input() audit: Audit;
    IsUpdate: boolean;
    oldVersion: any;
    newVersion: any;
    constructor(private auditService: AuditServcie) {
    }

    ngOnInit() {
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['audit']) {          
            this.getValues();
        }
    }  

    getValues() {
        if (this.audit.actionType.toLowerCase() == 'update') {
            this.IsUpdate = true;
            if (this.audit.previousValue) {
                this.auditService.getversionaudit(this.audit.previousValue).subscribe(
                    r => {
                        this.oldVersion = r;
                    }
                );
            }

            if (this.audit.newValue) {
                this.auditService.getversionaudit(this.audit.newValue).subscribe(
                    r => {
                        this.newVersion = r;
                    }
                );
            }
        }
        else {
            this.IsUpdate = false;
        }
    }

}
import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import '../../../../app.module.client';
import { Message, Growl, Messages} from 'primeng/primeng';
import { TableMapping } from '../../../../models/petro/petroTablemapping.model';
import { Audit } from '../../../../models/petro/audit.model';
import { AuditServcie } from '../../../../services/petro/audit.service';
import { DateServcie } from '../../../../services/petro/date.service';

@Component({
    selector: 'petroMapping',
    templateUrl: './petroMapping.component.html',
    styleUrls: ['./petroMapping.component.css'],
    providers: [AuditServcie],
})
export class PetroMappingComponent implements OnInit, OnChanges{
    @Input() audit: Audit;
    oldMapping: TableMapping;
    newMapping: TableMapping;
    mappings: TableMapping[];
    IsUpdate: boolean;
    constructor(private auditService: AuditServcie, private dateService: DateServcie) {
    }

    ngOnInit() {
    }   

    ngOnChanges(changes: SimpleChanges) {
        if (changes['audit']) {          
            this.getValues();
        }
    }  
    getValues() {
        if (this.audit && this.audit.actionType) {
            if (this.audit.actionType.toLowerCase() == 'update') {
                this.IsUpdate = true;
                if (this.audit.previousValue) {
                    this.auditService.getMapping(this.audit.previousValue).subscribe(
                        r => {
                            this.oldMapping = r;
                            this.oldMapping.effectiveBeginDate = this.dateService.convert(this.oldMapping.effectiveBeginDate);
                            this.oldMapping.effectiveEndDate = this.dateService.convert(this.oldMapping.effectiveEndDate);
                        }
                    );
                }
                if (this.audit.newValue) {
                    this.auditService.getMapping(this.audit.newValue).subscribe(
                        r => {
                            this.newMapping = r;
                            this.newMapping.effectiveBeginDate = this.dateService.convert(this.newMapping.effectiveBeginDate);
                            this.newMapping.effectiveEndDate = this.dateService.convert(this.newMapping.effectiveEndDate);
                        }
                    );
                }
            } else {
                this.IsUpdate = false;
                var value : string = this.audit.newValue;//insert
                if (this.audit.actionType.toLowerCase() == 'delete') {
                    value = this.audit.previousValue;
                } 
                if (value) {
                    this.auditService.getMappings(value).subscribe(
                        r => {
                            this.mappings = r;
                            if (this.mappings) {
                                this.mappings.forEach(
                                    mapping => {
                                        mapping.effectiveBeginDate = this.dateService.convert(mapping.effectiveBeginDate);
                                        mapping.effectiveEndDate = this.dateService.convert(mapping.effectiveEndDate);
                                    }
                                );
                            }
                        }
                    );
                }
            }
        }
    }
   
}
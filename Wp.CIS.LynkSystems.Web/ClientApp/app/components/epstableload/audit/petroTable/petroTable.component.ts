import { Component, OnInit, Input, OnChanges, SimpleChanges, forwardRef } from '@angular/core';
import '../../../../app.module.client';
import { Message, Growl, Messages} from 'primeng/primeng';
import {PetroTable} from '../../../../models/petro/petroTable.model';
import { Audit } from '../../../../models/petro/audit.model';
import { DateServcie } from '../../../../services/petro/date.service';
import { AuditServcie } from '../../../../services/petro/audit.service';
@Component({
    selector: 'petroTable',
    templateUrl: './petroTable.component.html',
    styleUrls: ['./petroTable.component.css'],
    providers: [AuditServcie],
})
export class PetroTableComponent implements OnInit, OnChanges {
    @Input() audit: Audit;  
    displayXml: boolean = false;
    xmlString: string = '';
    dialogTitle: string = '';
    msgs: Message[] = [];    
    oldTable: PetroTable;
    newTable: PetroTable;
    IsUpdate: boolean;
    petroTables: PetroTable[];
    constructor(private auditService: AuditServcie, private dateService: DateServcie) {
    }

    ngOnInit() {   }

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
                    this.auditService.getPetroTable(this.audit.previousValue).subscribe(
                        r => {
                            this.oldTable = r;
                        }
                    );
                }

                if (this.audit.newValue) {
                    this.auditService.getPetroTable(this.audit.newValue).subscribe(
                        r => {
                            this.newTable = r;
                        }
                    );
                }
            } else{
                this.IsUpdate = false;
                var value: string = this.audit.newValue;//insert
                if (this.audit.actionType.toLowerCase() == 'delete') {
                    value = this.audit.previousValue;
                } 
                if (value) {
                    this.auditService.getPetroTables(value).subscribe(
                        r => {
                            this.petroTables = r;
                            if (this.petroTables) {
                                this.petroTables.forEach(
                                    table => {
                                        table.effectiveDate = this.dateService.convert(table.effectiveDate);
                                    }
                                );
                            }
                        }
                    );
                }
            }
        }
    }

    displayOldSchemaDef() {
        if (!this.oldTable.schemaDef) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'Schema Def is empty', detail: 'Empty Schema Def!' });

        }
        else {
            this.displayXml = true;
            this.dialogTitle = this.oldTable.tableName + ' Schema Definition';         
            this.xmlString = this.oldTable.schemaDef;           
        }
    }

    displayOldDefaultXml() {
        if (!this.oldTable.defaultXML) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'xml is empty', detail: 'Empty xml!' });

        } else {
            this.displayXml = true;
            this.dialogTitle = this.oldTable.tableName + ' Default XML';           
            this.xmlString = this.oldTable.defaultXML;
        }
    }


    displayNewSchemaDef() {
        if (!this.newTable.schemaDef) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'Schema Def is empty', detail: 'Empty Schema Def!' });

        }
        else {
            this.displayXml = true;
            this.dialogTitle = this.newTable.tableName + ' Schema Definition';
            this.xmlString = this.newTable.schemaDef;
          }
    }

    displayNewDefaultXml() {
        if (!this.newTable.defaultXML) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'xml is empty', detail: 'Empty xml!' });

        } else {
            this.displayXml = true;
            this.dialogTitle = this.newTable.tableName + ' Default XML';
            this.xmlString = this.newTable.defaultXML;
        }
    }

    displaySchemaDef(event) {  
       if (!event.schemaDef) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'schema is empty', detail: 'Empty schema!' });

        } else {
            this.displayXml = true;
            this.dialogTitle = event.tableName + ' Schema Definition';
            this.xmlString = event.schemaDef;
       }
    }

    displayDefaultXml(event) {     
        if (!event.defaultXML) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'xml is empty', detail: 'Empty xml!' });
        } else {
            this.displayXml = true;
            this.dialogTitle = event.tableName + ' Default XML';
            this.xmlString = event.defaultXML;          
        }
    }
}
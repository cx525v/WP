import { Component, OnInit, ElementRef, EventEmitter, Input,Output, OnChanges, SimpleChanges } from '@angular/core';
import { TableTreeComponent } from '../treeview/tabletree.component';
import '../../../../app.module.client';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PetroTable, UpdateXmlModel, Updates, Tree } from '../../../../models/petro/petroTable.model';
import { EPSTableloadService } from '../../../../services/petro/epstableload.service';
import { XmlService } from '../../../../services/petro/Xml.service';
import { Dropdown, DataTable, Checkbox, FileUpload, Calendar } from 'primeng/primeng';
import { commanderversion, commanderbaseversion } from '../../../../models/petro/commanderversion.model';
import { Panel } from 'primeng/primeng';
import { ViewChildren, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import {SchemaUploadComponent } from '../schemaupload.component';
@Component({
    selector: 'tableschema',
    templateUrl: './tableschema.component.html',
    styleUrls: ['./tableschema.component.css'],
    providers: [EPSTableloadService, XmlService],
})

export class TableSchemaComponent implements OnInit, OnChanges {

    private _parent: SchemaUploadComponent;
    @Input() table: PetroTable;
    @Input() set parent(value: SchemaUploadComponent) {
        this._parent = value;
    }
    get parent(): SchemaUploadComponent {
        return this._parent;
    }
    @Output() submitted: EventEmitter<PetroTable> = new EventEmitter();
    @ViewChild('tableSchemaDefFile') tableSchemaDef: any;
    @ViewChild('tableDefaultXmlFile') tableDefaultXml: any;   
    version: commanderbaseversion;
    effectiveDate: Date;
    active: boolean;
    schemaDef: string;
    defaultXml: string;
    definitionOnly: boolean;
    schemaFileName: string;
    tableSubmitted() {
        this.submitted.next(this.table);
    }

    datePipe: DatePipe = new DatePipe('en-us');
    constructor(public service: EPSTableloadService,
        private xmlService: XmlService) {
    }
    ngOnInit() {

    }
    ngOnChanges(changes: SimpleChanges) {
        this.parent.msgs = [];
        if (changes['table']) {
            this.clearUploadFiles();
            if (this.table) {
                this.tableChange();
            }
        }
    }

    hasChanges(): boolean {   
        if (this.formatDate(this.effectiveDate) != this.formatDate(this.table.effectiveDate) ||
            this.active != this.table.active ||
            this.defaultXml != this.table.defaultXML ||
            this.schemaDef != this.table.schemaDef ||
            this.definitionOnly != this.table.definitionOnly){
            return true;
        } else {
            return false;
        }
    }
    tableChange(){
        this.version = this.parent.version;   
        if (this.table.effectiveDate) {      
            this.effectiveDate = new Date(this.table.effectiveDate);
            this.effectiveDate = new Date(this.effectiveDate.getUTCFullYear(), this.effectiveDate.getUTCMonth(), this.effectiveDate.getUTCDate());
        } else {
            this.effectiveDate = new Date();
        }
        if (this.table && this.table.tableID ==-1 && !this.dictionaryCreated()) {
            this.table.definitionOnly = true;
        }
        this.active = this.table.active;
        this.schemaDef = this.table.schemaDef;
        this.defaultXml = this.table.defaultXML;   
        this.definitionOnly = this.table.definitionOnly;
    }

    formatDate(date: any) {       
        return this.datePipe.transform(date, 'yyyy-MM-dd');
    }
      
    RemoveSchema() {
        this.table.schemaDef = undefined;
        if (this.table.tableID == -1) {
            this.table.tableName = undefined;
        }          
    }
   
    RemoveDefaultXml() {
        this.table.defaultXML = undefined;
    }

    cancelSave() {
        this.table = undefined;
        this.parent.tree.loadTree();
    }  

    dictionaryCreated(): boolean {
        let exists: boolean = false;
        this.parent.petroTables.forEach(
            table => {
                if (table.definitionOnly && table.active) {
                    exists = true;                   
                }
            }
        );       
        return exists;
    }
    dictionaryExists() {
        let exists: boolean = false;
        this.parent.petroTables.forEach(
            table => {
                if (table.definitionOnly && table.active) {
                    exists = true;
                }
            }
        );
        if (!exists) {           
            if (this.table.definitionOnly) {
                exists = true;
            }           
        }

        return exists;
    }

    Submit() {      
        this.parent.loading = true;
        this.parent.msgs = [];
        if (this.parent.tree && this.parent.tree.updates && this.parent.tree.updates.length > 0) {
            this.parent.tree.reloadTree();
            this.xmlService.updatePetroTableDefaultXml(this.table.defaultXML, this.parent.tree.updates)
                .subscribe(r => {
                    this.table.defaultXML = r;
                    this.SaveTable(this.table);
                    this.parent.tree.updates = [];
                },
                error => {
                    console.log(error);
                    this.parent.loading = false;
                    this.parent.tree.updates = [];
                }
            );
        } else {
            this.SaveTable(this.table);
        }
     }    

    onSchemaUpload(event) {   
         this.readSchema(event);   
    }

    onDefaultXmlUpload(event) {
        this.readDefaultXml(event);
    }

    readDefaultXml(inputValue: any) {
        this.parent.msgs = [];
        this.parent.loading = true;
        var file: File = inputValue.files[0];
        if (file.name.endsWith('.xml')) {
            var reader: FileReader = new FileReader();
            reader.onload = f => {
                this.table.defaultXML = reader.result;
                this.parent.loading = false;
            };
            reader.readAsText(file);
        } else {
            this.parent.loading = false;
        }

    }

    getTableNameFromSchema(){
        var tableName;
        if (!this.table.schemaDef) {
            if (this.table.tableID == -1) {
                this.table.tableName = undefined;
            }
        }
        else{
            this.xmlService.getTableSchema(this.table.schemaDef).subscribe(r => {
                if (r.tableName == 'FieldUsageIdentifierTable') {
                    tableName = this.schemaFileName;
                } else {
                    tableName = r.tableName;
                }

                this.setTableName(tableName);
            },
                error => {
                    console.log(error);
                });
        }       
    }

    readSchema(inputValue: any) { 
        this.parent.msgs = [];
        var file: File = inputValue.files[0];
        this.schemaFileName = file.name.replace(".xsd", "");
        if (file.name.endsWith('.xsd')) {
            this.parent.loading = true;
            var reader: FileReader = new FileReader();
            reader.onload = f => {
                this.table.schemaDef = reader.result;
                if (this.table.definitionOnly) {                    
                    this.parent.loading = false;
                    this.setTableName(this.schemaFileName);                    
                }
                else {
                   this.getTableNameFromSchema();
                }

                this.parent.loading = false;
            };
            reader.readAsText(file);
        } 
    }

    setTableName(tableName: string) {
        if (!tableName) {
            if (this.table.tableID == -1) {
                this.table.tableName = undefined;
            }
        } else {
            if (this.table.tableID != -1) {
                if (this.table.tableName != tableName) {
                    this.parent.msgs = [];
                    this.parent.msgs.push({
                        severity: 'warn', summary: 'Wrong Schema', detail: 'Please select correct schema for ' + this.table.tableName +'!' });

                    this.clearUploadFiles();
                }
            } else {
                this.table.tableName = tableName;
            }
        }
    }

    SaveTable(table: PetroTable) {
        this.parent.loading = true;
        this.parent.msgs = [];
        table.effectiveDate = this.formatDate(this.effectiveDate);
        if (table.definitionOnly || !table.active){        
            this.Save(table);
        }
        else {      
            this.xmlService.validateDefaultXml(this.getSchemaDictionary(), table.schemaDef, table.defaultXML)
                .subscribe(r => {
                if (r.isValid) {
                    this.Save(table);
                } else {
                    this.parent.msgs = [];
                    this.parent.msgs.push({ severity: 'error', summary: 'Xml Validation Failed', detail: r.errorMessage });
                    this.parent.loading = false;
                    if (this.table.tableID == -1) {
                        this.parent.hideNewTable();
                    }
                }
            });            
        }
    }

    Save(table: PetroTable) {
        this.parent.loading = true;
        this.clearUploadFiles();
        this.service.SavePetroTable(table).subscribe(
            (res) => {
                let saved: boolean = res as boolean;
                if (saved) {                    
                    this.parent.msgs = [];
                    if (table.tableID ==-1) {
                        this.parent.msgs.push({ severity: 'success', summary: 'Success Message', detail: table.tableName + ' was added new table sucessfully!' });
                        this.table = null;
                        this.parent.addNewTableDisplay = false;
                    } else {
                        this.parent.msgs.push({ severity: 'success', summary: 'Success Message', detail: table.tableName + ' was updated sucessfully!' });
                        this.parent.addNewTableDisplay = false;
                    }
                    this.tableSubmitted();
                    this.parent.loading = false;
                } else {

                    this.parent.msgs = [];
                    this.parent.msgs.push({ severity: 'warn', summary: 'Failed Message', detail: table.tableName + ' was not updated!' });
                    this.parent.loading = false;
                    if (this.table.tableID == -1) {
                        this.parent.hideNewTable();
                    }
                }
            },
            (error) => {

                this.parent.msgs = [];
                this.parent.msgs.push({ severity: 'error', summary: 'Error Message', detail: 'Error!' });
                this.parent.loading = false;
                if (this.table.tableID == -1) {
                    this.parent.hideNewTable();
                }
            }

        );

    }

    getSchemaDictionary(): string[] {
        var dict : string[] = [];
        this.parent.petroTables.forEach(
            t => {
                var effectiveDate = new Date(t.effectiveDate);
                var today = new Date();
                var effective: boolean = (today.getTime() - effectiveDate.getTime()) >= 0;

                if (t.definitionOnly && t.active && effective) {
                    dict.push(t.schemaDef);
                    
                }
            }
        );

        return dict;
    }


    tableValid(): boolean {
        if (!this.table ||
            !this.effectiveDate ||
            !this.table.tableName ||
            !this.table.schemaDef ||
            (!this.table.definitionOnly && !this.table.defaultXML) ||
            !this.dictionaryExists()){
            return false;
        } else {
            return true;
        }
    }

    cancel() {
        this.clearUploadFiles();
        if (this.table.tableID == -1) {
            this.parent.newTable = undefined;
            this.parent.addNewTableDisplay = false;
        } else {
            this.parent.copyTable();
        }
    }

    clearUploadFiles() {
        if (this.tableSchemaDef) {
            this.tableSchemaDef.clear();
        }
        if (this.tableDefaultXml) {
            this.tableDefaultXml.clear();
        }
    }

    definiationChange(event) {   
   
        if (this.table.definitionOnly) {
            this.table.defaultXML = undefined;
            this.setTableName(this.schemaFileName);
        } else {
           this.getTableNameFromSchema();           
        }
    }
    
}
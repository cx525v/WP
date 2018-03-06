import { Component, OnInit, Input, OnChanges,SimpleChanges, forwardRef } from '@angular/core';
import { TableTreeComponent } from './treeview/tabletree.component';
import { TableSchemaComponent} from './tableschema/tableschema.component';
import '../../../app.module.client';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PetroTable, UpdateXmlModel, Updates, Tree} from '../../../models/petro/petroTable.model';
import { DownloadService } from '../../../services/petro/download.service';
import { EPSTableloadService } from '../../../services/petro/epstableload.service';
import { Dropdown, DataTable, Checkbox, FileUpload } from 'primeng/primeng';
import { commanderversion, commanderbaseversion } from '../../../models/petro/commanderversion.model';
import { ConfirmationService, Panel, Message, Growl, Calendar } from 'primeng/primeng';
import { ViewChildren, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
@Component({
    selector: 'schemaupload',
    templateUrl: './schemaupload.component.html',
    styleUrls: ['./schemaupload.component.css'],
    providers: [EPSTableloadService, ConfirmationService, DownloadService],  
})

export class SchemaUploadComponent implements OnInit, OnChanges{
    @Input() version: commanderbaseversion;
    @ViewChild(TableTreeComponent) tree: TableTreeComponent;
    @ViewChild(forwardRef(() => TableSchemaComponent))
    private tableschema: TableSchemaComponent;
    selectedTable: PetroTable;
    editTable: PetroTable;
    newTable: PetroTable;
    petroTables: PetroTable[] = [];
    displayXml: boolean = false;
    xmlString: string = '';
    dialogTitle: string = '';
    addNewTableDisplay: boolean = false;    
    downloadFile: string;
    msgs: Message[] = [];    
    datePipe: DatePipe = new DatePipe('en-us');
    public loading: boolean = false;
  
    constructor(private downloadService: DownloadService,             
        private tableService: EPSTableloadService,
        private confirmationService: ConfirmationService) {      
    }
    ngOnInit() {       
    }

    get self(): SchemaUploadComponent {
        return this;
    }

    formatDate(dateString: string) {       
        return this.datePipe.transform(dateString, 'yyyy-MM-dd');           
    }
  
    ngOnChanges(changes: SimpleChanges) {
        this.msgs = [];
        this.selectedTable = undefined;
        if (changes['version']) { 
            this.editTable = undefined;
            if (this.version) {
                this.getTables();
            }
        }
     }  

    getTables() {
        this.msgs = [];
        this.loading = true;
        if (this.version) {
            this.tableService.GetAllPetroTablesByVersion(this.version.versionID).subscribe(
                res => {
                    this.petroTables = res;
                    this.petroTables.forEach(table => {
                        table.effectiveDate = this.formatDate(table.effectiveDate);
                        if (this.selectedTable && this.selectedTable.tableID == table.tableID ) {
                            this.selectedTable = table;
                            this.copyTable();
                        }
                    });
                   
                    if (!this.selectedTable && this.petroTables && this.petroTables.length > 0) {
                        this.selectedTable = this.petroTables[0];
                        this.copyTable();
                    }
                  
                    this.loading = false;
                },
                error => {
                    this.loading = false;                  
                    console.log(error);
                }
            );
        }

    }
    selectPetroTable(event) { 
        if ((this.tree.updates && this.tree.updates.length > 0) || this.tableschema.hasChanges()) {
            this.confirmChange(event);
        } else {
            this.switchTable(event);  
        }
    }

    confirmChange(table: PetroTable) {
        this.confirmationService.confirm({
            message: this.selectedTable.tableName + ' has unsaved change, do you want to dismiss the change?',
            accept: () => {      
                
                this.switchTable(table);  
            },
            header: 'Unsaved Changes',           
        });
    }

    switchTable(table: PetroTable) {       
        this.msgs = [];
        this.tree.updates = [];
        this.selectedTable = table;
        this.copyTable();      
    }

    copyTable() {
        this.editTable = {
            active: this.selectedTable.active,
            createdDate: this.selectedTable.createdDate,
            defaultXML: this.selectedTable.defaultXML,
            definitionOnly: this.selectedTable.definitionOnly,
            effectiveDate: this.selectedTable.effectiveDate,
            lastUpdatedBy: localStorage.getItem('WorldPay.cis.currentUser'),          
            schemaDef: this.selectedTable.schemaDef,
            tableID: this.selectedTable.tableID,
            tableName: this.selectedTable.tableName,
            versionID: this.selectedTable.versionID,
        };
    }
   

    displaySchemaDef(event) { 
        if (!event.schemaDef) {
            this.msgs = [];
            this.msgs.push({ severity: 'info', summary: 'Schema Def is empty', detail: 'Empty Schema Def!' });

        }
        else {
            this.displayXml = true;
            this.dialogTitle = event.tableName + ' Schema Definition';
            this.downloadFile = event.tableName + 'SchemaDef.xsd';
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
            this.downloadFile = event.tableName + 'DefaultValues.xml'
            this.xmlString = event.defaultXML;
        }
    }

  
    AddNewTable() {      
        this.newTable =  {            
            tableID: -1,
            tableName: '',
            versionID: this.version.versionID,
            active: true,
            definitionOnly: false,
            schemaDef: '',
            defaultXML: '',
            createdDate: '',
            effectiveDate: '',
            lastUpdatedBy: '',
            lastUpdatedDate:''    
        };       

        this.addNewTableDisplay = true;
    }

  
    download() {
        this.msgs = [];
        this.downloadService.download(this.xmlString, this.downloadFile);         
    }


    hideNewTable() {
        this.newTable = undefined;
    }

    reload(event) {      
        this.addNewTableDisplay = false;
        this.newTable = undefined;
        this.getTables();      
    }      
}  


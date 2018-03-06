import { Component, OnInit, Input, OnChanges, SimpleChanges, forwardRef, ViewChild } from '@angular/core';
import { DateServcie } from '../../../services/petro/date.service';
import { AuditServcie } from '../../../services/petro/audit.service';
import '../../../app.module.client';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Calendar } from 'primeng/primeng';
import { commanderversion, commanderbaseversion } from '../../../models/petro/commanderversion.model';
import { Audit } from '../../../models/petro/audit.model';
import { PetroTable } from '../../../models/petro/PetroTable.model';
import { TableMapping, Mapping } from '../../../models/petro/petroTablemapping.model';
import { ActionItem } from '../../../models/petro/auditActionItem.enum';
@Component({
    selector: 'audit',
    templateUrl: './audit.component.html',
    styleUrls: ['./audit.component.css'],
    providers: [AuditServcie, DateServcie],
})
export class AuditComponent implements OnInit, OnChanges{
    public loading: boolean = false;
    @Input() version: commanderbaseversion;
    @ViewChild('dt') dt: any;
    dataSource: Audit[] =[];
  
    oldTable: PetroTable;
    newTable: PetroTable;
    displayTable: boolean = false;
    oldMapping: TableMapping;
    newMapping: TableMapping;
    displayMapping: boolean = false;
    oldVersion: commanderbaseversion;
    newVersion: commanderbaseversion;
    displayVersion: boolean = false;
    selectedAudit: Audit;
    StartDate: Date;
    EndDate: Date;
    constructor(private auditService: AuditServcie, private dateService: DateServcie) {
    }
    ngOnInit() {   
        this.Get30DaysAudit();
   }   

    ngOnChanges(changes: SimpleChanges) {   
        if (changes['version']) {    
            this.dataSource = []; 
        }
    }  

  getAudit(){
     this.dataSource = [];     
      if (this.version) {
          this.loading = true;
          this.auditService.getAudit(this.version.versionID,
              this.dateService.convert(this.StartDate),
              this.dateService.convert(this.EndDate)).subscribe(
              data => {
                  this.dataSource = data as Audit[];
                  this.loading = false;
              },
              error => {
                  this.loading = false;
                  console.log(error);
              }
          )
      }
    }   

    public submit() {
        if (this.dateRangeIsvalid()) {
            if (this.version) {
                this.getAudit();
            }
        }
    }

    public Get30DaysAudit() {

        this.EndDate = new Date();
        var enddate = new Date();
        let date: Date = new Date();
        this.StartDate = new Date(date.setDate(date.getDate() - 29));
       
    }

    getErrorMsg(): string {
        if (!this.StartDate) {
            return 'Please enter Start Date';
        } else if (!this.EndDate) {
            return 'Please enter End Date';
        } else if (!this.dateRangeIsvalid()) {
            return 'Invalid date range must be less than 31 days';
        } else {
            return null;
        }
    }

    dateRangeIsvalid(): boolean {
        if (!this.StartDate || !this.EndDate) {
            return false;
        } else {
            var daysDiff = (this.EndDate.getTime() - this.StartDate.getTime()) / (24 * 60 * 60 * 1000);
            if (daysDiff >= 0 && daysDiff < 30) {
                return true;
            } else {
                return false;
            }
        }
    }

    toggleRow(event) { 
        this.selectedAudit = event as Audit;  
        console.log(event);
        this.dt.toggleRow(event);            
    }

    displayOldValue(event) {    
        let audit: Audit = event as Audit;
        if (audit.previousValue) {           
            this.dt.toggleRow(event);
        }
    }

    displayNewValue(event) {
        let audit: Audit = event as Audit;
        if (audit.newValue) {         
            this.dt.toggleRow(event);
        }
    }

}


import { Component, OnInit } from '@angular/core';
import { Http, Response } from '@angular/http';
import { EPSLogServcie } from '../../../services/petro/epslog.service';
import '../../../app.module.client';
import { EPSLog } from '../../../models/petro/epslog.model';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Message, Growl, Messages, Calendar} from 'primeng/primeng';
import { DatePipe } from '@angular/common';
@Component({
    selector: 'epslog',
    templateUrl: './epslog.component.html',
    styleUrls: ['./epslog.component.css'],
    providers: [EPSLogServcie],      
})
export class EPSLogComponent implements OnInit {
    public dataSource: EPSLog[];
    public loading: boolean = false;
    StartDate: Date;
    EndDate: Date;
    public msgs: Message[] = [];  
    constructor(private epslogSrv: EPSLogServcie) {       
    }
    
    ngOnInit() {    
        this.GetWeekLog(); 
    }

    public GetWeekLog() {  
        this.EndDate = new Date();
        var enddate = new Date();
        this.StartDate = new Date(enddate.setDate(enddate.getDate() - 6));       
        this.getEPSLogList();
    }

    convert(date: Date): string {
        var datePipe = new DatePipe('en-us');
        return datePipe.transform(date, 'yyyy-MM-dd');
    }

    public GetTodayLog() {      
        this.StartDate  = new Date();
        this.EndDate = new Date();
        this.getEPSLogList();
    }

    public GetMonthLog() {      
        
        this.EndDate = new Date();
        var enddate = new Date();
        this.StartDate = new Date(enddate.setMonth(enddate.getMonth() - 1));       
        this.StartDate = new Date(this.StartDate.setDate(this.StartDate.getDate() + 1));  
        this.getEPSLogList();
    }

    public submitData() {    
        if (this.dateRangeIsvalid()) {
            this.getEPSLogList();
        }    
    }

    getErrorMsg(): string {
        if (!this.StartDate) {
            return 'Please enter Start Date';
        } else if (!this.EndDate) {
            return 'Please enter End Date';
        } else if (!this.dateRangeIsvalid()) {
            return 'Invalid Date Range!(Date Range should less than 62 days.)';
        } else {
            return null;
        }
    }

    dateRangeIsvalid(): boolean {
        if (!this.StartDate || !this.EndDate) {
            return false;
        } else {           
            var daysDiff = (this.EndDate.getTime() - this.StartDate.getTime()) /(24 * 60 * 60 * 1000);
            if (daysDiff < 62 && daysDiff >= 0) {
                return true;
            } else {
                return false;
            }
        }
    }

    getEPSLogList() {
        this.loading = true;
        this.epslogSrv.getEPSLog(this.convert(this.StartDate), this.convert(this.EndDate)).subscribe(
            data => {
                this.dataSource = data as EPSLog[];
                this.loading = false;
            },
            error => {
                this.loading = false;
                this.dispayError('Server Error!');
            }
        )
    }    

    dispayError(errorMsg: string) {
        this.msgs.push({ severity: 'error', summary: 'Error', detail: errorMsg });
    }
}


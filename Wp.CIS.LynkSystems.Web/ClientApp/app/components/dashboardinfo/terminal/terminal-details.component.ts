import { Component, OnInit, OnDestroy, Input, OnChanges, SimpleChanges} from '@angular/core';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';
import { TerminalInfoService } from '../../../services/dashboardinfo/terminal.service';
import { DatePipe } from '@angular/common';
import { YearMonthDatePipe} from '../../../pipes/dateymd.pipe';
import { TerminalDetails, Terminal, TerminalSettlementInfo, TerminalDetail,SensitivityInfo } from '../../../models/dashboardInfo/terminal.model';
import { ActiveServices, TermInfo } from '../../../models/dashboard.model';

@Component({

    selector: 'terminal-details',
    templateUrl: './terminal-details.component.html',
    styleUrls: ['./terminal-details.component.css']
})
export class TerminalDetailsComponent implements OnInit, OnDestroy, OnChanges {

    displayDetail: boolean;
    terminal: Terminal;
    sensitivityInfo: SensitivityInfo;
    detail: TerminalDetail;
    terminalSettlementInfo: TerminalSettlementInfo;
    tid: string;
    errorMsg: string;
    @Input() terminalNbr: number;
    constructor(private terminalSrv: TerminalInfoService) {

    }

    ngOnChanges(changes: SimpleChanges) {   
        this.errorMsg = null;
        if (changes['terminalNbr']) {      
            if (this.terminalNbr) {
                this.getTerminalInfo(this.terminalNbr);
                this.getTerminalSettlementInfo(this.terminalNbr);
            }
        }
    }

    public ngOnInit(): void {
        
    }

    public ngOnDestroy(): void {

    }

    private getTerminalInfo(terminalNbr: number) {       
        this.terminalSrv.getTerminalInfoDetail(terminalNbr).subscribe(
            r => {  
                this.terminal = r as Terminal;
                if (this.terminal && this.terminal.activeServices && this.terminal.terminalDetails) {                  
                    this.terminal.services = this.getActiveServiesDesc(this.terminal.terminalDetails); 
                    this.tid = this.terminal.terminalDetails.tid;     
                    this.sensitivityInfo = this.terminal.sensitivityInfo;
                }
         
            },
            error => {
                this.errorMsg = error.text();
            }
        );
    }

    private getTerminalSettlementInfo(terminalNbr: number) {
        this.terminalSrv.getTerminalSettlementInfo(terminalNbr).subscribe(
            r => {
                this.terminalSettlementInfo = r as TerminalSettlementInfo;               
            },
            error => {
                this.errorMsg = error.text();
            }
        );
    }

    showDetail() {      
        if (this.terminal && this.terminal.activeServices && this.terminal.terminalInfo) {
            this.detail = {
                tid: this.terminal.terminalDetails.tid,       
                terminalEquipment: this.terminal.terminalDetails.terminalDescription,
                softDesc: this.terminal.activeServices.softDesc,
                statDesc: this.terminal.terminalInfo.statDesc,
                activationDt: this.terminal.terminalInfo.activationDt,
                deactivationDt: this.terminal.terminalInfo.deactivationDt,
                activeServiesDesc: this.terminal.activeServices.activeServiesDesc,
                billMtdDesc: this.terminal.activeServices.billMtdDesc,
                terminalType:this.terminal.terminalDetails.terminalType,
            };
            this.displayDetail = true;
        }
    }


    private getActiveServiesDesc(termInfo: TerminalDetails): string {
        var srv = '';
        if (termInfo.revPip == 1) {
            srv = srv.concat('Amex Reverse Pip ');
        }
        if (termInfo.debit == 1) {
            srv = srv.concat('Debit ');
        }
        if (termInfo.giftLynk == 1) {
            srv = srv.concat('Gift Card ');
        }
        if (termInfo.checkSvc == 1) {
            srv = srv.concat('Check Svcs ');
        }
        if (termInfo.ebt == 1) {
            srv = srv.concat('EBT ');
        }
        if (termInfo.credit == 1) {
            srv = srv.concat('Credit ');
        }
        if (termInfo.rewardsLynk == 1) {
            srv = srv.concat('Loyalty Card ');
        }
        return srv;
    }
    public getCutOff(hourString: string) {
        try {        
            if (hourString) {
                var hour = Number(hourString.trim());
                var min = hour % 100;
                hour = hour - min;
                var minString = min.toString();
                if (min < 10) {
                    minString = '0' + minString.toString();
                }
                hour = hour / 100;

                if (hour < 12) {
                    return hour + ':' + minString + 'AM';
                } else if (hour == 12) {
                    return hour + ':' + minString + 'PM';
                } else {
                    return (hour - 12) + ':' + minString + 'PM';
                }
            } else {
                return '';
            }
        } catch (Error) {

            return '';
        }
    }
}
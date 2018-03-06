import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { RecentStatement } from './../../../models/dashboardInfo/recentstatement.model';
import { DashboardInfoService} from './../../../services/dashboardinfo.service';
import { LidTypesEnum } from '../../../models/common/lid-types.enum';
import { DashboardSearchParamsPk } from '../../../models/dashboardInfo/dashboard-search-params-pk.model';

@Component({

    selector: 'recent-statements',
    templateUrl: './recent-statements.component.html',
    styleUrls: ['./recent-statements.component.css']
})
export class RecentStatementsComponent implements OnInit, OnDestroy { 
    errorMsg: string;
    statement: string;
    displayStatement: boolean = false;
    statements: RecentStatement[]=[];
    morestatements: RecentStatement[];
    recentStatements: RecentStatement[];
    merchantNbr: string
     @Input('merchantNbr')
     set MerchantNbr(value: string) {  
         this.errorMsg = null;
         if (value) {
             this.merchantNbr = value;
             this.getRecentStatements(this.merchantNbr);
          }
     }
     get MerchantNbr() {
         return this.merchantNbr;
     }
   
    constructor(private _dashboardService: DashboardInfoService) {        
    }

    public ngOnInit(): void {  
       
    }

    public ngOnDestroy(): void {

    }   

    getStatementDate(statement: RecentStatement):string {
        var year = statement.yearId;
        var month = statement.monthId;
        var monthString: string;

        switch (month) {
            case 1:
                monthString = 'January';
                break;
            case 2:
                monthString = 'Feburay';
                break;
            case 3:
                monthString = 'March';
                break;
            case 4:
                monthString = 'April';
                break;
            case 5:
                monthString = 'May';
                break;
            case 6:
                monthString = 'June';
                break;
            case 7:
                monthString = 'July';
                break;
            case 8:
                monthString = 'August';
                break;
            case 9:
                monthString = 'September';
                break;
            case 10:
                monthString = 'October';
                break;
            case 11:
                monthString = 'November';
                break;
            case 12:
                monthString = 'December';
                break;
            default:
                monthString = '';
                break;
        }

        return monthString + ', ' + year;
    }
    getStatement(event: RecentStatement) {      
        this.displayStatement = true;
        this.statement = event.html_String;
    }

     private getRecentStatements(merchantNbr: string) {
        this._dashboardService.getRecentStatements(merchantNbr).subscribe(
            r => {
                this.recentStatements = r as RecentStatement[];
                this.statements = [];
                this.morestatements = [];
                var i: number = 0;
                this.recentStatements.forEach(
                s => {
                    if (i < 6) {
                        this.statements.push(s);
                    } else {
                            this.morestatements.push(s);
                    }
                    i++;
                }
            );

            },
            error => {              
                this.errorMsg = error.text();
            }
        )

    }

print(): void {
    let printContents, popupWin;
    popupWin = window.open('', '_blank', 'top=0,left=0,height=100%,width=auto');
    popupWin.document.open();
    popupWin.document.write(`
      <html>
        <head>
          <title>Statement</title>          
        </head>
        <body onload="window.print();window.close()">${this.statement}</body>
      </html>`
    );
    popupWin.document.close();
}
}